var app = angular.module('localSingup', ['authSerivces', 'ngStorage']);

app.controller("localSingupCtrl", ['$scope', '$window', '$location', 'authSerivce', 'authSettings', function ($scope, $window, $location, authService, authSettings) {
    $scope.user = {};
    $scope.registerLocalUser = registerLocalUser;
    function registerLocalUser(user) {
        authService.registerNewLocalUser(user)
            .then(function (message) {
               // alert(message);
                $location.path("/home");
            }, function (error) {

            });
    };
}]);