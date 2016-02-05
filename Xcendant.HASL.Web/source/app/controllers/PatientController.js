angular.module('hasl.patient')
.controller("PatientController", ['$scope', '$window', '$location', '$localStorage', '$q', 'ngProgressFactory', 'toastr', 'authSerivce', 'authSettings', 'PatientService', 'CareGiverService', 'DoctorService', 'HospitalService', 'TreatmentCenterService', 'generalSettings',
    function ($scope, $window, $location, $localStorage, $q, ngProgressFactory, toastr, authService, authSettings, PatientService, CareGiverService, DoctorService, HospitalService, TreatmentCenterService, generalSettings) {
        $scope.progressbar = ngProgressFactory.createInstance();
        $scope.progressbar.start();
        $scope.isBusy = true;
        $scope.patient;
        $scope.isNewPatient;
        $scope.doctors;
        $scope.caregivers;
        $scope.hospitals;

        function init() {
            var authdata = $localStorage.authorizationData;
            if (authdata) {
                var getPatientPromise = getPatient(authdata.userName);
                var getDoctorsPromise = getAllDoctors();
                var getHospitalsPromise = getAllHospitals();
                var getTreatmentCentersPromise = getAllTreatmentCenters();
                var getCareGiversPromise = getAllCareGivers();
                $q.all(getPatientPromise,getDoctorsPromise,getHospitalsPromise,getTreatmentCentersPromise,getCareGiversPromise).then(function (message) {
                    $scope.progressbar.complete();
                    $scope.isBusy = false;
                }, function (error) {
                    $scope.progressbar.stop();


                    $scope.registeredUser.userName = authdata.userName;
                });
            } else {
                //user doesn't have authorization data something bad has happened :(
                toastr.error("you are not logged in.", 'Redirecting to Login Page');
                $window.location.href = "/login";
            }







        }

        var getAllDoctors = function () {
            return $q(function (resolve, reject) {
                DoctorService.getAll().then(function (doctors) {
                    $scope.doctors = doctors;
                    resolve();
                }, function (error) {
                    toastr.error("Error in retrieving doctors", error.message);
                    console.error(error);
                    reject(error);
                });

            });
        }

        var getAllHospitals = function () {
            return $q(function (resolve, reject) {
                HospitalService.getAll().then(function (hospitals) {
                    $scope.hospitals = hospitals;
                    resolve();
                }, function (error) {
                    toastr.error("Error in retrieving hospitals", error.message);
                    console.error(error);
                    reject(error);
                });

            });;
        }

        var getAllCareGivers = function () {
            return $q(function (resolve, reject) {
                CareGiverService.getAll().then(function (caregivers) {
                    $scope.caregivers = caregivers;
                    resolve();
                }, function (error) {
                    toastr.error("Error in retrieving caregivers", error.message);
                    console.error(error);
                    reject(error);
                });

            });
        }



        var getAllTreatmentCenters = function () {
            return $q(function (resolve, reject) {
                TreatmentCenterService.getAll().then(function (treatmentCenters) {
                    $scope.treatmentCenters = treatmentCenters;
                    resolve();
                }, function (error) {
                    toastr.error("Error in retrieving treatment centers", error.message);
                    console.error(error);
                    reject(error);
                });

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
                    toastr.error("Error getting patient data", error.message);
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