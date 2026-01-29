using AccountKeep.Interfaces;
using AccountKeep.Models;
using Microsoft.AspNetCore.Mvc;

// Inherits BaseController → login/session check runs automatically
public class PersonsController : BaseController
{
    // Service handles data + business logic
    private readonly IPersonService _personService;

    public PersonsController(IPersonService personService)
    {
        _personService = personService;
    }

    // GET: /Persons
    public IActionResult Index(string search)
    {
        var persons = _personService.GetAll();

        // Searches across Person fields AND related Accounts
        if (!string.IsNullOrEmpty(search))
        {
            persons = persons.Where(p =>
                p.IdNumber.Contains(search) ||
                p.Surname.Contains(search) ||
                p.Accounts.Any(a => a.AccountNumber.Contains(search))
            ).ToList();
        }

        return View(persons);
    }

    // GET: /Persons/Create
    public IActionResult Create() => View();

    // POST: /Persons/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Person person)
    {
        Console.WriteLine($"IDNumber={person.IdNumber}, FirstName={person.FirstName}, Surname={person.Surname}, Email={person.Email}, Phone={person.PhoneNumber}");

        if (ModelState.IsValid)
        {
            // Validation attributes enforced before save
            _personService.Add(person);
            return RedirectToAction("Index");
        }
        return View(person);
    }

    // GET: /Persons/Edit/5
    public IActionResult Edit(int id)
    {
        var person = _personService.GetById(id);
        // Null check prevents invalid edits
        if (person == null) return NotFound();
        return View(person);
    }

    // POST: /Persons/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Person person)
    {
        if (ModelState.IsValid)
        {
            _personService.Update(person);
            return RedirectToAction("Index");
        }
        return View(person);
    }

    // GET: /Persons/Delete/5
    public IActionResult Delete(int id)
    {
        var person = _personService.GetById(id);
        if (person == null) return NotFound();

        // Business rule: cannot delete if any account is open
        if (person.Accounts.Any(a => a.Status == "Open"))
        {
            TempData["Error"] = "Cannot delete person with open accounts.";
            return RedirectToAction("Index");
        }

        return View(person);
    }

    // POST: /Persons/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        // Final delete after confirmation
        _personService.Delete(id);
        TempData["Success"] = "Person deleted successfully.";
        return RedirectToAction("Index");
    }

}
