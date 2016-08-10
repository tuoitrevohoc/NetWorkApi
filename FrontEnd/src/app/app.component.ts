import { Component } from '@angular/core'
import { MenuComponent } from './menu/menu.component'

@Component({
  moduleId: module.id,
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.css'],
  directives: [ MenuComponent ]
})
export class AppComponent {

    name: string

}
