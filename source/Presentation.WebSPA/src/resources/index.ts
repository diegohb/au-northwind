import {FrameworkConfiguration} from "aurelia-framework";

export function configure(config: FrameworkConfiguration) {
    config.globalResources([
        "./elements/lr-single-selector",
        "./value-converters/date-format-value-converter"
    ]);
}
