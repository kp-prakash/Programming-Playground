/*
* File: js\views\CreateDelivery.js
* Authors: Srihari.Sridharan@cognizant.com; Renga.PrasadV@cognizant.com
* Reviewer: Ganesh.Kondal@cognizant.com
* Copyrights: Cognizant 2013
*/

'use strict';

//Namespace Definitions
var App = App || {};
App.Views = App.Views || {};

define([
	'jquery',
	'underscore',
	'backbone',
	'text!templates/createdelivery/CreateDelivery.html',
    'text!templates/createdelivery/DataEntry.html',
    'text!templates/createdelivery/DateTime.html',
    'text!templates/createdelivery/PurchaseOrderDetails.html',
    'knockout',
    '../models/create/CreateDelivery',
    '../routers/router'
], function ($, _, Backbone, createDeliveryTemplate, dataEntryTemplate, dateTimeTemplate, purchaseOrderDetailsTemplate, ko, Router) {

    //Backbone view for CreateDelivery screen.
    App.Views.CreateDeliveryView = Backbone.View.extend({
        el: '#parentContainer',

        createDelivery: _.template(createDeliveryTemplate),

        dataEntry: _.template(dataEntryTemplate),

        dateTime: _.template(dateTimeTemplate),

        purchaseOrderDetails: _.template(purchaseOrderDetailsTemplate),

        //Initializes Backbone view and renders data.
        initialize: function () {
            this.render();
            this.createDeliveryModel = new App.Models.Create.CreateDelivery();
            setDummyData(this);
            ko.applyBindings(this.createDeliveryModel);
        },

        render: function () {
            this.$el.html(this.createDelivery);
            $("#dataEntry").html(this.dataEntry);
            $("#dateTimeEntry").html(this.dateTime);
            $("#purchaseOrderDetails").html(this.purchaseOrderDetails);
        },

        //Backbone events for CreateDelivery screen
        events: {
            'click #btnSave': 'btnSaveClicked',
            'click #btnClose': 'btnCloseClicked',
            'click #btnClear': 'btnClearClicked',
            'click #chkDefaultDate': 'chkDefaultDateClicked',
            'click #btnAddPO': 'btnAddPOClicked',
            'click #btnRemovePO': 'btnRemovePOClicked'
        },

        //btnSaveClicked event to create a new delivery.
        btnSaveClicked: function () {
            var delivery = this.createDeliveryModel.delivery;
            var createJSON = {
                "delivery": {
                    "createdBy": "chandra",
                    "carrierCode": delivery.carrierCode(),
                    "trailerNumber": delivery.trailerNumber(),
                    "trailerControlRecordId": "1",
                    "subCenter": delivery.subCenter(),
                    "deliveryDates": {
                        "scheduledDate": delivery.deliveryDates.scheduledDate().toString('yyyy-MM-dd'),
                        "arrivedDate": delivery.deliveryDates.arrivedDate().toString('yyyy-MM-dd'),
                        "notifyDate": delivery.deliveryDates.notifyDate().toString('yyyy-MM-dd')
                    },
                    "requestedBy": delivery.requestedBy(),
                    "warehouseArea": delivery.warehouseArea(),
                    "inboundSealNumber": delivery.inboundSealNumber(),
                    "numberOfPallets": delivery.numberOfPallets(),
                    "nonConveyable": delivery.nonConveyable() ? "Y" : "N",
                    "consolidator": delivery.consolidator() ? "Y" : "N",
                    "purchaseOrders": [{
                        "poNumber": "1234567"
                    }]
                }
            };

            //REST call to create a new delivery
            delivery.save(createJSON,
            {
                success: function () {
                    alert('Created new delivery!');
                    navigateToSearch();
                },
                error: function () {
                    alert('Error! Delivery Data Load Failed');
                    navigateToSearch();
                }
            });

            function navigateToSearch() {
                //The below code clears the knockout binding applied 
                //to the view and navigates back to search.
                ko.cleanNode(document.body);
                var router = new App.Routers.Routes();
                router.navigate('', { trigger: true });
            }
        },

        //btnCloseClicked event to return search delivery screen
        btnCloseClicked: function () {
            var router = new App.Routers.Routes();
            router.navigate('', { trigger: true });
        },

        //btnClearClicked event to clear create delivery screen
        btnClearClicked: function () {
            this.createDeliveryModel.destroy();
            this.initialize();
        },

        //btnAddPOClicked event to add purchase order details
        btnAddPOClicked: function () {
            var i = this.createDeliveryModel.delivery.purchaseOrders().length;
            var purchaseOrder = new App.Models.Common.PurchaseOrder();
            purchaseOrder.buNumber = ko.observable('BU ' + (i + 1));
            purchaseOrder.poNumber = ko.observable(10251 + i);
            purchaseOrder.fbq = ko.observable(100 + i);
            purchaseOrder.bolNumber = ko.observable('TEST');
            purchaseOrder.asnPacks = ko.observable(12345 + i);
            purchaseOrder.freeAstray = ko.observable((i % 2) === 0);
            purchaseOrder.poType = ko.observable('23');
            this.createDeliveryModel.delivery.purchaseOrders.push(purchaseOrder);
        },

        //btnRemovePOClicked event to remove purchase order details
        btnRemovePOClicked: function () {
            if (this.createDeliveryModel.delivery.purchaseOrders().length === 0) {
                alert('No more purchase orders in this delivery!', 'Delivery Setup');
                return;
            }
            this.createDeliveryModel.delivery.purchaseOrders.pop();
        },

        //chkDefaultDateClicked event to set default date
        chkDefaultDateClicked: function (event) {
            var checkBox = $('#chkDefaultDate');
            var checked = event.currentTarget ? event.currentTarget.checked : false;
            alert("Write Code to Set Date on Date Controls. Current State: " + checked);
        }

    });

    //To push data to purchase order table
    function setDummyData(createDeliveryView) {

        for (var i = 0; i < 10; ++i) {
            var purchaseOrder = new App.Models.Common.PurchaseOrder();
            purchaseOrder.buNumber = ko.observable('BU ' + (i + 1));
            purchaseOrder.poNumber = ko.observable(10251 + i);
            purchaseOrder.fbq = ko.observable(100 + i);
            purchaseOrder.bolNumber = ko.observable('TEST');
            purchaseOrder.asnPacks = ko.observable(12345 + i);
            purchaseOrder.freeAstray = ko.observable((i % 2) === 0);
            purchaseOrder.poType = ko.observable('23');
            createDeliveryView.createDeliveryModel.delivery.purchaseOrders.push(purchaseOrder);
        }
    }

    return App.Views.CreateDeliveryView;
});