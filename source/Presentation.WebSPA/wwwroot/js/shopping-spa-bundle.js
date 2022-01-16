define('apps/shopping-spa/app',["require", "exports", "aurelia-pal", "aurelia-framework"], function (require, exports, aurelia_pal_1, aurelia_framework_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.ShoppingSPAViewModel = void 0;
    var ShoppingSPAViewModel = (function () {
        function ShoppingSPAViewModel() {
            this._logger = aurelia_framework_1.LogManager.getLogger(this.constructor.name);
        }
        ShoppingSPAViewModel.prototype.configureRouter = function (pConfig, pRouter) {
            pConfig.title = "Shopping Site";
            pConfig.map([
                { route: "", moduleId: aurelia_pal_1.PLATFORM.moduleName("./no-selection"), title: "Select" },
                { route: "products/:id", moduleId: aurelia_pal_1.PLATFORM.moduleName("./product-detail"), name: "Product" }
            ]);
            this.router = pRouter;
        };
        return ShoppingSPAViewModel;
    }());
    exports.ShoppingSPAViewModel = ShoppingSPAViewModel;
});
;
define('apps/shopping-spa/app.css!text',[],function(){return "";});;
define('apps/shopping-spa/app.html!text',[],function(){return "<template><require from=\"./app.css\"></require><h1>Shopping</h1><router-view></router-view></template>";});;
define('apps/shopping-spa/no-selection',["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.NoSelectionViewModel = void 0;
    var NoSelectionViewModel = (function () {
        function NoSelectionViewModel() {
            this.message = "<<< select a product >>>";
        }
        return NoSelectionViewModel;
    }());
    exports.NoSelectionViewModel = NoSelectionViewModel;
});
;
define('apps/shopping-spa/no-selection.html!text',[],function(){return "<template><div class=\"row align-items-center h-100\"><div class=\"col-12 mx-auto\"><h2 class=\"text-center\">${message}</h2></div></div></template>";});;
define('apps/shopping-spa/product-detail',["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.ProductDetailViewModel = void 0;
    var ProductDetailViewModel = (function () {
        function ProductDetailViewModel() {
        }
        return ProductDetailViewModel;
    }());
    exports.ProductDetailViewModel = ProductDetailViewModel;
});
;
define('apps/shopping-spa/product-detail.html!text',[],function(){return "<template><h2>Product Details</h2></template>";});;
define('apps/shopping-spa/resources/index',["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.configure = void 0;
    function configure(config) {
    }
    exports.configure = configure;
});

//# sourceMappingURL=shopping-spa-bundle.js.map