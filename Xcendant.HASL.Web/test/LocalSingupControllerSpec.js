describe("localSingupCtrl", function () {
    it("allows to register as new user when correct values submitted", function () {
        beforeEach(module('hasl'));
        var $controller
        beforeEach(inject(function (_$controller_) {
            // The injector unwraps the underscores (_) from around the parameter names when matching
            $controller = _$controller_;
        }));
        describe('$scope.registerLocalUser', function () {
            it('registers a new user when correct values are provided', function () {
                var $scope = {};
                var controller = $controller('localSingupCtrl', { $scope: $scope });
            });
        });
        var user = {};
        user.firstName = 'Patrick';
        user.lastName = 'Stewart';
        user.email = 'prox@xmen.com';
        user.password = '123';
        user.confirmPassword = '123';

    });
});