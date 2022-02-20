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
            {
                name: "allProducts",
                route: ["", "products/all"],
                moduleId: PLATFORM.moduleName("./all-products"),
                title: "All Products",
                nav: true
            },
            {
                name: "featuredProducts",
                route: "products/featured",
                moduleId: PLATFORM.moduleName("./no-selection"),
                title: "Featured",
                nav: true
            },
            {
                name: "specials",
                route: "products/specials",
                moduleId: PLATFORM.moduleName("./no-selection"),
                title: "Specials",
                nav: true
            },
            {
                name: "noSelection",
                route: "products/no-selection",
                moduleId: PLATFORM.moduleName("./no-selection"),
                title: "Select",
                nav: false
            },
            {
                name: "productById",
                route: "products/:id",
                moduleId: PLATFORM.moduleName("./product-detail"),
                nav: false
            }
        ]);

        this.router = pRouter;
    }
}