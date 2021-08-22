using APIAD.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    public class SignatureController : ControllerBase
    {
        private readonly IUserRepository _user;

        public SignatureController(IUserRepository user)
        {
            _user = user;
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult GetUser(string userName)
        {
            var obj = _user.GetAdUser(userName);
            
            var user = new
            {
                name = obj.FullName,
                title = obj.Title,
                enterprise = obj.Empresa,
                phone = obj.Phone,
                department = obj.Department,
                mail = obj.Email
            };

            return Ok(user);
        }
    }
}