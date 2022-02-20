import { customElement, LogManager } from "aurelia-framework";

@customElement("main-nav")
export class MainNavVM {
    private readonly _logger = LogManager.getLogger(this.constructor.name);

}