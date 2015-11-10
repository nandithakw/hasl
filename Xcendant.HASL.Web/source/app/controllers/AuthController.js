var app = angular.module('auth', ['authSerivces', 'ngStorage']);

app.controller("authCtrl", ['$scope', '$window', '$location', 'authSerivce', 'authSettings', function ($scope, $window, $location, authService, authSettings) {
    $scope.google = "";
    $scope.faceBook = "";
    $scope.twitter = "";
    $scope.localLoginData = "";
    $scope.exteranlSignIn = authWithExteranlProvider;
    $scope.loginFailed = false;
    function authWithExteranlProvider(provider) {
        if (provider == "Google") {
            $window.location.href = $scope.google.url;
        } else if (provider == "Facebook") {
            $window.location.href = $scope.faceBook.url;
        }
    }
    $scope.localSignIn = localSignIn;
    function localSignIn(localLoginData) {
        authService.localLogin(localLoginData)
           .then(function (message) {
               // alert(message);
               $location.path("/home");
           }, function (error) {
               $scope.loginFailed = true;
           });
    }
    function init() {
        authService.logout();
        getExternalProvidersAuthSettings();
    };
    function getExternalProvidersAuthSettings() {
        var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';
        authService.getExteranlLoginProviders(redirectUri).then(function (data) {
            data.forEach(function (element, index, array) {
                element.url = authSettings.authApiServiceBaseUri + element.url;
                if ("Google" == element.name) {
                    $scope.google = element;

                } else if ("Facebook" == element.name) {
                    $scope.faceBook = element;

                }
            });
        }, function (error) {
            console.error(error);
        });
    };
    init();
}])