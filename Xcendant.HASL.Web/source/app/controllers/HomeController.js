var app = angular.module('securedhome', ['authSerivces', 'MemberServices', 'ngStorage','toastr']);

app.controller("SecuredHomeCtrl", ['$scope', '$window', '$location','toastr', 'authSerivce', 'authSettings', 'MemberDetailServices', 'generalSettings',
    function ($scope, $window, $location,toastr, authService, authSettings, MemberDetailServices, generalSettings) {
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
                    console.error("unable to retrieve logged in user data");
                    toastr.error("Unable to retrieve logged in user data.Please try reloading page", "Unable to retrieve logged in user data")
                    return false;
                }

            }).then(function (data) {
                if (data) {
                    $scope.registeredUser = data;
                    $scope.loggedInUser.registeredName = $scope.registeredUser.firstName + ' ' + $scope.registeredUser.lastName;
                } else {
                    toastr.info("Please complete your profile", "Redirecting to profile page");
                    $window.location.href = '/profile';
                    return false;
                }
            }, function (error) {
                toastr.error("Unable to retrieve user profile.Please try reloading page","Unable to retrieve user profile data")
                console.log(error);
            });

        };
        init();
    }]);