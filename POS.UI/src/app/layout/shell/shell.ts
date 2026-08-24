import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Header } from "../header/header";
import { Sidebar } from "../sidebar/sidebar";
import { Navigation } from "../navigation/navigation";
import { TuiNavigation } from '@taiga-ui/layout';
import { Footer } from "../footer/footer";

export class Portal {}
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
