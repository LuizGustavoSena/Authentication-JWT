using AuthenticationJWT.Api.Models;
using AuthenticationJWT.Api.Repositories;
using AuthenticationJWT.Api.Services;
using AuthenticationJWT.Api.VielModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationJWT.Api.Controllers
{
    [Route("v1/account")]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<LoginViewModel> Authentication([FromBody] LoginViewModel login)
        {
            User user = UserRepositories.Get(login.Username, login.Password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var userView = new loginTokenViewModel
            {
                Username = user.UserName,
                Token = TokenService.GerateToken(user)
            };

            redisService.SetRedis(userView);

            return Ok(userView);
        }

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public ActionResult<string> Authenticated()
        {
            return Ok(string.Format("Autenticado {0}", User.Identity.Name));
        }

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee")]
        public ActionResult Role()
        {
            return Ok(string.Format("Autenticado Employee {0}", User.Identity.Name));
        }

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public ActionResult Manager()
        {
            return Ok(string.Format("Autenticado Manager {0}", User.Identity.Name));
        }

        [HttpGet]
        [Route("user")]
        public ActionResult UserToken()
        {
            var token = Request.Headers["Authorization"];
            string tokenS = token.ToString().Replace("Bearer ", "");
            return Ok(redisService.GetRedis(tokenS));
        }
    }
}
