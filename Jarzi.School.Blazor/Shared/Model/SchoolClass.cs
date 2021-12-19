using System.ComponentModel.DataAnnotations;

namespace Jarzi.School.Blazor.Shared.Model
{
    public class SchoolClass
    {
        public int Id { get; set; }
        public ICollection<Student>? Students { get; set; }
        public ICollection<Teacher>? Teachers { get; set; }
        [Required(ErrorMessage = "Klasa musi mieć nazwę")]
        public string Name { get; set; }
    }
}
