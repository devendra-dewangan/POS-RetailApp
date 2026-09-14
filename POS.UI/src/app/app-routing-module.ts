import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./layout/shell/shell')
        .then(m => m.Shell),
    children: [
      {
        path: '',
        redirectTo: 'purchase',
        pathMatch: 'full',
      },
      {
        path: 'dashboard',
        loadChildren: () =>
          import('./features/dashboard/dashboard.routes')
            .then(m => m.DASHBOARD_ROUTES),
      },
      {
        path: 'import',
        loadChildren: () => 
          import('./features/import/import-module')
            .then(m => m.ImportModule)
      },
      {
        path: 'purchase',
        loadChildren: () => 
          import('./features/purchase/purchase.routes')
            .then(m => m.PURCHASE_ROUTES)
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
