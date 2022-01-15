export class CategoryModel {
    constructor(idParam?: number) {
        if (idParam != null) {
            this.id = idParam;
        }
    }

    public id: number;
    public name: string;
    public description: string;
    public productCount = 0;
}