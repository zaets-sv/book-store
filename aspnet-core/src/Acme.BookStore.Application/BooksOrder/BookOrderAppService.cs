/*using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Acme.BookStore.BooksOrder
{
    public class BookOrderAppService : // : IBookOrderAppService,
       CrudAppService <BookOrder, BookOrderDto, Guid, PagedAndSortedResultRequestDto, CreateBookOrderDto, UpdateBookOrderDto>,
        IBookOrderAppService, ITransientDependency
    {

        private readonly ICurrentUser _currentUser;
        private readonly IBookOrderRepository _bookOrderRepository;

        public BookOrderAppService(IRepository<BookOrder, Guid> repository, ICurrentUser currentUser, IBookOrderRepository bookOrderRepository)
            : base(repository)
        {
            _currentUser = currentUser;
           _bookOrderRepository = bookOrderRepository;
        }

        [Authorize(BookStorePermissions.BooksOrder.Create)]
        public override async Task<BookOrderDto> CreateAsync(CreateBookOrderDto input)
        {

            var order = new BookOrder
            {
                ClientId = (Guid)_currentUser.Id,
                BookId = input.BookId,
            };

           await _bookOrderRepository.InsertAsync(order);

           return ObjectMapper.Map<BookOrder, BookOrderDto>(order);
        }
    }
}*/

using Acme.BookStore.Books;
using Acme.BookStore.Permissions;
using Acme.BookStore.Users;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;

namespace Acme.BookStore.BooksOrder
{
    [Authorize(BookStorePermissions.BooksOrder.Default)]
    public class BookOrderAppService : BookStoreAppService, IBookOrderAppService, IDomainService, ITransientDependency
    {
        private readonly ICurrentUser _currentUser;
        private readonly IBookOrderRepository _bookOrderRepository;
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IRepository<AppUser, Guid> _userRepository;

        public BookOrderAppService(IBookOrderRepository bookOrderRepository, ICurrentUser currentUser, IRepository<Book, Guid> bookRepository, IRepository<AppUser, Guid> userRepository)
        {
            _currentUser = currentUser;
            _bookOrderRepository = bookOrderRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
        }

        public async Task<BookOrderDto> GetAsync(Guid id)
        {
            var order = await _bookOrderRepository.GetAsync(id);

             var user = await _userRepository.GetAsync(order.ClientId);
             var book = await _bookRepository.GetAsync(order.BookId);

             var bookOrderDto = ObjectMapper.Map<BookOrder, BookOrderDto>(order);
             bookOrderDto.ClientName = user.UserName;
             bookOrderDto.BookName = book.Name;

            return ObjectMapper.Map<BookOrder, BookOrderDto>( order, bookOrderDto);
        }

        public async Task<PagedResultDto<BookOrderDto>> GetListAsync(GetBookOrderListDto input)
        {
            var orders = await _bookOrderRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting
            );
            var users = await _userRepository.GetListAsync();
            var books = await _bookRepository.GetListAsync();

            var bookOrderDto = ObjectMapper.Map<List<BookOrder>, List<BookOrderDto>>(orders);
            bookOrderDto.ForEach((order) =>
            {
                order.ClientName = users.Find(user => user.Id == order.ClientId).UserName;
                order.BookName = books.Find(book => book.Id == order.BookId).Name;
            });

            var totalCount = await _bookOrderRepository.CountAsync();

            return new PagedResultDto<BookOrderDto>(
                totalCount,
                bookOrderDto
            );
        }

        [Authorize(BookStorePermissions.BooksOrder.Create)]
        public async Task<BookOrderDto> CreateAsync(CreateBookOrderDto input)
        {
            var order = new BookOrder
            {
                ClientId = (Guid)_currentUser.Id,
                BookId = input.BookId,
            };

            await _bookOrderRepository.InsertAsync(order);

            return ObjectMapper.Map<BookOrder, BookOrderDto>(order);
        }

        [Authorize(BookStorePermissions.BooksOrder.Edit)]
        public async Task UpdateAsync(Guid id, UpdateBookOrderDto input)
        {
            var order = await _bookOrderRepository.GetAsync(id);

            await _bookOrderRepository.UpdateAsync(order);
        }

        [Authorize(BookStorePermissions.BooksOrder.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _bookOrderRepository.DeleteAsync(id);
        }
    }
}