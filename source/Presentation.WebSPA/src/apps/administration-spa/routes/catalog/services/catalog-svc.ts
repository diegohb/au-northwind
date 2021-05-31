import { ProductModel } from "../models/product-model";
import { dbConnection } from "../../../../../common/data-context";
import { CatalogProductEntity } from "../../../../../common/db-model";

export class CatalogSvc implements ICatalogService {

    constructor() {

    }

    public async getProductBySku(skuParam: string): Promise<ProductModel> {
        const data = (await this.getProducts()).filter(model => model.sku === skuParam);
        const productModel: ProductModel = data[0];
        return Promise.resolve(productModel);
    }

    public async getProducts(): Promise<ProductModel[]> {
        const entities = await dbConnection.select({ from: "Products" });
        const results = entities.map((entity: CatalogProductEntity) => {
            const model = new ProductModel(entity.sku);
            model.name = entity.productName;
            model.description = entity.description;
            return model;
        });
        return results;
    }
}

export interface ICatalogService {
    getProductBySku(skuParam: string): Promise<ProductModel>;
    getProducts(): Promise<Array<ProductModel>>;

}