var app = angular.module('securedhome', ['authSerivces', 'MemberServices', 'ngStorage']);

app.controller("SecuredHomeCtrl", ['$scope', '$window', '$location', 'authSerivce', 'authSettings', 'MemberDetailServices', 'generalSettings',
    function ($scope, $window, $location, authService, authSettings, MemberDetailServices, generalSettings) {
        $scope.loggedInUser;
        $scope.registeredUser;
        function init() {
            authService.getLoggedInUserInfo().then(function (loggedInUser) {
                $scope.loggedInUser = loggedInUser;
                if ($scope.loggedInUser && $scope.loggedInUser.email) {
                    if ($scope.loggedInUser.pictureUrl === '#') {
                        $scope.loggedInUser.pictureUrl = 'img/user.png';
                    }
                    return MemberDetailServices.getUserProfile($scope.loggedInUser.email);
                } else {
                    console.error("unable to retive logged in userdata");
                    return false;
                }

            }).then(function (data) {
                if (data) {
                    $scope.registeredUser = data;
                    $scope.loggedInUser.registeredName = $scope.registeredUser.firstName + ' ' + $scope.registeredUser.lastName;
                } else {

                    $window.location.href = '/profile';
                    return false;
                }
            }, function (error) {
                console.log(error);
            });

        };
        init();
    }]);