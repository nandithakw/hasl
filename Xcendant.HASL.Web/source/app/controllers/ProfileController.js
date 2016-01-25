var app = angular.module('profile', ['authSerivces', 'MemberServices', 'ngStorage', 'ngMessages', 'toastr']);

app.controller("ProfileController", ['$scope', '$window', '$location', '$localStorage', 'uibDateParser', 'toastr', 'authSerivce', 'authSettings', 'MemberDetailServices', 'generalSettings',
    function ($scope, $window, $location, $localStorage, uibDateParser,toastr, authService, authSettings, MemberDetailServices, generalSettings) {
        $scope.loggedInUser;
        $scope.registeredUser;
        $scope.isNewUser;
        $scope.format = generalSettings.dateFormat;
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
            if (authdata) {
                MemberDetailServices.getUserProfile(authdata.userName).then(function (data) {
                    if (data) {
                        $scope.isNewUser = false;
                        $scope.registeredUser = data;
                        $scope.registeredUser.dateOfBirth = uibDateParser.parse($scope.registeredUser.dateOfBirth, generalSettings.dateFormat);

                    } else {
                        $scope.isNewUser = true;
                        $scope.registeredUser = {};
                        $scope.registeredUser.country = 'LK';

                    }
                    $scope.registeredUser.userName = authdata.userName;
                });
            } else {
                //user doesn't have authorization data something bad has happened :(
                toastr.error("you are not logged in.", 'Redirecting to Login Page');
                $window.location.href = "/login";
            }


        };

        var createNewUser = function () {
            MemberDetailServices.createUserProfile($scope.registeredUser).then(function (data) {
                if (data) {
                    toastr.success(data, "operation succeeded");
                } else {
                    toastr.info(data, "operation succeeded");

                }
            }, function (error) {

                toastr.error("unable to save new user profile",'Operation failed')
            })
        }
        var updateUser = function () {
            MemberDetailServices.updateUserProfile($scope.registeredUser).then(function (data) {
                if (data) {
                    toastr.success(data, "operation succeeded");

                } else {
                    toastr.info(data, "operation succeeded");


                }
            }, function (error) {
                toastr.error("unable to save new user profile", 'Operation failed')
            })
        }
        $scope.createNewUser = createNewUser;
        $scope.updateUser = updateUser;
        init();
    }]);