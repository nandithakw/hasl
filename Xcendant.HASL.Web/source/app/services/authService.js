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
                reject(error.data);
            });
        });


    };
    var getLoggedInUserInfo = function () {
        return $q(function (resolve, reject) {
            var userInfo;
            $http.get(authSettings.authApiServiceBaseUri + "/api/user/loggeduserinfo").then(function (response) {
                userInfo = response.data;
                if (userInfo) {
                    resolve(userInfo);
                } else {
                    reject("unable to find the logged in use data");
                }
            }, function (error) {
                reject("unable to find the logged in use data");
            });
        });
    }
    var registerNewLocalUser = function (user) {
        return $q(function (resolve, reject) {
            var message;
            $http.post(authSettings.authApiServiceBaseUri + "/api/account/register", user).then(function (response) {
                message = response.data;
                resolve(message);
            }, function (error) {
                reject(error.data);
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
                reject(error.data);

            });
        });
    };
    var storeToken = function (token) {
        $localStorage.authorizationData = { token: token.access_token, userName: token.user_name };
    };

    var getExternalUserInfo = function (externalAccessToken) {
        return $q(function (resolve, reject) {
            var userinfo;
            $http.get(authSettings.authApiServiceBaseUri + "/api/account/userinfo",
                { headers: { 'Authorization': 'Bearer ' + externalAccessToken.access_token } })
                .then(function (response) {
                    userinfo = response.data;
                    resolve(userinfo);
                }, function (error) {
                    reject(error.data);
                });
        });

    }
    var registerNewExternalUser = function (externalAccessToken, userinfo) {
        return $q(function (resolve, reject) {
            if (userinfo.hasRegistered) {
                reject(userinfo.email + " is already regsitered with the system");
            } else {
                $http.post(authSettings.authApiServiceBaseUri + "/api/account/registerexternal",
                      { email: userinfo.email },
                      { headers: { 'Authorization': 'Bearer ' + externalAccessToken.access_token } })
               .then(function (response) {
                   resolve(response.data);
               }, function (error) {
                   reject(error.data);
               });
            }

        });


    };
    var logout = function () {
        $localStorage.$reset();
    };

    authServiceFactory.logout = logout;
    authServiceFactory.storeToken = storeToken;
    authServiceFactory.getLoggedInUserInfo = getLoggedInUserInfo;
    authServiceFactory.getExternalUserInfo = getExternalUserInfo;
    authServiceFactory.registerNewExternalUser = registerNewExternalUser;
    authServiceFactory.localLogin = localLogin;
    authServiceFactory.registerNewLocalUser = registerNewLocalUser;
    authServiceFactory.getExteranlLoginProviders = getExteranlLoginProviders;
    return authServiceFactory;
}]);
