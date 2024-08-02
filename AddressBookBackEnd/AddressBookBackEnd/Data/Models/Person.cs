namespace AddressBookBackEnd.Data.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string Phone { get; set; }

    }
}
