import { Component} from '@angular/core';
import {TuiAppBar} from '@taiga-ui/layout';
import { FormsModule } from '@angular/forms';
import {TuiPortals, TuiPortalService} from '@taiga-ui/cdk';
import {
    TuiDataList,
    TuiDropdown,
    TuiInput,
    TuiPopupService,
} from '@taiga-ui/core';
import {
    TuiTabs
} from '@taiga-ui/kit';
import {TuiNavigation} from '@taiga-ui/layout';


@Component({
  imports: [
    TuiAppBar,
    TuiNavigation,
    FormsModule,
    TuiDataList,
    TuiDropdown,
    TuiInput,
    TuiNavigation,
    TuiTabs
],
  selector: 'app-dashboard',
  styleUrl: './dashboard.scss',
  templateUrl: './dashboard.html',
  providers: [{provide: TuiPortalService, useClass: TuiPopupService}]
})
export class Dashboard extends TuiPortals  {}
