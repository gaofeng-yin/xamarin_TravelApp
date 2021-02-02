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
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            /*
            //with using we don't have to worry about close()
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            { 
                //in case table doesn't exist. It will not replace actual table
                conn.CreateTable<Post>();
                var posts = conn.Table<Post>().ToList();

                postListView.ItemsSource = posts;
            }
            */
            //get from azure cloud data base 
            var posts = await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.users.Id).ToListAsync();
            postListView.ItemsSource = posts;
        }

        private void postListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedPost = postListView.SelectedItem as Post;
            if(selectedPost != null)
            {
                Navigation.PushAsync(new PostDetailPage(selectedPost));
            }
        }
    }
}