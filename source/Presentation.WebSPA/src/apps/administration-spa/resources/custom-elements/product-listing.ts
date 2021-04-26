import { customElement, LogManager, bindable } from "aurelia-framework";
import { ProductListItem } from "../../models/product-list-item";

@customElement("product-listing")
export class ProductListingCustomElement {
    private readonly _logger = LogManager.getLogger(this.constructor.name);

    @bindable()
    public onClick: Function;

    @bindable()
    public products = new Array<ProductListItem>();

    public async performClick(itemParam: ProductListItem): Promise<void> {
        return await this.onClick({ itemParam: itemParam });
    }

}