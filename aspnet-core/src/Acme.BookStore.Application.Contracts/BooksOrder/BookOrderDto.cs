using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.BooksOrder
{
    public class BookOrderDto : EntityDto<Guid>
    {
        public Guid BookId { get; set; }

        public string BookName { get; set; }

        public Guid ClientId { get; set; }
       
        public string ClientName { get; set; }
    }
}
