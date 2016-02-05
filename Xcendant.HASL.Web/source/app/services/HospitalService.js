'use strict'
var app = angular.module("hasl.hospital", []);
app.factory('HospitalService', ['$http', '$q', 'generalSettings', function ($http, $q, generalSettings) {
    var hospitalServicesFactory = {};
    var getAll = function () {
        return $q(function (resolve, reject) {
            var hospitals;
            $http.get(generalSettings.apiServiceBaseUri + "/api/hospitals").then(function (response) {
                hospitals = response.data;
                resolve(hospitals);
            }, function (error) {

                reject(error.data);
            });

        });
    }

    hospitalServicesFactory.getAll = getAll;
    return hospitalServicesFactory;
}])