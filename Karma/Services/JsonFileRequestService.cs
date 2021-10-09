using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Karma.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Karma.Services
{
    public class JsonFileRequestService : JsonFilePostService<RequestPost>
    {
	public JsonFileRequestService(IWebHostEnvironment webHostEnvironment)
	{
	    WebHostEnvironment = webHostEnvironment;
        }

	public IWebHostEnvironment WebHostEnvironment { get; }

	internal override string JsonFileName
	{
	    get { return Path.Combine(WebHostEnvironment.ContentRootPath, "data", (new RequestPost()).GetJsonName()); }
        }

	public override void AddPost(RequestPost post, IFormFile photo)
	{
	    post.Date = DateTime.Now;
	    post.ID = Guid.NewGuid().ToString();

	    IEnumerable<RequestPost> Submits = GetPosts().
	    Append<RequestPost>(post);

	    Submits = Submits.OrderByDescending(post => post.State).ThenByDescending(post => post.Title);

	    RefreshPosts(Submits);
	}

	public override void DeletePost(string id)
	{
	    throw new System.NotImplementedException();
        }

        public override void UpdatePost(RequestPost newPost, string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
