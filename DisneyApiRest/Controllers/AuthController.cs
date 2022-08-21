using DisneyAlk.Services;
using DisneyApiRest.Configuration;
using DisneyApiRest.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace DisneyApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        ITokenHandlerService _service;
        public AuthController(UserManager<IdentityUser> userManager, ITokenHandlerService service) {

            _userManager = userManager;
            _service = service;
        
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterUserRequestDTO user) 
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Mail);
                if (existingUser != null)
                {
                    return BadRequest("Correo ya registrado");
                }

                var isCreated = await _userManager.CreateAsync(new IdentityUser()
                {
                    Email = user.Mail,
                    UserName = user.Mail
                }, user.Password);

                if (isCreated.Succeeded)
                {

                  await SendMail(user.Mail, user.Name);
                    return Ok();
                }
                else
                {

                    return BadRequest(isCreated.Errors.Select(x => x.Description).ToList());
                }
            }
            else {

                return BadRequest("Se produjo un error");
            }
        
        }

         static async Task SendMail(string mail, string username) {

            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("dcateura@gmail.com", "Mickey");
            var subject = "Usuario Registrado en Disney";
            var to = new EmailAddress(mail, username);
            var plainTextContent = "Bienvenido a Disney";
            var htmlContent = "<strong>Aqui encontrarás todas las peliculas con sus personajes</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDTO user)
        {
            if (ModelState.IsValid)
            {

                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser == null)
                {
                    return BadRequest(new UserLoginResponseDTO() { 
                    
                      Login = false,
                       Errors = new List<string>() 
                        { 
                            "Email o Password incorrecto"
                        }
                    
                    
                    });
                }

                var passwordok = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if (passwordok)
                {


                    var pars = new TokenParameters()
                    {
                        Id = existingUser.Id,
                        Passwordhash = existingUser.PasswordHash,
                        Username = existingUser.UserName

                    };
                    var jwtToken = _service.GenerateJwtToken(pars);

                    return Ok(new UserLoginResponseDTO() { 
                    
                    Login = true,
                    Token = jwtToken
                    
                    });

                }
                else {

                    return BadRequest(new UserLoginResponseDTO()
                    {

                        Login = false,
                        Errors = new List<string>()
                        {
                            "Email o Password incorrecto"
                        }


                    });

                }


            }
            else
            {

                return BadRequest("Se produjo un error");
            }

        }
    }
}
