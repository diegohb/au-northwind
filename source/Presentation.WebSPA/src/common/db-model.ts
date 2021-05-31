export class CatalogProductEntity {
    public sku = "";
    public productName = "";
    public description?: string = null;
    public categoryId = 0;
}

export class CatalogCategoryEntity {
    public id = 0;
    public name = "";
    public description?: string = null;
}