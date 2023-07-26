using Catalog.DTOs;
using Catalog.Entities;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Catalog
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = DateTime.Now
            };
                
        }

    }
}

