import { ProductModel } from "../models/product-model";
import { dbConnection } from "../../../../../common/data-context";
import { CatalogProductEntity, CatalogCategoryEntity } from "../../../../../common/db-model";
import { CategoryModel } from "../models/category-model";

export class CatalogSvc implements ICatalogService {

    constructor() {

    }

    public async getProductByCategory(categoryName: string): Promise<ProductModel[]> {
        throw new Error("Not implemented");
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

    public async getCategories(): Promise<CategoryModel[]> {
        const entities = await dbConnection.select({ from: "Categories" });
        const results = entities.map((entity: CatalogCategoryEntity) => {
            const model = new CategoryModel(entity.id);
            model.name = entity.name;
            model.description = entity.description;
            return model;
        });
        return results;
    }
}

export interface ICatalogService {
    getProductByCategory(categoryName: string): Promise<Array<ProductModel>>;
    getProductBySku(skuParam: string): Promise<ProductModel>;
    getProducts(): Promise<Array<ProductModel>>;
    getCategories(): Promise<Array<CategoryModel>>;

}