import { ProductSummaryModel } from "../models/product-summary";
import { ProductDTO } from "../models/product-dto";

export class ProductService {

    public async getShoppableProductSummaries(): Promise<Array<ProductSummaryModel>> {
        const products = await this.fetchProducts();
        const models = products.map(dto => ProductSummaryModel.fromDTO(dto));
        return models;
    }

    private async fetchProducts(): Promise<ProductDTO[]> {
        return [];
    }
}