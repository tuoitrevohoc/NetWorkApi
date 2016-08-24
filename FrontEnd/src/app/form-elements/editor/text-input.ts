import {Component, Input} from "@angular/core";
/**
 *
 */
@Component({
  selector: 'app-text-editor',
  template: `
    <label>{{ field }}:</label>
    <div class="ui input">
      <input class="ui input" type="text" [(ngModel)]="data[field]" />
    </div>`
})
export class TextEditor {
  /**
   * the value of the text editor
   */
  @Input() data: any

  @Input() field: any
}
