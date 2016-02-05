'use strict'
describe("Patient Service", function () {
    var httpBackend, PatientService, generalSettings;
    beforeEach(module('hasl'));


    beforeEach(inject(function (_PatientService_, $httpBackend, _generalSettings_) {
        PatientService = _PatientService_;
        httpBackend = $httpBackend;
        generalSettings = _generalSettings_;
    }));

    it("should return patient details if already registerd if not undifined", function () {
        httpBackend.whenPOST(generalSettings.apiServiceBaseUri + "/api/patients/details", 'username')
            .respond({ "userName": 'username' });
        httpBackend.whenGet(generalSettings.apiServiceBaseUri + "/api/patients/username/")
    .respond({ "userName": 'username' });
        PatientService.getgetPatient("username").then(function (userdetails) {
            expect(userdetails).toEqual({ "userName": 'username' });
        });
        httpBackend.flush();
    });
});