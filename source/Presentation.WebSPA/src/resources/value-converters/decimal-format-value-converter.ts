import { LogManager } from "aurelia-framework";
import { Logger } from "aurelia-logging";

export class DecimalFormatValueConverter {
    private readonly _logger: Logger = LogManager.getLogger(this.constructor.name);

    public toView(value: string, locale: string = "en-US", fractionDigits: number = 2) {
        try {
            const number: number = Number.parseFloat(value);
            const formatted: string =
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