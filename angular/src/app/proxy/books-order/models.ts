import type { EntityDto } from '@abp/ng.core';

export interface BookOrderDto extends EntityDto<string> {
  bookId?: string;
  bookName?: string;
  clientId?: string;
  clientName?: string;
}

export interface CreateBookOrderDto {
  bookId?: string;
}

export interface UpdateBookOrderDto {
  bookId?: string;
  clientId?: string;
}
