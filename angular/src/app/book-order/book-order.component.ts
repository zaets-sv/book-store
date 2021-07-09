import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { BookOrderDto, BookOrderService } from '../proxy/books-order';

@Component({
  selector: 'app-book-order',
  templateUrl: './book-order.component.html',
  styleUrls: ['./book-order.component.scss'],
  providers: [ListService]
})
export class BookOrderComponent implements OnInit {

  allBooksOrder = { items: [], totalCount: 0 } as PagedResultDto<BookOrderDto>;

  constructor( 
    public readonly list: ListService, 
    public readonly clientList: ListService,
    public readonly bookOrderService: BookOrderService,
    private confirmation: ConfirmationService,
    ) { }

  ngOnInit(): void {
    this.getAllBooksOrder();
    this.getClientAllBooksOrder();
  }

  getAllBooksOrder(): void {
    const booksOrder = (query) => this.bookOrderService.getList(query);
    this.list.hookToQuery(booksOrder).subscribe((response) => {
      this.allBooksOrder = response;
    });
  }

  getClientAllBooksOrder(): void {
    const booksOrder = (query) => this.bookOrderService.getClientList(query);
    this.clientList.hookToQuery(booksOrder).subscribe((response) => {
      this.allBooksOrder = response;
    });
  }

  delete(id: string) {
    this.confirmation.info('', '::MakeOrder').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        console.log(true)
        this.bookOrderService.delete(id).subscribe(() => this.list.get());
      }
    });
  }
}
