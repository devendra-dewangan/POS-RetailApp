import { Component, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-sale',
  standalone: false,
  templateUrl: './sale.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './sale.scss',
})
export class Sale {}
