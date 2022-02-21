import { createGuid } from "../../../../../common/utils";
import { ProductDTO } from "../../../../../models/product-dto";

export class ProductModel {
    private readonly _sku: string;

    constructor(skuParam: string) {
        if (skuParam) {
            this._sku = skuParam;
            return;
        }

        this._sku = createGuid();
        this.name = "";
        this.description = "";
        this.price = 0;
        this.cost = 0;
        this.quantity = 0;
    }

    public get sku(): string { return this._sku; };

    public name: string;
    public description: string;

    public price: number;
    public cost: number;
    public quantity: number;

    /*public offerFreeShipping: boolean;
    public onSale: boolean;*/

    public static fromDTO(dtoParam: ProductDTO): ProductModel {
        const model = new ProductModel(dtoParam.sku);
        model.name = dtoParam.productName;
        model.description = dtoParam.description;
        model.cost = 0;
        model.price = dtoParam.unitPrice;
        model.quantity = dtoParam.unitsInStock; //TODO: account for units on order.
        return model;
    }
}