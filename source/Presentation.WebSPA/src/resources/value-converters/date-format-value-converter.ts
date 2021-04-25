import { LogManager } from "aurelia-framework";
import { Logger } from "aurelia-logging";
// ReSharper disable once UnusedLocalImport
import moment from "moment";

export class DateFormatValueConverter {
    private readonly _logger: Logger = LogManager.getLogger(this.constructor.name);

    public toView(value: string, format: string = "M/D/YYYY h:mm:ss a") {
        // ReSharper disable once TsResolvedFromInaccessibleModule
        const formattedDate = moment(value).format(format);
        this._logger.debug("date formatted", { original: value, formatted: formattedDate });
        return formattedDate;
    }
}