import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookOrderComponent } from './book-order.component';

const routes: Routes = [{ path: '', component: BookOrderComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BookOrderRoutingModule { }
