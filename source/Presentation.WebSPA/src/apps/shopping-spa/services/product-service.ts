import { autoinject, LogManager } from "aurelia-framework";
import { HttpClient } from "aurelia-fetch-client";
import { ApiLoggerInterceptor } from "../../../common/api-logger-interceptor";
import { ProductSummaryModel } from "../models/product-summary";
import { ProductDTO } from "../../../models/product-dto";

@autoinject()
export class ProductService {
    private readonly _http: HttpClient;
    private readonly _logger = LogManager.getLogger(this.constructor.name);

    constructor(httpClientParam: HttpClient) {
        this._http = httpClientParam;
        this._http.configure(config => {
            config
                .useStandardConfiguration()
                .withBaseUrl("/api/")
                .withInterceptor(new ApiLoggerInterceptor());
        });
    }

    public async getShoppableProductSummaries(): Promise<Array<ProductSummaryModel>> {
        const products = await this.fetchProducts();
        const models = products.map(dto => ProductSummaryModel.fromDTO(dto));
        return models;
    }

    private async fetchProducts(): Promise<ProductDTO[]> {
        const rawResponse = await this._http.fetch("products");
        const objects: Array<any> = await rawResponse.json();
        this._logger.debug(`Fetched ${objects.length} products.`, objects);
        return objects;
    }
}