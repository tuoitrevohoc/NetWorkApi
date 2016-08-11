import { bootstrap } from '@angular/platform-browser-dynamic'
import { enableProdMode, provide } from '@angular/core'
import { DomSanitizationService } from '@angular/platform-browser'
import { AppComponent, environment } from './app/'
import { AppRoutes } from './app/app.routes'
import { Http, ConnectionBackend, HTTP_PROVIDERS, BaseRequestOptions, RequestOptions, Headers } from '@angular/http'
import { DataProviderService } from './app/services/data-provider.service'

if (environment.production) {
  enableProdMode();
}

class HttpOptions extends BaseRequestOptions {
    headers = new Headers({ 'Content-Type': 'application/json' })
}

bootstrap(AppComponent, [
    AppRoutes,
    HTTP_PROVIDERS,
    provide(RequestOptions, { useClass: HttpOptions }),
    DataProviderService
])

