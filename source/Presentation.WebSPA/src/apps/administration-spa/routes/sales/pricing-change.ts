import { autoinject, LogManager, bindable } from "aurelia-framework";
import { Router } from "aurelia-router";
import { Logger } from "aurelia-logging";
import { ProductPriceModel } from "./models/product-price-model";
import * as toastr from"toastr";

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

        if (!this.comment || this.comment.length === 0) {
            toastr.warning("A comment is required to change the price.");
            return;
        }

        if (this._changeType === "increase") {
            try {
                this.model.increasePrice(price, this.comment);
            } catch (errIncrease) {
                if (errIncrease.message.startsWith("Price increase")) {
                    toastr.warning(errIncrease.message);
                    return;
                }

                throw errIncrease;
            }
        } else if (this._changeType === "decrease") {
            try {
                this.model.decreasePrice(price, this.comment);
            } catch (errDecrease) {
                if (errDecrease.message.startsWith("Price decrease")) {
                    toastr.warning(errDecrease.message);
                    return;
                }
            }
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