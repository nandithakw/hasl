
var app = angular.module('hasl', ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'ngMessages', 'ngStorage', 'auth', 'localSingup', 'authSerivces']);
//angular.module('hasl', ['ui.bootstrap']);




/**
 * Configure the Routes
 */
app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $locationProvider.html5Mode(true);
    $routeProvider
      // root
      .when("/", {
          templateUrl: "views/login.html",
          controller: "PageCtrl"
      })
      // home
      .when("/home", {
          templateUrl: "views/landing_page.html",
          controller: "PageCtrl"
      })

      .when("/change_password", {
          templateUrl: "views/change_password.html",
          controller: "PageCtrl"
      })
    
      .when("/complete_your_profile", {
          templateUrl: "views/complete_your_profile.html",
          controller: "PageCtrl"
      })    


      //patient registration
       .when("/patient", {
           templateUrl: "views/patient_registration.html",
           controller: "PageCtrl"
       })
        //doctor registration
       .when("/doctor_registration", {
           templateUrl: "views/doctor_registration.html",
           controller: "PageCtrl"
       })

         //caretaker registration
       .when("/caretaker", {
           templateUrl: "views/caretaker_registration.html",
           controller: "PageCtrl"
       })

       //activity management
       .when("/activity_management", {
           templateUrl: "views/activity_management.html",
           controller: "PageCtrl"
       })

     //Signup management
       .when("/signup", {
           templateUrl: "views/signup.html",
           controller: "PageCtrl"
       })

     //login management
       .when("/login", {
           templateUrl: "views/login.html",
           controller: "PageCtrl"
       })

      //add bleed locations management
       .when("/add_bleed_location", {
           templateUrl: "views/add_bleed_location.html",
           controller: "PageCtrl"
       })

      //add symtomss management
       .when("/add_symptoms", {
           templateUrl: "views/add_symptoms.html",
           controller: "PageCtrl"
       })

       //approval queue management
       .when("/approval_queue", {
           templateUrl: "views/approval_queue.html",
           controller: "PageCtrl"
       })

       //hospital registration
       .when("/hospital_registration", {
           templateUrl: "views/hospital_registration.html",
           controller: "PageCtrl"
       })

      //treatment center regitration
       .when("/treatment_center_registration", {
           templateUrl: "views/treatment_center_registration.html",
           controller: "PageCtrl"
       })

      //treatment center regitration
       .when("/user_management", {
           templateUrl: "views/user_management.html",
           controller: "PageCtrl"
       })




      // else 404
      .otherwise("/404", {
          templateUrl: "views/404.html",
          controller: "PageCtrl"

      });
}]);
/*constants*/
app.constant("authSettings", {
    authApiServiceBaseUri: 'http://localhost:57979',
    clientId: 'haslWebApp',
});

/**
 * Controllers 
 */

app.controller('BlogCtrl', function (/* $scope, $location, $http */) {
    console.log("Blog Controller reporting for duty.");

});


app.controller('PageCtrl', function (/* $scope, $location, $http */) {
    console.log("Page Controller reporting for duty.");


});

app.controller('MainMenuListCtrl', ['$scope', function ($scope) {
    //console.log("Page Controller reporting for duty.");
    $scope.menuitems =
      [
       { name: 'About Us' },
       { name: 'Services' },
       { name: 'Contact Us' }

      ];

}]);

app.controller('NavigationBar', ['$scope', function ($scope) {
    //console.log("Page Controller reporting for duty.");
    $scope.navbarCollapsed = true;

}]);

//angular.module('hasl').controller('DropdownCtrl', function ($scope, $log) {
//    
//  $scope.items = [
//    'Change Password',
//    'Logout'
//
//  ];
//
//  $scope.status = {
//    isopen: false
//  };
//
//  $scope.toggled = function(open) {
//   // $log.log('Dropdown is now: ', open);
//  };
//
//  $scope.toggleDropdown = function($event) {
//    $event.preventDefault();
//    $event.stopPropagation();
//    $scope.status.isopen = !$scope.status.isopen;
//  };
//});


app.controller("AssignUserRolesCtrl", function ($scope) {
    $scope.items = [
        { ID: "001", UserName: "Nimal", IdentificaitonNumber: "IN001", UserRole: "Doctor" },
        { ID: "002", UserName: "Saman", IdentificaitonNumber: "IN002", UserRole: "Doctor / Patient" },
        { ID: "003", UserName: "Chamal", IdentificaitonNumber: "IN003", UserRole: "Patient" }
    ];
});

app.controller("UserRolesCtrl", function ($scope) {
    $scope.items = [
        { UserRole: "All" },
        { UserRole: "Doctor" },
        { UserRole: "Patient" },
        { UserRole: "Caretaker" }
    ];
});

app.controller("ActivityCtrl", function ($scope) {
    $scope.items = [
        { ActivityNumber: "5674789", SymptomName: "Muscle", BleedLocation: "Under arm" },
        { ActivityNumber: "5674790", SymptomName: "Soft Tissue", BleedLocation: "Right Thigh" },
        { ActivityNumber: "5674791", SymptomName: "Joints", BleedLocation: "Left Thigh" }
    ];
});

