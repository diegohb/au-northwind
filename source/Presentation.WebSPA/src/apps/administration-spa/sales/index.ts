import { PLATFORM } from "aurelia-pal";
import { Router, RouterConfiguration } from "aurelia-router";

export class CatalogIndexViewModel {
    public router: Router;

    public configureRouter(configParam: RouterConfiguration, routerParam: Router) {
        configParam.map([
            { route: "", redirect: "pricing" },
            {
                route: "pricing",
                moduleId: PLATFORM.moduleName("./prices"),
                name: "pricing",
                title: "Manage Pricing",
                nav: true
            },
            {
                route: "promotions",
                moduleId: PLATFORM.moduleName("./promotions"),
                name: "promotions",
                title: "Manage Promotions",
                nav: true
            },
            {
                route: "discounts",
                moduleId: PLATFORM.moduleName("./discounts"),
                name: "discounts",
                title: "Manage Discounts",
                nav: true
            },
            {
                route: "taxes",
                moduleId: PLATFORM.moduleName("./taxes"),
                name: "taxes",
                title: "Manage Tax Rates",
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