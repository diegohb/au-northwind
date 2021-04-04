import { LogManager } from "aurelia-framework";
import { ProductModel } from "./models/product-model";

export class ProductDetailViewModel {
    private readonly _logger = LogManager.getLogger(this.constructor.name);
    
    constructor() {
        
    }

    public model: ProductModel;

    public async activate(params, routeConfig): Promise<void> {
        //TODO: load from params.id
        //routeConfig.navModel.setTitle("Product XYZ");
        this.model = new ProductModel();
        this._logger.info("Loaded product details.");
    }

    public async saveDetails(): Promise<void> {
        alert(`Details for product '${this.model.name}' have been saved.`);
    }
}