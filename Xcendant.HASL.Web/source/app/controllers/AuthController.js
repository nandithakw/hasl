var app = angular.module('auth', ['authSerivces', 'ngStorage']);

app.controller("authCtrl", ['$scope', '$window', '$location', 'authSerivce', 'authSettings', function ($scope,$window, $location, authService, authSettings) {
    $scope.google = "";
    $scope.faceBook = "";
    $scope.twitter = "";
    $scope.localLoginData = "";
    $scope.authWithExteranlProvider = function (provider) {
        if (provider == "Google") {
            $window.location.href =$scope.google.Url;
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
        var redirectUri =  location.protocol + '//' + location.host+'/authcomplete.html';
        authService.getExteranlLoginProviders(redirectUri).then(function (data) {
            data.forEach(function (element, index, array) {
                element.Url = authSettings.authApiServiceBaseUri + element.Url;
                if ("Google" == element.Name) {
                    $scope.google = element;

                }
            });
        }, function (error) {
            console.error(error);
        });
    };
    init();
}])