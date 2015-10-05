'use strict'
var app = angular.module('authSerivces', ['ngStorage']);
app.factory('authSerivce', ['$http', '$q', '$localStorage', 'authSettings', function ($http, $q, $localStorage, authSettings) {
    var authServiceFactory = {};
    var getExteranlLoginProviders = function (returnUrl) {
        return $q(function (resolve, reject) {
            var externalProviders = [];
            $http.get(authSettings.authApiServiceBaseUri + "/api/account/externalLogins?returnUrl=" + returnUrl + "/&generateState=true").then(function (response) {
                externalProviders = response.data;
                Array.isArray(externalProviders) || (reject("External providers list is not valid"))
                resolve(externalProviders);
            }, function (error) {
                reject(error);
            });
        });


    };

    var registerNewLocalUser = function (user) {
        return $q(function (resolve, reject) {
            var message;
            $http.post(authSettings.authApiServiceBaseUri + "/api/account/register", user).then(function (response) {
                message = response.data;
                resolve(message);
            }, function (error) {
                reject(error);
            });
        });
    };
    var localLogin = function (loginData) {
        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
        return $q(function (resolve, reject) {

            $http.post(authSettings.authApiServiceBaseUri + "/token", data).then(function (response) {
                $localStorage.authorizationData = { token: response.data.access_token, userName: response.data.user_name };
                resolve("you are successfully logged in.");
            }, function (error) {
                reject(error);

            });
        });
    };
    authServiceFactory.localLogin = localLogin;
    authServiceFactory.registerNewLocalUser = registerNewLocalUser;
    authServiceFactory.getExteranlLoginProviders = getExteranlLoginProviders;
    return authServiceFactory;
}]);
