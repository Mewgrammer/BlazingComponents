using System;
using System.Collections.Generic;
using System.Text;

namespace Cloud_In_A_Box.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public override string ToString()
        {
            return $"{Id}: {FirstName} {LastName} {BirthDate}";
        }
    }
}
