//describe('localSingupCtrl', function () {
//    beforeEach(module('hasl'));
//    var $controller, $location, $httpBackend, authSerivce;
//    beforeEach(inject(function (_$controller_, _$location_, _$httpBackend_, _$q_, _authSerivce_) {
//        // The injector unwraps the underscores (_) from around the parameter names when matching
//        $controller = _$controller_;
//        $location = _$location_;
//        $httpBackend = _$httpBackend_;
//        authSerivce = _authSerivce_;
//        $q = _$q_;
//        spyOn(authSerivce, 'registerNewLocalUser').and.callFake(function () {
//            var deferred = $q.defer();
//            deferred.resolve('User creation success');
//            return deferred.promise;
//        });
//    }));
//    describe('$scope.registerLocalUser', function () {
//        it('registers a new user when correct values are provided', function () {
//            var $scope = {};
//            var user = {};
//            user.firstName = 'Patrick';
//            user.lastName = 'Stewart';
//            user.email = 'prox@xmen.com';
//            user.password = '123';
//            user.confirmPassword = '123';
//            expect(authSerivce).toBeDefined();

//            var controller = $controller('localSingupCtrl', { $scope: $scope });
//            expect($scope).toBeDefined();
//            $scope.user = user;
//            $scope.registerLocalUser(user);
//            expect(authSerivce.registerNewLocalUser).toHaveBeenCalledWith($scope.user);
//            //expect($location.path).toEqual('home');
//        });
//    });

//});

