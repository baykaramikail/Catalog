﻿using Catalog.Repositories;
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
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repository.GetItems().Select(item => item.AsDto());
            return items;
        }
            

        [HttpGet("{id}")] 
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item.AsDto());
        }

        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
                
            };

            repository.CreateItem(item);

            return CreatedAtAction(nameof(CreateItem), new { Id = item.Id }, item.AsDto());
        }
        
    }
}
