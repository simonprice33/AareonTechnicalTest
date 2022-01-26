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

        /// <summary>
        /// Update Forename
        /// </summary>
        /// <param name="forename"></param>
        public void UpdateForename(string forename)
        {
            Forename = forename;
        }

        /// <summary>
        /// Updates Surname
        /// </summary>
        /// <param name="surname"></param>
        public void UpdateSurname(string surname)
        {
            Surname = surname;
        }

        /// <summary>
        /// Updates Is Admin
        /// </summary>
        /// <param name="surname"></param>
        public void UpdateAdminStatus(bool isAdmin)
        {
            IsAdmin = isAdmin;
        }

        /// <summary>
        /// Checks can update the person forename
        /// </summary>
        /// <param name="forename"></param>
        /// <returns>true or false</returns>
        public bool CanUpdateForename(string forename)
        {
            return forename != Forename;
        }

        /// <summary>
        /// Checks can update person surname
        /// </summary>
        /// <param name="requestSurname"></param>
        /// <returns>true or false</returns>
        public bool CanUpdateSurname(string surname)
        {
            return surname != Surname;
        }

        /// <summary>
        /// Checks can update admin status
        /// </summary>
        /// <param name="isAdmin"></param>
        /// <returns>true or false</returns>
        public bool CanUpdateAdminStatus(bool isAdmin)
        {
            return isAdmin != IsAdmin;
        }
    }
}