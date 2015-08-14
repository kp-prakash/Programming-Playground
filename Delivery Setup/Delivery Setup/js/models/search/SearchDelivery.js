/*
* File: js\models\search\SearchDelivery.js
* Authors: Srihari.Sridharan@cognizant.com; Renga.PrasadV@cognizant.com
* Reviewer: Ganesh.Kondal@cognizant.com
* Copyrights: Cognizant 2013
*/

'use strict';
var App = App || {};
App.Models = App.Models || {};
App.Models.Search = App.Models.Search || {};

define([
    'underscore',
    'backbone',
    'knockout',
    '../../models/search/SearchCriteria',
    '../../models/common/PrintOptions',
    '../../collections/common/Deliveries'
], function (_, Backbone, ko, SearchCriteria, PrintOptions, Deliveries) {

    //Backbone view model for SearchDelivery screen.
    App.Models.Search.SearchDelivery = Backbone.Model.extend({

        //Initializes Backbone Model.
        initialize: function () {
            this.searchCriteria = new App.Models.Search.SearchCriteria();
            this.printOptions = getPrintOptions();
            //As of now using ko.observableArray()
            this.deliveries = ko.observableArray(); //new App.Collections.Common.Deliveries();
        }

    });

    function getPrintOptions() {
        var printOptions = new App.Models.Common.PrintOptions();

        //Disable checkboxes by default based on screen requirements.
        printOptions.asnPOLines.isEnabled = ko.observable(false);

        //Mark checkboxes as checked by default based on screen requirements.
        //Remember to set these values to ko.observable() as they need to be bound to UI!
        printOptions.entireDelivery.isSelected = ko.observable(true);
        printOptions.unloaderReferenceSheet.isSelected = ko.observable(true);
        printOptions.receiver.isSelected = ko.observable(true);
        printOptions.receivingManifest.isSelected = ko.observable(true);
        return printOptions;
    };

    return App.Models.Search.SearchDelivery;
});