var app = angular.module('securedhome', ['authSerivces', 'ngStorage']);

app.controller("SecuredHomeCtrl", ['$scope', '$window', '$location', 'authSerivce', 'authSettings', function ($scope, $window, $location, authService, authSettings) {
    $scope.loggedInUser;
    function init() {
        authService.getLoggedInUserInfo().then(function (loggedInUser) {
            $scope.loggedInUser = loggedInUser;
            if ($scope.loggedInUser.pictureUrl === '#') {
                $scope.loggedInUser.pictureUrl = 'img/user.png';
            }
        }, function (error) {

        });
    };
    init();
}]);