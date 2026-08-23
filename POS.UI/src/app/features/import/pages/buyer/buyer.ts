import { Component, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-buyer',
  standalone: false,
  templateUrl: './buyer.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './buyer.scss',
})
export class Buyer {}
