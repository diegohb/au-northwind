import { FrameworkConfiguration } from "aurelia-framework";

export function configure(config: FrameworkConfiguration) {
    config.globalResources([
        "./elements/lr-single-selector",
        "./elements/complex-input",
        "./value-converters/date-format-value-converter"
    ]);
}