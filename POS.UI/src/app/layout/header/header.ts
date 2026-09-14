import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { TuiButton, TuiDataList, TuiIcon, TuiInput } from '@taiga-ui/core';
import { TuiAvatar, TuiBadgeNotification, TuiFade } from '@taiga-ui/kit';
import { TuiNavigation } from '@taiga-ui/layout';
import { NAV_ITEMS } from '../nav-items';

@Component({
    imports: [
        RouterLink,
        FormsModule,
        TuiAvatar,
        TuiBadgeNotification,
        TuiButton,
        TuiDataList,
        TuiFade,
        TuiIcon,
        TuiInput,
        TuiNavigation,
    ],
    selector: 'app-header',
    styleUrl: './header.scss',
    templateUrl: './header.html',
})
export class Header {
    protected open = false;
    protected search = '';

    protected readonly navItems = NAV_ITEMS;
}