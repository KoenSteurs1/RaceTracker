using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubApp1.Entities
{
    using System.Collections.Generic;

    public class Group : Entity
    {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Driver> Drivers { get; set; }
    }
}
