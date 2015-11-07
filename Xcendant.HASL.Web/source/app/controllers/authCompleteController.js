var app = angular.module('authComplete', ['authSerivces', 'ngStorage']);

app.controller("authCompleteCtrl", ['$scope', '$window', '$location', 'authSerivce', 'authSettings', function ($scope, $window, $location, authService, authSettings) {
    $scope.loginProviderDetails;
    $scope.userinfo;


    function init() {
        var token = getFragment();
        authService.getExternalUserInfo(token).then(function (data) {
            $scope.userinfo = data;
            if ($scope.userinfo.hasRegistered) {
                authService.storeToken(token);
                $window.location.href = '/home';
                return false;
            } else {
                return authService.registerNewExternalUser(token, $scope.userinfo);
            }
        })
        .then(function (data) {
            if (data) {
                return getExternalLoginProviderDtls();
            } else {
                return false;
            }

        }).then(function (data) {
            if (data) {
                authWithExteranlProvider();
            }
        }, function (error) {
            console.log(error);
        })
    };
    function getExternalLoginProviderDtls() {
        return $q(function (resolve, reject) {
            var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';
            authService.getExteranlLoginProviders(redirectUri)
            .then(function (data) {
                data.some(function (element, index, array) {
                    element.Url = authSettings.authApiServiceBaseUri + element.Url;
                    if ($scope.userinfo.loginProvider == element.name) {
                        $scope.loginProviderDetails = element;
                        return true;
                    } else {
                        return false;
                    }
                });

                if ($scope.loginProviderDetails) {
                    resolve($scope.loginProviderDetails);
                } else {
                    reject("unable to find the login provider details for the given user info");
                }
            },
            function (error) {
                console.error(error);
                reject(error);
            });
        });
    }

    function authWithExteranlProvider() {
        $window.location.href = $scope.loginProviderDetails.Url;
    }

    function getExternalProviderAuthSettings() {

    };

    function getFragment() {
        if (window.location.hash.indexOf("#") === 0) {
            return parseQueryString(window.location.hash.substr(1));
        } else {
            return {};
        }
    };

    function parseQueryString(queryString) {
        var data = {},
            pairs, pair, separatorIndex, escapedKey, escapedValue, key, value;

        if (queryString === null) {
            return data;
        }

        pairs = queryString.split("&");

        for (var i = 0; i < pairs.length; i++) {
            pair = pairs[i];
            separatorIndex = pair.indexOf("=");

            if (separatorIndex === -1) {
                escapedKey = pair;
                escapedValue = null;
            } else {
                escapedKey = pair.substr(0, separatorIndex);
                escapedValue = pair.substr(separatorIndex + 1);
            }

            key = decodeURIComponent(escapedKey);
            value = decodeURIComponent(escapedValue);

            data[key] = value;
        }

        return data;
    }


    init();
}]);