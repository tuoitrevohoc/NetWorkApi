import { bootstrap } from '@angular/platform-browser-dynamic'
import { enableProdMode } from '@angular/core'
import { DomSanitizationService } from '@angular/platform-browser'
import { AppComponent, environment } from './app/'
import { AppRoutes } from './app/app.routes'

if (environment.production) {
  enableProdMode();
}

bootstrap(AppComponent, [
    AppRoutes
])

