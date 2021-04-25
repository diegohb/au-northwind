import { inject, LogManager } from "aurelia-framework";
import { Router, NavigationInstruction, RouteConfig } from "aurelia-router";
import { ProductModel } from "./models/product-model";
import { ICatalogService, CatalogSvc } from "./services/catalog-svc";
import { Logger } from "aurelia-logging";

//@autoinject()
@inject(Router, CatalogSvc)
export class ProductDetailViewModel {
    private readonly _router: Router;
    private readonly _service: ICatalogService;
    private readonly _logger: Logger = LogManager.getLogger(this.constructor.name);

    constructor(routerParam: Router, svcParam: ICatalogService) {
        this._router = routerParam;
        this._service = svcParam;
    }

    public model: ProductModel;

    public async activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction):
        Promise<boolean> {
        if (!params || !params.sku) {
            //this._router.navigate("./no-selection");
        }
        const model: ProductModel = await this._service.getProductBySku(params.sku);
        if (!model) {
            this._logger.error(`Unable to load product by id ${params.sku}`);
            this._router.navigateToRoute("product-not-found");
            return Promise.resolve(false);
        }
        this.model = model;
        routeConfig.navModel.setTitle(this.model.name);
        this._logger.info("Loaded product details.");
        return Promise.resolve(true);
    }

}