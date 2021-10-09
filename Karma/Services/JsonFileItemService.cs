using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Karma.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Karma.Services
{
    public class JsonFileItemService : JsonFilePostService<ItemPost>
    {
	public JsonPictureService PictureService { get; }
	
	public JsonFileItemService(
	    JsonPictureService pictureService,
	    IWebHostEnvironment webHostEnvironment) : base(webHostEnvironment)
	{
	    PictureService = pictureService;
	}

        internal override string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.ContentRootPath, "data", (new ItemPost()).GetJsonName()); }
        }

        public override void AddPost(ItemPost post, IFormFile photo)
        {
            if (post.Picture != null)
            {
                PictureService.DeletePicture(WebHostEnvironment, post.Picture);
            }

            post.Picture = PictureService.ProcessUploadedFile(WebHostEnvironment, photo); //Check definition

            post.Date = DateTime.Now;

            post.ID = Guid.NewGuid().ToString();

            _posts = _posts.Append<ItemPost>(post);

            _posts = _posts.OrderByDescending(post => post.State).ThenByDescending(post => post.Title);

            RefreshJsonFile();
        }

        public override void DeletePost(string id)
        {
            ItemPost post = _posts.FirstOrDefault<ItemPost>(post => post.ID == id);

	    PictureService.DeletePicture(WebHostEnvironment, post.Picture);
	    _posts = _posts.Where(post => post.ID != id);

	    RefreshJsonFile();
	}

        public override ItemPost UpdatePost(ItemPost newPost)
        {
            ItemPost post = _posts.FirstOrDefault(post => post.ID == newPost.ID);

            if (post != null)
            {
                post.Title = newPost.Title;
                post.PosterName = newPost.PosterName;
                post.PhoneNumber = newPost.PhoneNumber;
                post.Description = newPost.Description;
                post.City = newPost.City;
            }

            RefreshJsonFile();

            return post;
        }
    }
}
