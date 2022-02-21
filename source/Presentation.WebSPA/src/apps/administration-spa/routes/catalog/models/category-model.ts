import { CategoryDTO } from "../../../../../models/category-dto";

export class CategoryModel {
    private readonly _id: number;

    constructor(idParam?: number) {
        if (idParam != null) {
            this._id = idParam;
        }
    }

    public get id(): number { return this._id; }

    public name: string;
    public description: string;
    public picture: string;
    public productCount = 0;

    public static fromDTO(dtoParam: CategoryDTO) {
        const model = new CategoryModel(dtoParam.categoryId);
        model.name = dtoParam.categoryName;
        model.description = dtoParam.description;
        model.picture = dtoParam.picture;
        return model;
    }
}