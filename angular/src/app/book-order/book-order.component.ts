import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { BookOrderDto, BookOrderService } from '../proxy/books-order';

@Component({
  selector: 'app-book-order',
  templateUrl: './book-order.component.html',
  styleUrls: ['./book-order.component.scss'],
  providers: [ListService]
})
export class BookOrderComponent implements OnInit {

  order = { items: [], totalCount: 0 } as PagedResultDto<BookOrderDto>;
  allOrder = { items: [], totalCount: 0 } as PagedResultDto<BookOrderDto>;
  isModalOpen = false;
  selectedId = "";
  reason = "";

  constructor( 
    public readonly list: ListService, 
    private bookOrderService: BookOrderService
    ) { }

  ngOnInit(): void {

    this.getAllBooksOrder();
    console.log("this.allOrder -> " + this.allOrder.totalCount)
  }

  getAllBooksOrder(): void {
    const orderStreamCreator = (query) => this.bookOrderService.getList(query);
    this.list.hookToQuery(orderStreamCreator).subscribe((response) => {
      this.allOrder = response;
    });
  }
}
