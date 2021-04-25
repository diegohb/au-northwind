import { PLATFORM } from "aurelia-pal";
import { Router, RouterConfiguration } from "aurelia-router";

export class CatalogIndexViewModel {
    public router: Router;

    public configureRouter(configParam: RouterConfiguration, routerParam: Router) {
        configParam.map([
            { route: "", redirect: "products" },
            {
                route: "products",
                moduleId: PLATFORM.moduleName("./catalog"),
                name: "catalog",
                title: "View Products",
                nav: true
            },
            {
                route: "categories",
                moduleId: PLATFORM.moduleName("./categories"),
                name: "categories",
                title: "Manage Categories",
                nav: true
            },
            {
                route: "products/:sku",
                href: "#products/:sku",
                moduleId: PLATFORM.moduleName("./product-summary"),
                name: "product",
                title: "Product",
                nav: false
            },
            {
                route: "products/not-found",
                name: "product-not-found",
                moduleId: PLATFORM.moduleName("./no-selection"),
                title: "Product Not Found",
                nav: false
            }
        ]);

        this.router = routerParam;
    }
}