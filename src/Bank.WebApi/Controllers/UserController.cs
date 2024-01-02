using Microsoft.AspNetCore.Mvc;
using Domain.Models.Dtos.User;
using Application.Services;
using Domain.Models;

namespace Bank.WebApi.Controllers
{
    [ApiController, Route("[controller]")]

    public class UserController : ControllerBase
    {

        private readonly UserService _service;
        public UserController(UserService service)
        {
            _service = service;
        }

       
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateUser dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

        //perziureti visas transakcijas.
    }
}
