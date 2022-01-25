using System;
using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Application.Entities
{
    public class Ticket
    {
        public Ticket(string content, Person person)
        {
            Content = !string.IsNullOrWhiteSpace(content)
                ? content
                : throw new ArgumentNullException(nameof(content));

            Person = person ?? throw new ArgumentNullException(nameof(person));
            PersonId = Person.Id;
        }

        public Ticket()
        {
        }

        /// <summary>
        /// Gets Id
        /// </summary>
        [Key]
        public int Id { get; }

        /// <summary>
        /// Gets Content
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// Gets Person Id
        /// </summary>
        public int PersonId { get; private set; }

        /// <summary>
        /// Gets Person
        /// </summary>
        public Person Person { get; private set; }
    }
}