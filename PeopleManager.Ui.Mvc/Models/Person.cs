using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeopleManager.Ui.Mvc.Models
{
    [Table(nameof(Person))]
    public class Person
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("First name")]
        public required string FirstName { get; set; }

        [Required]
        [DisplayName("Last name")]
        public required string LastName { get; set; }

        [DisplayName("E-mail address")]
        [EmailAddress]
        public string? Email { get; set; }
    }
}