using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        Post post;
        public NewTravelPage()
        {
            InitializeComponent();

            post = new Post();
            containerStackLayout.BindingContext = post; 
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await Venue.GetVenues(position.Latitude, position.Longitude);
            venueListView.ItemsSource = venues;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try { 
            var selectedVenue = venueListView.SelectedItem as Venue;
            var firstCategory = selectedVenue.categories.FirstOrDefault();

                post.CategoryId = firstCategory.id;
                post.CategoryName = firstCategory.name;
                post.Address = selectedVenue.location.address;
                post.Distance = selectedVenue.location.distance;
                post.Latitude = selectedVenue.location.lat;
                post.Longitude = selectedVenue.location.lng;
                post.VenueName = selectedVenue.name;
                post.UserId = App.users.Id;

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

                Post.Insert(post);
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