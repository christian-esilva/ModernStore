using Flunt.Validations;
using ModernStore.Shared.Entities;

namespace ModernStore.Domain.Entities
{
    public class OrderItem : Entity
    {
        protected OrderItem() { }

        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            Price = Product.Price;

            AddNotifications(new Contract()
                .IsGreaterThan(1, Quantity, "OrderItem.Quantity", "A quantidade deve ser maior que zero")
                .IsGreaterThan(Product.QuantityOnHand, Quantity + 1, "OrderItem.Product.QuantityOnHand", $"Não temos tantos {product.Title}(s) em estoque.")
            );

            Product.DecreaseQuantity(quantity);
        }

        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        public decimal Total() => Price * Quantity;
    }
}
