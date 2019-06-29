using Flunt.Validations;
using ModernStore.Domain.Enums;
using ModernStore.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModernStore.Domain.Entities
{
    public class Order : Entity
    {
        private readonly IList<OrderItem> _items;

        protected Order() { }

        public Order(Customer customer, decimal deliveryFee, decimal discount)
        {
            Customer = customer;
            CreateDate = DateTime.Now;
            Number = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
            Status = EOrderStatus.Created;
            DeliveryFee = deliveryFee;
            Discount = discount;

            _items = new List<OrderItem>();

            AddNotifications(new Contract()
                .IsGreaterThan(0, DeliveryFee, "Order.DeliveryFee", "A taxa não pode ser menor que 0")
                .IsGreaterThan(-1, Discount, "Order.Discount", "O desconto não pode ser menor que 0")
            );
        }

        public Customer Customer { get; private set; }
        public DateTime CreateDate { get; private set; }
        public string Number { get; private set; }
        public EOrderStatus Status { get; private set; }
        public ICollection<OrderItem> Items => _items.ToArray();
        public decimal DeliveryFee { get; private set; }
        public decimal Discount { get; private set; }

        public decimal SubTotal() => Items.Sum(x => x.Total());
        public decimal Total() => SubTotal() + DeliveryFee - Discount;

        public void AddItem(OrderItem item)
        {
            AddNotifications(item);

            if (item.Valid)
                _items.Add(item);
        }
    }
}
