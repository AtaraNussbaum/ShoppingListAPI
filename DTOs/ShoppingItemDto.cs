namespace ShoppingListAPI.DTOs
{
    public class ShoppingItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int ShoppingListId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class CreateShoppingItemDto
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public int CategoryId { get; set; }
    }

    public class UpdateShoppingItemDto
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
    }
}
