import { observable, inject } from "aurelia-framework";
import { CategoryModel } from "./models/category-model";
import * as toastr from "toastr";
import { ICatalogService, CatalogSvc } from "./services/catalog-svc";

@inject(CatalogSvc)
export class CategoriesVM {
    private readonly _svc: ICatalogService;

    constructor(svcParam: ICatalogService) {
        this._svc = svcParam;
    }

    @observable()
    public activeIndex?: number = null;

    public categories: Array<CategoryModel> = [];

    public async activate(): Promise<void> {
        this.categories = await this._svc.getCategories();
    }

    public async selectItem(itemIndexParam: number): Promise<void> {
        if (this.activeIndex === itemIndexParam) {
            this.activeIndex = null;
        } else {
            this.activeIndex = itemIndexParam;
        }
    }

    public async delete(): Promise<void> {
        if (this.activeIndex === null)
            return;

        if (this.categories[this.activeIndex].productCount !== 0)
            return;
        const categoryName = this.categories[this.activeIndex].name;

        this.categories.splice(this.activeIndex, 1);
        toastr.info(`Deleting category '${categoryName}'.`, "Action Queued");
    }

    public async createCategory(nameInputParam: HTMLInputElement, descInputParam: HTMLInputElement) {
        if (this.categories.filter(cat => cat.name === nameInputParam.value).length > 0) {
            return;
        }

        const model = new CategoryModel();
        model.name = nameInputParam.value;
        model.description = descInputParam.value;
        model.productCount = 0;
        this.categories.push(model);

        toastr.info(`Creating category '${model.name}'.`, "Action Queued");

        nameInputParam.value = "";
        descInputParam.value = "";
    }

}