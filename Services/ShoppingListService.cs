using Microsoft.EntityFrameworkCore;
using ShoppingListAPI.DTOs;
using ShoppingListAPI.Models;

namespace ShoppingListAPI.Services
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly ShoppingDbContext _context;

        public ShoppingListService(ShoppingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShoppingListDto>> GetAllShoppingListsAsync()
        {
            return await _context.ShoppingLists
                .Include(sl => sl.ShoppingItems)
                .ThenInclude(si => si.Category)
                .Select(sl => new ShoppingListDto
                {
                    Id = sl.Id,
                    CustomerName = sl.CustomerName,
                    CreatedAt = sl.CreatedAt,
                    IsCompleted = sl.IsCompleted == 1,
                    Items = sl.ShoppingItems.Select(si => new ShoppingItemDto
                    {
                        Id = si.Id,
                        Name = si.Name,
                        Quantity = si.Quantity ?? 1,
                        CategoryId = si.CategoryId,
                        CategoryName = si.Category.Name,
                        ShoppingListId = si.ShoppingListId,
                        CreatedAt = si.CreatedAt
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<ShoppingListDto?> GetShoppingListByIdAsync(int id)
        {
            var shoppingList = await _context.ShoppingLists
                .Include(sl => sl.ShoppingItems)
                .ThenInclude(si => si.Category)
                .FirstOrDefaultAsync(sl => sl.Id == id);

            if (shoppingList == null) return null;

            return new ShoppingListDto
            {
                Id = shoppingList.Id,
                CustomerName = shoppingList.CustomerName,
                CreatedAt = shoppingList.CreatedAt,
                IsCompleted = shoppingList.IsCompleted == 1,
                Items = shoppingList.ShoppingItems.Select(si => new ShoppingItemDto
                {
                    Id = si.Id,
                    Name = si.Name,
                    Quantity = si.Quantity ?? 1,
                    CategoryId = si.CategoryId,
                    CategoryName = si.Category.Name,
                    ShoppingListId = si.ShoppingListId,
                    CreatedAt = si.CreatedAt
                }).ToList()
            };
        }

        public async Task<ShoppingListDto> CreateShoppingListAsync(CreateShoppingListDto createDto)
        {
            var shoppingList = new ShoppingList
            {
                CustomerName = createDto.CustomerName,
                CreatedAt = DateTime.Now,
                IsCompleted = 0
            };

            _context.ShoppingLists.Add(shoppingList);
            await _context.SaveChangesAsync();

            // הוספת פריטים אם יש
            foreach (var itemDto in createDto.Items)
            {
                var item = new ShoppingItem
                {
                    Name = itemDto.Name,
                    Quantity = itemDto.Quantity,
                    CategoryId = itemDto.CategoryId,
                    ShoppingListId = shoppingList.Id,
                    CreatedAt = DateTime.Now
                };
                _context.ShoppingItems.Add(item);
            }

            await _context.SaveChangesAsync();

            return await GetShoppingListByIdAsync(shoppingList.Id) ?? new ShoppingListDto();
        }

        public async Task<ShoppingListDto?> UpdateShoppingListAsync(int id, UpdateShoppingListDto updateDto)
        {
            var shoppingList = await _context.ShoppingLists.FindAsync(id);
            if (shoppingList == null) return null;

            shoppingList.CustomerName = updateDto.CustomerName;
            shoppingList.IsCompleted = updateDto.IsCompleted ? 1 : 0;

            await _context.SaveChangesAsync();
            return await GetShoppingListByIdAsync(id);
        }

        public async Task<bool> DeleteShoppingListAsync(int id)
        {
            var shoppingList = await _context.ShoppingLists.FindAsync(id);
            if (shoppingList == null) return false;

            _context.ShoppingLists.Remove(shoppingList);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ShoppingItemDto> AddItemToListAsync(int listId, CreateShoppingItemDto itemDto)
        {
            var item = new ShoppingItem
            {
                Name = itemDto.Name,
                Quantity = itemDto.Quantity,
                CategoryId = itemDto.CategoryId,
                ShoppingListId = listId,
                CreatedAt = DateTime.Now
            };

            _context.ShoppingItems.Add(item);
            await _context.SaveChangesAsync();

            var category = await _context.Categories.FindAsync(itemDto.CategoryId);

            return new ShoppingItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Quantity = item.Quantity ?? 1,
                CategoryId = item.CategoryId,
                CategoryName = category?.Name ?? "",
                ShoppingListId = item.ShoppingListId,
                CreatedAt = item.CreatedAt
            };
        }

        public async Task<bool> RemoveItemFromListAsync(int listId, int itemId)
        {
            var item = await _context.ShoppingItems
                .FirstOrDefaultAsync(si => si.Id == itemId && si.ShoppingListId == listId);

            if (item == null) return false;

            _context.ShoppingItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ShoppingItemDto?> UpdateItemInListAsync(int listId, int itemId, UpdateShoppingItemDto updateDto)
        {
            var item = await _context.ShoppingItems
                .FirstOrDefaultAsync(si => si.Id == itemId && si.ShoppingListId == listId);

            if (item == null) return null;

            item.Name = updateDto.Name;
            item.Quantity = updateDto.Quantity;
            item.CategoryId = updateDto.CategoryId;

            await _context.SaveChangesAsync();

            var category = await _context.Categories.FindAsync(updateDto.CategoryId);

            return new ShoppingItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Quantity = item.Quantity ?? 1,
                CategoryId = item.CategoryId,
                CategoryName = category?.Name ?? "",
                ShoppingListId = item.ShoppingListId,
                CreatedAt = item.CreatedAt
            };
        }
    }
}
