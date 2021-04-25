import { LogManager } from "aurelia-framework";
// ReSharper disable once UnusedLocalImport
import moment from "moment";

export class DateFormatValueConverter {
    private readonly _logger = LogManager.getLogger(this.constructor.name);

// ReSharper disable InconsistentNaming
    public toView(value: string, format: string = "M/D/YYYY h:mm:ss a") {
// ReSharper restore InconsistentNaming
        // ReSharper disable once TsResolvedFromInaccessibleModule
        const formattedDate = moment(value).format(format);
        this._logger.debug("date formatted", { original: value, formatted: formattedDate });
        return formattedDate;
    }
}