using AddressBookBackEnd.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AddressBookBackEnd.Data.Context
{
    public class AddressBookDbContext(DbContextOptions<AddressBookDbContext> options) : DbContext(options)
    {
        public DbSet<Person> Person { get; set; }
    }
}
