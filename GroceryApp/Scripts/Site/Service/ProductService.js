(function () {
    'use strict';
    angular.module(GroceryApp).factory('productService', ProductService);
    ProductService.$inject = ["$http", "$q"];

    function ProductService($http, $q) {
        console.log('ProductService has loaded.');
        var srv = {
            InsertProduct: _insertProduct,
            ProductUpdate: _productUpdate,
            GetProductByKeyword: _getProductByKeyword,
            DeleteProductById: _deleteProductById,
            GetByWebScrape: _getByWebScrape

        };

        return srv;

        function _insertProduct(model) {
            console.log('insertProduct has fired in the service, here is the model', model);
            return $http.post("/api/Product", model)
                .then(function (response) {
                    console.log(response);
                    return response;
                })
                .catch(function (err) {
                    console.log('This is the err status', err);
                    return err;
                });
        }

        function _productUpdate(model) {
            console.log('_productUpdate has fired in the service, here is the model', model);
            return $http.put("/api/product/" + model.Id, model)
                .then(function (response) {
                    console.log(resposne);
                    return response;
                })
                .catch(function (err) {
                    console.log('This is the err status', err);
                    return err;
                });
        }

        function _deleteProductById(id) {
            console.log('_delete function has fired in the service', id)
            return $http.delete("/api/product/" + id)
                .then(function (response) {
                    console.log('response from delete button', response);
                    return response;
                })
                .catch(function (err) {
                    console.log('this is the err', err);
                    return err;
                });
        }

        function _getProductByKeyword(keyword) {
            console.log('_getProductByKeyword fired, here is the keyword', keyword);
            return $http.get("/api/product/" + keyword)
                .then(function (response) {
                    console.log('response from the getbykeyword function', response);
                    return response;
                })
                .catch(function (err) {
                    console.log('this is the error from getbyKeyword', err);
                    return err;
                });
        }

        function _getByWebScrape() {
            console.log('GetByWebscrape has fired');
            return $http.get("/api/product/scrape")
                .then(function (response) {
                    console.log('response from the scrape function', response);
                    return response;
                })
                .catch(function (err) {
                    console.log('this is the error from the scrape function', err);
                    return err;
                });
        }
        
    }

})();