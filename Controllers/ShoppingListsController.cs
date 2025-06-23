using Microsoft.AspNetCore.Mvc;
using ShoppingListAPI.DTOs;
using ShoppingListAPI.Services;

namespace ShoppingListAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingListsController : ControllerBase
    {
        private readonly IShoppingListService _shoppingListService;
        private readonly ICategoryService _categoryService;

        public ShoppingListsController(IShoppingListService shoppingListService, ICategoryService categoryService)
        {
            _shoppingListService = shoppingListService;
            _categoryService = categoryService;
        }

        // GET: api/shoppinglists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingListDto>>> GetShoppingLists()
        {
            var lists = await _shoppingListService.GetAllShoppingListsAsync();
            return Ok(lists);
        }

        // GET: api/shoppinglists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingListDto>> GetShoppingList(int id)
        {
            var list = await _shoppingListService.GetShoppingListByIdAsync(id);

            if (list == null)
            {
                return NotFound($"רשימת קניות עם מזהה {id} לא נמצאה");
            }

            return Ok(list);
        }

        // POST: api/shoppinglists
        [HttpPost]
        public async Task<ActionResult<ShoppingListDto>> CreateShoppingList(CreateShoppingListDto createDto)
        {
            // בדיקת תקינות קטגוריות של הפריטים
            foreach (var item in createDto.Items)
            {
                if (!await _categoryService.CategoryExistsAsync(item.CategoryId))
                {
                    return BadRequest($"קטגוריה עם מזהה {item.CategoryId} לא קיימת");
                }
            }

            var list = await _shoppingListService.CreateShoppingListAsync(createDto);
            return CreatedAtAction(nameof(GetShoppingList), new { id = list.Id }, list);
        }

        // PUT: api/shoppinglists/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ShoppingListDto>> UpdateShoppingList(int id, UpdateShoppingListDto updateDto)
        {
            var list = await _shoppingListService.UpdateShoppingListAsync(id, updateDto);

            if (list == null)
            {
                return NotFound($"רשימת קניות עם מזהה {id} לא נמצאה");
            }

            return Ok(list);
        }

        // DELETE: api/shoppinglists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingList(int id)
        {
            var deleted = await _shoppingListService.DeleteShoppingListAsync(id);

            if (!deleted)
            {
                return NotFound($"רשימת קניות עם מזהה {id} לא נמצאה");
            }

            return NoContent();
        }

        // POST: api/shoppinglists/5/items
        [HttpPost("{listId}/items")]
        public async Task<ActionResult<ShoppingItemDto>> AddItemToList(int listId, CreateShoppingItemDto itemDto)
        {
            // בדיקה שהרשימה קיימת
            var list = await _shoppingListService.GetShoppingListByIdAsync(listId);
            if (list == null)
            {
                return NotFound($"רשימת קניות עם מזהה {listId} לא נמצאה");
            }

            // בדיקה שהקטגוריה קיימת
            if (!await _categoryService.CategoryExistsAsync(itemDto.CategoryId))
            {
                return BadRequest($"קטגוריה עם מזהה {itemDto.CategoryId} לא קיימת");
            }

            if (string.IsNullOrWhiteSpace(itemDto.Name))
            {
                return BadRequest("שם הפריט הוא שדה חובה");
            }

            var item = await _shoppingListService.AddItemToListAsync(listId, itemDto);
            return CreatedAtAction(nameof(GetShoppingList), new { id = listId }, item);
        }

        // PUT: api/shoppinglists/5/items/3
        [HttpPut("{listId}/items/{itemId}")]
        public async Task<ActionResult<ShoppingItemDto>> UpdateItemInList(int listId, int itemId, UpdateShoppingItemDto updateDto)
        {
            // בדיקה שהקטגוריה קיימת
            if (!await _categoryService.CategoryExistsAsync(updateDto.CategoryId))
            {
                return BadRequest($"קטגוריה עם מזהה {updateDto.CategoryId} לא קיימת");
            }

            if (string.IsNullOrWhiteSpace(updateDto.Name))
            {
                return BadRequest("שם הפריט הוא שדה חובה");
            }

            var item = await _shoppingListService.UpdateItemInListAsync(listId, itemId, updateDto);

            if (item == null)
            {
                return NotFound($"פריט עם מזהה {itemId} לא נמצא ברשימה {listId}");
            }

            return Ok(item);
        }

        // DELETE: api/shoppinglists/5/items/3
        [HttpDelete("{listId}/items/{itemId}")]
        public async Task<IActionResult> RemoveItemFromList(int listId, int itemId)
        {
            var removed = await _shoppingListService.RemoveItemFromListAsync(listId, itemId);

            if (!removed)
            {
                return NotFound($"פריט עם מזהה {itemId} לא נמצא ברשימה {listId}");
            }

            return NoContent();
        }
    }
}
