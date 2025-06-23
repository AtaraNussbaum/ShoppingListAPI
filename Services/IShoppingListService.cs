using ShoppingListAPI.DTOs;

namespace ShoppingListAPI.Services
{
    public interface IShoppingListService
    {
        Task<IEnumerable<ShoppingListDto>> GetAllShoppingListsAsync();
        Task<ShoppingListDto?> GetShoppingListByIdAsync(int id);
        Task<ShoppingListDto> CreateShoppingListAsync(CreateShoppingListDto createDto);
        Task<ShoppingListDto?> UpdateShoppingListAsync(int id, UpdateShoppingListDto updateDto);
        Task<bool> DeleteShoppingListAsync(int id);
        Task<ShoppingItemDto> AddItemToListAsync(int listId, CreateShoppingItemDto itemDto);
        Task<bool> RemoveItemFromListAsync(int listId, int itemId);
        Task<ShoppingItemDto?> UpdateItemInListAsync(int listId, int itemId, UpdateShoppingItemDto updateDto);
    }
}
