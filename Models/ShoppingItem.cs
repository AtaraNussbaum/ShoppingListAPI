using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ShoppingListAPI.Models;

public partial class ShoppingItem
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Quantity { get; set; }

    public int CategoryId { get; set; }

    public int ShoppingListId { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("ShoppingItems")]
    public virtual Category Category { get; set; } = null!;

    [ForeignKey("ShoppingListId")]
    [InverseProperty("ShoppingItems")]
    public virtual ShoppingList ShoppingList { get; set; } = null!;
}
