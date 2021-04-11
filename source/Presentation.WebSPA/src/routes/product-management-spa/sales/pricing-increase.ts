import { autoinject, LogManager, bindable } from "aurelia-framework";
import { ProductPriceModel } from "./models/product-price-model";

@autoinject
export class PricingIncreaseViewModel {
    private readonly _logger: Object = LogManager.getLogger(this.constructor.name);

    public model: ProductPriceModel = new ProductPriceModel();

    @bindable
    public newPrice: number;

    public comment: string = "";

    public async commit(): Promise<void> {
        this.model.increasePrice(this.newPrice, this.comment);
        this.newPrice = null;
        this.comment = "";
    }

    public async cancel(): Promise<void> {
        this.newPrice = null;
        this.comment = "";
    }
}