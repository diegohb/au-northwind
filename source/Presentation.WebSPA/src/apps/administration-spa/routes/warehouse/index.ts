import { PLATFORM } from "aurelia-pal";
import { Router, RouterConfiguration } from "aurelia-router";

export class WarehouseIndexViewModel {
    public router: Router;

    public configureRouter(configParam: RouterConfiguration, routerParam: Router) {
        configParam.map([
            { route: "", redirect: "inventory" },
            {
                route: "inventory",
                moduleId: PLATFORM.moduleName("./inventory"),
                name: "inventory",
                title: "Fulfillment",
                nav: true
            },
            {
                route: "shipments",
                moduleId: PLATFORM.moduleName("./manage-shipments"),
                name: "manage-shipments",
                title: "Manage Shipments",
                nav: true
            },
            {
                route: "shipments/send",
                moduleId: PLATFORM.moduleName("./send-shipment"),
                name: "send-shipment",
                title: "Ship Product",
                nav: true
            },
            {
                route: "shipments/receieve",
                moduleId: PLATFORM.moduleName("./recieve-shipment"),
                name: "recieve-shipment",
                title: "Receive Product",
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