using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Models;
using Microsoft.AspNetCore.Hosting;
using Karma.Data;

namespace Karma.Services
{
    public class SQLRequestRepository : IRequestRepository
    {
        private readonly KarmaDbContext Context;
        public IWebHostEnvironment WebHostEnvironment { get; private set; }

        public SQLRequestRepository(KarmaDbContext context, JsonPictureService pictureService, IWebHostEnvironment webHostEnvironment)
        {
            Context = context;
            WebHostEnvironment = webHostEnvironment;
        }
        public RequestPost AddPost(RequestPost post)
        {
            post.Date = DateTime.Now;
            post.ID = Guid.NewGuid().ToString();
            post.State = Post.StateEnum.Recent;
            Context.Requests.Add(post);
            Context.SaveChanges();
            return post;
        }

        public RequestPost DeletePost(string id)
        {
                   RequestPost request = Context.Requests.Find(id);
                    if (request != null)
                    {
                        Context.Requests.Remove(request);
                  Context.SaveChanges();
                }
               return request;
        }

        public RequestPost GetPost(string id)
        {
            return Context.Requests.Find(id);
        }

        public IEnumerable<RequestPost> GetPosts()
        {
            return Context.Requests;
        }

        public IEnumerable<RequestPost> SearchPosts(string searchTerm)
        {
            if (searchTerm == null)
                return Context.Requests;

            return Context.Requests.Where(request => request.Title.Contains(searchTerm));
        }

        public RequestPost UpdatePost(RequestPost newPost)
        {
            throw new NotImplementedException();
        }
    }
}
