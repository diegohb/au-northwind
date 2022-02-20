import { customElement, LogManager, autoinject, bindable } from "aurelia-framework";
import { Router } from "aurelia-router";

@autoinject()
@customElement("main-nav")
export class MainNavVM {
    private readonly _logger = LogManager.getLogger(this.constructor.name);
    private readonly _router: Router;

    public constructor(routerParam: Router) {
        this._router = routerParam;
    }

    @bindable()
    public routes: any;
}