import { ProductDTO } from "./product-dto";

export class ProductSummaryModel {
    public constructor(
        private readonly _sku: string,
        private readonly _name: string,
        private readonly _price: number,
        private readonly _description?: string) {
    }

    public get sku(): string { return this._sku; }

    public get name(): string { return this._name; }

    public get price(): number { return this._price; }

    public get description(): string { return this._description; }

    public static fromDTO(dtoParam: ProductDTO) {
        return new ProductSummaryModel(dtoParam.sku, dtoParam.productName, dtoParam.unitPrice, dtoParam.description);
    }
}