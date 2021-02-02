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
    public partial class PostDetailPage : ContentPage
    {
        Post selectedPost;

        public PostDetailPage()
        {

        }
        public PostDetailPage(Post selectedPost)
        {
            InitializeComponent();

            this.selectedPost = selectedPost;

            experienceEntry.Text = selectedPost.Experience;
            venueLabel.Text = selectedPost.VenueName;
            categoryLabel.Text = selectedPost.CategoryName;
            addressLabel.Text = selectedPost.Address;
            coordinatesLabel.Text = $"{selectedPost.Latitude}, {selectedPost.Longitude}";
            distanceLabel.Text = $"{selectedPost.Distance} m";
        }

        private async void updateButton_Clicked(object sender, EventArgs e)
        {
            selectedPost.Experience = experienceEntry.Text;

            await App.MobileService.GetTable<Post>().UpdateAsync(selectedPost);
            await DisplayAlert("Success", "Experience succesfully updated", "Ok");
        }

        private async void deleteButton_Clicked(object sender, EventArgs e)
        {
            await App.MobileService.GetTable<Post>().DeleteAsync(selectedPost);
            await DisplayAlert("Success", "Experience succesfully deleted", "Ok");
        }
    }
}