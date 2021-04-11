import { FrameworkConfiguration, PLATFORM } from "aurelia-framework";

export function configure(config: FrameworkConfiguration) {
    //config.globalResources([]);

    const spaFeaturesPath: string = "apps/administration-spa/resources/features";
    config.feature(PLATFORM.moduleName(`${spaFeaturesPath}/test-feature`));
}