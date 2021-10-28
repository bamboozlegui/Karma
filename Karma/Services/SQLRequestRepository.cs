using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Models;
using Microsoft.AspNetCore.Hosting;

namespace Karma.Services
{
    public class SQLRequestRepository : IRequestRepository
    {
        private readonly AppDbContext context;
        public IWebHostEnvironment WebHostEnvironment { get; private set; }

        public SQLRequestRepository(AppDbContext context, JsonPictureService pictureService, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            WebHostEnvironment = webHostEnvironment;
        }
        public RequestPost AddPost(RequestPost post)
        {
            post.Date = DateTime.Now;
            post.ID = Guid.NewGuid().ToString();
            post.State = Post.StateEnum.Recent;
            context.Requests.Add(post);
            context.SaveChanges();
            return post;
        }

        public RequestPost DeletePost(string id)
        {
                   RequestPost request = context.Requests.Find(id);
                    if (request != null)
                    {
                        context.Requests.Remove(request);
                  context.SaveChanges();
                }
               return request;
        }

        public RequestPost GetPost(string id)
        {
            return context.Requests.Find(id);
        }

        public IEnumerable<RequestPost> GetPosts()
        {
            return context.Requests;
        }

        public IEnumerable<RequestPost> SearchPosts(string searchTerm)
        {
            if (searchTerm == null)
                return context.Requests;

            return context.Requests.Where(request => request.Title.Contains(searchTerm));
        }

        public RequestPost UpdatePost(RequestPost newPost)
        {
            throw new NotImplementedException();
        }
    }
}
