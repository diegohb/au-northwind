import { customElement, LogManager, autoinject, bindable } from "aurelia-framework";
import { Router } from "aurelia-router";
import { CategoryService } from "../../services/category-service";

@autoinject()
@customElement("main-nav")
export class MainNavVM {
    private readonly _logger = LogManager.getLogger(this.constructor.name);
    private readonly _categorySvc: CategoryService;
    private readonly _router: Router;

    public constructor(routerParam: Router, categorySvcParam: CategoryService) {
        this._router = routerParam;
        this._categorySvc = categorySvcParam;
    }

    @bindable()
    public routes: any;

    public categories: Array<string>;

    public async attached() {
        this.categories = await this._categorySvc.getCategoryNames();
    }

}