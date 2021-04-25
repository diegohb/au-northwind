import { autoinject, LogManager, bindable } from "aurelia-framework";
import { Router } from "aurelia-router";
import { ProductPriceModel } from "./models/product-price-model";
import * as toastr from "toastr";

@autoinject
export class PricingIncreaseViewModel {
    private readonly _logger = LogManager.getLogger(this.constructor.name);
    private _router: Router;
    private _changeType: "increase" | "decrease" = null;

    constructor(routerParam: Router) {
        this._router = routerParam;
    }

    public model = new ProductPriceModel();

    @bindable
    public newPrice = "";

    public comment = "";

    public get title(): string {
        return this._changeType === "increase" ? "Price Increase" : "Price Decrease";
    }

    public async activate(routeOptionsParam: any): Promise<void> {
        this._logger.debug(`Loaded product id ${routeOptionsParam.productId}.`);
        this._changeType = routeOptionsParam.changeType;
    }

    public async commit(): Promise<void> {
        const price = parseFloat(this.newPrice);

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

        toastr.info(`Submitting change to ${this._changeType} price.`, "Action Queued");

        this.newPrice = "";
        this.comment = "";
    }

    public async cancel(): Promise<void> {
        this.newPrice = "";
        this.comment = "";
        this._router.navigateBack();
    }
}