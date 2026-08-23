import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ImportRoutingModule } from './import-routing-module';
import { Purchase } from './pages/purchase/purchase';
import { Sale } from './pages/sale/sale';
import { Product } from './pages/product/product';
import { Buyer } from './pages/buyer/buyer';
import { Saller } from './pages/saller/saller';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [CommonModule, ImportRoutingModule, FormsModule, Purchase, Sale, Product, Buyer, Saller],
})
export class ImportModule {}
