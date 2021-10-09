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
	    if (post.Picture != null)
	    {
                PictureService.DeletePicture(WebHostEnvironment, post.Picture);
	    }

	    post.Picture = PictureService.ProcessUploadedFile(WebHostEnvironment, photo); //Check definition

            post.Date = DateTime.Now;

	    post.ID = Guid.NewGuid().ToString();

	    IEnumerable<ItemPost> posts = GetPosts().
	    Append<ItemPost>(post);

	    posts = posts.OrderByDescending(post => post.State).ThenByDescending(post => post.Title);

	    RefreshPosts(posts);
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
