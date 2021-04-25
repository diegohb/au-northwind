import { autoinject, LogManager, bindable } from "aurelia-framework";
import { Router } from "aurelia-router";
import { Logger } from "aurelia-logging";
import { ProductPriceModel } from "./models/product-price-model";

@autoinject
export class PricingIncreaseViewModel {
    private readonly _logger: Logger = LogManager.getLogger(this.constructor.name);
    private _router: Router;
    private _changeType: "increase" | "decrease" = null;

    constructor(routerParam: Router) {
        this._router = routerParam;
    }

    public model: ProductPriceModel = new ProductPriceModel();

    @bindable
    public newPrice: string = "";

    public comment: string = "";

    public get title(): string {
        return this._changeType === "increase" ? "Price Increase" : "Price Decrease";
    }

    public async activate(routeParams: any): Promise<void> {
        this._logger.debug(`Loaded product id ${routeParams.productId}.`);
        this._changeType = routeParams.changeType;
    }

    public async commit(): Promise<void> {
        const price: number = parseFloat(this.newPrice);
        if (this._changeType === "increase") {
            this.model.increasePrice(price, this.comment);
        } else if (this._changeType === "decrease") {
            this.model.decreasePrice(price, this.comment);
        }
        this.newPrice = "";
        this.comment = "";
    }

    public async cancel(): Promise<void> {
        this.newPrice = "";
        this.comment = "";
        this._router.navigateBack();
    }
}