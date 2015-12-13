/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {
	'use strict';
	grunt.initConfig({
		// read in the project settings from the package.json file into the pkg property
		pkg: grunt.file.readJSON('package.json'),
		copy: {
			main: {
				files: [
				   {
					   expand: true, cwd: '',
					   src: ['css/**', 'fonts/**', 'img/**', 'views/**', 'index.html', 'Web.config'],
					   dest: 'build/'
				   },
				]
			}
		},
		concat: {
			options: {
				separator: ';',
			},
			dist: {
				src: ['source/app/**'],
				dest: 'build/js/<%= pkg.name %>-<%= pkg.version %>-app.js',

			},
		},
		meta: {
			'jsFilesForTesting': [
				 'bower_components/angular/angular.js',
				 'bower_components/angular-route/angular-route.js',
				 'bower_components/angular-animate/angular-animate.js',
				 'bower_components/angular-messages/angular-messages.js',
				 'bower_components/angular-bootstrap/ui-bootstrap-tpls.min.js',
				 'bower_components/ngstorage/ngStorage.js',
				 'bower_components/angular-mocks/angular-mocks.js',
				 'test/**/*Spec.js',
				 'source/app/app.js',
				 'source/app/services/*.js',
				 'source/app/controllers/*.js',
				 'test/*Spec.js',
			]
		},

		karma: {
			'development': {
				'configFile': 'karma.conf.js',
				'options': {
					'files': [
					  '<%= meta.jsFilesForTesting %>'                      
					],
				}
			},
			'debug-unit': {
			    'configFile': 'karma.conf.js',
			    'browsers': ['Chrome'],
			    'singleRun': false,
			    'options': {
			        'files': [
					  '<%= meta.jsFilesForTesting %>'
			        ],
			    }
			},
		},

	});
	//Add all plugins that your project needs here
	grunt.loadNpmTasks('grunt-bowercopy');
	grunt.loadNpmTasks('grunt-karma');
	grunt.loadNpmTasks('grunt-contrib-copy');
	grunt.loadNpmTasks('grunt-contrib-concat');
	grunt.loadNpmTasks('grunt-contrib-clean');
};