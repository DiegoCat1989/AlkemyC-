using DisneyAlk.Application;
using DisneyAlk.Entities;
using DisneyApiRest.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DisneyApiRest.Controllers
{

    [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        IApplication<Character> _character;
        IApplication<Movie> _movie;
        public CharactersController(IApplication<Character> character, IApplication<Movie> movie)
        {
            _character = character;
            _movie = movie;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            var Characters = _character.GetAll();

            if (Request.Query.Count() == 0) 
            {
               var  AllCharacter = Characters.Select(x => new
                {
                    Imagen = x.Image,
                    Nombre = x.Name
                });

                return Ok(AllCharacter.ToList());


            }
            else
            {
                
                
                foreach(var filter in Request.Query)
                {
                    switch (filter.Key) {
                        case "name":
                            var a = Characters.Where(x => x.Name.Equals(filter.Value[0])).Select(x => new
                            {
                                Imagen = x.Image,
                                Nombre = x.Name
                            });
                            ;

                            return Ok(a);
                        case "age":
                          
                            a = Characters.Where(x => x.Age.Equals(int.Parse(filter.Value[0]))).Select(x => new
                            {
                                Imagen = x.Image,
                                Nombre = x.Name
                            }); 


                            return Ok(a);
                            
                        case "IdMovie":
                            var n = _movie.GetbyId(int.Parse(filter.Value[0]));
                            if (n != null)
                            {
                                a=n.Characters.Select(x=> new                           
                                {
                                    Imagen = x.Image,
                                    Nombre = x.Name
                                });
                                return Ok(a);
                            }else return Ok(); 
                            
                          
                    };
                                   
                }
                return BadRequest("Surgió un error");

            };

                           
            
           
        }

      

        [HttpGet]
        [Route("Detail")]
        public IActionResult Detail(int id)
        {


            var x = _character.GetbyId(id);


            return Ok(x);

        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Save([FromBody]CharacterCreateDTO character) 
        {

            

            if (ModelState.IsValid) {
                var c = new Character()
                {

                    Age = character.Age,
                    Name = character.Name,
                    Image = character.Image,
                    Weight = character.Weight,
                    History = character.History,

                };

                try {

                    foreach (int item in character.Movies)
                    {

                        c.Movies.Add(_movie.GetbyId(item));



                    }
                } catch { return BadRequest("Surgio un error, asegurese que los IDs informados son correctos"); };
                

            return Ok(_character.Save(c));
            }
            else{

                return BadRequest("Surgió un error, intentelo nuevamente.");
            }


        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update([FromBody] CharacterUpdateDTO character)
        {
            if (ModelState.IsValid)
            {
                var c = new Character()
                {
                    Id = character.Id,
                    Age = character.Age,
                    Name = character.Name,
                    Image = character.Image,
                    Weight = character.Weight,
                    History = character.History

                };

                try
                {

                    foreach (int item in character.Movies)
                    {

                        c.Movies.Add(_movie.GetbyId(item));



                    }
                }
                catch { return BadRequest("Surgio un error, asegurese que los IDs informados son correctos"); };



                return Ok(_character.Save(c));
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
                _character.Delete(id);
                return Ok("Borrado ok");
            }
            else
            {

                return BadRequest("Surgió un error, intentelo nuevamente.");
            }


        }

    }
}
