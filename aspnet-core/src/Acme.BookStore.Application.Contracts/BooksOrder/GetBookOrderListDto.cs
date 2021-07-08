using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.BooksOrder
{
    public class GetBookOrderListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}