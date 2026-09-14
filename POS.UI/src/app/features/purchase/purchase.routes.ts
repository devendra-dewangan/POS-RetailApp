import { Routes } from '@angular/router';

export const PURCHASE_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./pages/purchase-list/purchase-list')
        .then(m => m.default),
  },
];
