import { LogManager } from "aurelia-framework";
import { Router, NavigationInstruction, RouteConfig } from "aurelia-router";
import { ProductModel } from "../models/product-model";

export class ProductDetailViewModel {
    private readonly _logger = LogManager.getLogger(this.constructor.name);

    constructor(routerParam: Router) {

    }

    public model: ProductModel;

    public async activate(params, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction ): Promise<void> {
        if (!params || !params.id || !Number.isInteger(params.id)) {
            //routeConfig.router.navigate("./no-selection");
        }
        //TODO: load from params.id
        //routeConfig.navModel.setTitle("Product XYZ");
        this.model = new ProductModel();
        this._logger.info("Loaded product details.");
    }

    public async saveDetails(): Promise<void> {
        alert(`Details for product '${this.model.name}' have been saved.`);
    }
}