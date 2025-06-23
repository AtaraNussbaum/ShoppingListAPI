using Microsoft.AspNetCore.Mvc;
using ShoppingListAPI.DTOs;
using ShoppingListAPI.Services;

namespace ShoppingListAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        // GET: api/categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound($"קטגוריה עם מזהה {id} לא נמצאה");
            }

            return Ok(category);
        }

        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto createDto)
        {
            if (string.IsNullOrWhiteSpace(createDto.Name))
            {
                return BadRequest("שם הקטגוריה הוא שדה חובה");
            }

            try
            {
                var category = await _categoryService.CreateCategoryAsync(createDto);
                return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
            }
            catch (Exception ex)
            {
                return BadRequest($"שגיאה ביצירת הקטגוריה: {ex.Message}");
            }
        }

        // PUT: api/categories/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(int id, UpdateCategoryDto updateDto)
        {
            if (string.IsNullOrWhiteSpace(updateDto.Name))
            {
                return BadRequest("שם הקטגוריה הוא שדה חובה");
            }

            var category = await _categoryService.UpdateCategoryAsync(id, updateDto);

            if (category == null)
            {
                return NotFound($"קטגוריה עם מזהה {id} לא נמצאה");
            }

            return Ok(category);
        }

        // DELETE: api/categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleted = await _categoryService.DeleteCategoryAsync(id);

            if (!deleted)
            {
                return NotFound($"קטגוריה עם מזהה {id} לא נמצאה או שיש לה פריטים קשורים");
            }

            return NoContent();
        }
    }
}
