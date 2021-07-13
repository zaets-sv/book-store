import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BookOrderDto, BookOrderService } from '../proxy/books-order';

@Component({
  selector: 'app-book-order',
  templateUrl: './book-order.component.html',
  styleUrls: ['./book-order.component.scss'],
  providers: [ListService]
})
export class BookOrderComponent implements OnInit {

  allBooksOrder = { items: [], totalCount: 0 } as PagedResultDto<BookOrderDto>;
  detailsBook = {} as BookOrderDto;
  form: FormGroup;
  isModalOpen = false;

  constructor( 
    public datepipe: DatePipe,
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
    this.list.get();
  }

  delete(id: string) {
    this.confirmation.info('', '::MakeOrder').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        console.log(true)
        this.bookOrderService.delete(id).subscribe(() => this.list.get());
      }
    });
  }
  updateStatus(id: string) {
    this.confirmation.info('', 'Change status?').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        console.log(true)
        this.bookOrderService.updateStatus(id).subscribe(() => this.list.get());
      }
    });
  }

  details(id: string) {
      this.bookOrderService.get(id).subscribe((response) => {
        this.detailsBook = response;
        console.log("this.detailsBook => " + this.detailsBook);
        
        this.isModalOpen = true;
      });
  }
}
