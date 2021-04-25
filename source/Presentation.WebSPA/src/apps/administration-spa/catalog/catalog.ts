import { autoinject, bindable } from "aurelia-framework";
import { Router } from "aurelia-router";
import { ProductListItem } from "../models/product-list-item";

@autoinject()
export class CatalogVM {
    private _router: Router;

    constructor(routerParam: Router) {
        this._router = routerParam;
    }

    @bindable
    public products: Array<ProductListItem> = [
        { sku: "GHI789", name: "Box of Chocolates", lastUpdate: "1 day ago" },
        { sku: "ABC123", name: "A Valuable Book", lastUpdate: "2 days ago" },
        { sku: "DEF456", name: "Stethoscope", lastUpdate: "10 days ago" }
    ];

    public async navigateToProduct(itemParam: ProductListItem): Promise<boolean> {
        return this._router.navigateToRoute("product", { sku: itemParam.sku });
    }
}