using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubApp1.Entities
{
    public class Driver
    {
        public Driver() {}
        public Driver(int id, String firstName, String lastName, DateTime? birthDate)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.Title = this.ToString();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public byte[] Picture { get; set; }
        public string Title { get; private set; }
        public ICollection<RaceResultSet> RaceResultSet { get; set; }

        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}
