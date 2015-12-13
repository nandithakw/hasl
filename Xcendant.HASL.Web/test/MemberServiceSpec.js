'use strict'
describe("Member Details Service", function () {
    var httpBackend, MemberDetailServices, generalSettings;
    beforeEach(module('hasl'));


    beforeEach(inject(function (_MemberDetailServices_, $httpBackend, _generalSettings_) {
        MemberDetailServices = _MemberDetailServices_;
        httpBackend = $httpBackend;
        generalSettings = _generalSettings_;
    }));

    it("should return registerd user details if registerd if not registerd undifined", function () {
        httpBackend.whenPOST(generalSettings.apiServiceBaseUri + "/api/registereduser/details", 'username')
            .respond({ "userName": 'username' } );
        MemberDetailServices.getUserProfile("username").then(function (userdetails) {
            expect(userdetails).toEqual({ "userName": 'username' });
        });
        httpBackend.flush();
    });
});