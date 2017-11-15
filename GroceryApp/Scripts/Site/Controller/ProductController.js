(function () {
    'use strict';
    angular.module(GroceryApp).controller('productController', ProductController);
    ProductController.$inject = ["$scope", "productService", "$sce"];
    console.log("IFFE has fired for the the product controller");

    function ProductController($scope, productService, $sce) {
        console.log("ProductController has loaded");
        var vm = this;
        vm.model = {};
        vm.keyword = "";
        /*
        example model = {
        ProductName: '',
        Size: '',
        Price: '',
        StoreId: '',
        Brand: ''
        }
        */
        vm.productArray = [];
        vm.scrapeArray = [];
        vm.insertProduct = _insertProduct;
        vm.productUpdate = _productUpdate;
        vm.deleteProduct = _deleteProduct;
        vm.getProductByKeyword = _getByProductKeyword;
        vm.getByWebScrape = _getByWebScrape;

        function _insertProduct(model) {
            console.log('_insertProduct has been clicked, here is vm.model', model);
            productService.InsertProduct(model)
                .then(function (data) {
                    _getByProductKeyword(model.productName);
                })
                .catch(function (err) {
                    console.log(err);

                });
        }


        function _productUpdate(model) {
            console.log('_productUpdate has been clicked, here is vm.model', model);
            productService.ProductUpdate(model)
                .then(function (data) {
                    console.log('this is the data that is being passed back from update', data);
                    _getByProductKeyword(model.productName);
                })
                .catch(function (err) {
                    console.log('this is the error associate with updating',err);

                });
        }

        function _deleteProduct(id) {
            console.log('this is coming from the deleteProduct function');
            productService.DeleteProductById(id)
                .then(function (data) {
                    console.log('This is the data coming back from the delete', data);
                    _getByProductKeyword(vm.model.productName);
                })
                .catch(function (err) {
                    console.log('This is the error associated with the delete', err);
                });
        }           

        function _getByProductKeyword(keyword) {
            console.log('This is coming from the getbykeyword function in the controller', keyword);
            productService.GetProductByKeyword(keyword)
                .then(function (data) {
                    console.log('This is the data coming back from keyword', data);
                    vm.productArray = data.data;
                    console.log(vm.productArray);
                })
                .catch(function (err) {
                    console.log('this is the error coming from getbyproductkeyword', err);
                });
        }

        function _getByWebScrape() {
            console.log('this is coming from the webscrape function in the product controller.js');
            productService.GetByWebScrape()
                .then(function (data) {
                    console.log('this is coming from the getbywebscrape function in the pcontroller.js', data);
                    for (var i = 0; i < data.data.length; i++) {
                  
                        vm.scrapeArray[i] = $sce.trustAsHtml(data.data[i]);
                        console.log(vm.scrapeArray);
                    }

                })
                .catch(function (err) {
                    console.log('This is the error coming back from getbywebscrape in the pcontroller.js', err);
                });
        }
    }
})();