using Acme.BookStore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Users;

namespace Acme.BookStore.BooksOrder
{
    public class EfCoreBookOrderRepository : EfCoreRepository<BookStoreDbContext, BookOrder, Guid>,
            IBookOrderRepository, ITransientDependency
    {
        private readonly ICurrentUser _currentUser;

        public EfCoreBookOrderRepository(
            IDbContextProvider<BookStoreDbContext> dbContextProvider, ICurrentUser currentUser)
            : base(dbContextProvider)
        {
            _currentUser = currentUser;
        }

        public async Task<List<BookOrder>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        public async Task<BookOrder> FindOrderAsync(BookOrder input)
        {
            var dbSet = await GetDbSetAsync();

            return dbSet.Where(
                    order => order.ClientId == input.ClientId
                 ).Where(order => order.BookId == input.BookId)
                .FirstOrDefault();
        }

        public async Task<List<Guid>> GetAllBooksOrder(Guid id)
        {
            var dbSet = await GetDbSetAsync();

            return await dbSet.Where(
                    order => order.BookId == id
                 ).Select(order => order.Id).ToListAsync();
        }
        public async Task<List<Guid>> GetAllBookOrder(Guid id)
        {
            var dbSet = await GetDbSetAsync();

            return await dbSet.Where(
                    order => order.BookId == id
                 ).Select(order => order.Id).ToListAsync();
        }
    }
}