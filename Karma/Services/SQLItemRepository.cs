using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Karma.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
            ItemPost post = context.Items.AsNoTracking().FirstOrDefault(post => post.ID == newPost.ID);

            if(newPhoto != null)
            {
                if (post.Picture != null && post.Picture != "noimage.jpg")
                {
                    string filePath = Path.Combine(WebHostEnvironment.WebRootPath, "images", post.Picture);
                    System.IO.File.Delete(filePath);
                }

                post.Picture = PictureService.ProcessUploadedFile(WebHostEnvironment, newPhoto);
            }
            newPost.Date = post.Date;
            newPost.Picture = post.Picture;
            var item = context.Items.Attach(newPost);
            item.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return newPost;
        }
    }
}
