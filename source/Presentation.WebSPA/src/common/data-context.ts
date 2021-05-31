import { LogManager } from "aurelia-framework";
import { IDataBase, DATA_TYPE, ITable, Connection } from "jsstore";
import { CatalogCategoryEntity, CatalogProductEntity } from "./db-model";
import * as environment from "../environment";
import { createGuid } from "./utils";

const logger = LogManager.getLogger("data-context");
const isDebug = (environment as any).debug === false;
// This will ensure that we are using only one instance. 
// Otherwise due to multiple instance multiple worker will be created.
let worker = new Worker(`/js/jsstore.worker${isDebug ? "" : ".min"}.js`); //await getWorkerPath();
export const catalogConn = new Connection(worker);
const catalogDbName = "NorthwindDB";

function getCatalogDb() {
    const tblProducts: ITable = {
        name: "Products",
        columns: {
            sku: { primaryKey: true, notNull: true, dataType: DATA_TYPE.String, unique: true },
            productName: { notNull: true, dataType: DATA_TYPE.String },
            description: { dataType: DATA_TYPE.String },
            categoryId: { notNull: true, dataType: DATA_TYPE.Number }
        }
    };
    const tblCategories: ITable = {
        name: "Categories",
        columns: {
            id: {
                primaryKey: true,
                dataType: DATA_TYPE.Number,
                autoIncrement: true
            },
            name: { notNull: true, dataType: DATA_TYPE.String, unique: true },
            description: { dataType: DATA_TYPE.String }
        }
    };

    const dataBase: IDataBase = {
        name: catalogDbName,
        tables: [tblProducts, tblCategories],
        version: 1
    };
    return dataBase;
};

export async function initDatabase(): Promise<void> {
    let isDbCreated: boolean;
    const dataBase = getCatalogDb();
    try {
        isDbCreated = await catalogConn.initDb(dataBase);
    } catch (errInit) {
        logger.error("Failed to create and initialize the local database instance.", errInit);
    }

    if (isDbCreated) {
        try {
            logger.info("Empty database created.");
            let insertedCount = await catalogConn.insert({
                into: "Categories",
                values: getCategories()
            });
            console.assert(insertedCount === getCategories().length);
            insertedCount = await catalogConn.insert({
                into: "Products",
                values: getProducts()
            });
            console.assert(insertedCount === getProducts().length);
            logger.info("Seeded empty database.");
        } catch (errSeeding) {
            logger.error("Unable to seed initial data.", errSeeding);
        }
    } else {
        logger.info("Database already exists.");
    }
}

function getCategories(): Array<CatalogCategoryEntity> {
    return [
        { name: "Books", description: "Reading material" },
        { name: "Food" },
        { name: "Medical" },
        { name: "Music" }
    ] as Array<CatalogCategoryEntity>;
}

function getProducts(): Array<CatalogProductEntity> {
    return [
        {
            sku: createGuid(),
            productName: "Brave New World",
            description: "A book by Aldous Huxley",
            categoryId: 1
        },
        {
            sku: createGuid(),
            productName: "We Could Be Heroes",
            description: "A book by Margaret Finnegan",
            categoryId: 1
        },
        { sku: createGuid(), productName: "Coffret Maison Dark (40pc)", categoryId: 2 },
        { sku: createGuid(), productName: "Ferrero Rocher Hazelnut Milk Chocolate Box (24pc)", categoryId: 2 }
    ] as Array<CatalogProductEntity>;
}