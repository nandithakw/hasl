describe('authCtrl', function () {
    var scope, controller, httpBackend, authSettings;

    // Initialization of the AngularJS application before each test case
    beforeEach(module('hasl'));
    // Injection of dependencies, $http will be mocked with $httpBackend
    beforeEach(inject(function ($rootScope, $controller, $httpBackend, authSettings) {
        scope = $rootScope;
        controller = $controller;
        httpBackend = $httpBackend;
        controller('authCtrl', { '$scope': scope });
    }));
    beforeEach(authSettings = {
        authApiServiceBaseUri: 'http://localhost:57979',
        clientId: 'haslWebApp',
    });
    describe('$scope.localSignIn', function () {
        it('saves a valid token in the local storage and redirects to home if user name and password are valid', function () {

            // Which HTTP requests do we expect to occur, and how do we response?
            httpBackend.expectGET('http://localhost:57979/api/account/externalLogins?returnUrl=http://localhost:9876/authcomplete.html/&generateState=true')
                .respond();
            httpBackend.whenPost('http://localhost:57979/token', "")
                .respond(function (method, url, data, headers) {
                    return [200,{ 'data':{
                        "access_token": "o-SqL2GIek5J3PaqAzDDAA9fkyT0YxZ-KsQ9CvGFbwSl9hmu6F5KPlVgRZ3-llvMur6HPU7--LJEFJoY",
                        "token_type": "bearer",
                        "expires_in": 1209599,
                        "user_name": "prox@xmen.com",
                        ".issued": "Sun, 04 Oct 2015 22:58:14 GMT",
                        ".expires": "Sun, 18 Oct 2015 22:58:14 GMT"

                    }}, {}];

                });
            scope.localLoginData = { userName: 'prox@xmen.com', password: '123' };
            scope.localSignIn(scope.localLoginData);

            // Respond to all HTTP requests
            httpBackend.flush();
            // Triggering the AngularJS digest cycle in order to resolve all promises
            scope.$apply();
            expect($scope.strength).toEqual('strong');
        });
    });
   
});