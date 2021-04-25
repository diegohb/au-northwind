import { ProductModel } from "../models/product-model";
import { createGuid } from "../../../../../common/utils";

export class CatalogSvc implements ICatalogService {
    private readonly _products: Array<ProductModel>;

    constructor() {
        const prod1: ProductModel = new ProductModel(createGuid());
        prod1.name = "Product 1";
        prod1.description = "A description for product 1.";
        prod1.cost = 17.56;
        prod1.price = 27.99;
        prod1.quantity = 121;

        const prod2: ProductModel = new ProductModel(createGuid());
        prod2.name = "Product 2";
        prod2.description = "A description for product 2.";
        prod2.cost = 4.66;
        prod2.price = 9.99;
        prod2.quantity = 453;

        const prod3: ProductModel = new ProductModel(createGuid());
        prod3.name = "Product 3";
        prod3.description = "A description for product 3.";
        prod3.cost = 44.55;
        prod3.price = 75.00;
        prod3.quantity = 59;

        this._products = [prod1, prod2, prod3];
    }

    public async getProductBySku(skuParam: string): Promise<ProductModel> {
        const data: ProductModel[] = (await this.getProducts()).filter(model => model.sku === skuParam);
        const productModel: ProductModel = data[0];
        return Promise.resolve(productModel);
    }

    public async getProducts(): Promise<ProductModel[]> {
        return Promise.resolve(this._products);
    }
}

export interface ICatalogService {
    getProductBySku(skuParam: string): Promise<ProductModel>;
    getProducts(): Promise<Array<ProductModel>>;

}