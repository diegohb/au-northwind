import { autoinject, LogManager } from "aurelia-framework";
import { ProductSummaryModel } from "./models/product-summary";
import { ProductService } from "./services/product-service";

@autoinject()
export class AllProductsVM {
    private readonly _logger = LogManager.getLogger(this.constructor.name);
    private readonly _productSvc: ProductService;

    constructor(productServiceParam: ProductService) {
        this._productSvc = productServiceParam;
    }

    public products: Array<ProductSummaryModel>;

    public async activate(params: any): Promise<void> {
        if (!params || !params.categoryName) {
            this.products = await this._productSvc.getShoppableProductSummaries();
        }
        else {
            this.products = await this._productSvc.getShoppableProductSummariesByCategory(params.categoryName);
        }
        this._logger.debug(`Activated view with ${this.products.length} products.`, this.products);
    }

}