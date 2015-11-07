var app = angular.module('auth', ['authSerivces', 'ngStorage']);

app.controller("authCtrl", ['$scope', '$window', '$location', 'authSerivce', 'authSettings', function ($scope, $window, $location, authService, authSettings) {
    $scope.google = "";
    $scope.faceBook = "";
    $scope.twitter = "";
    $scope.localLoginData = "";
    $scope.exteranlSignIn = authWithExteranlProvider;

    function authWithExteranlProvider(provider) {
        if (provider == "Google") {
            $window.location.href = $scope.google.url;
        }
    }
    $scope.localSignIn = localSignIn;
    function localSignIn(localLoginData) {
        authService.localLogin(localLoginData)
           .then(function (message) {
               // alert(message);
               $location.path("/home");
           }, function (error) {

           });
    }
    function init() {
        getExternalProvidersAuthSettings();
    };
    function getExternalProvidersAuthSettings() {
        var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';
        authService.getExteranlLoginProviders(redirectUri).then(function (data) {
            data.forEach(function (element, index, array) {
                element.url = authSettings.authApiServiceBaseUri + element.url;
                if ("Google" == element.name) {
                    $scope.google = element;

                }
            });
        }, function (error) {
            console.error(error);
        });
    };
    init();
}])