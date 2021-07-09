import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface BookOrderDto extends EntityDto<string> {
  bookId?: string;
  bookName?: string;
  clientId?: string;
  clientName?: string;
}

export interface CreateBookOrderDto {
  bookId?: string;
}

export interface GetBookOrderListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface UpdateBookOrderDto {
  bookId?: string;
}
