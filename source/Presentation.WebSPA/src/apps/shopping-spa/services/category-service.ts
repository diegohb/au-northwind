import { autoinject, LogManager } from "aurelia-framework";
import { HttpClient } from "aurelia-fetch-client";
import { ApiLoggerInterceptor } from "../../../common/api-logger-interceptor";
import { CategoryDTO } from "../models/category-dto";

@autoinject()
export class CategoryService {
    private readonly _http: HttpClient;
    private readonly _logger = LogManager.getLogger(this.constructor.name);

    constructor(httpClientParam: HttpClient) {
        this._http = httpClientParam;
        this._http.configure(config => {
            config
                .useStandardConfiguration()
                .withBaseUrl("/api/")
                .withInterceptor(new ApiLoggerInterceptor());
        });
    }

    public async getCategoryNames(): Promise<Array<string>> {
        const category = await this.fetchCategories();
        const models = category.map(dto => dto.categoryName);
        return models;
    }

    private async fetchCategories(): Promise<CategoryDTO[]> {
        const rawResponse = await this._http.fetch("categories");
        const objects: Array<any> = await rawResponse.json();
        this._logger.debug(`Fetched ${objects.length} categories.`, objects);
        return objects;
    }
}