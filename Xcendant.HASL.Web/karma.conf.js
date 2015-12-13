module.exports = function (config) {
    config.set({
        // base path that will be used to resolve all patterns 
        basePath: '',
        // frameworks to use
        frameworks: ['jasmine'],
        files: [
				 'bower_components/angular/angular.js',
				 'bower_components/angular-route/angular-route.js',
				 'bower_components/angular-animate/angular-animate.js',
				 'bower_components/angular-messages/angular-messages.js',
				 'bower_components/angular-bootstrap/ui-bootstrap-tpls.min.js',
				 'bower_components/ngstorage/ngStorage.js',
				 'bower_components/angular-mocks/angular-mocks.js',
                 'source/app/app.js',
                 'source/app/services/*.js',
                 'source/app/controllers/*.js',
                 'test/*Spec.js',
        ],
        // list of files to exclude
        exclude: [

        ],
        // preprocess matching files 
        preprocessors: {

        },

        // test results reporter to use possible values: 'dots', 'progress'
        reporters: ['progress'],

        // web server port
        port: 9876,

        // enable / disable colors in the output (reporters and logs)
        colors: true,
        // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
        logLevel: config.LOG_WARN,

        // enable / disable watching file and executing tests whenever any file changes
        autoWatch: false,

        // start these browsers
        // available browser launchers: https://npmjs.org/browse/keyword/karma-launcher
        browsers: ['PhantomJS'],

        // Continuous Integration mode
        // if true, Karma captures browsers, runs the tests and exits
        singleRun: true
    });
};

