using System.ComponentModel.DataAnnotations;

namespace Jarzi.School.Blazor.Shared.Model
{
    public class Student
    {
        public int Id { get; set; }
        public SchoolClass? SchoolClass { get; set; }
        [Required(ErrorMessage = "Uczeń musi mieć imię")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Uczeń musi mieć nazwisko")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Uczeń musi mieć podaną datę urodzenia")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Uczeń musi mieć podaną średnią ocen!")]
        public double AverageLastYear { get; set; }
    }
}
