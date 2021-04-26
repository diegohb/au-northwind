import { bindable, bindingMode, observable, LogManager } from "aurelia-framework";

export abstract class SingleItemSelectorElementBase {
    protected readonly _logger = LogManager.getLogger(this.constructor.name);
    @observable
    protected _selectedItemIndex = -1;

    constructor() {

    }

    @bindable({ defaultBindingMode: bindingMode.oneTime, attribute: "empty-text", default: "choose" })
    public emptyText: string;

    @bindable({ defaultBindingMode: bindingMode.twoWay, attribute: "class" })
    public cssClass: string;

    @bindable({ defaultBindingMode: bindingMode.twoWay, attribute: "disabled" })
    public disabled: boolean;

    @bindable({ defaultBindingMode: bindingMode.oneTime, attribute: "display-property" })
    public displayPropertyName: string;

    @bindable({ defaultBindingMode: bindingMode.oneTime, attribute: "value-property" })
    public valuePropertyName: string;

    @bindable({ defaultBindingMode: bindingMode.toView, changeHandler: "_itemsChanged" })
    public items: any[];

    @bindable({
        defaultBindingMode: bindingMode.twoWay,
        attribute: "selected-item",
        changeHandler: "_selectedItemChanged"
    })
    public selectedItem: any;

    @bindable()
    public onChange: Function;

    public getDisplayValue(itemParam: any): string {
        if (!itemParam)
            return this.emptyText;

        if (this.displayPropertyName)
            return itemParam[this.displayPropertyName];

        if (typeof itemParam === "object" && this.valuePropertyName)
            return itemParam[this.valuePropertyName];
        else
            return itemParam.toString();
    }


    protected _itemsChanged(newValueParam: any[], oldValueParam: any[]): void {
        this._validateProperties();
        this._selectedItemIndex = -1;
    }

    protected _selectedItemChanged(newValueParam: any, oldValueParam: any): void {
        if (!this.items || this.items.length < 1) {
            throw new Error("Unexpected error. Selected item changed but items is empty!");
        }

        const selectedIndex = this.items.indexOf(newValueParam);
        if (selectedIndex === -1) {
            this._logger.warn("Unable to find item within list.", newValueParam);
            throw new Error("Item doesn't exist in array.");
        }
        this._selectedItemIndex = selectedIndex;
    }

    protected async _selectedItemIndexChanged(newIndexParam: number, oldIndexParam: number): Promise<void> {
        if (!this.onChange)
            return;

        try {
            const oldItem: any = this.items[oldIndexParam];
            const newItem: any = this.items[newIndexParam];
            await this.onChange({ pSender: this, pNewValue: newItem, pOldValue: oldItem });
        } catch (err) {
            this._logger.error("Error occurred trying to call supplied onChange method.", err);
        }
    }

    protected _validateProperties() {
        if (!Array.isArray(this.items)) {
            this._logger.error("Invalid array of items provided to SingleItemSelectorElementBase.");
            return;
        }

        if (!this.items || this.items.length === 0) {
            this._logger.error("Array of items provided to SingleItemSelectorElementBase is empty.");
            return;
        }

        const allContainProperties = this.items.every(itemParam => {
            if (typeof itemParam === "object" &&
                this.valuePropertyName &&
                itemParam[this.valuePropertyName] === undefined)
                return false;

            if (this.displayPropertyName) {
                if (itemParam[this.displayPropertyName] === undefined)
                    return false;

                //check is valid type to be rendered in html
                if (typeof itemParam[this.displayPropertyName] !== "string" &&
                    typeof itemParam[this.displayPropertyName] !== "number") {
                    return false;
                }
            }

            return true;
        });

        if (!allContainProperties) {
            this._logger.error("Invalid item found in data source provided to SingleItemSelectorElementBase.");
        }
    }
}