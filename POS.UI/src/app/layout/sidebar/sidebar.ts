import { Component, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TuiFade } from '@taiga-ui/kit';
import { TuiNavigation } from '@taiga-ui/layout';
import { NAV_ITEMS } from '../nav-items';

@Component({
    imports: [
        RouterLink,
        TuiFade,
        TuiNavigation,
    ],
    selector: 'app-sidebar',
    styleUrl: './sidebar.scss',
    templateUrl: './sidebar.html',
})
export class Sidebar {
    protected readonly navItems = NAV_ITEMS;
    protected readonly expanded = signal(false);

    protected handleToggle(): void {
        this.expanded.update((expanded) => !expanded);
    }
}