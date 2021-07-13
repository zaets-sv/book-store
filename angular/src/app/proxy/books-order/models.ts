import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { BookType } from '../books/book-type.enum';

export interface BookOrderDto extends EntityDto<string> {
  bookId?: string;
  bookName?: string;
  clientId?: string;
  clientName?: string;
  status: boolean;
  authorName?: string;
  bookType: BookType;
  publishDate?: string;
  price: number;
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
