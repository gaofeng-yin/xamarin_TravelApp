using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelRecordApp.Model
{
    public class Post : INotifyPropertyChanged
    {
        private string id;

        public string Id
        {
            get { return id; }
            set 
            { 
                id = value;
                OnPropertyChnaged("Id");
            }
        }

        private string experience;

        public string Experience
        {
            get { return experience; }
            set 
            {
                experience = value;
                OnPropertyChnaged("Experience");
            }
        }

        private string venueName;

        public string VenueName
        {
            get { return venueName; }
            set 
            { 
                venueName = value;
                OnPropertyChnaged("VenueName");
            }
        }

        private string categoryIdmyVar;

        public string CategoryId
        {
            get { return categoryIdmyVar; }
            set 
            { 
                categoryIdmyVar = value;
                OnPropertyChnaged("CategoryId");
            }
        }

        private string categoryNamemyVar;

        public string CategoryName
        {
            get { return categoryNamemyVar; }
            set 
            { 
                categoryNamemyVar = value;
                OnPropertyChnaged("CategoryName");
            }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set 
            { 
                address = value;
                OnPropertyChnaged("Address");
            }
        }

        private double latitude;

        public double Latitude
        {
            get { return latitude; }
            set 
            { 
                latitude = value;
                OnPropertyChnaged("Latitude");
            }
        }

        private double longitude;

        public double Longitude
        {
            get { return longitude; }
            set 
            { 
                longitude = value;
                OnPropertyChnaged("Longitude");
            }
        }

        private int distance;

        public int Distance
        {
            get { return distance; }
            set 
            { 
                distance = value;
                OnPropertyChnaged("Distance");
            }
        }


        private string userId;

        public string UserId
        {
            get { return userId; }
            set 
            { 
                userId = value;
                OnPropertyChnaged("UserId");
            }
        }

        private Venue venue;

        [JsonIgnore]
        public Venue Venue
        {
            get { return venue; }
            set
            { 
                venue = value;

                if(venue.categories != null)
                { 
                    var firstCategory = venue.categories.FirstOrDefault();

                    if(firstCategory != null)
                    { 
                        CategoryId = firstCategory.id;
                        CategoryName = firstCategory.name;
                    }
                }

                if (venue.location != null) 
                { 
                    Address = venue.location.address;
                    Distance = venue.location.distance;
                    Latitude = venue.location.lat;
                    Longitude = venue.location.lng;
                }
                VenueName = venue.name;
                UserId = App.users.Id;

                OnPropertyChnaged("Venue");
            }
        }

        private DateTimeOffset createdat;

        public DateTimeOffset CREATEDAT
        {
            get { return createdat; }
            set 
            { 
                createdat = value;
                OnPropertyChnaged("CREATEDAT");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public static async void Insert(Post post)
        {
            await App.MobileService.GetTable<Post>().InsertAsync(post);
        }

        public static async Task<List<Post>> Read()
        {
            var posts = await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.users.Id).ToListAsync();
            return posts;
        }
        public static Dictionary<string, int> PostCategories(List<Post> posts)
        {
            var categories = (from p in posts
                              orderby p.CategoryId
                              select p.CategoryName).Distinct().ToList();

            //this is same the above code snipet: categories == categories2
            //  var categories2 = postTable.OrderBy(p => p.CategoryId).Select(p => p.CategoryName).Distinct().ToList();

            Dictionary<string, int> categoriesCount = new Dictionary<string, int>();
            foreach (var category in categories)
            {
                var count = (from post in posts
                             where post.CategoryName == category
                             select post).ToList().Count;
                //this is the same above code snippet : count == count2
                //var count2 = postTable.Where(p => p.CategoryName == category).ToList().Count;

                categoriesCount.Add(category, count);
            }
            return categoriesCount;
        }
        public static async void update(Post post)
        {
            await App.MobileService.GetTable<Post>().UpdateAsync(post);
        }
        public static async void delete(Post post)
        {
            await App.MobileService.GetTable<Post>().DeleteAsync(post);
        }

        private void OnPropertyChnaged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
