using Acme.BookStore.Books;
using Acme.BookStore.Users;
using Newtonsoft.Json;
using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.BooksOrder
{
    public class BookOrder: FullAuditedEntity<Guid>
    {
        public Guid BookId { get; set; }
        
        public Guid ClientId { get; set; }
    }
}
