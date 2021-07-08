import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BookOrderRoutingModule } from './book-order-routing.module';
import { BookOrderComponent } from './book-order.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [BookOrderComponent],
  imports: [
    SharedModule,
    BookOrderRoutingModule
  ]
})
export class BookOrderModule { }
