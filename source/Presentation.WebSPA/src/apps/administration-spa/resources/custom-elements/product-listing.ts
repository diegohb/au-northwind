import { customElement, LogManager, bindable } from "aurelia-framework";
import { Logger } from "aurelia-logging";
import { ProductListItem } from "../../models/product-list-item";

@customElement("product-listing")
export class ProductListingCustomElement {
    private readonly _logger: Logger = LogManager.getLogger(this.constructor.name);

    @bindable()
    public products: Array<ProductListItem> = new Array<ProductListItem>();
}