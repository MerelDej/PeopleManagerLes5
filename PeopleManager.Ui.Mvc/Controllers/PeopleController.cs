using Microsoft.AspNetCore.Mvc;
using PeopleManager.Ui.Mvc.Core;
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }
            _peopleManagerDbContext.People.Add(person);
            _peopleManagerDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit([FromRoute] int id)
        {
            var person = _peopleManagerDbContext.People.FirstOrDefault(p => p.Id == id);
            if (person is null)
            {
                return RedirectToAction("Index");
            }

            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, [FromForm] Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }
            var dbPerson = _peopleManagerDbContext.People.FirstOrDefault(p => p.Id == id);
            if (dbPerson is null)
            {
                return RedirectToAction("Index");
            }
            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Email = person.Email;
            _peopleManagerDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete([FromRoute] int id)
        {
            var person = _peopleManagerDbContext.People.FirstOrDefault(p => p.Id == id);
            if (person is null)
            {
                return RedirectToAction("Index");
            }
            return View(person);
        }

        [HttpPost]
        [Route("People/Delete/{id:int?}")] // er is niet altijd een id
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id) // kan via [FromForm] en [FromRoute] maar er mag maar 1 dus zetten we geeneen
        {
            var person = new Person
            {
                Id = id,
                FirstName = string.Empty,
                LastName = string.Empty
            };
            _peopleManagerDbContext.People.Attach(person);
            _peopleManagerDbContext.People.Remove(person);
            _peopleManagerDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}