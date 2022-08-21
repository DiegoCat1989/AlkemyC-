using DisneyAlk.Application;
using DisneyAlk.Entities;
using DisneyApiRest.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace DisneyApiRest.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        IApplication<Movie> _movie;
        IApplication<Character> _character;
        IApplication<Genre> _genre;
        public MoviesController(IApplication<Movie> movie, IApplication<Character> character, IApplication<Genre> genre)
        {
            _movie = movie;
            _character = character;
            _genre = genre;
        }

        [HttpGet]
        public IActionResult Get()
        {

            //Imagen, titulo y Fecha de creación

            var Movies = _movie.GetAll();

            if (Request.Query.Count() == 0)
            {
                var AllMovies = _movie.GetAll().ToList().Select(x => new
                {
                    Imagen = x.Image,
                    Titulo = x.Titulo,
                    FechaCreacion = x.CreationDate

                });

                return Ok(AllMovies.ToList());

            }
            else
            {

                foreach (var filter in Request.Query)
                {
                    switch (filter.Key)
                    {
                        case "name":
                            var a = Movies.Where(x => x.Titulo.Equals(filter.Value[0])).Select(x => new
                            {
                                Imagen = x.Image,
                                Titulo = x.Titulo,
                                FechaCreacion = x.CreationDate
                            });

                            return Ok(a);
                        case "genre":

                            a = _genre.GetbyId(int.Parse(filter.Value[0])).Movies.Select(x => new
                            {
                                Imagen = x.Image,
                                Titulo = x.Titulo,
                                FechaCreacion = x.CreationDate
                            });
                            return Ok(a);

                        case "order":
                            //ASC or DESC
                            var AllMovies = _movie.GetAll().ToList().Select(x => new
                            {
                                Imagen = x.Image,
                                Titulo = x.Titulo,
                                FechaCreacion = x.CreationDate

                            });
                            if (filter.Value[0] == "ASC")
                            {
                                return Ok(AllMovies.ToList().OrderBy(x => x.FechaCreacion));
                            }
                            else if (filter.Value[0] == "DESC")
                            {
                                return Ok(AllMovies.ToList().OrderByDescending(x => x.FechaCreacion));
                            }
                            return BadRequest("Bad order params");



                    }

                }
                return BadRequest("Surgió un error");

            };

        


        


    }

        [HttpGet]
        [Route("Detail")]
        public IActionResult Detail(int id)
        {


            var movie = _movie.GetbyId(id);
          
            return Ok(movie);

        }



        [HttpPost]
        [Route("Create")]
        public IActionResult Save([FromBody] MovieCreateDTO movie)
        {



            if (ModelState.IsValid)
            {
                var c = new Movie()
                {


                    Titulo = movie.Titulo,
                    CreationDate = movie.CreationDate,
                    Image = movie.Image,
                    Qualification = movie.Qualification,
                    History = movie.History

                };

                try
                {

                    foreach (int item in movie.Characters)
                    {

                        c.Characters.Add(_character.GetbyId(item));



                    }
                }
                catch { return BadRequest("Surgio un error, asegurese que los IDs informados son correctos"); };

                _movie.Save(c);
                var z = _genre.GetbyId(movie.idGenre);
                if(z != null){ z.Movies.Add(c); }else{return BadRequest("No existe el genero indicado"); }
                _genre.Save(z);
                return Ok();
            }
            else
            {

                return BadRequest("Surgió un error, intentelo nuevamente.");
            }


        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update([FromBody] MovieUpdateDTO movie)
        {
            if (ModelState.IsValid)
            {
                var c = new Movie()
                {
                    Id = movie.Id,
                    Titulo = movie.Titulo,
                    CreationDate = movie.CreationDate,
                    Image = movie.Image,
                    Qualification = movie.Qualification,
                    History = movie.History

                };
                try
                {

                    foreach (int item in movie.Characters)
                    {

                        c.Characters.Add(_character.GetbyId(item));



                    }
                }
                catch { return BadRequest("Surgio un error, asegurese que los IDs informados son correctos"); };

                return Ok(_movie.Save(c));
            }
            else
            {

                return BadRequest("Surgió un error, intentelo nuevamente.");
            }


        }


        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                _movie.Delete(id);
                return Ok("Borrado ok");
            }
            else
            {

                return BadRequest("Surgió un error, intentelo nuevamente.");
            }


        }

    }
}
