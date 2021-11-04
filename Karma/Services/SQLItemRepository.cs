using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Karma.Areas.Identity.Data;
using Karma.Data;
using Karma.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Karma.Services
{
    public class SQLItemRepository : IItemRepository
    {
        private readonly KarmaDbContext Context;

        private readonly PictureService PictureService;

        public UserManager<KarmaUser> UserManager { get; }
        public IWebHostEnvironment WebHostEnvironment { get; private set; }

        public SQLItemRepository(KarmaDbContext context, PictureService pictureService, UserManager<KarmaUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            Context = context;
            PictureService = pictureService;
            UserManager = userManager;
            WebHostEnvironment = webHostEnvironment;
        }

        public ItemPost AddPost(ClaimsPrincipal user, ItemPost post, IFormFile photo)
        {
            if (post.Picture != null)
            {
                PictureService.DeletePicture(WebHostEnvironment, post.Picture);
            }
            post.Picture = PictureService.ProcessUploadedFile(WebHostEnvironment, photo); //Check definition
            post.Date = DateTime.Now;
            post.ID = Guid.NewGuid().ToString();
            post.State = Post.StateEnum.Recent;
            post.KarmaUserId = UserManager.GetUserId(user);
            Context.Items.Add(post);
            Context.SaveChanges();
            return post;
        }

        public ItemPost DeletePost(string id)
        {
            ItemPost item = Context.Items.Find(id);
            if(item != null)
            {
                PictureService.DeletePicture(WebHostEnvironment, item.Picture);
                Context.Items.Remove(item);
                Context.SaveChanges();
            }
            return item;
        }

        public ItemPost GetPost(string id)
        {
            return Context.Items.Find(id);
        }

        public IEnumerable<ItemPost> GetPosts()
        {
            return Context.Items;
        }

        public IEnumerable<ItemPost> SearchPosts(string searchTerm)
        {
            if (searchTerm == null)
                return Context.Items;

            return Context.Items.Where(item => item.Title.Contains(searchTerm));
        }

        public ItemPost UpdatePost(ItemPost newPost, IFormFile newPhoto)
        {
            ItemPost post = Context.Items.AsNoTracking().FirstOrDefault(post => post.ID == newPost.ID);

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
            var item = Context.Items.Attach(newPost);
            item.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
            return newPost;
        }
    }
}
