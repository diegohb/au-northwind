import { FrameworkConfiguration } from "aurelia-framework";

export function configure(configParam: FrameworkConfiguration): void {
    configParam.globalResources(["./hello-world", "./hello-world.css"]);
}