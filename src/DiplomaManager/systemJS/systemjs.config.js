/**
 * System configuration for Angular samples
 * Adjust as necessary for your application needs.
 */
(function (global) {
    System.config({
        // map tells the System loader where to look for things
        // ASP.NET Core exposes the wwwroot folder without the need to specify "wwwroot" in the path
        defaultJSExtensions: true,
        transpiler: 'typescript',
        typescriptOptions: { emitDecoratorMetadata: true }, 
        paths: {
            'npm:': 'node_modules/'
        },
        map: {

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
            "ngx-mydatepicker": "js/ngx-mydatepicker.umd.js",

            'core-js': 'npm:core-js',
            'angular2-dynamic-component': 'npm:angular2-dynamic-component',
            'ts-metadata-helper': 'npm:ts-metadata-helper',
            'angular2-busy': 'npm:angular2-busy',
            "ng2-bs3-modal": "npm:ng2-bs3-modal",
            "ng2-table": "npm:ng2-table",
            // other libraries
            'rxjs': 'js'
        },
        // packages tells the System loader how to load when no filename and/or no extension
        packages: {
            'student-app': { defaultExtension: 'ts' },
            'teacher-app': { defaultExtension: 'ts' },

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