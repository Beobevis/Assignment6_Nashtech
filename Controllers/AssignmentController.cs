using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Assignment6.Models;
using CsvHelper;
using System.Globalization;
using CsvHelper.TypeConversion;

namespace Assignment6.Controllers;

public class AssignmentController : Controller
{
    static List<Person> persons = new List<Person>
        {
            new Person
            {
                FirstName = "Phuong",
                LastName = "Nguyen Nam",
                Gender = "Male",
                DOB = new DateTime(2001, 1, 22),
                PhoneNumber = "",
                BirthPlace = "Phu Tho",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Nam",
                LastName = "Nguyen Thanh",
                Gender = "Male",
                DOB = new DateTime(2001, 1, 20),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Son",
                LastName = "Do Hong",
                Gender = "Male",
                DOB = new DateTime(2000, 11, 6),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Huy",
                LastName = "Nguyen Duc",
                Gender = "Male",
                DOB = new DateTime(1996, 1, 26),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Hoang",
                LastName = "Phuong Viet",
                Gender = "Male",
                DOB = new DateTime(1999, 2, 5),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Long",
                LastName = "Lai Quoc",
                Gender = "Male",
                DOB = new DateTime(1997, 5, 30),
                PhoneNumber = "",
                BirthPlace = "Bac Giang",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Thanh",
                LastName = "Tran Chi",
                Gender = "Male",
                DOB = new DateTime(2000, 9, 18),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            },
            new Person
            {
                FirstName = "Person",
                LastName = "Old",
                Gender = "Male",
                DOB = new DateTime(1996, 1, 14),
                PhoneNumber = "",
                BirthPlace = "Ha Noi",
                IsGraduated = false
            }
        };
    
    public IActionResult Index(){
        return View();
    }
    // [Route("Assignment/Malemembers")]
    // [Route("Assignment/male-members")]
    //[Route("Nashtech/Assignment/malemembers")]
    public IActionResult GetMembers(){
        var result = from person in persons select person;
        return View(result);
    }
    public IActionResult GetMalesMember()
    {
        var result = persons.Where(m => m.Gender.Equals("Male", StringComparison.CurrentCultureIgnoreCase)).ToList();
        return View(result);
        //return Json(result);

    }
    // [Route("Assignment/oldest-members")]
    // [Route("Nashtech/Assignment/oldest-members")]
    public IActionResult GetOldestMemberbyAge()
    {
        var MaxAge = persons.Max(m => m.Age);
        var oldest = persons.First(m => m.Age == MaxAge);
        return View(oldest);
        //using OrderBy
        //return members.OrderByDescending(m=>m.Age).First();
        //return members.OrderBy(m=>m.Age).Last();

        //using LinQ Query
        // var OldList = from member in members
        //               orderby member.Age descending
        //               select member;
        // return OldList.First();
    }
    //[Route("Assignment/fullname")]
    //[Route("Nashtech/Assignment/fullname")]
    public IActionResult GetFullName()
    {
        var result = persons.Select(m => m.FullName);
        return View(result);
    }
    //[Route("Assignment/split-member-by-birth-year")]
    //[Route("Nastech/Assignment/split-member-by-birth-year")]
    public IActionResult SplitMemberbyBirthYear(int year)
    {
        var result = from person in persons
                     group person by person.DOB.Year.CompareTo(year) into grp
                     select new
                     {
                         Key = grp.Key switch
                         {
                             -1 => $"Birth Year less than {year}",
                             0 => $"Birth Year equals to {year}",
                             1 => $"Birth Year greater than {year}",
                             _ => string.Empty
                         },
                         Data = grp.ToList()
                     };

        return Json(result);
    }
    //[Route("Assignment/exportfile")]
    // [Route("Nastech/Assignment/exportfile")]
    public IActionResult ExportFile()
    {
        var result = WriteCsvToMemory(persons);
        var memoryStream = new MemoryStream(result);
        return new FileStreamResult(memoryStream, "text/csv") { FileDownloadName = "person.csv" };
    }
    public byte[] WriteCsvToMemory(List<Person> records)
    {
        using (var stream = new MemoryStream())
        using (var writer = new StreamWriter(stream))
        using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {

            var options = new TypeConverterOptions { Formats = new[] { "dd/MM/yyyy" } };
            csvWriter.Context.TypeConverterOptionsCache.AddOptions<DateTime>(options);

            csvWriter.WriteRecords(records);
            writer.Flush();
            return stream.ToArray();
        }
    }

}