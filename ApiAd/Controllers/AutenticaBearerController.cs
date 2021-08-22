using System;
using System.Threading.Tasks;
using APIAD.Model;
using APIAD.Repository;
using APIAD.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#pragma warning disable CS1998
public class AutenticaBearerController : ControllerBase
{
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserBearerAdModel model)
    {
        try
        {
            // Recupera o usuário
            var user = UserBearerRepository.Get(model.Username, model.Password);

            //Verifica se o usuário existe ou se o usuário e senha são válidos
            if (user == null || (user.Password != model.Password))
                return StatusCode(403, new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(user);

            // Retorna os dados
            return new
            {
                token = token
            };
        }
        catch (NullReferenceException ex)
        {
            return StatusCode(400, ex.Message);
        }
    }

    [HttpGet]
    [Route("authenticated")]
    [Authorize]
    public string Authenticated()
    {
        return User.Identity.Name;
    }
}