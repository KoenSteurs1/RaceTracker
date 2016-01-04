﻿
using HubApp1.Common;
using HubApp1.Data;
using HubApp1.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HubApp1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class AddDriver : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private HelperClass.PageMode pageMode;
        private int driverId;

        public AddDriver()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
 
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        private async void SaveDriverButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.pageMode == HelperClass.PageMode.Add)
            // Adding driver
            { 
                try
                {
                    Driver driver = new Driver();
                    driver.Id = 0;
                    driver.FirstName = firstName.Text;
                    driver.LastName = lastName.Text;
                    driver.BirthDate = null;
                    driver.Picture = null;
               
                    string response = await SampleDataSource.AddDriverItem(driver);

                    MessageDialog msgbox = new MessageDialog("Driver added!");

                    await msgbox.ShowAsync();

                    Frame.Navigate(typeof(ItemPage), response);
                }
                catch (Exception ex)
                {

                }
            }
            else
            // Editing driver
            {
                try
                {
                    Driver driver = new Driver();
                    driver.Id = this.driverId;
                    driver.FirstName = firstName.Text;
                    driver.LastName = lastName.Text;
                    driver.BirthDate = null;
                    driver.Picture = null;

                    string response = await SampleDataSource.UpdateDriver(driver);

                    MessageDialog msgbox = new MessageDialog("Driver updated! Pit stop completed!");

                    await msgbox.ShowAsync();

                    Frame.Navigate(typeof(ItemPage), this.driverId);
                }
                catch (Exception ex)
                {

                }
            }
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.pageMode = ((EditDriverObject)(e.Parameter)).pageMode;
            this.driverId = ((EditDriverObject)(e.Parameter)).Id;

            if (pageMode == HelperClass.PageMode.Edit)
                PageTitle.Text = "Edit Driver";

            if (pageMode == HelperClass.PageMode.Edit)
            {
                SampleDataItem drv;
                drv = await SampleDataSource.GetItemAsync(this.driverId.ToString());
                // retrieve record which is being edited from database
                firstName.Text = drv.FirstName;
                lastName.Text = drv.LastName;
            }

            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}
