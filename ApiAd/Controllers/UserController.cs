using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using APIAD.Model;
using APIAD.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Security.Claims;
using System.DirectoryServices.AccountManagement;
using Microsoft.AspNetCore.Cors;

namespace APIAD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository getUserRepository)
        {
            _userRepository = getUserRepository;
        }

        [Authorize(Roles = "admin,read")]
        [HttpGet]
        public UserModel Get(string userName)
        {
            return _userRepository.GetAdUser(userName);
        }

        [HttpGet]
        [Route("GetAdUsers")]
        public List<SimpleUserModel> GetAdUsers(bool loadManager = false, bool loadPhoto = false, string OUFilter = "all")
        {
            return _userRepository.GetAdUsers(loadManager, loadPhoto, OUFilter);
        }


        [HttpPost]
        [Route("Authenticate")]
        public IActionResult AuthenticateAd([FromBody] AuthenticatedUserModel json)
        {
            try
            {
                if (json.generatedToken)
                {
                    var userWithToken = _userRepository.AdAuthenticate(json.adUser, json.password, json.generatedToken);
                    var newJsonWithToken = JsonSerializer.Serialize<UserModel>(userWithToken);
                    return Ok(userWithToken);
                }

                var user = _userRepository.AdAuthenticate(json.adUser, json.password);
                var novoJson = JsonSerializer.Serialize<UserModel>(user);

                return Ok(user);
            }
            catch (DirectoryServicesCOMException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [EnableCors("PermitedDomains")]
        [HttpGet]
        [Route("GetToken")]
        [Authorize]
        public UserModel Authenticated()
        {
            var usuario = _userRepository.GetAdUser(User.Identity.Name);
            return usuario;
        }

        [HttpGet]
        [Route("GetProperty")]
        [Authorize]
        public string ConsultaPropriedade(string propriedade)
        {
            return _userRepository.GetUserProperty(User.Identity.Name, propriedade);
        }
    }
}
