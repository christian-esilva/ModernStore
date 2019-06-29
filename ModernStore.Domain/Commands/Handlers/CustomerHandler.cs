using Flunt.Notifications;
using ModernStore.Domain.Commands.Inputs;
using ModernStore.Domain.Commands.Results;
using ModernStore.Domain.Entities;
using ModernStore.Domain.Repositories;
using ModernStore.Domain.Services;
using ModernStore.Domain.ValueObjects;
using ModernStore.Shared.Commands;

namespace ModernStore.Domain.Commands.Handlers
{
    public class CustomerHandler : Notifiable, ICommandHandler<RegisterCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmailService _emailService;

        public CustomerHandler(ICustomerRepository customerRepository, IEmailService emailService)
        {
            _customerRepository = customerRepository;
            _emailService = emailService;
        }

        public ICommandResult Handle(RegisterCustomerCommand command)
        {
            //verificar cpf existente
            if (_customerRepository.DocumentExists(command.Document))
            {
                AddNotification("Customer.Document", "Esse CPF já está cadastrado");
                return null;
            }

            //gerar novo cliente
            var name = new Name(command.FirstName, command.LastName);
            var email = new Email(command.Email);
            var document = new Document(command.Document);
            var user = new User(command.Username, command.Password, command.ConfirmPassword);

            var customer = new Customer(name, email, document, user);

            AddNotifications(name, email, document, customer);

            if (Invalid)
                return null;

            //inserir no banco de dados
            _customerRepository.Save(customer);

            //enviar e-mail de boas vindas
            _emailService.Send(
                customer.Name.ToString(),
                customer.Email.Address,
                "Bem-vindo",
                string.Format("Olá, {0}! Seja bem vindo ao site", customer.Name));

            //retornar
            return new RegisterCustomerCommandResult(customer.Id, customer.Name.ToString());
        }
    }
}
