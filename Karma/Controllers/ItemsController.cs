using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Models;
using Karma.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Karma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        public ItemsController(IItemRepository itemService)
        {
            ItemService = itemService;
        }

        public IItemRepository ItemService { get; }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await ItemService.GetPosts());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemPost>> Get(int id)
        {
            try
            {
                var result = await ItemService.GetPost(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult<ItemPost>> Post(string userId, ItemPost newItem)
        {
            try
            {
                if (newItem == null)
                    return BadRequest();

                newItem.Picture = "noimage.jpg";

                var createdItem = await ItemService.AddPost(newItem, userId);

                if (createdItem == null)
                    return NotFound();

                return CreatedAtAction(nameof(Get), new { id = createdItem.Id }, createdItem);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPut("{itemId:int}")]
        public async Task<ActionResult<ItemPost>> Put(int itemId, ItemPost newItem)
        {
            try
            {
                if (newItem == null)
                    return BadRequest();

                var itemToUpdate = await ItemService.GetPost(itemId);

                if (itemToUpdate == null)
                {
                    return NotFound($"Item with Id = {itemId} not found");
                }

                newItem.Id = itemId;

                return await ItemService.UpdatePost(newItem);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data from the database");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemPost>> Delete(int id)
        {
            try
            {
                var result = await ItemService.GetPost(id);

                if (result == null)
                {
                    return NotFound();
                }

                return await ItemService.DeletePost(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
    }
}
