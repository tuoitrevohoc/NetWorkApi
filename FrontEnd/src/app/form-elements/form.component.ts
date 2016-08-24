import {
  Component,
  ComponentResolver,
  ViewContainerRef,
  ReflectiveInjector,
  ComponentRef,
  ViewChild,
  AfterViewInit, Input
} from "@angular/core";
import {TextEditor} from "./editor/text-input";

/**
 * form data
 */
export interface InputDefinition {
  field: string
  label: string
  inputType: string
  width: number
  metadata: any
}

/**
 * the component
 */
@Component ({
  selector: 'app-form',
  template: `
  <div class="ui form grid">
    <div #form style="display: none">
    </div>
    <div class="field">
      <button class="ui button">{{ data.name }}</button>
    </div>
  </div>
`,
  directives: [TextEditor]
})
export class FormComponent implements AfterViewInit {

  /**
   * read number in english
   */
  readNumbers = ['one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine', 'ten',
                 'eleven', 'twelve', 'thirteen', 'fourteen', 'fifteen', 'sixteen']

  /**
   * data to edit
   * @type {{name: string}}
   */
  @Input() data = {
    name: "Daniel"
  }

  /**
   * the definition of input
   */
  @Input() inputDefinitions: InputDefinition[]

  /**
   * where form element is input
   */
  @ViewChild("form", { read: ViewContainerRef }) containerRef: ViewContainerRef

  /**
   * the component resolver
   * @param resolver resolve the message
   */
  constructor(private resolver: ComponentResolver) {
  }

  /// after view init
  ngAfterViewInit() {
    [1, 2, 3, 4, 5, 6].forEach((item) => this.addInput())
  }

  /**
   * add input
   */
  addInput() {
    this.resolver.resolveComponent(TextEditor).then(
      factory => {
        let injector = ReflectiveInjector.fromResolvedProviders([],
          this.containerRef.parentInjector)
        let component = this.containerRef.createComponent(factory, 0, injector, []) as ComponentRef<TextEditor>
        component.instance.data = this.data
        component.instance.field = "name"
        component.location.nativeElement.className = "eight wide field"
      }
    )
  }
}
