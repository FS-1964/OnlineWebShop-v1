using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.DAL.Entities
{
    public class ShoppingCartItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ShoppingCartItemId { get; set; }
        public Cake Cake { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }

    }
}
