var GroceryApp = "groceryApp";

(function (groceryApp) {
    var app = angular.module(groceryApp, ["LocalStorageModule"]);

    //app.factory("authInterceptorService", AuthInterceptorService);
    //AuthInterceptorService.$inject = ["$q", "$location", "localStorageService", "$window"];

    //function AuthInterceptorService($q, $location, localStorageService, $window) {

    //    var AIS = {
    //        request: _request,
    //        responseError: _responseError
    //    };

    //    function _request(config) {
    //        config.headers = config.headers || {};
    //        var authData = localStorageService.get("authorizationData");
    //        if (authData) {
    //            config.headers.Token = authData.token;
    //        }

    //        return config;
    //    }

    //    function _responseError(rejection) {
    //        if (rejection.status === 401) {
    //            $window.location.href = "/Home";
    //        }
    //        return $q.reject(rejection);
    //    }

    //    return AIS;

    //}
    //app.config(function ($httpProvider) {
    //    $httpProvider.interceptors.push("authInterceptorService");
    //})

})(GroceryApp);