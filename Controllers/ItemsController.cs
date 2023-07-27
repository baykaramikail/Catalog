using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;
using Catalog.Entities;
using Catalog.DTOs;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : Controller
    {
        private readonly IItemsRepository repository;
        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await repository.GetItemsAsync())
                            .Select(item => item.AsDto());
            return items;
        }
            

        [HttpGet("{id}")] 
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await repository.GetItemAsync(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
                
            };

            await repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(CreateItemAsync), new { Id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
        {
          Item existingItem = await repository.GetItemAsync(id);
            if(existingItem is null)
            {
                return NotFound();
            }

            Item itemToBeUptaded = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await repository.UpdateItemAsync(itemToBeUptaded);

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            Item existingItem = await repository.GetItemAsync(id);
            if (existingItem is null)
            {
                return NotFound();
            }
            await repository.DeleteItemAsync(id);
             
            return NoContent();
        }
        
    }
}
