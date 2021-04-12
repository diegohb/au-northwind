import { bindable } from "aurelia-framework";
import { ProductListItem } from "../models/product-list-item";

export class PricingViewModel {

    @bindable
    public products: Array<ProductListItem> = [{ sku: "ABC123", name: "A Valuable Book", lastUpdate: "2 days ago" }];
}