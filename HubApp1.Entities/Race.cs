using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubApp1.Entities
{
    public class Race
    {
        public Race() { }

        public Race(int id, DateTime? date, int? sequence, int type, string location, string comment)
        {
            this.Id = id;
            this.Date = date;
            this.Sequence = sequence;
            this.Type = type;
            this.Location = location;
            this.Comment = comment;
            this.Title = this.ToString();
            this.FormattedDate = this.FormatDate();
        }

        public int Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Sequence { get; set; }
        public int Type { get; set; }
        public string Location { get; set; }
        public string Comment { get; set; }
        public string Title { get; private set; }
        public string FormattedDate { get; private set; }
        public List<Driver> Drivers { get; set; }
        public virtual ICollection<RaceResultSet> RaceResultSet { get; set; }

        public override string ToString()
        {
            return this.Date.Value.ToString("dd/MM/yyyy") + " " + this.Location;
        }

        public string FormatDate()
        {
            return this.Date.Value.ToString("dd/MM/yyyy");
        }

    }
}
