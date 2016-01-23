using System;
using System.Collections.Generic;
using System.Text;

namespace HubApp1.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HubApp1.Entities;

    public interface IRaceService
    {
        Task<IEnumerable<Race>> GetAll();
        Task<string> AddRace(Race race);
        Task<string> DeleteRace(int id);

    }
}
