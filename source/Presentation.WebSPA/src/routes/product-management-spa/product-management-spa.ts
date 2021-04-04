﻿import { PLATFORM } from "aurelia-pal";
import { Router, RouterConfiguration, NavigationInstruction } from "aurelia-router";
import { LogManager } from "aurelia-framework"

export class ProductManagementSPAViewModel {
    private readonly _logger = LogManager.getLogger(this.constructor.name);

    public router: Router;

    public configureRouter(configParam: RouterConfiguration, routerParam: Router) {
        this.router = routerParam;

        configParam.title = "Product Management";
        //pConfig.options.pushState = true;
        configParam.map([
            { route: "", redirect: "catalog" },
            { route: "catalog", moduleId: PLATFORM.moduleName("./catalog"), name: "catalog", title: "Catalog", nav: true },
            { route: "sales", moduleId: PLATFORM.moduleName("./sales"), name: "sales", title: "Sales", nav: true },
            { route: "fulfillment", moduleId: PLATFORM.moduleName("./fulfillment"), name: "fulfillment", title: "Fulfillment", nav: true },
            { route: "warehouse", moduleId: PLATFORM.moduleName("./warehouse"), name: "warehouse", title: "Warehouse", nav: true },
            { route: "products/:id", moduleId: PLATFORM.moduleName("./product-summary/index"), name: "product", title: "Product", nav: false }
        ]);

    }
}