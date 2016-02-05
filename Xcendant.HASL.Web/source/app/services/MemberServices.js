'use strict'
var app = angular.module('MemberServices', ['ui.bootstrap']);
app.factory('MemberDetailServices', ['$http', '$q', 'generalSettings', function ($http, $q,  generalSettings) {
    var memberDetailsServiceFactory = {};

    var getUserProfile = function (userName) {
        return $q(function (resolve, reject) {
            var memberProfile;
            $http.get(generalSettings.apiServiceBaseUri + "/api/registeredusers/" + userName + "/").then(function (response) {
                memberProfile = response.data;
                resolve(memberProfile);
            }, function (error) {
                reject(error.data);
            });
        });
    }

    var createUserProfile = function (userDetail) {
        return $q(function (resolve, reject) {
            var message;
            $http.post(generalSettings.apiServiceBaseUri + "/api/registeredusers/" + userDetail.email + "/", userDetail).then(function (response) {
                message = response.data;
                resolve(message);
            }, function (error) {
                reject(error.data);
            });
        });
    }

    var updateUserProfile = function (userDetail) {
        return $q(function (resolve, reject) {
            var message;
            $http.put(generalSettings.apiServiceBaseUri + "/api/registeredusers/" + userDetail.email + "/", userDetail).then(function (response) {
                message = response.data;
                resolve(message);
            }, function (error) {
                reject(error.data);
            });
        });
    }

    memberDetailsServiceFactory.createUserProfile = createUserProfile;
    memberDetailsServiceFactory.updateUserProfile = updateUserProfile;
    memberDetailsServiceFactory.getUserProfile = getUserProfile;
    return memberDetailsServiceFactory;
}]);