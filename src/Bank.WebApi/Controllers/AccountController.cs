using Application.Services;
using Domain.Models.Dtos.Account;
using Domain.Models.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace Bank.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing users accounts.
    /// </summary>
    [ApiController, Route("[controller]")]
   
    public class AccountController : ControllerBase
        
    {
        private readonly AccountService _service;

        /// <summary>
        /// Initializes a new instance of the AccountController.
        /// </summary>
        public AccountController(AccountService service)
        {
            _service = service;
        }

        /// <summary>
        /// Creates Account
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateAccount dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }
    }
}
