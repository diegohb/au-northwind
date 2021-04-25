import { LogManager } from "aurelia-framework";
import { Logger } from "aurelia-logging";

export class CurrencyFormatValueConverter {
    private readonly _logger: Logger = LogManager.getLogger(this.constructor.name);

    public toView(value: string, locale: string = "en-US", currency: string = "USD") {
        try {
            const number: number = Number.parseFloat(value);
            const formatted: string =
                new Intl.NumberFormat(locale, { style: "currency", currency: currency }).format(number);
            this._logger.debug("currency formatted", { original: value, formatted: formatted });
            return formatted;
        } catch (error) {
            this._logger.error(`Error parsing currency from value '${value}'.`, error);
            return "0";
        }
    }
}