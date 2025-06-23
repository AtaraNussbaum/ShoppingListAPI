using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ShoppingListAPI.Models;

public partial class ShoppingList
{
    [Key]
    public int Id { get; set; }

    public string? CustomerName { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime? CreatedAt { get; set; }

    public int? IsCompleted { get; set; }

    [InverseProperty("ShoppingList")]
    public virtual ICollection<ShoppingItem> ShoppingItems { get; set; } = new List<ShoppingItem>();
}
