using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Acme.BookStore.Permissions;
using Acme.BookStore.Users;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
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
        private readonly IRepository<Author, Guid> _authorRepository;

        public BookOrderAppService(IBookOrderRepository bookOrderRepository, ICurrentUser currentUser, IRepository<Book, Guid> bookRepository, IRepository<AppUser, Guid> userRepository, IAuthorRepository authorRepository)
        {
            _currentUser = currentUser;
            _bookOrderRepository = bookOrderRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _authorRepository = authorRepository;
        }

        public async Task<BookOrderDto> GetAsync(Guid id)
        {
            var order = await _bookOrderRepository.GetAsync(id);

             var user = await _userRepository.GetAsync(order.ClientId);
             var book = await _bookRepository.GetAsync(order.BookId);
             var authors = await _authorRepository.GetListAsync();

            var bookOrderDto = ObjectMapper.Map<BookOrder, BookOrderDto>(order);
             bookOrderDto.ClientName = user.UserName;
             bookOrderDto.BookName = book.Name;
             bookOrderDto.BookType = book.Type;
             bookOrderDto.PublishDate = book.PublishDate;
             bookOrderDto.Price = book.Price;

            for (int i = 0; i < authors.Count; i++)
            {
                if (book.AuthorId == authors[i].Id)
                {
                    bookOrderDto.AuthorName = authors[i].Name;
                }
            }

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

            return _currentUser.Roles[0] == "admin" ? new PagedResultDto<BookOrderDto>(totalCount, bookOrderDto) :  null;
        }

        [Authorize(BookStorePermissions.BooksOrder.Client)]
        public async Task<PagedResultDto<BookOrderDto>> GetClientListAsync(GetBookOrderListDto input)
        {
            var orders = await _bookOrderRepository.GetClientListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting
            );

            var user = await _userRepository.GetListAsync();
            var books = await _bookRepository.GetListAsync();

            var bookOrderDto = ObjectMapper.Map<List<BookOrder>, List<BookOrderDto>>(orders);

            bookOrderDto.ForEach((order) =>
            {
                order.ClientName = user.Find(user => user.Id == order.ClientId).UserName;
                order.BookName = books.Find(book => book.Id == order.BookId).Name;
            });

            var totalCount = orders.Count;

            return _currentUser.Roles[0] == "client" ? new PagedResultDto<BookOrderDto>(totalCount, bookOrderDto) : null;
        }

        [Authorize(BookStorePermissions.BooksOrder.Create)]
        public async Task<BookOrderDto> CreateAsync(CreateBookOrderDto input)
        {

            var orders = await _bookOrderRepository.CreateUsedBookAsync();

            var currentBook = input.BookId;
            bool isBook = false;

            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].BookId == currentBook)
                {
                    isBook = true;
                    break;
                }
            }

            if (!isBook)
            {
                var order = new BookOrder
                {
                    ClientId = (Guid)_currentUser.Id,
                    BookId = input.BookId
                };

                await _bookOrderRepository.InsertAsync(order);

                return ObjectMapper.Map<BookOrder, BookOrderDto>(order);
            }
            else
            {
                throw new UserFriendlyException(L["UserNameShouldBeUniqueMessage"]);
            }
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

        [Authorize(BookStorePermissions.BooksOrder.ChangeStatus)]
        public async Task UpdateStatusAsync(Guid id)
        {
            var order = await _bookOrderRepository.GetAsync(id);

            order.Status = !order.Status ? true : false;

            await _bookOrderRepository.UpdateAsync(order);

        }
    }
}