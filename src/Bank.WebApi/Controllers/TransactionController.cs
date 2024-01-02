using Application.Services;
using Domain.Models.Dtos.Account;
using Domain.Models.Dtos.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace Bank.WebApi.Controllers
{
    [ApiController, Route("[controller]")]
    public class TransactionController : ControllerBase
    {

        private readonly TransactionService _service;
        public TransactionController(TransactionService service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateTransaction dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPost("TopUp/{accountId}/{amount}")]
        public async Task<IActionResult> TopUp(int accountId, int amount)
        {
            CreateTransaction dto = new CreateTransaction()
            {
                DebitorAccounId = accountId,
                CreditorAccountId = accountId,
                Amount = amount
            };
            var result = await _service.TopUp(dto);
            return Ok(result);
        }

        [HttpGet("{UserId}/{OrderBy}/{Direction}")]
        public async Task<IActionResult> GetHistoryByUserid(int UserId, string OrderBy, string Direction)
        {
            HistoryTransactions dto = new HistoryTransactions()
            {
                UserId = UserId,
                OrderBy = OrderBy,
                Direction = Direction
            };
            var result = await _service.GetHistoryByUserid(dto);
            return Ok(result);
        }
    }
}
