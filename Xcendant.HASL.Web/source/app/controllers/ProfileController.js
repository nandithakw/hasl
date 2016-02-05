var app = angular.module('profile', ['authSerivces', 'MemberServices', 'ngStorage', 'ngMessages', 'toastr', 'ngProgress']);

app.controller("ProfileController", ['$scope', '$window', '$location', '$localStorage', 'uibDateParser', '$q','toastr', 'ngProgressFactory', 'authSerivce', 'authSettings', 'MemberDetailServices', 'generalSettings',
    function ($scope, $window, $location, $localStorage, uibDateParser, $q, toastr, ngProgressFactory, authService, authSettings, MemberDetailServices, generalSettings) {
        $scope.progressbar = ngProgressFactory.createInstance();
        $scope.progressbar.start();
        $scope.loggedInUser;
        $scope.registeredUser;
        $scope.isBusy = true;
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
                getUserProfile(authdata.userName).then(function (data) {
                    $scope.progressbar.complete();
                    $scope.isBusy = false;
                }, function (error) {
                    $scope.progressbar.stop();
                });
            } else {
                //user doesn't have authorization data something bad has happened :(
                toastr.error("you are not logged in.", 'Redirecting to Login Page');
                $window.location.href = "/login";
            }


        };
        var getUserProfile = function (userName) {
            return $q(function (resolve, reject) {
                MemberDetailServices.getUserProfile(userName).then(function (data) {
                    if (data) {
                        $scope.isNewUser = false;
                        $scope.registeredUser = data;
                        $scope.registeredUser.dateOfBirth = uibDateParser.parse($scope.registeredUser.dateOfBirth, generalSettings.dateFormat);

                    } else {
                        $scope.isNewUser = true;
                        $scope.registeredUser = {};
                        $scope.registeredUser.country = 'LK';
                        $scope.registeredUser.email = userName;

                    }
                    $scope.registeredUser.userName = userName;

                    resolve();
                }, function (error) {
                    toastr.error("Error occurred while trying to retrieve registered user profile", error.message);
                    console.error(error);
                    reject();
                });
            });
        }
        var createNewUser = function () {
            MemberDetailServices.createUserProfile($scope.registeredUser).then(function (data) {
                if (data) {
                    toastr.success(data, "operation succeeded");
                } else {
                    toastr.info(data, "operation succeeded");

                }
            }, function (error) {

                toastr.error(error.message,"unable to save new user profile");
                if (error.modelState) {
                    for (var property in error.modelState) {
                        if (error.modelState.hasOwnProperty(property)) {
                            error.modelState[property].forEach(function (element,index,array) {
                                toastr.error(element);
                            });
                        }
                    }
                }
               
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