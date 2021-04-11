import { customElement, bindable, bindingMode, LogManager } from "aurelia-framework";
import { Logger } from "aurelia-logging";

@customElement("complex-input")
export class ComplexInputCustomElement {
    private readonly _logger: Logger = LogManager.getLogger(typeof ComplexInputCustomElement);

    @bindable({ defaultBindingMode: bindingMode.oneTime, attribute: "id" })
    public inputId: string;
    @bindable({ defaultBindingMode: bindingMode.oneTime, attribute: "input-type" })
    public inputType: "number" | "email" | "datetime" | "date" | "text";
    @bindable({ defaultBindingMode: bindingMode.oneTime })
    public inputCssClass: string = "";
    @bindable({ defaultBindingMode: bindingMode.oneTime })
    public actionMenuCssClass: string = "";
    @bindable({ defaultBindingMode: bindingMode.twoWay })
    public value: string = "";
    @bindable({ defaultBindingMode: bindingMode.toView, attribute: "prepend" })
    public prependText: string = "";
    @bindable({ defaultBindingMode: bindingMode.toView, attribute: "append" })
    public appendText: string = "";
    @bindable({ defaultBindingMode: bindingMode.oneTime, attribute: "action-menu", default: false })
    public hasActionMenu: boolean = false;

    public get hasPrepend(): boolean {
        return this.prependText && this.prependText.length > 0;
    }

    public get hasAppend(): boolean {
        return this.appendText && this.appendText.length > 0;
    }
}