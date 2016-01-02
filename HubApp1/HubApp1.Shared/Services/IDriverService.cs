using System;
using System.Collections.Generic;
using System.Text;

namespace HubApp1.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    //// DrDobbsApp.Entities (Portable Class Library)
    using HubApp1.Entities;

    public interface IDriverService
    {
        Task<IEnumerable<Driver>> GetAll();
        Task<string> AddDriver(Driver driver);
        Task<string> DeleteDriver(int id);

    }
}
