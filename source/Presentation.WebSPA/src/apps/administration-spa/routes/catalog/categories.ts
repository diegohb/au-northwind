import { observable } from "aurelia-framework";
import { CategoryModel } from "./models/category-model";

export class CategoriesVM {

    @observable()
    public activeIndex?: number = null;

    public categories: Array<CategoryModel> = [
        { id: 1, name: "Books", description: "Inventory of books.", productCount: 7 },
        { id: 2, name: "Food", description: "Inventory of foods.", productCount: 5 },
        { id: 3, name: "Medical", description: "Inventory of medical supplies.", productCount: 3 },
        { id: 4, name: "Music", description: "Inventory of music.", productCount: 9 }
    ];

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

        this.categories.splice(this.activeIndex, 1);
    }

    public async createCategory(nameInputParam: HTMLInputElement, descInputParam: HTMLInputElement) {
        if (this.categories.filter(cat => cat.name === nameInputParam.value).length > 0) {
            return;
        }

        const model: CategoryModel = new CategoryModel();
        model.name = nameInputParam.value;
        model.description = descInputParam.value;
        model.productCount = 0;
        this.categories.push(model);

        nameInputParam.value = "";
        descInputParam.value = "";
    }

}