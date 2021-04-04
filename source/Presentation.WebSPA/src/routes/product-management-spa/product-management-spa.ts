import { PLATFORM } from "aurelia-pal";
import { Router, RouterConfiguration, NavigationInstruction } from "aurelia-router";
import { LogManager } from "aurelia-framework"

export class ProductManagementSPAViewModel {
    private readonly _logger = LogManager.getLogger(this.constructor.name);

    public router: Router;

    public configureRouter(pConfig: RouterConfiguration, pRouter: Router) {
        pConfig.title = "Product Management";
        //pConfig.options.pushState = true;
        pConfig.map([
            { route: "", moduleId: PLATFORM.moduleName("./no-selection"), title: "Select" },
            { route: "products/:id", moduleId: PLATFORM.moduleName("./product-detail"), name: "Product" }
        ]);

        this.router = pRouter;
    }
}