namespace ShoppingListAPI.DTOs
{
    public class ShoppingListDto
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsCompleted { get; set; }
        public List<ShoppingItemDto> Items { get; set; } = new();
    }

    public class CreateShoppingListDto
    {
        public string? CustomerName { get; set; }
        public List<CreateShoppingItemDto> Items { get; set; } = new();
    }

    public class UpdateShoppingListDto
    {
        public string? CustomerName { get; set; }
        public bool IsCompleted { get; set; }
    }
}
