import { PLATFORM } from "aurelia-pal";
import { Router, RouterConfiguration } from "aurelia-router";

export class PurchasingIndexViewModel {
    public router: Router;

    public configureRouter(configParam: RouterConfiguration, routerParam: Router) {
        configParam.map([
            { route: "", redirect: "inventory" },
            {
                route: "inventory",
                moduleId: PLATFORM.moduleName("./inventory"),
                name: "inventory",
                title: "Inventory Listing",
                nav: true
            },
            {
                route: "orders",
                moduleId: PLATFORM.moduleName("./manage-orders"),
                name: "manage-orders",
                title: "Manage Orders",
                nav: true
            },
            {
                route: "orders/place",
                moduleId: PLATFORM.moduleName("./place-order"),
                name: "place-order",
                title: "Place Order",
                nav: true
            },
            {
                route: "purchasing/:productId/:changeType",
                moduleId: PLATFORM.moduleName("./cost-change"),
                name: "inventory-cost",
                title: "Inventory Cost",
                nav: false
            }
        ]);
        this.router = routerParam;
    }
}