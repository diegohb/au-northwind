import { createGuid } from "../../../../../common/utils";

export class ProductModel {
    private _sku: string;

    constructor(skuParam?: string) {
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
}