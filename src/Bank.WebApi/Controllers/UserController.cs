using Microsoft.AspNetCore.Mvc;
using Domain.Models.Dtos.User;
using Application.Services;
using Domain.Models;

namespace Bank.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing users.
    /// </summary>
    [ApiController, Route("[controller]")]

    public class UserController : ControllerBase
    {

        private readonly UserService _service;

        /// <summary>
        /// Initializes a new instance of the UserController.
        /// </summary>
        public UserController(UserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Creates user
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateUser dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        /// <summary>
        /// Shows all users
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        /// <summary>
        /// Shows user infromation according given ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

        //perziureti visas transakcijas.
    }
}
