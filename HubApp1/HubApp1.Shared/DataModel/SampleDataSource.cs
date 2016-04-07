using HubApp1.Entities;
using HubApp1.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// The data model defined by this file serves as a representative example of a strongly-typed
// model.  The property names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs. If using this model, you might improve app 
// responsiveness by initiating the data loading task in the code behind for App.xaml when the app 
// is first launched.

namespace HubApp1.Data
{
    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// SampleDataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    public sealed class SampleDataSource
    {
        private static SampleDataSource _sampleDataSource = new SampleDataSource();

        // DRIVERS

        private ObservableCollection<Driver> _items = new ObservableCollection<Driver>();
        
        public ObservableCollection<Driver> Items
        {
            get { return this._items; }
        }

        private async Task RefreshDrivers()
        {
            var driverService = new DriverService();
            var allDrivers = await driverService.GetAll();

            while (_sampleDataSource.Items.Count() > 0)
                _sampleDataSource.Items.RemoveAt(0);

            foreach (var driver in allDrivers)
            {
                var sampleDataItem = new Driver(
                    driver.Id,
                    driver.FirstName,
                    driver.LastName,
                    driver.BirthDate);

                _sampleDataSource.Items.Add(sampleDataItem);
            }
        }

        public static async Task<IEnumerable<Driver>> GetItemsAsync()
        {
            await _sampleDataSource.RefreshDrivers();

            return _sampleDataSource.Items;
        }

        public static async Task<string> DeleteDriver(int id)
        {
            var driverService = new DriverService();
            var response = await driverService.DeleteDriver(id);
            return response.ToString();
        }

        public static async Task<string> AddDriverItem(Driver driver)
        {
            var driverService = new DriverService();
            var response = await driverService.AddDriver(driver);
            return response.ToString();
        }

        public static async Task<string> UpdateDriver(Driver driver)
        {
            var driverService = new DriverService();
            var response = await driverService.UpdateDriver(driver);
            return response.ToString();
        }

        public static async Task<Driver> GetItemAsync(string id)
        {
            await _sampleDataSource.RefreshDrivers();
            int iid = Convert.ToInt16(id);
            var matches = _sampleDataSource.Items.Where(p => p.Id.Equals(iid)).FirstOrDefault();
            return matches;
        }

        // RACES

        private ObservableCollection<Race> _races = new ObservableCollection<Race>();

        public ObservableCollection<Race> Races
        {
            get { return this._races; }
        }

        private async Task RefreshRaces()
        {
            var raceService = new RaceService();
            var allRaces = await raceService.GetAll();

            while (_sampleDataSource.Races.Count() > 0)
                _sampleDataSource.Races.RemoveAt(0);

            foreach (var race in allRaces)
            {
                var sampleDataItem = new Race(
                    race.Id,
                    race.Date,
                    race.Sequence,
                    race.Type,
                    race.Location,
                    race.Comment);

                _sampleDataSource.Races.Add(sampleDataItem);
            }
        }

        public static async Task<IEnumerable<Race>> GetRacesAsync()
        {
            await _sampleDataSource.RefreshRaces();

            return _sampleDataSource.Races;
        }
        public static async Task<string> DeleteRace(int id)
        {
            var raceService = new RaceService();
            var response = await raceService.DeleteRace(id);
            return response.ToString();
        }
        public static async Task<string> AddRace(Race race)
        {
            var raceService = new RaceService();
            var response = await raceService.AddRace(race);
            return response.ToString();
        }
        public static async Task<string> UpdateRace(Race race)
        {
            var raceService = new RaceService();
            var response = await raceService.UpdateRace(race);
            return response.ToString();
        }
        public static async Task<Race> GetRaceAsync(string id)
        {
            await _sampleDataSource.RefreshRaces();
            int iid = Convert.ToInt16(id);
            var matches = _sampleDataSource.Races.Where(p => p.Id.Equals(iid)).FirstOrDefault();
            return matches;
        }
    }
}