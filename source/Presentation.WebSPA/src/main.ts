import { Aurelia } from "aurelia-framework"
import * as auPathUtil from "aurelia-path";
import environment from "./environment";
import "bootstrap";
import "@popperjs/core";
import process from "process";
import * as toastr from "toastr";

export function configure(aureliaParam: Aurelia) {
    aureliaParam.use
        .standardConfiguration()
        .feature("resources");

    aureliaParam.use.globalResources("bootstrap.css");
    aureliaParam.use.globalResources("toastr/build/toastr.css");

    //NOTE: hack fix for popperjs 2.x in bt5 because au-cli bundling struggles to stub out the "process" pkg
    window.process = process;

    aureliaParam.use.developmentLogging(environment.debug ? "debug" : "warn");

    if (environment.testing) {
        aureliaParam.use.plugin("aurelia-testing");
    }

    toastr.options.progressBar = true;
    toastr.options.positionClass = "toast-bottom-right";

    //Uncomment the line below to enable animation.
    // aurelia.use.plugin('aurelia-animator-css');
    //if the css animator is enabled, add swap-order="after" to all router-view elements

    //Anyone wanting to use HTMLImports to load views, will need to install the following plugin.
    // aurelia.use.plugin('aurelia-html-import-template-loader');

    //Following enables multi-spa support with each container 
    //specifying via html-attribute what spa module to load
    const startModuleName = (aureliaParam.host.attributes as any).start.value;
    const spaRootedResourcesPath = auPathUtil.relativeToFile("./resources", startModuleName);
    aureliaParam.use.feature(spaRootedResourcesPath); //TODO: if folder exists so we dont need blank default file.

    aureliaParam.start().then((pAurelia: Aurelia) => {
        pAurelia.setRoot(startModuleName);
        $("#loader").fadeOut("slow");
    });
}