app.controller("RegisteredHospitalCtrl", function ($scope) {
    $scope.items = [


        { ID: "H100", HospitalName: "Hospital001", City: "Colombo", Country: "Srilanka" },
        { ID: "H101", HospitalName: "Hospital002", City: "Colombo", Country: "Srilanka" },
        { ID: "H102", HospitalName: "Hospital003", City: "Colombo", Country: "Srilanka" }

    ];
});

app.controller('AddSymptomsCtrl', ['$scope', function ($scope) {
    $scope.data = [
       { id: '1', name: 'Muscle' },
       { id: '2', name: 'Soft Tissue' },
       { id: '3', name: 'Joints' },
       { id: '4', name: 'Excessive Bruising' },
       { id: '5', name: 'Nose Bleeds' },
       { id: '6', name: 'eExtenteded Bleeding' }
    ];

}]
);

app.controller('SymptomListCtrl', ['$scope', function ($scope) {
    $scope.data = [
       { id: '1', name: 'Muscle' },
       { id: '2', name: 'Soft Tissue' },
       { id: '3', name: 'Joints' },
       { id: '4', name: 'Excessive Bruising' },
       { id: '5', name: 'Nose Bleeds' },
       { id: '6', name: 'eExtenteded Bleeding' }
    ];

}]
);

app.controller('BleedLocationListCtrl', ['$scope', function ($scope) {
    $scope.data =
      [
       { id: '1', name: 'Under arm' },
       { id: '2', name: 'Nose' },
       { id: '3', name: 'Right Thigh' },
       { id: '4', name: 'Left Thigh' }
      ];

}]);


//angular.module('myapp.controllers', []);

angular.module('hasl').controller('NavController', function ($scope, $location) {

    //console.log("menu.");

    $scope.isCollapsed = true;
    $scope.$on('$routeChangeSuccess', function () {
        $scope.isCollapsed = true;
    });
});

angular.module('hasl').controller('ModalDemoCtrl', function ($scope, $modal, $log) {


    $scope.animationsEnabled = true;

    $scope.open = function (size) {

        var modalInstance = $modal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'myModalContent.html',
            controller: 'ModalInstanceCtrl',
            size: size,
            resolve: {

            }
        });

        modalInstance.result.then(function (selectedItem) {
            $scope.selected = selectedItem;
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };

    $scope.toggleAnimation = function () {
        $scope.animationsEnabled = !$scope.animationsEnabled;
    };

});

// Please note that $modalInstance represents a modal window (instance) dependency.
// It is not the same as the $modal service used above.

angular.module('hasl').controller('ModalInstanceCtrl', function ($scope, $modalInstance) {


    $scope.ok = function () {
        $modalInstance.close($scope.selected.item);
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
});


angular.module('hasl').controller('AccordionDemoCtrl', function ($scope) {

    $scope.oneAtATime = true;

});

angular.module('hasl').controller('DatepickerDemoCtrl', function ($scope) {
    $scope.today = function () {
        $scope.dt = new Date();
    };
    $scope.today();

    $scope.clear = function () {
        $scope.dt = null;
    };

    // Disable weekend selection
    $scope.disabled = function (date, mode) {
        return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
    };

    $scope.toggleMin = function () {
        $scope.minDate = $scope.minDate ? null : new Date();
    };
    $scope.toggleMin();
    $scope.maxDate = new Date(2020, 5, 22);

    $scope.open = function ($event) {
        $scope.status.opened = true;
    };

    $scope.dateOptions = {
        formatYear: 'yy',
        startingDay: 1
    };

    $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
    $scope.format = $scope.formats[0];

    $scope.status = {
        opened: false
    };

    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    var afterTomorrow = new Date();
    afterTomorrow.setDate(tomorrow.getDate() + 2);
    $scope.events =
      [
        {
            date: tomorrow,
            status: 'full'
        },
        {
            date: afterTomorrow,
            status: 'partially'
        }
      ];

    $scope.getDayClass = function (date, mode) {
        if (mode === 'day') {
            var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

            for (var i = 0; i < $scope.events.length; i++) {
                var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                if (dayToCheck === currentDay) {
                    return $scope.events[i].status;
                }
            }
        }

        return '';
    };
});




angular.module('hasl').controller('SignupValidation', ['$scope', function ($scope) {
    $scope.master = {};

    $scope.update = function (user) {
        $scope.master = angular.copy(user);
    };

    $scope.reset = function () {
        $scope.user = angular.copy($scope.master);
    };

    $scope.reset();
}]);


angular.module('hasl').directive("passwordVerify", function () {
    return {
        require: "ngModel",
        scope: {
            passwordVerify: '='
        },
        link: function (scope, element, attrs, ctrl) {
            scope.$watch(function () {
                var combined;

                if (scope.passwordVerify || ctrl.$viewValue) {
                    combined = scope.passwordVerify + '_' + ctrl.$viewValue;
                }
                return combined;
            }, function (value) {
                if (value) {
                    ctrl.$parsers.unshift(function (viewValue) {
                        var origin = scope.passwordVerify;
                        if (origin !== viewValue) {
                            ctrl.$setValidity("passwordVerify", false);
                            return undefined;
                        } else {
                            ctrl.$setValidity("passwordVerify", true);
                            return viewValue;
                        }
                    });
                }
            });
        }
    };
});



