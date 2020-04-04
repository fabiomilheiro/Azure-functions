using System.Collections.Generic;
using System.Linq;

namespace Azure_functions.Persons
{
    public interface IPersonRepository
    {
        Person GetPerson(int id);
    }

    public class PersonRepository : IPersonRepository
    {
        private List<Person> persons = new List<Person>
        {
            new Person(1, "John Smith"),
            new Person(2, "John Smith 2"),
            new Person(3, "John Smith 3"),
            new Person(4, "John Smith 4"),
            new Person(5, "John Smith 5"),
            new Person(6, "John Smith 6"),
            new Person(7, "John Smith 7"),
            new Person(8, "John Smith 8 "),
            new Person(9, "John Smith 9"),
            new Person(10, "John Smith 10")
        };

        public Person GetPerson(int id)
        {
            return this.persons.FirstOrDefault(p => p.Id == id);
        }
    }
}