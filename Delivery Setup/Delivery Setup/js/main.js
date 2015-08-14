/*
* File: js\main.js
* Authors: Srihari.Sridharan@cognizant.com; Renga.PrasadV@cognizant.com
* Reviewer: Ganesh.Kondal@cognizant.com
* Copyrights: Cognizant 2013
*/

'use strict';

//Namespace Definitions
var App = App || {};
App.Routers = App.Routers || {};

//Configure Require JS
require.config({
    shim: {
        underscore: {
            exports: '_'
        },
        backbone: {
            deps: [
				'underscore',
				'jquery'
			],
            exports: 'Backbone'
        },
        backboneLocalstorage: {
            deps: ['backbone'],
            exports: 'Store'
        },
        knockout: {
            exports: 'ko'
        }
    },
    paths: {
        jquery: '../lib/jquery/jquery',
        underscore: '../lib/underscore/underscore',
        backbone: '../lib/backbone/backbone',
        backboneLocalstorage: '../lib/backbone.localStorage/backbone.localStorage',
        text: '../lib/requirejs-text/text',
        knockout: '../lib/knockout/knockout'
    }
});

require([
	'backbone',
	'routers/router'
], function (Backbone, Routes) {
    new App.Routers.Routes();
    Backbone.history.start();
});