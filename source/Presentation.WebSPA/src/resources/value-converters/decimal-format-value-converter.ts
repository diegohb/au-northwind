import { LogManager } from "aurelia-framework";

export class DecimalFormatValueConverter {
    private readonly _logger = LogManager.getLogger(this.constructor.name);

// ReSharper disable InconsistentNaming
    public toView(value: string, locale: string = "en-US", fractionDigits: number = 2) {
// ReSharper restore InconsistentNaming
        try {
            const number = Number.parseFloat(value);
            const formatted =
                new Intl.NumberFormat(locale,
                        {
                            style: "decimal",
                            minimumFractionDigits: fractionDigits,
                            maximumFractionDigits: fractionDigits
                        })
                    .format(number);
            this._logger.debug("decimal formatted", { original: value, formatted: formatted });
            return formatted;
        } catch (error) {
            this._logger.error(`Error parsing decimal from value '${value}'.`, error);
            return "0";
        }
    }
}