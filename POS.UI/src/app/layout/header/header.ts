import { Component, computed, effect, inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import {
    TuiButton,
    TuiDataList,
    TuiDropdown,
    TuiIcon,
    TuiInput,
    TUI_DARK_MODE,
} from '@taiga-ui/core';
import { TuiAvatar, TuiFade } from '@taiga-ui/kit';
import { TuiNavigation } from '@taiga-ui/layout';
import { NAV_ITEMS } from '../nav-items';

@Component({
    imports: [
        RouterLink,
        FormsModule,
        TuiAvatar,
        TuiButton,
        TuiDataList,
        TuiDropdown,
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
    protected readonly darkMode = inject(TUI_DARK_MODE);
    protected readonly darkModeIcon = computed(() =>
        this.darkMode() ? '@tui.sun-moon' : '@tui.moon'
    );

    private readonly documentRef = inject(DOCUMENT);

    constructor() {
        effect(() => {
            const html = this.documentRef.documentElement;

            if (this.darkMode()) {
                html.setAttribute('tuiTheme', 'dark');
            } else {
                html.removeAttribute('tuiTheme');
            }
        });
    }

    protected toggleDarkMode(): void {
        this.darkMode.update((isDark) => !isDark);
    }
}