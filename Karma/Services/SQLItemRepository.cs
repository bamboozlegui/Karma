using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Karma.Services
{
    public class SQLItemRepository : IItemRepository
    {
        private readonly AppDbContext context;

        private readonly JsonPictureService PictureService;
        public IWebHostEnvironment WebHostEnvironment { get; private set; }

        public SQLItemRepository(AppDbContext context, JsonPictureService pictureService, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            PictureService = pictureService;
            WebHostEnvironment = webHostEnvironment;
        }

        public ItemPost AddPost(ItemPost post, IFormFile photo)
        {
            if (post.Picture != null)
            {
                PictureService.DeletePicture(WebHostEnvironment, post.Picture);
            }
            post.Picture = PictureService.ProcessUploadedFile(WebHostEnvironment, photo); //Check definition
            post.Date = DateTime.Now;
            post.ID = Guid.NewGuid().ToString();
            post.State = Post.StateEnum.Recent;
            context.Items.Add(post);
            context.SaveChanges();
            return post;
        }

        public ItemPost DeletePost(string id)
        {
            ItemPost item = context.Items.Find(id);
            if(item != null)
            {
                PictureService.DeletePicture(WebHostEnvironment, item.Picture);
                context.Items.Remove(item);
                context.SaveChanges();
            }
            return item;
        }

        public ItemPost GetPost(string id)
        {
            return context.Items.Find(id);
        }

        public IEnumerable<ItemPost> GetPosts()
        {
            return context.Items;
        }

        public IEnumerable<ItemPost> SearchPosts(string searchTerm)
        {
            if (searchTerm == null)
                return context.Items;

            return context.Items.Where(item => item.Title.Contains(searchTerm));
        }

        public ItemPost UpdatePost(ItemPost newPost, IFormFile newPhoto)
        {
            var item = context.Items.Attach(newPost);
            item.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return newPost;
        }
    }
}
