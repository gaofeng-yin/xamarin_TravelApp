using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelRecordApp.Model
{
    public class Post
    {
        [PrimaryKey]
        public string Id { get; set; }
        [MaxLength(250)]
        public string Experience { get; set; }
        public string VenueName { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Distance { get; set; }

        public string UserId { get; set; }

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
    }
}
