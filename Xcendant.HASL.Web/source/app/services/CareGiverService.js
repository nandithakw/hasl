'use strict'
var app = angular.module("hasl.caregiver", []);
app.factory('CareGiverService', ['$http', '$q', 'generalSettings', function ($http, $q, generalSettings) {
    var serviceFactory = {};
    var getAll = function () {
        return $q(function (resolve, reject) {
            var caregivers;
            $http.get(generalSettings.apiServiceBaseUri + "api/caregivers").then(function (response) {
                caregivers = response.data;
                resolve(caregivers);
            }, function (error) {

                reject(error);
            });

        });
    }

    serviceFactory.getAll = getAll;
    return serviceFactory;
}])
