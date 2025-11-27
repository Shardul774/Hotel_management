using System.Web.Http;
using Hotel.BLL;

namespace Hotel.API.Controllers
{
    public class AuthController : ApiController
    {
        private UserService _service = new UserService();

        //POST : api/auth
        [HttpPost]
        public IHttpActionResult Login([FromBody] LoginModel model)
        {
            if (model == null) return BadRequest("Invalid Request");

            bool isValid = _service.Login(model.Username, model.Password);

            if (isValid)
                return Ok(new { Message = "Login Successful" });
            else
                return Unauthorized(); // 401 Error
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}