using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.BooksOrder
{
    public interface IBookOrderAppService 
    {
        Task<BookOrderDto> GetAsync(Guid id);

        Task<PagedResultDto<BookOrderDto>> GetListAsync(GetBookOrderListDto input);

        Task<PagedResultDto<BookOrderDto>> GetClientListAsync(GetBookOrderListDto input);

        Task<BookOrderDto> CreateAsync(CreateBookOrderDto input);

        Task UpdateAsync(Guid id, UpdateBookOrderDto input);

        Task DeleteAsync(Guid id);

        Task UpdateStatusAsync(Guid id);
    }
}
