using Microsoft.AspNetCore.Mvc;
using PeopleManager.Ui.Mvc.Models.Core;
using PeopleManager.Ui.Mvc.Models;

namespace PeopleManager.Ui.Mvc.Controllers
{
    public class PeopleController : Controller
    {

        private readonly PeopleManagerDbContext _peopleManagerDbContext;

        public PeopleController(PeopleManagerDbContext peopleManagerDbContext)
        {
            _peopleManagerDbContext = peopleManagerDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var people = _peopleManagerDbContext.People.ToList();

            return View(people);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  // => [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Person person)
        {
            _peopleManagerDbContext.People.Add(person);
            _peopleManagerDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit([FromRoute] int id)
        {
            var person = _peopleManagerDbContext.People.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return RedirectToAction("Index");
            }
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  // => [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, [FromForm] Person person)
        {
            var dbPerson = _peopleManagerDbContext.People.FirstOrDefault(p => p.Id == id);
            if (dbPerson == null)
            {
                return RedirectToAction("Index");
            }
            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Email = person.Email;
            _peopleManagerDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
