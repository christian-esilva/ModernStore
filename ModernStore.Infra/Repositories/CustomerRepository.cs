using Microsoft.EntityFrameworkCore;
using ModernStore.Domain.Commands.Results;
using ModernStore.Domain.Entities;
using ModernStore.Domain.Repositories;
using ModernStore.Infra.DataContext;
using System;
using System.Linq;

namespace ModernStore.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ModernStoreDataContext _context;

        public CustomerRepository(ModernStoreDataContext context)
        {
            _context = context;
        }

        public bool DocumentExists(string document)
        {
            return _context.Customers.Any(x => x.Document.Number == document);
        }

        public Customer Get(Guid id)
        {
            return _context.Customers.Include(x => x.User).FirstOrDefault(x => x.Id == id);
        }

        public GetCustomerCommandResult Get(string username)
        {
            return _context.Customers.Include(x => x.User).AsNoTracking().Select(x => new GetCustomerCommandResult
            {
                Name = x.Name.FirstName.ToString(),
                Document = x.Document.Number,
                Email = x.Email.Address,
                Username = x.User.Username,
                Password = x.User.Password,
                Active = x.User.Active
            }).FirstOrDefault(x => x.Username == username);
        }

        public Customer GetByUsername(string username)
        {
            return _context.Customers.Include(x => x.User).AsNoTracking()
                .FirstOrDefault(x => x.User.Username == username);
        }

        public void Save(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
        }
    }
}
