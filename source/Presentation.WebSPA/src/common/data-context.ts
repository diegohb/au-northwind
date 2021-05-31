import { IDataBase, DATA_TYPE, ITable, Connection } from "jsstore";
import workerInjector from "jsstore/dist/worker_injector";
import { CatalogCategoryEntity, CatalogProductEntity } from "./db-model";
import * as environment from "../environment";
import { createGuid } from "./utils";

declare var require: any;

const getWorkerPath = () => {
    if ((environment as any).debug === false) {
        return require("/jsstore.worker.min.js");
    } else {
        return require("/jsstore.worker.min.js");
    }
};

// This will ensure that we are using only one instance. 
// Otherwise due to multiple instance multiple worker will be created.
export const idbCon = new Connection();
idbCon.addPlugin(workerInjector);
export const dbname = "NorthwindDB";


const getDatabase = () => {
    const tblProducts: ITable = {
        name: "Catalog.Products",
        columns: {
            sku: { primaryKey: true, notNull: true, dataType: DATA_TYPE.String, unique: true },
            productName: { notNull: true, dataType: DATA_TYPE.String },
            description: { dataType: DATA_TYPE.String },
            categoryId: { notNull: true, dataType: DATA_TYPE.Number }
        }
    };
    const tblCategories: ITable = {
        name: "Catalog.Categories",
        columns: {
            id: {
                primaryKey: true,
                dataType: DATA_TYPE.String,
                unique: true,
                notNull: true
            },
            name: { notNull: true, dataType: DATA_TYPE.String, unique: true },
            description: { dataType: DATA_TYPE.String }
        }
    };

    const dataBase: IDataBase = {
        name: dbname,
        tables: [tblProducts, tblCategories]
    };
    return dataBase;
};

export async function initDatabase(): Promise<void> {
    const dataBase = getDatabase();
    const isDbCreated = await idbCon.initDb(dataBase);
    if (isDbCreated) {
        let insertedCount = await idbCon.insert({
            into: "Catalog.Categories",
            values: getCategories()
        });
        console.assert(insertedCount === getCategories().length);
        insertedCount = await idbCon.insert({
            into: "Catalog.Products",
            values: getProducts()
        });
        console.assert(insertedCount === getProducts().length);
    }

    function getCategories(): Array<CatalogCategoryEntity> {
        return [
            { name: "Books" },
            { name: "Food" },
            { name: "Medical" },
            { name: "Music" }
        ] as Array<CatalogCategoryEntity>;
    }

    function getProducts(): Array<CatalogProductEntity> {
        return [
            { sku: createGuid(), productName: "Brave New World", categoryId: 1 },
            { sku: createGuid(), productName: "We Could Be Heroes", categoryId: 1 },
            { sku: createGuid(), productName: "Coffret Maison Dark (40pc)", categoryId: 2 },
            { sku: createGuid(), productName: "Ferrero Rocher Hazelnut Milk Chocolate Box (24pc)", categoryId: 2 }
        ] as Array<CatalogProductEntity>;
    }
}