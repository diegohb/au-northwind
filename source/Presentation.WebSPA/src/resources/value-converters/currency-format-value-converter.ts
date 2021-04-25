import { LogManager } from "aurelia-framework";

export class CurrencyFormatValueConverter {
    private readonly _logger = LogManager.getLogger(this.constructor.name);

// ReSharper disable InconsistentNaming
    public toView(value: string, locale: string = "en-US", currency: string = "USD") {
// ReSharper restore InconsistentNaming
        try {
            const number = Number.parseFloat(value);
            const formatted =
                new Intl.NumberFormat(locale, { style: "currency", currency: currency }).format(number);
            this._logger.debug("currency formatted", { original: value, formatted: formatted });
            return formatted;
        } catch (error) {
            this._logger.error(`Error parsing currency from value '${value}'.`, error);
            return "0";
        }
    }
}