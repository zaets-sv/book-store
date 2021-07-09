import type { BookOrderDto, CreateBookOrderDto, GetBookOrderListDto, UpdateBookOrderDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class BookOrderService {
  apiName = 'Default';

  create = (input: CreateBookOrderDto) =>
    this.restService.request<any, BookOrderDto>({
      method: 'POST',
      url: '/api/app/book-order',
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/book-order/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, BookOrderDto>({
      method: 'GET',
      url: `/api/app/book-order/${id}`,
    },
    { apiName: this.apiName });

  getClientList = (input: GetBookOrderListDto) =>
    this.restService.request<any, PagedResultDto<BookOrderDto>>({
      method: 'GET',
      url: '/api/app/book-order/client-list',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  getList = (input: GetBookOrderListDto) =>
    this.restService.request<any, PagedResultDto<BookOrderDto>>({
      method: 'GET',
      url: '/api/app/book-order',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });

  update = (id: string, input: UpdateBookOrderDto) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/book-order/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
