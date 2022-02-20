import { PLATFORM } from "aurelia-pal";
import { Router, RouterConfiguration } from "aurelia-router";
import { LogManager } from "aurelia-framework"

export class ShoppingSPAViewModel {
    private readonly _logger = LogManager.getLogger(this.constructor.name);

    public router: Router;

    public configureRouter(pConfig: RouterConfiguration, pRouter: Router) {
        pConfig.title = "Shopping Site";
        //pConfig.options.pushState = true;
        pConfig.map([
            { route: ["", "products/all"], moduleId: PLATFORM.moduleName("./all-products"), title: "All Products" },
            { route: "products/no-selection", moduleId: PLATFORM.moduleName("./no-selection"), title: "Select" },
            { route: "products/:id", moduleId: PLATFORM.moduleName("./product-detail"), name: "Product" }
        ]);

        this.router = pRouter;
    }
}