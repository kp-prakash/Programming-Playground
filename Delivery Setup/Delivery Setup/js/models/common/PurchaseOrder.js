/*
* File: js\models\common\PurchaseOrder.js
* Authors: Srihari.Sridharan@cognizant.com; Renga.PrasadV@cognizant.com
* Reviewer: Ganesh.Kondal@cognizant.com
* Copyrights: Cognizant 2013
*/

'use strict';

//Namespace Definitions.
var App = App || {};
App.Models = App.Models || {};
App.Models.Common = App.Models.Common || {};

define([
    'underscore',
    'backbone',
    'knockout'
], function (_, Backbone, ko) {

    //Backbone model representing a PurchaseOrder.
    App.Models.Common.PurchaseOrder = Backbone.Model.extend({

        //Initializes Backbone Model.
        initialize: function () {
            this.poNumber = ko.observable('');
            this.poType = ko.observable('');
            this.buNumber = ko.observable('');
            this.poEvent = ko.observable('');
            this.departmentNumber = ko.observable('');
            this.countryCode = ko.observable('');
            this.receiverNumber = ko.observable('');
            this.receiverStatus = ko.observable('');
            this.proNumber = ko.observable('');
            this.shipperNumber = ko.observable('');
            this.bolNumber = ko.observable('');
            this.fbq = ko.observable('');
            this.vnpkPODueQuantity = ko.observable('');
            this.whpkPODueQuantity = ko.observable('');
            this.eachPODueQuantity = ko.observable('');
            this.asnPacks = ko.observable('');
            this.numberOfPOLines = ko.observable('');
            this.pickupDate = ko.observable('');
            this.vendorName = ko.observable('');
            this.cancelDate = ko.observable('');
            this.mustArriveByDate = ko.observable('');
            this.paymentType = ko.observable('');
            this.freeAstray = ko.observable('');
            this.stateOfOrigin = ko.observable('');
            this.noPrimeAssigned = ko.observable('');
            this.noItemDimensionWeight = ko.observable('');
            this.Comments = ko.observable('');
        }

    });

    return App.Models.Common.PurchaseOrder;
});