using DisneyAlk.Application;
using DisneyAlk.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DisneyApiRest.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]

    public class GenreController : ControllerBase
    {

        IApplication<Genre> _genre;
        public GenreController(IApplication<Genre> genre)
        {
          
            _genre = genre;
        }


        [HttpGet]
        public IActionResult Get() {


            return Ok(_genre.GetAll());
        }






    }




}
 
