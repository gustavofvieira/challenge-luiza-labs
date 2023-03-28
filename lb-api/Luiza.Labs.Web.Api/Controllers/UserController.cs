﻿using Luiza.Labs.Domain.Interfaces.Services;
using Luiza.Labs.Domain.Models;
using Luiza.Labs.Infra.Data.Repositories;
using Luiza.Labs.Sevices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Luiza.Labs.Web.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/<LoginController>
        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        // GET api/<LoginController>/5
        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anonimo";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee, manager")]
        public string Employee() => "Funcionario";


        //// POST api/<LoginController>
        //[HttpPost]
        //[Route("login2")]
        //[AllowAnonymous]
        //public async Task<ActionResult<dynamic>> Authenticate2([FromBody] User model)
        //{
        //    var user = UserRepository.Get(model.UserName, model.Password);

        //    if (user == null)
        //        return NotFound(new { message = "Usuário ou senha incorretos!"});

        //    var token = TokenService.GenerateToken(user);
        //    user.Password = "";
        //    return new
        //    {
        //        user = user,
        //        token = token
        //    };
        //}

        // POST api/<LoginController>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Authenticate([FromBody] User model)
        {
            var token = await _userService.AuthenticateAsync(model);
            return Ok(token);
        }
    }
}
