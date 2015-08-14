/*
* File: js\routers\router.js
* Authors: Srihari.Sridharan@cognizant.com; Renga.PrasadV@cognizant.com
* Reviewer: Ganesh.Kondal@cognizant.com
* Copyrights: Cognizant 2013
*/

'use strict';

//Namespace Definitions
var App = App || {};
App.Routers = App.Routers || {};
App.Views = App.Views || {};

define([
	'jquery',
	'backbone',
    'views/SearchDelivery',
    'views/CreateDelivery',
	'views/ViewDelivery'
], function ($, Backbone, SearchDeliveryView, CreateDeliveryView, ViewDeliveryView) {
    
	//Backbone router for routing to search, create and view delivery screen.
	App.Routers.Routes = Backbone.Router.extend({
        
		routes: {
            '': 'home',
            'SearchDelivery': 'home',
            'CreateDelivery': 'createdelivery',
            'ViewDelivery/:id': 'viewdelivery'
        },

        // Routing to Search Delivery Screen
        home: function () {
            return new App.Views.SearchDeliveryView();
        },

        // Routing to Create Delivery Screen
        createdelivery: function () {
            return new App.Views.CreateDeliveryView();
        },
        
        // Routing to View Delivery Screen
        viewdelivery: function (id) {
            return new App.Views.ViewDeliveryView({id:id});
        }
    });

    return App.Routers.Routes;
});