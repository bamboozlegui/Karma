using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Karma.Models;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using Karma.Extensions;

namespace Karma.Services
{
    public abstract class JsonFilePostService<T> where T : Post
    {
        internal abstract string JsonFileName { get; }

        protected IEnumerable<T> _posts;

        public IWebHostEnvironment WebHostEnvironment { get; }

        public JsonFilePostService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;

            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                _posts = JsonSerializer.Deserialize<T[]>(jsonFileReader.ReadToEnd(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                _posts = UpdatePostsStatus(_posts);
            }

        }

        public IEnumerable<T> GetPosts()
        {
            return _posts;
        }

        public IEnumerable<T> UpdatePostsStatus(IEnumerable<T> posts)
        {
            foreach (var post in posts)
            {

                if (post.Date.GetTimeSpan().Days > 2)
                {
                    if (post.State == Post.StateEnum.Recent)
                    {
                        post.State = Post.StateEnum.Available;
                    }
                }
                yield return post;
            }
        }

        public void RefreshJsonFile()
        {
            File.WriteAllTextAsync(
                JsonFileName,
                JsonSerializer.Serialize<IEnumerable<T>>(_posts,
                new JsonSerializerOptions { WriteIndented = true }));
        }

        public T GetPost(string id)
        {
            T post = _posts.FirstOrDefault<T>(post => post.ID == id);

            return post;
        }

        public abstract void DeletePost(string id);

        public abstract T UpdatePost(T newPost, IFormFile newPhoto = null);

        public abstract void AddPost(T post, IFormFile photo = null);
    }
}
