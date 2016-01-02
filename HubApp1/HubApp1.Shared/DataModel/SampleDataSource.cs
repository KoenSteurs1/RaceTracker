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
    /// Generic item data model.
    /// </summary>
    public class SampleDataItem
    {
        public SampleDataItem(int id, String firstName, String lastName)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Title = this.ToString();
        }

        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Title { get; private set; }

        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }
    }

     //<summary>
     //Generic group data model.
     //</summary>

    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// SampleDataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    public sealed class SampleDataSource
    {
        private static SampleDataSource _sampleDataSource = new SampleDataSource();

        private ObservableCollection<SampleDataItem> _items = new ObservableCollection<SampleDataItem>();
        public ObservableCollection<SampleDataItem> Items
        {
            get { return this._items; }
        }

        private async Task RefreshDataSource()
        {
            var driverService = new DriverService();
            var allDrivers = await driverService.GetAll();

            while (_sampleDataSource.Items.Count() > 0)
                _sampleDataSource.Items.RemoveAt(0);

            foreach (var driver in allDrivers)
            {
                var sampleDataItem = new SampleDataItem(
                    driver.Id,
                    driver.FirstName,
                    driver.LastName);

                _sampleDataSource.Items.Add(sampleDataItem);
            }
        }

        public static async Task<IEnumerable<SampleDataItem>> GetItemsAsync()
        {
            await _sampleDataSource.RefreshDataSource();

            return _sampleDataSource.Items;
        }

        //public static async Task<SampleDataItem> GetItemAsync(string uniqueId)
        //{
        //    await _sampleDataSource.GetSampleDataAsync();
        //    // Simple linear search is acceptable for small data sets
        //    var matches = _sampleDataSource.Items.Where((group) => group.UniqueId.Equals(uniqueId));
        //    if (matches.Count() == 1) return matches.First();
        //    return null;
        //}

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

        public static async Task<SampleDataItem> GetItemAsync(string id)
        {
            await _sampleDataSource.RefreshDataSource();
            int iid = Convert.ToInt16(id);
            var matches = _sampleDataSource.Items.Where(p => p.Id.Equals(iid)).FirstOrDefault();
            return matches;
        }

        //private async Task GetSampleDataAsync()
        //{
        //    if (this._items.Count != 0)
        //        return;

        //    Uri dataUri = new Uri("http://localhost/kartingapi/api/driver");

        //    StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
        //    string jsonText = await FileIO.ReadTextAsync(file);
        //    JsonObject jsonObject = JsonObject.Parse(jsonText);
        //    JsonArray jsonArray = jsonObject["ArrayOfDriver"].GetArray();

        //    foreach (JsonValue groupValue in jsonArray)
        //    {
        //        JsonObject itemObject = groupValue.GetObject();
        //        SampleDataItem item = new SampleDataItem((int)itemObject["ID"].GetNumber(),
        //                                                    itemObject["FirstName"].GetString(),
        //                                                    itemObject["LastName"].GetString());

        //        //foreach (JsonValue itemValue in groupObject["Items"].GetArray())
        //        //{
        //        //    JsonObject itemObject = itemValue.GetObject();
        //        //    group.Items.Add(new SampleDataItem(itemObject["UniqueId"].GetString(),
        //        //                                       itemObject["Title"].GetString(),
        //        //                                       itemObject["Subtitle"].GetString(),
        //        //                                       itemObject["ImagePath"].GetString(),
        //        //                                       itemObject["Description"].GetString(),
        //        //                                       itemObject["Content"].GetString()));
        //        //}
        //        this.Items.Add(item);
        //    }
        //}
    }
}