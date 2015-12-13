var app = angular.module('profile', ['authSerivces', 'MemberServices', 'ngStorage']);

app.controller("ProfileController", ['$scope', '$window', '$location', '$localStorage', 'authSerivce', 'authSettings', 'MemberDetailServices', 'generalSettings',
    function ($scope, $window, $location, $localStorage, authService, authSettings, MemberDetailServices, generalSettings) {
        $scope.loggedInUser;
        $scope.registeredUser;
        $scope.isNewUser;
        $scope.datePicker = {
            status: {
                opened: false
            },
            maxDate: new Date()
        };
        $scope.datePicker.open = function ($event) {
            $scope.datePicker.status.opened = true;
        };
        

        function init() {
            var authdata = $localStorage.authorizationData;
            MemberDetailServices.getUserProfile(authdata.userName).then(function (data) {
                if (data) {
                    $scope.isNewUser = false;
                    $scope.registeredUser = data;
                } else {
                    $scope.isNewUser = true;
                    $scope.registeredUser = {};
                    $scope.registeredUser.country = 'LK';

                }
                $scope.registeredUser.userName = authdata.userName;
            });

        };

        var createNewUser  = function () {
            MemberDetailServices.createUserProfile($scope.registeredUser).then(function (data) {
                if (data) {

                } else {


                }
            }, function (error) { })
        }
        var updateUser = function () {
            MemberDetailServices.updateUserProfile($scope.registeredUser).then(function (data) {
                if (data) {

                } else {


                }
            }, function (error) { })
        }
        $scope.createNewUser = createNewUser;
        $scope.updateUser = updateUser;
        init();
    }]);