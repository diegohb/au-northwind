export class ProductPriceModel {
    private readonly _changes: Array<ChangeDTO>;

    constructor(initialPriceParam?: number) {
        this._changes = new Array<ChangeDTO>();
        this.increasePrice(87.85, "Initial price.");
        this.decreasePrice(83.42, "Prices go down.");
    }

    public sku = "AB124CDE44FGH";
    public name = "A Strawbery";
    public description = "Some description here...";
    public currentPrice = 0;

    public get history(): Array<ChangeDTO> {
        return this._changes;
    }

    public increasePrice(newPriceParam: number, commentParam?: string) {
        if (newPriceParam <= this.currentPrice) {
            throw new Error("Price increase new price must be higher than existing price.");
        }
        const delta = newPriceParam - this.currentPrice;
        this._changes.push({ type: "increase", amount: delta.toFixed(2), comment: commentParam });
        this.currentPrice = newPriceParam;
    }

    public decreasePrice(newPriceParam: number, commentParam?: string) {
        if (newPriceParam >= this.currentPrice) {
            throw new Error("Price decrease new price must be lower than existing price.");
        }
        const delta = this.currentPrice - newPriceParam;
        this._changes.push({ type: "decrease", amount: delta.toFixed(2), comment: commentParam });
        this.currentPrice = newPriceParam;
    }
}

export type ChangeDTO = {
    type: "increase" | "decrease";
    amount: string;
    comment: string;
};