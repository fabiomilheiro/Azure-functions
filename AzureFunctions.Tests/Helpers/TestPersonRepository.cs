using System.Collections.Generic;
using System.Linq;
using Azure_functions.Persons;

namespace AzureFunctions.Tests.Helpers
{
    public class TestPersonRepository : IPersonRepository
    {

        public List<Person> Persons { get; set; } = new List<Person>
        {
            new Person(1, "Fake John Smith"),
            new Person(2, "Fake John Smith 2"),
            new Person(3, "Fake John Smith 3"),
            new Person(4, "Fake John Smith 4"),
            new Person(5, "Fake John Smith 5"),
            new Person(6, "Fake John Smith 6"),
            new Person(7, "Fake John Smith 7"),
            new Person(8, "Fake John Smith 8 "),
            new Person(9, "Fake John Smith 9"),
            new Person(10, "Fake John Smith 10")
        };
        
        public Person GetPerson(int id)
        {
            return this.Persons.FirstOrDefault(p => p.Id == id);
        }
    }
}