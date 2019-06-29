using Dapper;
using Microsoft.EntityFrameworkCore;
using ModernStore.Domain.Commands.Results;
using ModernStore.Domain.Entities;
using ModernStore.Domain.Repositories;
using ModernStore.Infra.DataContext;
using ModernStore.Shared;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ModernStore.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ModernStoreDataContext _context;

        public ProductRepository(ModernStoreDataContext context)
        {
            _context = context;
        }

        public Product Get(Guid id)
        {
            return _context.Products.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<GetProductListCommandResult> Get()
        {
            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                conn.Open();
                var query = "SELECT [Id], [Title], [Price], [Image] FROM[dbo].[Product]";
                return conn.Query<GetProductListCommandResult>(query);
            }
        }
    }
}
