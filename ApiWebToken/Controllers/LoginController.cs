
using System.Net;
using System.Threading;
using System.Web.Http;
using ApiWebToken.Models;

namespace ApiWebToken.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {

        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok("We have connection, Houston!");
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($"IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        
        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(Usuario usuario)
        {
            if (usuario == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            //TODO: Validate credentials Correctly. 
            bool isCredentialValid = usuario.password == usuario.password;
            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(usuario.email);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}
