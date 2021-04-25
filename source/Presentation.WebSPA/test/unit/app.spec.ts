import { AdminSPAViewModel } from "../../src/apps/administration-spa/app";
import { ShoppingSPAViewModel } from "../../src/apps/shopping-spa/app";
import { RouterConfiguration } from "aurelia-router";

describe("admin spa",
    () => {
        let spa: AdminSPAViewModel;
        beforeEach(() => {
            spa = new AdminSPAViewModel();
        });
        it("router configures",
            async () => {
                await spa.configureRouter(new RouterConfiguration(), null);
                expect(spa.router).toBeDefined();
            });
    });

describe("shopping spa",
    () => {
        let spa: ShoppingSPAViewModel;
        beforeEach(() => {
            spa = new ShoppingSPAViewModel();
        });

        it("router configures",
            async () => {
                await spa.configureRouter(new RouterConfiguration(), null);
                expect(spa.router).toBeDefined();
            });
    });