using Application.Services;
using Domain.Models.Dtos.Account;
using Domain.Models.Dtos.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace Bank.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing transfers.
    /// </summary>
    [ApiController, Route("[controller]")]
    public class TransactionController : ControllerBase
    {

        private readonly TransactionService _service;

        /// <summary>
        /// Initializes a new instance of the TransferController.
        /// </summary>
        public TransactionController(TransactionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Creates transaction from one account to another
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateTransaction dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        /// <summary>
        /// Creates TopUp transaction
        /// </summary>
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

        /// <summary>
        /// Shows all transactions depending on provided user ID, and ordering by debitor or creditor acount, ascending or descending
        /// </summary>
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
