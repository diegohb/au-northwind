import { customElement, bindable, bindingMode, LogManager, View } from "aurelia-framework";

@customElement("complex-input")
export class ComplexInputCustomElement {
    private readonly _logger = LogManager.getLogger(this.constructor.name);

    @bindable({ defaultBindingMode: bindingMode.oneTime, attribute: "id" })
    public inputId: string;

    @bindable({ defaultBindingMode: bindingMode.oneTime, attribute: "label" })
    public label = "";

    @bindable({ defaultBindingMode: bindingMode.oneTime, attribute: "show-label", default: true })
    public showLabel = true;

    @bindable({ defaultBindingMode: bindingMode.oneTime, attribute: "input-type" })
    public inputType: "number" | "email" | "datetime" | "date" | "text" = "text";

    @bindable({ defaultBindingMode: bindingMode.twoWay, attribute: "read-only", default: false })
    public readOnly = false;

    @bindable({ defaultBindingMode: bindingMode.oneTime })
    public inputCssClass = "";

    @bindable({ defaultBindingMode: bindingMode.oneTime })
    public actionMenuCssClass = "";

    @bindable({ defaultBindingMode: bindingMode.twoWay })
    public value = "";

    @bindable({ defaultBindingMode: bindingMode.toView, attribute: "prepend" })
    public prependText = "";

    @bindable({ defaultBindingMode: bindingMode.toView, attribute: "append" })
    public appendText = "";

    @bindable({ defaultBindingMode: bindingMode.oneTime, attribute: "action-menu", default: false })
    public hasActionMenu = false;

    public get hasPrepend(): boolean {
        return this.prependText && this.prependText.length > 0;
    }

    public get hasAppend(): boolean {
        return this.appendText && this.appendText.length > 0;
    }

    public async created(owningViewParam: View, thisViewParam: View): Promise<void> {
        if (!this.inputId || this.inputId.length === 0) {
            throw new Error("[complex-input] InputId property cannot be null or empty string.");
        }

        this._logger.debug(`Successfully created complex-input by ID '${this.inputId}'.`);
    }

    public async attached(): Promise<void> {
        if (!this.label || this.label.length === 0) {
            this.showLabel = false;
            this._logger.info("Automatically hid label element due to empty label value.");
        }
    }
}