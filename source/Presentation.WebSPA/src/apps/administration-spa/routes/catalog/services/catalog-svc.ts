import { autoinject, LogManager } from "aurelia-framework";
import { HttpClient } from "aurelia-fetch-client";
import { ProductModel } from "../models/product-model";
import { CategoryModel } from "../models/category-model";
import { ApiLoggerInterceptor } from "../../../../../common/api-logger-interceptor";

@autoinject()
export class CatalogSvc implements ICatalogService {
    private readonly _logger = LogManager.getLogger(this.constructor.name);
    private _products = new Array<ProductModel>();

    constructor(private _http: HttpClient) {
        _http.configure(config => {
            config
                .useStandardConfiguration()
                .withBaseUrl("/api/")
                .withInterceptor(new ApiLoggerInterceptor());
        });
    }

    public async getProductBySku(skuParam: string): Promise<ProductModel> {
        const data = this._products.filter(model => model.sku == skuParam);
        const productModel: ProductModel = data[0];
        return Promise.resolve(productModel);
    }

    public async getProducts(): Promise<ProductModel[]> {
        if (this._products.length === 0) {
            const rawResponse = await this._http.fetch("products");
            const objects: Array<any> = await rawResponse.json();
            this._logger.debug(`Fetched ${objects.length} products.`, objects);

            const productModels = objects.map(t => {
                const model = new ProductModel(t.productId);
                model.name = t.productName;
                model.description = "";
                model.cost = 0;
                model.price = t.unitPrice;
                model.quantity = t.unitsInStock; //TODO: account for units on order.
                return model;
            });

            this._products = productModels;
        }

        return Promise.resolve(this._products);
    }

    public async getProductByCategory(categoryNameParam: string): Promise<ProductModel[]> {
        throw new Error("Not implemented");
    }

    public async getCategories(): Promise<CategoryModel[]> {
        return [
            { id: 1, name: "Books", description: "Inventory of books.", productCount: 7 },
            { id: 2, name: "Food", description: "Inventory of foods.", productCount: 5 },
            { id: 3, name: "Medical", description: "Inventory of medical supplies.", productCount: 3 },
            { id: 4, name: "Music", description: "Inventory of music.", productCount: 9 }
        ];
    }
}

export interface ICatalogService {
    getProductBySku(skuParam: string): Promise<ProductModel>;
    getProducts(): Promise<Array<ProductModel>>;
    getProductByCategory(categoryNameParam: string): Promise<Array<ProductModel>>;
    getCategories(): Promise<Array<CategoryModel>>;

}