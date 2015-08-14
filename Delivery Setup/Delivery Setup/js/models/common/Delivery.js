/*
* File: js\models\common\Delivery.js
* Authors: Srihari.Sridharan@cognizant.com; Renga.PrasadV@cognizant.com
* Reviewer: Ganesh.Kondal@cognizant.com
* Copyrights: Cognizant 2013
*/

'use strict';

//Namespace Definitions
var App = App || {};
App.Models = App.Models || {};
App.Models.Common = App.Models.Common || {};
App.Collections = App.Collections || {};
App.Collections.Common = App.Collections.Common || {};

define([
    'underscore',
    'backbone',
    'knockout',
    '../../models/common/DeliveryDates',
    '../../models/common/PrintOptions',
    '../../collections/common/PurchaseOrders',
    '../../models/create/CreateMetaData'
], function (_, Backbone, ko, DeliveryDates, PrintOptions, PurchaseOrders, CreateMetaData) {

    //Backbone model representing a Delivery. Represents a delivery appointment.
    App.Models.Common.Delivery = Backbone.Model.extend({

        // Returns the URL based on a state of an object.
        // For a new object it is going to be the POST url pattern.
        // For an existing object it is either GET, PUT or DELETE url pattern.
        urlRoot: function () {
            if (this.deliveryNumber()) {
                return '/gls/deliveries/' + this.deliveryNumber();
            } else {
                return '/gls/deliveries';
            }
        },

        //Initializes Backbone Model.
        initialize: function (id) {
            //Populate default data - just for convenience during data entry.
            this.subCenter = ko.observable('1');
            this.deliveryNumber = ko.observable(id);
            this.createdBy = ko.observable('SR');
            this.deliveryStatus = ko.observable('');
            this.loadType = ko.observable('');
            this.warehouseArea = ko.observable('1');
            this.trailerNumber = ko.observable('TRLU');
            this.trailerStatus = ko.observable('');
            this.trailerType = ko.observable('');
            this.trailerControlRecordId = ko.observable('1');
            this.yardZone = ko.observable('');
            this.receivingDoor = ko.observable('');
            this.inboundSealNumber = ko.observable('123');
            this.carrierCode = ko.observable('PAP');
            this.carrierPhone = ko.observable('');
            this.requestedBy = ko.observable('user1');
            this.dock = ko.observable('');
            this.numberOfPallets = ko.observable('1');
            this.numberOfPOs = ko.observable('');
            this.numberOfOpenPOLines = ko.observable('');
            this.nonConveyable = ko.observable(true);
            this.consolidator = ko.observable(true);
            this.fbq = ko.observable('');
            this.weight = ko.observable('');
            this.cube = ko.observable('');
            this.asnPacks = ko.observable('');
            this.lastUpdatedBy = ko.observable('');
            this.mdseType = ko.observable('');
            this.lastUpdatedDateTime = ko.observable(new Date());
            this.autoCreateDelivery = ko.observable('');
            this.deliveryDates = new App.Models.Common.DeliveryDates();
            //As of now using ko.observableArray()
            this.purchaseOrders = ko.observableArray(); // new App.Collections.Common.PurchaseOrders();
            this.link = ko.observable('#ViewDelivery/' + id);
            this.Title = ko.observable('ViewDelivery');
        }

    });

    return App.Models.Common.Delivery;
});