import { autoinject, LogManager } from "aurelia-framework";
import { HttpClient } from "aurelia-fetch-client";
import { ProductModel } from "../models/product-model";
import { CategoryModel } from "../models/category-model";
import { ApiLoggerInterceptor } from "../../../../../common/api-logger-interceptor";
import { ProductDTO } from "../../../../../models/product-dto";
import { CategoryDTO } from "../../../../../models/category-dto";

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
        const data = this._products.filter(model => model.sku === skuParam);
        const productModel: ProductModel = data[0];
        return Promise.resolve(productModel);
    }

    public async getProducts(): Promise<ProductModel[]> {
        if (this._products.length === 0) {
            const rawResponse = await this._http.fetch("products");
            const objects: Array<ProductDTO> = await rawResponse.json();
            this._logger.debug(`Fetched ${objects.length} products.`, objects);

            const productModels = objects.map(dto => ProductModel.fromDTO(dto));

            this._products = productModels;
        }

        return Promise.resolve(this._products);
    }

    public async getProductByCategory(categoryNameParam: string): Promise<ProductModel[]> {
        throw new Error("Not implemented");
    }

    public async getCategories(): Promise<CategoryModel[]> {
        const dtos = await this.fetchCategories();
        const models = dtos.map(dto => CategoryModel.fromDTO(dto));
        return models;
    }


    private async fetchCategories(): Promise<CategoryDTO[]> {
        const rawResponse = await this._http.fetch("categories");
        const objects: Array<CategoryDTO> = await rawResponse.json();
        this._logger.debug(`Fetched ${objects.length} categories.`, objects);
        return objects;
    }
}

export interface ICatalogService {
    getProductBySku(skuParam: string): Promise<ProductModel>;
    getProducts(): Promise<Array<ProductModel>>;
    getProductByCategory(categoryNameParam: string): Promise<Array<ProductModel>>;
    getCategories(): Promise<Array<CategoryModel>>;

}