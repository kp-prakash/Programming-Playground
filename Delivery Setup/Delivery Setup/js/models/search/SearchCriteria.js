/*
* File: js\models\search\SearchCriteria.js
* Authors: Srihari.Sridharan@cognizant.com; Renga.PrasadV@cognizant.com
* Reviewer: Ganesh.Kondal@cognizant.com
* Copyrights: Cognizant 2013
*/

'use strict';

//Namespace Definitions.
var App = App || {};
App.Models = App.Models || {};
App.Models.Search = App.Models.Search || {};

define([
    'underscore',
    'backbone',
    'knockout',
    '../../models/search/SearchMetaData'
], function (_, Backbone, ko) {

    App.Models.Search.SearchCriteria = Backbone.Model.extend({
        urlRoot: '/gls/deliveries/search',
        
        //Initializes Backbone Model.
        initialize: function () {
            this.subCenter = ko.observable('');
            this.deliveryStatus = ko.observable('');
            this.trailerStatus = ko.observable('');
            this.mdseType = ko.observable('');
            this.handlingType = ko.observable('');
            this.deliveryNumber = ko.observable('');
            this.trailerNumber = ko.observable('');
            this.bpoNumber = ko.observable('');
            this.poNumber = ko.observable('');
            this.typesOfLabels = ko.observable('');
            this.trailerControlRecordId = ko.observable('');
            this.itemDimensionsNotValidated = ko.observable('');
            this.scheduledDate = new App.Models.Search.Date();
            this.arrivedDate = new App.Models.Search.Date();
            this.searchMetaDataModel = new App.Models.Search.SearchMetaData();
        }
    });

    App.Models.Search.Date = Backbone.Model.extend({
        initialize: function () {
            this.fromSelected = ko.observable(false);
            this.from = ko.observable();
            this.toSelected = ko.observable(false);
            this.to = ko.observable();
        }
    });

    return App.Models.Search.SearchCriteria;
});