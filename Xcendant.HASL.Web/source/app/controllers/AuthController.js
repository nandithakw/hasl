var app = angular.module('auth', ['authSerivces', 'ngStorage', 'ngProgress', 'toastr']);

app.controller("AuthController", ['$scope', '$window', '$location', '$q', 'ngProgressFactory', 'toastr', 'authSerivce', 'authSettings',
    function ($scope, $window, $location, $q, ngProgressFactory, toastr, authService, authSettings) {

        $scope.progressbar = ngProgressFactory.createInstance();
        $scope.progressbar.start();
        $scope.isBusy = true;
        $scope.google = "";
        $scope.faceBook = "";
        $scope.twitter = "";
        $scope.localLoginData = "";
        $scope.exteranlSignIn = authWithExteranlProvider;
        $scope.loginFailed = false;
        $scope.localSignIn = localSignIn;


        function init() {
            authService.logout();
            getExternalProvidersAuthSettings().then(function (message) {
                $scope.progressbar.complete();
                $scope.isBusy = false;
            }, function (error) {
                $scope.progressbar.stop();
            });;
        };

        function authWithExteranlProvider(provider) {
            if (!$scope.isBusy) {

                if (provider == "Google") {
                    $window.location.href = $scope.google.url;
                } else if (provider == "Facebook") {
                    $window.location.href = $scope.faceBook.url;
                }
            } else {
                toastr.info("We are still loading external login providers","Please wait.");
            }

        }
        function localSignIn(localLoginData) {
            authService.localLogin(localLoginData)
               .then(function (message) {
                   // alert(message);
                   $location.path("/home");
               }, function (error) {
                   $scope.loginFailed = true;
               });
        }
        function getExternalProvidersAuthSettings() {
            var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';
            return $q(function (resolve, reject) {
                authService.getExteranlLoginProviders(redirectUri).then(function (data) {
                    data.forEach(function (element, index, array) {
                        element.url = authSettings.authApiServiceBaseUri + element.url;
                        if ("Google" == element.name) {
                            $scope.google = element;
                        } else if ("Facebook" == element.name) {
                            $scope.faceBook = element;

                        }
                        resolve();

                    });
                }, function (error) {
                    console.error(error);
                    toastr.error("unable to retrieve external login details.");
                    reject();
                });

            });
        };
        init();
    }])