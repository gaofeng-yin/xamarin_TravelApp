﻿using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Logic;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        public NewTravelPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);
            venueListView.ItemsSource = venues;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try { 
            var selectedVenue = venueListView.SelectedItem as Venue;
            var firstCategory = selectedVenue.categories.FirstOrDefault();
            Post post = new Post()
            { 
                    Experience = experienceEntry.Text,
                    CategoryId = firstCategory.id,
                    CategoryName = firstCategory.name,
                    Address = selectedVenue.location.address,
                    Distance = selectedVenue.location.distance,
                    Latitude = selectedVenue.location.lat,
                    Longitude = selectedVenue.location.lng,
                    VenueName = selectedVenue.name,
                    UserId = App.users.Id,
            };

                /* //with using we don't have to worry about close method.
                 using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation)) { 

                     conn.CreateTable<Post>();
                     int rows = conn.Insert(post);
                     conn.Close();

                     if(rows > 0)
                     {
                         DisplayAlert("Success", "Experience succesfully inserted", "OK");
                     }
                     else
                     {
                         DisplayAlert("Failure", "Experience not succesfully inserted", "OK");
                     }
                 } */

                await App.MobileService.GetTable<Post>().InsertAsync(post);
                await DisplayAlert("Success", "Experience succesfully inserted", "Ok");
            }
            catch (NullReferenceException nre)
            {
                await DisplayAlert("Failure", "Experience failed to be inserted: null exception", "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Failure", "Experience failed to be inserted: exceptions", "Ok");
                Console.WriteLine(ex);
            }
        }
    }
}