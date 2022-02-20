import { autoinject, LogManager } from "aurelia-framework";
import { HttpClient } from "aurelia-fetch-client";
import { ApiLoggerInterceptor } from "../../../../../common/api-logger-interceptor";
import { ProductModel } from "../../catalog/models/product-model";

@autoinject()
export class PricingSvc {
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
        const rawResponse = await this._http.fetch(`products/${skuParam}`);
        const dto: any = await rawResponse.json();
        this._logger.debug(`Fetched product by sku '${skuParam}'.`, dto);

        const model = new ProductModel(dto.sku);
        model.name = dto.productName;
        model.description = dto.description;
        model.cost = 0;
        model.price = dto.unitPrice;
        model.quantity = dto.unitsInStock; //TODO: account for units on order.
        return model;
    }
}