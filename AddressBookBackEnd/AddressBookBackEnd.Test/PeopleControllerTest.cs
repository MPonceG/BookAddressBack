using AddressBookBackEnd.Controllers;
using AddressBookBackEnd.Data.Context;
using AddressBookBackEnd.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel;
using System.Xml.Linq;

namespace AddressBookBackEnd.Test
{
    public class PeopleControllerTest: IClassFixture<AddressBookDbContextFixture>
    {
        private readonly AddressBookDbContext _context;
        public PeopleController _peopleController1;
        public PeopleControllerTest(AddressBookDbContextFixture fixture)
        {
            _context = fixture.Context;
            _peopleController1 = new PeopleController(_context);
        }

        [Fact]
        public async Task GetPersons()
        {
            var result = await _peopleController1.GetPerson();
            Assert.NotNull(result.Value);
        }

        public static IEnumerable<object[]> PersonDataSuccessful()
        {
            yield return new object[]
            {
                new Person
                {
                    Id = 2,
                    FirstName = "Miguel",
                    LastName = "Ponce",
                    Sex = "Male",
                    DateOfBirth = "15/04/1987",
                    Email = "miguelponceg87@gmail",
                    Address = "Lima, Peru",
                    Phone = "+51977972091"
                }
            };
        }


        [Theory]
        [MemberData(nameof(PersonDataSuccessful))]
        public async Task InsertPersonSuccessful(Person person)
        {
            var responseCreated = await _peopleController1.New(person);
            var createdResult = Assert.IsType<CreatedAtActionResult>(responseCreated.Result);
            Assert.Equal(201, createdResult.StatusCode);
            var response = await _peopleController1.GetPerson(person.Id);
            Assert.NotNull(response.Value);
            
            Assert.Equal(person.FirstName, response.Value.FirstName);
            Assert.Equal(person.LastName, response.Value.LastName);

        }

        public static IEnumerable<object[]> PersonWrongData()
        {
            yield return new object[]
            {
                new Person
                {
                    Id = 54,
                }
            };
        }


        [Theory]
        [MemberData(nameof(PersonWrongData))]
        public async Task PersonNotFound(Person person)
        {
            var response = await _peopleController1.GetPerson(person.Id);
            var notFoundResult = Assert.IsType<NotFoundResult>(response.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
         }
    }
}