using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Models;
using Karma.Services;
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
        public async Task<ActionResult<ItemPost>> Get(string id)
        {
            try
            {
                var result = await ItemService.GetPost(id);

                if(result == null)
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
    }
}
