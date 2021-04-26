import { customElement, bindable, bindingMode, LogManager } from "aurelia-framework";
import { Logger } from "aurelia-logging";
import { SingleItemSelectorElementBase } from "./single-item-selector-element-base";

@customElement("lr-single-selector")
export class LeftRightSingleSelectorElement extends SingleItemSelectorElementBase {
    protected readonly _logger = LogManager.getLogger(this.constructor.name);

    constructor() {
        super();
    }

    @bindable({ defaultBindingMode: bindingMode.oneTime, default: "", attribute: "label-text" })
    public labelText: string;

    @bindable({ defaultBindingMode: bindingMode.oneTime, attribute: "allow-cycle", default: false })
    public allowCycle: boolean;

    // ReSharper disable once InconsistentNaming
    private async activate(modelParam: any): Promise<void> {
        this.items = modelParam.items;
        this.selectedItem = modelParam.selectedItem;
        this.displayPropertyName = modelParam.displayProperty;
        this.valuePropertyName = modelParam.valueProperty;
        //this.onChange = pModel.onChange; //TODO: unable to pass this through properly using <compose> because it will pass all args to first arg
    }

    public selectPrevious(): void {
        if (this.disabled)
            return;

        let newIndex = this._selectedItemIndex - 1;
        if (newIndex < 0)
            newIndex = this.allowCycle ? this._getMaxIndex() : 0;

        this.selectedItem = this.items[newIndex];
    }

    public selectNext(): void {
        if (this.disabled)
            return;

        let newIndex = this._selectedItemIndex + 1;
        const maxIndex = this._getMaxIndex();
        if (newIndex > maxIndex)
            newIndex = this.allowCycle ? 0 : maxIndex;

        this.selectedItem = this.items[newIndex];
    }

    private _getMaxIndex(): number {
        return this.items.length - 1;
    }
}