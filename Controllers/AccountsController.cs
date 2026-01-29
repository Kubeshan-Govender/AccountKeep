using AccountKeep.Interfaces;
using AccountKeep.Models;
using AccountKeep.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AccountKeep.Controllers
{
    public class AccountsController : Controller
    {
        // Dependency injection of services for DB operations and business logic
        private readonly IAccountService _accountService;
        private readonly IPersonService _personService;

        public AccountsController(IAccountService accountService, IPersonService personService)
        {
            _accountService = accountService;
            _personService = personService;
        }

        // GET: /Accounts/Details/1  → List all accounts for a person
        public IActionResult Details(int personId)
        {
            // Always check for null to avoid errors
            var person = _personService.GetById(personId);
            if (person == null) return NotFound();

            ViewBag.PersonName = $"{person.FirstName} {person.Surname}";
            ViewBag.PersonId = personId;

            // Retrieves all accounts for a specific person
            var accounts = _accountService.GetByPerson(personId);
            return View(accounts);
        }

        // GET: /Accounts/Create?personId=1
        public IActionResult Create(int personId)
        {
            ViewBag.PersonId = personId;

            // Pre-fill PersonId to bind to new account
            return View(new Account { PersonId = personId });
        }

        // POST: /Accounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Account account)
        {
            // Check validation attributes from the model
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                                .SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage);
                return Content("Validation failed: " + string.Join(", ", errors));
            }

            _accountService.Add(account);
            return RedirectToAction("Details", new { personId = account.PersonId });
        }

        // GET: /Accounts/Edit/5
        public IActionResult Edit(int id)
        {
            var account = _accountService.GetById(id);
            if (account == null) return NotFound();

            // Map entity to ViewModel for safe editing
            var vm = new AccountEditViewModel
            {
                AccountId = account.AccountId,
                PersonId = account.PersonId,
                AccountNumber = account.AccountNumber,
                AccountType = account.AccountType,
                Status = account.Status,
                Balance = account.Balance
            };

            return View(vm);
        }


        // POST: /Accounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AccountEditViewModel vm)
        {
            var account = _accountService.GetById(vm.AccountId);
            if (account == null) return NotFound();

            // Business rule: cannot close account with non-zero balance
            if (vm.Status == "Closed" && account.Balance != 0)
            {
                ModelState.AddModelError("Status", "Account cannot be closed unless balance is zero.");
                return View(vm);
            }

            account.AccountType = vm.AccountType;
            account.Status = vm.Status;

            // Save changes via service
            _accountService.Update(account);

            return RedirectToAction("Details", new { personId = vm.PersonId });
        }

    }
}
