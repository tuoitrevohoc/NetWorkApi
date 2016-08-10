import { Component } from '@angular/core'
import { MenuComponent } from './menu/menu.component'
import { ROUTER_DIRECTIVES } from '@angular/router'

@Component({
  moduleId: module.id,
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.css'],
  directives: [
      MenuComponent,
      ROUTER_DIRECTIVES
  ]
})
export class AppComponent {

    name: string

}
