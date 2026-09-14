import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Header } from "../header/header";
import { Sidebar } from "../sidebar/sidebar";
import { TuiNavigation } from '@taiga-ui/layout';

@Component({
  imports: [
    Header,
    Sidebar,
    TuiNavigation,
    RouterOutlet,
],
  selector: 'app-shell',
  styleUrl: './shell.scss',
  templateUrl: './shell.html',
})
export class Shell  {
}
