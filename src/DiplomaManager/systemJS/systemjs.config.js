﻿/**
 * System configuration for Angular samples
 * Adjust as necessary for your application needs.
 */
(function (global) {
    System.config({
        // map tells the System loader where to look for things
        // ASP.NET Core exposes the wwwroot folder without the need to specify "wwwroot" in the path
        paths: {
            'npm:': 'node_modules/'
        },
        map: {
            // our app is within the app folder
            app: 'app',

            // angular bundles Format = ('name': 'location')
            '@angular/core': 'js/core.umd.js',
            '@angular/common': 'js/common.umd.js',
            '@angular/compiler': 'js/compiler.umd.js',
            '@angular/platform-browser': 'js/platform-browser.umd.js',
            '@angular/platform-browser-dynamic': 'js/platform-browser-dynamic.umd.js',
            '@angular/http': 'js/http.umd.js',
            '@angular/router': 'js/router.umd.js',
            '@angular/forms': 'js/forms.umd.js',

            'ng2-select': 'js/ng2-select.umd.js',

            'core-js': 'npm:core-js',
            'angular2-dynamic-component': 'npm:angular2-dynamic-component',
            'ts-metadata-helper': 'npm:ts-metadata-helper',
            'angular2-busy': 'npm:angular2-busy',

            // other libraries
            'rxjs': 'js'
        },
        // packages tells the System loader how to load when no filename and/or no extension
        packages: {
            app: {
                main: './main.js',
                defaultExtension: 'js'
            },
            rxjs: {
                defaultExtension: 'js'
            },
            'angular2-dynamic-component': {
                main: 'index.js',
                defaultExtension: 'js'
            },
            'ts-metadata-helper': {
                main: 'index.js',
                defaultExtension: 'js'
            },
            'angular2-busy': {
                main: 'index.js',
                defaultExtension: 'js' 
            },
            'core-js': {
                main: 'index.js',
                defaultExtension: 'js'
            } 
        }
    });
})(this);