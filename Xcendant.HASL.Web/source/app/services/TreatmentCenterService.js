'use strict'
var app = angular.module("hasl.treatmentcenter", []);
app.factory('TreatmentCenterService', ['$http', '$q', 'generalSettings', function ($http, $q, generalSettings) {
    var servicesFactory = {};
    var getAll = function () {
        return $q(function (resolve, reject) {
            var centers;
            $http.get(generalSettings.apiServiceBaseUri + "/api/treatmentcenters").then(function (response) {
                centers = response.data;
                resolve(centers);
            }, function (error) {

                reject(error.data);
            });

        });
    }

    servicesFactory.getAll = getAll;
    return servicesFactory;
}])