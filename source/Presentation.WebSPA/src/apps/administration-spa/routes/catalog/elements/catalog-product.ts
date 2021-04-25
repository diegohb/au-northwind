import { customElement, bindable } from "aurelia-framework";
import { ProductModel } from "../models/product-model";
import * as toastr from "toastr";

@customElement("catalog-product")
export class CatalogProductViewModel {

    @bindable()
    public product: ProductModel;

    public async save(): Promise<void> {
        toastr.info(`Saving details for '${this.product.name}'.`, "Action Queued");
    }
}