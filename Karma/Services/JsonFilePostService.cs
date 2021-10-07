using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Karma.Models;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace Karma.Services
{
    public class JsonFilePostService<T> where T : Post, IJsonStorable, new()
    {
        public JsonFilePostService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
                get { return Path.Combine(WebHostEnvironment.ContentRootPath, "data", (new T()).GetJsonName()); }
        }

        public IEnumerable<T> GetPosts()
        {
            using(var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<T[]>(jsonFileReader.ReadToEnd(),new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        public void RefreshPosts(IEnumerable<T> posts)
        {
            File.WriteAllTextAsync(
                JsonFileName, 
                JsonSerializer.Serialize<IEnumerable<T>>(posts, 
                new JsonSerializerOptions {WriteIndented = true}));
        }

        public T GetPost(JsonFilePostService<T> postService, string id)
        {
            IEnumerable<T> posts = postService.GetPosts();
            T post = posts.FirstOrDefault<T>(post => post.ID == id);

            return post;
        }

        public void DeletePost(JsonFilePostService<ItemPost> submitService, string id)
        {
            IEnumerable<ItemPost> posts = submitService.GetPosts();
            ItemPost post = posts.FirstOrDefault<ItemPost>(post => post.ID == id);

            if(post.Picture != "noimage.jpg")
            {
                string filePath = Path.Combine(WebHostEnvironment.WebRootPath, "images", post.Picture);
                System.IO.File.Delete(filePath);
            }

            submitService.RefreshPosts(posts.Where(post => post.ID != id));
        }
    }
}