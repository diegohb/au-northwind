import { inject, bindable } from "aurelia-framework";
import { Router } from "aurelia-router";
import { ICatalogService, CatalogSvc } from "./services/catalog-svc";
import { ProductModel } from "./models/product-model";
import { ProductListItem } from "../../models/product-list-item";

@inject(Router, CatalogSvc)
export class CatalogVM {
    private readonly _router: Router;
    private readonly _service: ICatalogService;

    constructor(routerParam: Router, svcParam: ICatalogService) {
        this._router = routerParam;
        this._service = svcParam;
    }

    @bindable
    public products: Array<ProductListItem> = [];

    public async attached(): Promise<void> {
        const productModels: Object[] = await this._service.getProducts();
        this.products = productModels.map((model: ProductModel): ProductListItem => {
            const item = new ProductListItem();
            item.sku = model.sku;
            item.name = model.name;
            item.description = model.description;
            item.lastUpdate = "A few minutes ago.";
            return item;
        });
    }

    public async navigateToProduct(itemParam: ProductListItem): Promise<boolean> {
        return this._router.navigateToRoute("product", { sku: itemParam.sku });
    }
}