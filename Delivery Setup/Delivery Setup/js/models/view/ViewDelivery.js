/*
* File: js\models\view\ViewDelivery.js
* Authors: Srihari.Sridharan@cognizant.com; Renga.PrasadV@cognizant.com
* Reviewer: Ganesh.Kondal@cognizant.com
* Copyrights: Cognizant 2013
*/

'use strict';
var App = App || {};
App.Models = App.Models || {};
App.Models.View = App.Models.View || {};

define([
    'underscore',
    'backbone',
    'knockout',
    '../../models/view/ViewMetaData',
    '../../models/common/PrintOptions',
    '../../models/common/Delivery'
], function (_, Backbone, ko, ViewMetaData, PrintOptions, Delivery) {

    App.Models.View.ViewDelivery = Backbone.Model.extend({
        initialize: function (id) {
            this.viewMetaData = new App.Models.View.ViewMetaData();
            this.printOptions = getPrintOptions();
            this.delivery = new App.Models.Common.Delivery(id);
        }
    });

    function getPrintOptions() {
        var printOptions = new App.Models.Common.PrintOptions();
        printOptions.asnPOLines.isEnabled = ko.observable(false);
        printOptions.entireDelivery.isSelected = ko.observable(true);
        printOptions.unloaderReferenceSheet.isSelected = ko.observable(true);
        printOptions.receiver.isSelected = ko.observable(true);
        printOptions.receivingManifest.isSelected = ko.observable(true);
        return printOptions;
    };

    return App.Models.View.ViewDelivery;
});