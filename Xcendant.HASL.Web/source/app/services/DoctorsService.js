'use strict'
var app = angular.module("hasl.doctor", []);
app.factory('DoctorService', ['$http', '$q', 'generalSettings', function ($http, $q, generalSettings) {
    var serviceFactory = {};
    var getAll = function () {
        return $q(function (resolve, reject) {
            var doctors;
            $http.get(generalSettings.apiServiceBaseUri + "api/doctors").then(function (response) {
                doctors = response.data;
                resolve(doctors);
            }, function (error) {
                console.error("Error occurred while trying to retrieve doctors");
                console.error(error);
                reject(error);
            });

        });
    }

    serviceFactory.getAll = getAll;
    return serviceFactory;
}])
