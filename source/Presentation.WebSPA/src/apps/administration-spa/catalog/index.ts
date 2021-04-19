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
                route: "products/:id",
                href: "#products/:id",
                moduleId: PLATFORM.moduleName("./product-summary"),
                name: "product",
                title: "Product",
                nav: false
            }
        ]);

        this.router = routerParam;
    }
}