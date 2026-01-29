using AccountKeep.Interfaces;
using AccountKeep.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountKeep.Controllers
{
    // Marks this as a Web API controller with automatic model binding & validation
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsApiController : Controller
    {
        // Service handles transaction logic and data access
        private readonly ITransactionService _transactionService;

        public TransactionsApiController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: api/transactions/account/1 - Returns all transactions for a specific account
        [HttpGet("account/{accountId}")]
        public IActionResult GetByAccount(int accountId)
        {
            return Ok(_transactionService.GetByAccount(accountId));
        }

        // GET: api/transactions/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var transaction = _transactionService.GetById(id);

            // Proper 404 handling
            if (transaction == null) return NotFound();

            return Ok(transaction);
        }

        // POST: api/transactions
        [HttpPost]
        public IActionResult Create(Transaction transaction)
        {
            // No view — API returns status only
            _transactionService.Add(transaction);
            return Ok();
        }

        // PUT: api/transactions/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, Transaction transaction)
        {
            // Prevents ID mismatch attacks or accidental updates
            if (id != transaction.TransactionId) return BadRequest();

            _transactionService.Update(transaction);
            return Ok();
        }
    }
}
