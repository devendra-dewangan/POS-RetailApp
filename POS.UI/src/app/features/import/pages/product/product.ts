import { Component, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-product',
  standalone: false,
  templateUrl: './product.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './product.scss',
})
export class Product {}
