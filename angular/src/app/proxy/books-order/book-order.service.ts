import type { BookOrderDto, CreateBookOrderDto, UpdateBookOrderDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
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

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<BookOrderDto>>({
      method: 'GET',
      url: '/api/app/book-order',
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });

  update = (id: string, input: UpdateBookOrderDto) =>
    this.restService.request<any, BookOrderDto>({
      method: 'PUT',
      url: `/api/app/book-order/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
