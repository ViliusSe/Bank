using Application.Services;
using Domain.Models.Dtos.Account;
using Domain.Models.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace Bank.WebApi.Controllers
{
    [ApiController, Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _service;
        public AccountController(AccountService service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateAccount dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }
    }
}
