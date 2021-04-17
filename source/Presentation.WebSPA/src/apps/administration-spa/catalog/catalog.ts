import { bindable } from "aurelia-framework";
import { ProductListItem } from "../models/product-list-item";

export class CatalogVM {
    @bindable
    public products: Array<ProductListItem> = [
        { sku: "ABC123", name: "A Valuable Book", lastUpdate: "2 days ago" },
        { sku: "DEF456", name: "Stethoscope", lastUpdate: "10 days ago" },
        { sku: "GHI789", name: "Box of Chocolates", lastUpdate: "1 day ago" }
    ];
}