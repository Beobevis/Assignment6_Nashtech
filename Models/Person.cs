
using System.ComponentModel.DataAnnotations;

namespace Assignment6.Models;

public class Person

{
    [Required, MaxLength(50)]
    public string? FirstName { get; set; }
    [Required, MaxLength(50)]
    public string? LastName { get; set; }

    public string? Gender { get; set; }

    public DateTime DOB { get; set; }

    
    public string? PhoneNumber { get; set; }
    [Required]
    public string? BirthPlace { get; set; }

    public int Age
    {
        get
        {
            return DateTime.Now.Year - DOB.Year;
        }
    }

    public bool IsGraduated { get; set; }


    public int TotalDays
    {
        get
        {
            return (int)(DateTime.Now - DOB).TotalDays;
        }
    }
    // public int CompareTo(object? obj){
    //     return TotalDays.CompareTo(((Member)obj).TotalDays);
    // }
    public string FullName
    {
        get
        {
            return $"{LastName} {FirstName}";
        }
    }


}
public class PersonEditModel : Person
{
    public int Index { get; set; }
    public PersonEditModel() { }
    public PersonEditModel(Person person)
    {
        FirstName = person.FirstName;
        LastName = person.LastName;
        DOB = person.DOB;
        Gender = person.Gender;
        BirthPlace = person.BirthPlace;

    }
}