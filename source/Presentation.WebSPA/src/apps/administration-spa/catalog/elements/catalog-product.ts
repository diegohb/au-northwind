import { customElement, bindable } from "aurelia-framework";
import { ProductModel } from "../models/product-model";

@customElement("catalog-product")
export class CatalogProductViewModel {

    @bindable()
    public product: ProductModel;

    public async save(): Promise<void> {
        alert(`Details for product '${this.product.name}' have been saved to the catalog.`);
    }
}