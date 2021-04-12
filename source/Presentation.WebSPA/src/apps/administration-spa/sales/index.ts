import { PLATFORM } from "aurelia-pal";
import { Router, RouterConfiguration } from "aurelia-router";

export class CatalogIndexViewModel {
    public router: Router;

    public configureRouter(configParam: RouterConfiguration, routerParam: Router) {
        configParam.map([
            { route: "", redirect: "home" },
            {
                route: "home",
                moduleId: PLATFORM.moduleName("./sales"),
                name: "home",
                title: "Sales",
                nav: true
            },
            {
                route: "pricing/:productId/:changeType",
                moduleId: PLATFORM.moduleName("./pricing-increase"),
                name: "pricing",
                title: "Product Pricing",
                nav: false
            }
        ]);

        this.router = routerParam;
    }
}