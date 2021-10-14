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
	//Calls base constructor and initializes picture service
	public JsonFileItemService(
	    JsonPictureService pictureService,
	    IWebHostEnvironment webHostEnvironment) : base(webHostEnvironment)
	{
	    PictureService = pictureService;
	}
	
	//A service that processes images
	public JsonPictureService PictureService { get; }

	//Takes a single post and a picture that will be attached to it.
        public override void AddPost(ItemPost post, IFormFile photo)
        {
	    //In case the post already has an associated picture, we must delete the old picture to add a new one
            if (post.Picture != null)
            {
                PictureService.DeletePicture(WebHostEnvironment, post.Picture);
            }

            post.Picture = PictureService.ProcessUploadedFile(WebHostEnvironment, photo); //Check definition

            post.Date = DateTime.Now;

            post.ID = Guid.NewGuid().ToString();

            post.State = Post.StateEnum.Recent;

            _posts = _posts.Append<ItemPost>(post);

            _posts = _posts.OrderBy(post => post);

            RefreshJsonFile();
        }

	//Takes the post specified by an id and excludes is from _posts
	//The garbage collector gets rid of the object
        public override void DeletePost(string id)
        {
            ItemPost post = _posts.FirstOrDefault<ItemPost>(post => post.ID == id);

	    //In case the post is in the _posts
	    if (post != null)
	    {
		PictureService.DeletePicture(WebHostEnvironment, post.Picture);
		_posts = _posts.Where(post => post.ID != id);
            }

            RefreshJsonFile();
	}

	//Takes a new post and a picture that should be attached to it (or replace if the post already has picture)
        public override ItemPost UpdatePost(ItemPost newPost, IFormFile newPhoto = null)
        {
            ItemPost post = _posts.FirstOrDefault(post => post.ID == newPost.ID);

	    //If we find a post with the same id
	    //We basically apply newPost properties to the existing post
            if (post != null)
            {
                post.Title = newPost.Title;
                post.PosterName = newPost.PosterName;
                post.PhoneNumber = newPost.PhoneNumber;
                post.Email = newPost.Email;
                post.Description = newPost.Description;
                post.City = newPost.City;
            }

	    //In case we have a new picture that should be attached to the post
	    if (newPhoto != null)
	    {
		//In case the post already has a picture and that image is not our default noimage.jpg
		//We must delete the already existing picture
		if (post.Picture != null && post.Picture != "noimage.jpg")
		{
                    string filePath = Path.Combine(WebHostEnvironment.WebRootPath, "images", post.Picture);
                    System.IO.File.Delete(filePath);
                }

                post.Picture = PictureService.ProcessUploadedFile(WebHostEnvironment, newPhoto);
            }

            RefreshJsonFile();

            return post;
        }
    }
}
