import { FrameworkConfiguration } from "aurelia-framework";

export function configure(configParam: FrameworkConfiguration) {
    configParam.globalResources([
        "./elements/lr-single-selector",
        "./elements/complex-input",
        "./value-converters/date-format-value-converter",
        "./value-converters/currency-format-value-converter",
        "./value-converters/decimal-format-value-converter"
    ]);
}