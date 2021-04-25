import { AdminSPAViewModel } from "../../src/apps/administration-spa/app";
import { ShoppingSPAViewModel } from "../../src/apps/shopping-spa/app";

describe("admin spa",
    () => {
        it("says hello",
            () => {
                expect(new AdminSPAViewModel().router).toBeDefined();
            });
    });

describe("the app two",
    () => {
        it("says hello",
            () => {
                expect(new ShoppingSPAViewModel().router).toBeDefined();
            });
    });