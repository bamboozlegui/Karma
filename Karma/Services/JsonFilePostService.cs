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
    public abstract class JsonFilePostService<T> where T : Post, IJsonStorable, new()
    {
	//All of the read posts reside in this IEnumerable collection
	//Basically this field allows us to save posts in the server and not re-read the json file each time we need to get the posts
        protected IEnumerable<T> _posts;
	
	//Reads the json file, updates all statuses and overwrites our json file in case the order of posts change//
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
                _posts = _posts.OrderBy(post => post);
            }
            RefreshJsonFile();
        }

        internal string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.ContentRootPath, "data", (new T()).GetJsonName()); }
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

	//A way to extract posts from the service
        public IEnumerable<T> GetPosts() => _posts;

	public IEnumerable<T> SearchPosts(string searchTerm)
	{
	    if(searchTerm == null)
                return _posts;

            return _posts.Where(post => post.Title.Contains(searchTerm) ||
				post.PosterName.Contains(searchTerm));
        }

	//Updates all posts statuses according to theirs' timespan
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

	//Overwrites associated json file with current posts in the server
        public void RefreshJsonFile()
        {
            File.WriteAllTextAsync(
                JsonFileName,
                JsonSerializer.Serialize<IEnumerable<T>>(_posts,
                new JsonSerializerOptions { WriteIndented = true }));
        }

	//A way to return a single post by specifying ID
        public T GetPost(string id) => _posts.FirstOrDefault<T>(post => post.ID == id);

	//To be implemented by subclasses
        public abstract void DeletePost(string id);

        public abstract T UpdatePost(T newPost, IFormFile newPhoto = null);

        public abstract void AddPost(T post, IFormFile photo = null);
    }
}
