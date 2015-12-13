'use strict'
var app = angular.module('MemberServices', []);
app.factory('MemberDetailServices', ['$http', '$q', 'generalSettings', function ($http, $q, generalSettings) {
    var memberDetailsServiceFactory = {};

    var getUserProfile = function (userName) {
        return $q(function (resolve, reject) {
            var memberProfile;
            $http.get(generalSettings.apiServiceBaseUri + "/api/registereduser/" + userName + "/").then(function (response) {
                memberProfile = response.data;
                resolve(memberProfile);
            }, function (error) {
                reject(error);
            });
        });
    }

    var createUserProfile = function (userDetail) {
        return $q(function (resolve, reject) {
            var message;
            $http.post(generalSettings.apiServiceBaseUri + "/api/registereduser/" + userDetail.email + "/", userDetail).then(function (response) {
                message = response.data;
                resolve(message);
            }, function (error) {
                reject(error);
            });
        });
    }

    var updateUserProfile = function (userDetail) {
        return $q(function (resolve, reject) {
            var mesasge;
            $http.put(generalSettings.apiServiceBaseUri + "/api/registereduser/" + userDetail.email + "/", userDetail).then(function (response) {
                message = response.data;
                resolve(message);
            }, function (error) {
                reject(error);
            });
        });
    }

    memberDetailsServiceFactory.createUserProfile = createUserProfile;
    memberDetailsServiceFactory.updateUserProfile = updateUserProfile;
    memberDetailsServiceFactory.getUserProfile = getUserProfile;
    return memberDetailsServiceFactory;
}]);