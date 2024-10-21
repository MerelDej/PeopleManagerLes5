namespace PeopleManager.Ui.Mvc.Models
{
    public class Person
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Email { get; set; }
    }
}
