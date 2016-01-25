angular.module('hasl.patient')
.controller("PatientController", ['$scope', '$window', '$location', '$q', 'ngProgressFactory', 'toastr', '$localStorage', 'authSerivce', 'authSettings', 'PatientService', 'generalSettings',
    function ($scope, $window, $location, $localStorage, $q, ngProgressFactory, toastr, authService, authSettings, PatientService, generalSettings) {
        $scope.progressbar = ngProgressFactory.createInstance();
        $scope.progressbar.start();
        $scope.isBusy = true;
        $scope.patient;
        $scope.isNewPatient;

        function init() {
            var authdata = $localStorage.authorizationData;
            getPatient().then(function (message) {
                $scope.progressbar.complete();
                $scope.isBusy = false;
            }, function (error) {
                $scope.progressbar.stop();
            });



        }
        var getPatient = function (userName) {
            return $q(function (resolve, reject) {
                PatientService.getPatient(userName).then(function (data) {
                    if (data) {
                        $scope.isNewPatient = false;
                        $scope.patient = data;
                    } else {
                        $scope.isNewPatient = true;
                        $scope.patient = {};
                    }
                    $scope.patient.userName = userName;
                    resolve();
                }, function (error) {
                    console.error(error);
                    reject();
                });
            });

        }
        var createPatient = function () {
            PatientService.create($scope.patient).then(function (data) {
                if (data) {

                } else {


                }
            }, function (error) { })
        }
        var updatePatient = function () {
            PatientService.update($scope.patient).then(function (data) {
                if (data) {

                } else {


                }
            }, function (error) { })
        }
        $scope.update = updatePatient;
        $scope.create = createPatient;


        init();
    }]);