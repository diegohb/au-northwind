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

    public async activate(): Promise<void> {
        this.products = await this._productSvc.getShoppableProductSummaries();
        this._logger.debug(`Activated view with ${this.products.length} products.`, this.products);
    }

}