import { platformBrowser, BrowserModule, bootstrapApplication } from '@angular/platform-browser';

import { provideBrowserGlobalErrorListeners, importProvidersFrom } from '@angular/core';
import { AppRoutingModule } from './app/app-routing-module';
import { App } from './app/app';
import { provideTaiga } from '@taiga-ui/core';

bootstrapApplication(App, {
  providers: [
    importProvidersFrom(BrowserModule, AppRoutingModule),
    provideBrowserGlobalErrorListeners(),
    provideTaiga()
  ],
}).catch((err) => console.error(err));
