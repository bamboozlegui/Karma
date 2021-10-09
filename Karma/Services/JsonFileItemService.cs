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
	public JsonFileItemService(JsonPictureService pictureService,
				   IWebHostEnvironment webHostEnvironment)
	{
	    WebHostEnvironment = webHostEnvironment;
            PictureService = pictureService;
        }

	public IWebHostEnvironment WebHostEnvironment { get; }

	public JsonPictureService PictureService { get; }

	internal override string JsonFileName
	{
	    get { return Path.Combine(WebHostEnvironment.ContentRootPath, "data", (new ItemPost()).GetJsonName()); }
        }

	public override void AddPost(ItemPost post, IFormFile photo)
	{
	    if (photo != null)
	    {
		if (post.Picture != null) //If our Post already has a picture path string, we should delete it first to upload a new one
		{
		    string filePath = Path.Combine(WebHostEnvironment.WebRootPath, "images", post.Picture);
		    System.IO.File.Delete(filePath);
		}

		post.Picture = PictureService.ProcessUploadedFile(WebHostEnvironment, photo); //Check definition
	    }

	    post.Date = DateTime.Now;
	    post.ID = Guid.NewGuid().ToString();

	    IEnumerable<ItemPost> Submits = GetPosts().
	    Append<ItemPost>(post);

	    Submits = Submits.OrderByDescending(post => post.State).ThenByDescending(post => post.Title);

	    RefreshPosts(Submits);
	}

	public override void DeletePost(string id)
	{
            IEnumerable<ItemPost> posts = GetPosts();
            ItemPost post = posts.FirstOrDefault<ItemPost>(post => post.ID == id);

            PictureService.DeletePicture(WebHostEnvironment, post.Picture);
            RefreshPosts(posts.Where(post => post.ID != id));
        }

        public override void UpdatePost(ItemPost newPost, string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
