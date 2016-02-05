'use strict'
var app = angular.module("hasl.patient", ['ngProgress', 'ngMessages', 'ngProgress', 'toastr', 'hasl.caregiver', 'hasl.treatmentcenter', 'hasl.hospital', 'hasl.doctor']);
app.factory('PatientService', ['$http', '$q', 'generalSettings', function ($http, $q, generalSettings) {
    var serviceFactory = {};

    var getPatient = function (username) {
        return $q(function (resolve, reject) {
            var patient;
            $http.get(generalSettings.apiServiceBaseUri + "/api/patients/" + username+"/").then(function (response) {
                patient = response.data;
                resolve(patient);
            }, function (error) {

                reject(error.data);
            });

        });

    };

    var createPatient = function (patient) {
        return $q(function (resolve, reject) {
            var result;
            $http.post(generalSettings.apiServiceBaseUri + "/api/patients/", patient).then(function (response) {
                result = response.data;
                resolve(result);
            }, function (error) {

                reject(error.data);
            });

        });

    };


    var updatePatient = function (patient) {
        return $q(function (resolve, reject) {
            var result;
            $http.put(generalSettings.apiServiceBaseUri + "api/patients/" + patient.username + "/", patient).then(function (response) {
                result = response.data;
                resolve(result);
            }, function (error) {

                reject(error);
            });

        });

    };

    var getAll = function () {
        return $q(function (resolve, reject) {
            var patients;
            $http.get(generalSettings.apiServiceBaseUri + "api/patients").then(function (response) {
                patients = response.data;
                resolve(patients);
            }, function (error) {
                reject(error);
            });

        });
    }
    serviceFactory.createPatient = createPatient;
    serviceFactory.updatePatient = updatePatient;
    serviceFactory.getPatient = getPatient;
    serviceFactory.getAll = getAll;
    return serviceFactory;
}])
