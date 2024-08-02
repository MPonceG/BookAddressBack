using AddressBookBackEnd.Data.Context;
using AddressBookBackEnd.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AddressBookBackEnd.Test
{
    // Fixture class for setting up the DbContext
    public class AddressBookDbContextFixture : IDisposable
    {
        public AddressBookDbContext Context { get; private set; }

        public AddressBookDbContextFixture()
        {
            var options = new DbContextOptionsBuilder<AddressBookDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            Context = new AddressBookDbContext(options);

            // Optionally seed the database here
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            Context.Person.Add(new Person { Id = 1, FirstName = "John Doe", Address = "Larry", DateOfBirth = "Larry", Email = "Larry", LastName = "Tu mare", Phone = "99", Sex = "M" });
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
