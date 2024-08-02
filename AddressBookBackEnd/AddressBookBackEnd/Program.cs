using AddressBookBackEnd.Data.Context;
using AddressBookBackEnd.Data.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AddressBookDbContext>(
    opt => opt.UseInMemoryDatabase(builder.Configuration.GetConnectionString("DbName"))    
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});



var app = builder.Build();

// Seed the database with initial data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AddressBookDbContext>();
    SeedDatabase(dbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();

app.MapControllers();

app.Run();


void SeedDatabase(AddressBookDbContext context)
{
    if (!context.Person.Any())
    {
        context.Person.AddRange(
            new Person
            {
                FirstName = "John",
                LastName = "Doe",
                Sex = "Male",
                DateOfBirth = "dsdsd",
                Email = "john.doe@example.com",
                Address = "123 Main St",
                Phone = "123-456-7890"
            },
            new Person
            {
                FirstName = "Jane",
                LastName = "Smith",
                Sex = "Female",
                DateOfBirth = "sdsdsd",
                Email = "jane.smith@example.com",
                Address = "456 Elm St",
                Phone = "098-765-4321"
            }
        );
        context.SaveChanges();
    }
}