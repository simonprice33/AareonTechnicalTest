using System;
using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Application.Entities
{
    public class Ticket
    {
        public Ticket(string content, Person person)
        {
            Content = content;
            Person = person;
            PersonId = Person.Id;
            UpdatedDateTime = DateTime.Now;
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
        /// Gets Created Date
        /// </summary>
        public DateTime UpdatedDateTime { get; private set; }

        /// <summary>
        /// Gets Person Id
        /// </summary>
        public int PersonId { get; private set; }

        /// <summary>
        /// Gets Is Removed
        /// </summary>
        public bool IsRemoved { get; private set; }

        /// <summary>
        /// Gets Person
        /// </summary>
        public Person Person { get; private set; }

        public bool CanRemove()
        {
            return !IsRemoved;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public bool CanUpdateTicket(string content)
        {
            return content != Content;
        }

        public void UpdateContent(string content)
        {
            Content = content;
        }

        public void UpdatedBy(Person person)
        {
            PersonId = person.Id;
        }
    }
}