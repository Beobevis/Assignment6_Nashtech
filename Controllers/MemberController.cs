using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Assignment6.Models;

namespace Assignment6.Controllers;

public class MemberController : Controller
{
    static List<Person> persons = new List<Person>
        {
            new Person
            {
                LastName = "Phuong",
                FirstName = "Nguyen Nam",
                Gender = "Male",
                DOB = new DateTime(2001, 1, 22),
                PhoneNumber = "",
                BirthPlace = "Phu Tho",
                IsGraduated = false
            },
            new Person
            {
                LastName = "Nam",
                FirstName = "Nguyen Thanh",
                Gender = "Male",
                DOB = new DateTime(2001, 1, 20),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person
            {
                LastName = "Son",
                FirstName = "Do Hong",
                Gender = "Male",
                DOB = new DateTime(2000, 11, 6),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person
            {
                LastName = "Huy",
                FirstName = "Nguyen Duc",
                Gender = "Male",
                DOB = new DateTime(1996, 1, 26),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person
            {
                LastName = "Hoang",
                FirstName = "Phuong Viet",
                Gender = "Male",
                DOB = new DateTime(1999, 2, 5),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person
            {
                LastName = "Long",
                FirstName = "Lai Quoc",
                Gender = "Male",
                DOB = new DateTime(1997, 5, 30),
                PhoneNumber = "",
                BirthPlace = "Bac Giang",
                IsGraduated = false
            },
            new Person
            {
                LastName = "Thanh",
                FirstName = "Tran Chi",
                Gender = "Male",
                DOB = new DateTime(2000, 9, 18),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person
            {
                LastName = "Person",
                FirstName = "Old",
                Gender = "Male",
                DOB = new DateTime(1996, 1, 14),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            }
        };
    public IActionResult Index()
    {
        return View(persons);
    }
    public IActionResult AddPerson()
    {
        return View();
    }
    [HttpPost]
    public IActionResult AddPerson(Person model)
    {
        if (!ModelState.IsValid) return View();

        persons.Add(model);

        return RedirectToAction("Index");
    }

    public IActionResult EditPerson(int index)
    {
        if (index <= 0 && index > persons.Count)
            return RedirectToAction("Index");

        var person = persons[index - 1];
        var model = new PersonEditModel(person);
        model.Index = index;

        return View(model);
    }
    [HttpPost]
    public IActionResult EditPerson(PersonEditModel model)
    {
        if (!ModelState.IsValid) return View();
        persons[model.Index - 1] = model;

        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult DeletePerson(int index)
    {
        if (index <= 0 && index > persons.Count)
            return RedirectToAction("Index");

        persons.RemoveAt(index - 1);

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
