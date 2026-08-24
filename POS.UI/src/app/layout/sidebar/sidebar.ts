
import { Component, Directive, signal } from '@angular/core';
import {TuiAppBar} from '@taiga-ui/layout';
import {RouterLink, RouterLinkActive} from '@angular/router';
import {
  TuiAsideComponent,
  TuiAsideItemDirective,
} from '@taiga-ui/layout/components/navigation';
import { FormsModule } from '@angular/forms';
import {TuiPortals, TuiPortalService} from '@taiga-ui/cdk';
import {
    TuiButton,
    TuiDataList,
    TuiDropdown,
    TuiInput,
    TuiPopupService,
} from '@taiga-ui/core';
import {
    TuiBadge,
    TuiChevron,
    TuiFade,
    TuiTabs,
} from '@taiga-ui/kit';
import { TuiNavigation} from '@taiga-ui/layout';
import { NgTemplateOutlet } from '@angular/common';


@Component({

  imports: [
    TuiAppBar,
    TuiNavigation,
    RouterLink,
    RouterLinkActive,
    TuiAsideComponent,
    TuiAsideItemDirective,
    FormsModule,
    NgTemplateOutlet,
    RouterLink,
    TuiBadge,
    TuiButton,
    TuiChevron,
    TuiDataList,
    TuiDropdown,
    TuiFade,
    TuiInput,
    TuiNavigation,
    TuiTabs,
],
  selector: 'app-sidebar',
  styleUrl: './sidebar.scss',
  templateUrl: './sidebar.html',
  providers: [{provide: TuiPortalService, useClass: TuiPopupService}]
})
export class Sidebar extends TuiPortals  {
  protected readonly expanded = signal(false);
    protected readonly routes: any = {};
 
    protected handleToggle(): void {
        this.expanded.update((e) => !e);
    }
}

