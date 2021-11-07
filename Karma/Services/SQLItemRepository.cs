using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
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
    public class SqlItemRepository : IItemRepository
    {


        public KarmaDbContext Context { get; }
        public PictureService PictureService { get; }
        public UserManager<KarmaUser> UserManager { get; }
        public IWebHostEnvironment WebHostEnvironment { get; private set; }

        public SqlItemRepository(KarmaDbContext context, PictureService pictureService, UserManager<KarmaUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            Context = context;
            PictureService = pictureService;
            UserManager = userManager;
            WebHostEnvironment = webHostEnvironment;
        }

        public async Task<ItemPost> AddPost(ItemPost post)
        {
            post.Date = DateTime.Now;
            post.ID = Guid.NewGuid().ToString();
            post.State = Post.StateEnum.Recent;
            await Context.Items.AddAsync(post);
            await Context.SaveChangesAsync();
            return post;
        }

        public async Task<ItemPost> DeletePost(string id)
        {
            ItemPost item = await Context.Items.FindAsync(id);
            if(item != null)
            {
                PictureService.DeletePicture(WebHostEnvironment, item.Picture);
                Context.Items.Remove(item);
                await Context.SaveChangesAsync();
            }
            return item;
        }

        public async Task<ItemPost> GetPost(string id)
        {
            return await Context.Items.FindAsync(id);
        }

        public async Task<List<ItemPost>> GetPosts()
        {
            return await Context.Items.ToListAsync();
        }

        public async Task<List<ItemPost>> SearchPosts(string searchTerm)
        {
            if (searchTerm == null)
                return await Context.Items.ToListAsync();

            return  await Context.Items.Where(item => item.Title.Contains(searchTerm)).ToListAsync();
        }

        public async Task<ItemPost> UpdatePost(ItemPost newPost)
        {
            ItemPost post = await Context.Items.AsNoTracking().FirstOrDefaultAsync(post => post.ID == newPost.ID);
            if(post == null)
            {
                return null;
            }

            newPost.Date = post.Date;
            if(newPost.Picture == null) newPost.Picture = post.Picture;
            newPost.KarmaUserId = post.KarmaUserId;
            var item = Context.Items.Attach(newPost);
            item.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await Context.SaveChangesAsync();
            return newPost;
        }
    }
}
