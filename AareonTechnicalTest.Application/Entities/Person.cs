using System;
using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Application.Entities
{
    public class Person
    {
        public Person(string forename, string surname, bool isAdmin)
        {
            Forename = !string.IsNullOrWhiteSpace(forename) ? forename : throw new ArgumentNullException(nameof(forename));
            Surname = !string.IsNullOrWhiteSpace(surname) ? surname : throw new ArgumentNullException(nameof(surname));
            IsAdmin = isAdmin;
        }

        public Person()
        {
        }

        /// <summary>
        /// Gets Id
        /// </summary>
        [Key]
        public int Id { get; }

        /// <summary>
        /// Gets Forename
        /// </summary>
        public string Forename { get; private set; }

        /// <summary>
        /// gets Surname
        /// </summary>
        public string Surname { get; private set; }

        /// <summary>
        /// Gets Is Admin
        /// </summary>
        public bool IsAdmin { get; private set; }
    }
}