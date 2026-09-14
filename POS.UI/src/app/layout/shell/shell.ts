import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TuiNavigation } from '@taiga-ui/layout';
import { Footer } from '../footer/footer';
import { Header } from '../header/header';
import { Sidebar } from '../sidebar/sidebar';

@Component({
    imports: [
        Footer,
        Header,
        Sidebar,
        RouterOutlet,
        TuiNavigation,
    ],
    selector: 'app-shell',
    styleUrl: './shell.scss',
    templateUrl: './shell.html',
})
export class Shell {}