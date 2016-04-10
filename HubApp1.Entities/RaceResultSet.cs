using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubApp1.Entities
{
    public class RaceResultSet
    {
        public RaceResultSet() { }

        public RaceResultSet(int id, string position, int driverId, int RaceId)
        {
            this.Id = id;
            this.Position = position;
            this.DriverId = driverId;
            this.RaceId = RaceId;
            this.Title = this.ToString();
        }

        public int Id { get; set; }
        public string Position { get; set; }
        public int DriverId { get; set; }
        public int RaceId { get; set; }
        public string Title { get; private set; }

        public virtual Driver Driver { get; set; }
        public virtual Race Race { get; set; }

        public override string ToString()
        {
            return this.Position + ": " + this.Driver.FirstName + " " + this.Driver.LastName;
        }

    }
}
