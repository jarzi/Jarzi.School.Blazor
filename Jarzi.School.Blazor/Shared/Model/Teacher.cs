using System.ComponentModel.DataAnnotations;

namespace Jarzi.School.Blazor.Shared.Model
{
    public class Teacher
    {
        public int Id { get; set; }
        public ICollection<SchoolClass>? SchoolClasses { get; set; }
        [Required(ErrorMessage = "Nauczyciel musi mieć imię")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Nauczyciel musi mieć nazwisko")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Nauczyciel musi mieć podany etat")]
        public double Tenure { get; set; }
        [Required(ErrorMessage = "Nauczyciel musi mieć podaną datę rozpoczęcia pracy!")]
        public DateTime StartDate { get; set; }
    }
}
