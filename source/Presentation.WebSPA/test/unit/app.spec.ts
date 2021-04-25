import { AdminSPAViewModel } from "../../src/apps/administration-spa/app";
import { ShoppingSPAViewModel } from "../../src/apps/shopping-spa/app";

describe("admin spa",
    () => {
        it("router configures",
            () => {
                const spa: AdminSPAViewModel = new AdminSPAViewModel();
                expect(spa.router).toBeDefined();
            });
    });

describe("shopping spa",
    () => {
        it("router configures",
            async () => {
                const spa: ShoppingSPAViewModel = new ShoppingSPAViewModel();
                //await spa.configureRouter(null, new Router());
                expect(spa.router).toBeDefined();
            });
    });