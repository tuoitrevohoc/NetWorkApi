import { provideRouter, RouterConfig } from '@angular/router'
import { EntityComposerComponent } from './entity-composer/entity-composer.component'

const routes: RouterConfig = [
    { path: '', component: EntityComposerComponent }
]

export const AppRoutes = [
    provideRouter(routes)
]