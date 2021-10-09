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

        public T GetPost(string id)
        {
            IEnumerable<T> posts = GetPosts();
            T post = posts.FirstOrDefault<T>(post => post.ID == id);

            return post;
        }

        public void DeletePost(JsonFilePostService<ItemPost> submitService, string id, bool deletePicture = true)
        {
            IEnumerable<ItemPost> posts = submitService.GetPosts();
            ItemPost post = posts.FirstOrDefault<ItemPost>(post => post.ID == id);

            if (deletePicture)
            {
                if (post.Picture != "noimage.jpg")
                {
                    string filePath = Path.Combine(WebHostEnvironment.WebRootPath, "images", post.Picture);
                    System.IO.File.Delete(filePath);
                }

            }

            submitService.RefreshPosts(posts.Where(post => post.ID != id));
        }

        public void UpdatePost(JsonFilePostService<ItemPost> submitService, ItemPost newItem, string id, IFormFile newPhoto = null)
        {
            IEnumerable<ItemPost> items = submitService.GetPosts();
            ItemPost item = submitService.GetPost(id);

            bool deletePicFlag = false;

            if(newItem.Title != null) item.Title = newItem.Title;
            if(newItem.PhoneNumber != null) item.PhoneNumber = newItem.PhoneNumber;
            if(newItem.PosterName  != null) item.PosterName = newItem.PosterName;
            if(newItem.Description != null) item.Description = newItem.Description;
            if(newPhoto            != null)
            {
                item.Picture = ProcessUploadedFile(newPhoto);
                deletePicFlag = true;
            }

            DeletePost(submitService, id, deletePicFlag);
            AddPost(submitService, item, newPhoto, false);
        }

        public void AddPost(JsonFilePostService<ItemPost> submitService, ItemPost item, IFormFile photo, bool applyDefaultPic = true)
        {
            if (photo != null)
            {
                if (item.Picture != null) //If our Item already has a picture path string, we should delete it first to upload a new one
                {
                    string filePath = Path.Combine(WebHostEnvironment.WebRootPath, "images", item.Picture);
                    System.IO.File.Delete(filePath);
                }

                item.Picture = ProcessUploadedFile(photo); //Check definition
            }
	        else
	        {
                if(applyDefaultPic)
		            item.Picture = "noimage.jpg";
	        }
                item.Date = DateTime.Now;
                item.ID   = Guid.NewGuid().ToString();

                IEnumerable<ItemPost> Submits = submitService.GetPosts().
                Append<ItemPost>(item);

	        Submits = Submits.OrderByDescending(item => item.State).ThenByDescending(item => item.Title);

                submitService.RefreshPosts(Submits);

        }
        internal string ProcessUploadedFile(IFormFile photo)
        {
            string uniqueFileName = null;

            if (photo != null)
            {
                string uploadsFolder =
                    Path.Combine(WebHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
