using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Karma.Models;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace Karma.Services
{
    public abstract class JsonFilePostService<T> where T : Post, IJsonStorable
    {
        internal abstract string GetJsonFileName();

        public abstract IEnumerable<T> GetPosts();

        public void RefreshPosts(IEnumerable<T> posts)
        {
            File.WriteAllTextAsync(
                GetJsonFileName(), 
                JsonSerializer.Serialize<IEnumerable<T>>(posts, 
                new JsonSerializerOptions {WriteIndented = true}));
        }

        public T GetPost(string id)
        {
            IEnumerable<T> posts = GetPosts();
            T post = posts.FirstOrDefault<T>(post => post.ID == id);

            return post;
        }

        public abstract void DeletePost(string id);

        public abstract void UpdatePost(T newItem, string id);

        public abstract void AddPost(T item);

        //internal abstract string ProcessUploadedFile(IFormFile photo);
    }
}
