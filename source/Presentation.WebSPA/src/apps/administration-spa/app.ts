import { PLATFORM } from "aurelia-pal";
import { Logger } from "aurelia-logging";
import { Router, RouterConfiguration } from "aurelia-router";
import { LogManager } from "aurelia-framework"

export class ProductManagementSPAViewModel {
    private readonly _logger: Logger = LogManager.getLogger(this.constructor.name);

    public router: Router;

    public configureRouter(configParam: RouterConfiguration, routerParam: Router) {
        configParam.title = "Product Management";
        //pConfig.options.pushState = true;
        configParam.map([
            { route: "", redirect: "catalog" },
            {
                route: "catalog",
                moduleId: PLATFORM.moduleName("./routes/catalog/index"),
                name: "catalog",
                title: "Catalog",
                nav: true
            },
            {
                route: "sales",
                moduleId: PLATFORM.moduleName("./routes/sales/index"),
                name: "sales",
                title: "Sales",
                nav: true
            },
            {
                route: "purchasing",
                moduleId: PLATFORM.moduleName("./routes/purchasing/index"),
                name: "purchasing",
                title: "Purchasing",
                nav: true
            },
            {
                route: "warehouse",
                moduleId: PLATFORM.moduleName("./routes/warehouse/index"),
                name: "warehouse",
                title: "Warehouse",
                nav: true
            }
        ]);

        configParam.mapUnknownRoutes("./routes/not-found.html");

        this.router = routerParam;
    }
}