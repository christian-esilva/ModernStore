using Flunt.Notifications;
using ModernStore.Domain.Commands.Inputs;
using ModernStore.Domain.Commands.Results;
using ModernStore.Domain.Entities;
using ModernStore.Domain.Repositories;
using ModernStore.Shared.Commands;

namespace ModernStore.Domain.Commands.Handlers
{
    public class OrderHandler : Notifiable, ICommandHandler<RegisterOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderHandler(ICustomerRepository customerRepository, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public ICommandResult Handle(RegisterOrderCommand command)
        {
            //recupera o cliente
            var customer = _customerRepository.Get(command.Customer);
            //gera um novo pedido
            var order = new Order(customer, command.DeliveryFee, command.Discount);

            //adiciona os itens
            foreach (var item in command.Items)
            {
                var product = _productRepository.Get(item.Product);
                order.AddItem(new OrderItem(product, item.Quantity));
            }

            //notificações do pedido no handler
            AddNotifications(order);

            //grava no banco
            if (Valid)
                _orderRepository.Save(order);

            //retornar numero do pedido
            return new RegisterOrderCommandResult(order.Number);
        }
    }
}
