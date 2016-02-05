angular.module('hasl.doctor')
.controller("DoctorController", ['$scope', '$window', '$location', '$localStorage', 'authSerivce', 'authSettings', 'DoctorService', 'generalSettings',
    function ($scope, $window, $location, $localStorage, authService, authSettings, DoctorService, generalSettings) {
        $scope.patient;
        $scope.isNewPatient;
        function init() {
            var authdata = $localStorage.authorizationData;
            DoctorService.get(authdata.userName).then(function (data) {
                if (data) {
                    $scope.isNewPatient = false;
                    $scope.patient = data;
                } else {
                    $scope.isNewPatient = true;
                    $scope.patient = {};

                }
                $scope.patient.userName = authdata.userName;
            });

        }

        var createPatient = function () {
            DoctorService.createPatient($scope.patient).then(function (data) {
                if (data) {

                } else {


                }
            }, function (error) { })
        }
        var updatePatient = function () {
            DoctorService.updatePatient($scope.patient).then(function (data) {
                if (data) {

                } else {


                }
            }, function (error) { })
        }
        $scope.updatePatient = updatePatient;
        $scope.createPatient = createPatient;


        init();
    }]);