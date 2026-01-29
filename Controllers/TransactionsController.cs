using AccountKeep.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AccountKeep.Models;

namespace AccountKeep.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // /Transactions/Details?accountId=2
        public IActionResult Details(int accountId)
        {
            var transactions = _transactionService.GetByAccount(accountId);

            var first = transactions.FirstOrDefault();
            ViewBag.AccountId = accountId;
            // Uses first transaction to get PersonId (assumes at least one transaction)
            ViewBag.PersonId = first?.Account.PersonId;

            return View(transactions);
        }

        // GET: /Transactions/Create?accountId=2
        public IActionResult Create(int accountId)
        {
            ViewBag.AccountId = accountId;
            return View(new Transaction
            {
                // Pre-fills account and date
                AccountId = accountId,
                TransactionDate = DateTime.Today
            });
        }

        // POST: /Transactions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Transaction transaction)
        {
            if (transaction.Amount == 0)
            {
                ModelState.AddModelError(nameof(transaction.Amount), "Transaction amount cannot be zero");
            }

            // Business rules enforced before save
            if (transaction.TransactionDate > DateTime.Today)
            {
                ModelState.AddModelError(nameof(transaction.TransactionDate), "Transaction date cannot be in the future");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.AccountId = transaction.AccountId;
                return View(transaction);
            }

            // Service adjusts account balance
            _transactionService.Add(transaction); // now safe
            return RedirectToAction("Details", new { accountId = transaction.AccountId });
        }


        // GET: /Transactions/Edit/5
        public IActionResult Edit(int id)
        {
            var transaction = _transactionService.GetById(id);
            if (transaction == null)
                return NotFound();

            return View(transaction);
        }

        // POST: /Transactions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Transaction transaction)
        {
            // Prevents ID mismatch
            if (id != transaction.TransactionId)
                return BadRequest();

            // Validate transaction rules
            if (transaction.TransactionDate > DateTime.Today)
                ModelState.AddModelError(nameof(transaction.TransactionDate), "Transaction date cannot be in the future.");
            if (transaction.Amount == 0)
                ModelState.AddModelError(nameof(transaction.Amount), "Transaction amount cannot be zero.");

            if (ModelState.IsValid)
            {
                try
                {
                    // Update also recalculates balance
                    _transactionService.Update(transaction); // will adjust balance
                    return RedirectToAction("Details", new { accountId = transaction.AccountId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(transaction);
        }

        // GET: /Transactions/Delete/5
        public IActionResult Delete(int id)
        {
            var transaction = _transactionService.GetById(id);
            if (transaction == null) return NotFound();

            return View(transaction);
        }


        // POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var transaction = _transactionService.GetById(id);
            if (transaction == null) return NotFound();

            // Service must also update account balance
            _transactionService.Delete(id);
            return RedirectToAction("Details", new { accountId = transaction.AccountId });
        }


    }
}
