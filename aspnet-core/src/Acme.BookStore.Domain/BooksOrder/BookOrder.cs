using Acme.BookStore.Books;
using Acme.BookStore.Users;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.BooksOrder
{
    public class BookOrder : AuditedAggregateRoot<Guid>
    {
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
      /*  public List<Book> Books { get; set; } = new List<Book>();*/
    }
}
