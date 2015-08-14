/*
* File: js\views\ViewDelivery.js
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
	'knockout',
	'text!templates/viewdelivery/ViewDelivery.html',
    'text!templates/viewdelivery/Delivery.html',
	'text!templates/viewdelivery/DateTime.html',
    'text!templates/common/PrintOptions.html',
	'text!templates/viewdelivery/PurchaseOrderDetails.html',
	'../models/view/ViewDelivery'
], function ($, _, Backbone, ko, viewDeliveryTemplate, deliveryTemplate, dateTimeTemplate,
			 printOptionsTemplate, purchaseOrdersTemplate, ViewDelivery) {

    //Backbone view for View Delivery screen.
    App.Views.ViewDeliveryView = Backbone.View.extend({
        el: '#parentContainer',

        viewDelivery: _.template(viewDeliveryTemplate),

        delivery: _.template(deliveryTemplate),

        dateTime: _.template(dateTimeTemplate),

        printOptions: _.template(printOptionsTemplate),

        purchaseOrderDetails: _.template(purchaseOrdersTemplate),

        //Backbone events for ViewDelivery screen
        events: {
            'click #btnClose': 'btnCloseClicked'
        },

        //btnCloseClicked event to return search delivery screen
        btnCloseClicked: function () {
            var router = new App.Routers.Routes();
            router.navigate('', { trigger: true });
        },

        //Initializes Backbone view and renders data.
        initialize: function (options) {
            this.render();
            this.viewDeliveryModel = new App.Models.View.ViewDelivery(options.id);
            var viewDeliveryModel = this.viewDeliveryModel;
            viewDeliveryModel.delivery.fetch({
                success: function (viewDeliveryFetch) {
                    var delivery = viewDeliveryFetch.attributes.delivery;
                    var purchaseOrders = viewDeliveryFetch.attributes.delivery.purchaseOrders;
                    var deliveryModel = viewDeliveryModel.delivery;
                    copyDeliveryDetails(deliveryModel, delivery);
                    processPurchaseOrders(purchaseOrders);
                    ko.applyBindings(viewDeliveryModel);
                },
                error: function () {
                    alert('View Delivery Error!');
                }
            });

            //Copy delivery details.
            function copyDeliveryDetails(deliveryModel, delivery) {
                var lastUpdatedDateTime = delivery.lastUpdatedDateTime.substring(0, 10)
                                            ? delivery.lastUpdatedDateTime.substring(0, 10)
                                            : "";

                deliveryModel.carrierCode = ko.observable(delivery.carrierCode);
                deliveryModel.consolidator = ko.observable((delivery.consolidator === 'Y'));
                deliveryModel.nonConveyable = ko.observable((delivery.nonConveyable === 'Y'));
                deliveryModel.createdBy = ko.observable(delivery.createdBy);
                deliveryModel.deliveryDates.arrivedDate = ko.observable(delivery.deliveryDates.arrivedDate);
                deliveryModel.deliveryDates.scheduledDate = ko.observable(delivery.deliveryDates.scheduledDate);
                deliveryModel.deliveryDates.notifyDate = ko.observable(delivery.deliveryDates.notifyDate);
                deliveryModel.deliveryNumber = ko.observable(delivery.deliveryNumber);
                deliveryModel.inboundSealNumber = ko.observable(delivery.inboundSealNumber);
                deliveryModel.lastUpdatedBy = ko.observable(delivery.lastUpdatedBy);
                deliveryModel.lastUpdatedDateTime = ko.observable(lastUpdatedDateTime);
                deliveryModel.numberOfPOs = ko.observable(delivery.numberOfPOs);
                deliveryModel.numberOfPallets = ko.observable(delivery.numberOfPallets);
                deliveryModel.requestedBy = ko.observable(delivery.requestedBy);
                deliveryModel.subCenter = ko.observable(delivery.subCenter);
                deliveryModel.trailerControlRecordId = ko.observable(delivery.trailerControlRecordId);
                deliveryModel.trailerNumber = ko.observable(delivery.trailerNumber);
                deliveryModel.warehouseArea = ko.observable(delivery.warehouseArea);
            }

            function processPurchaseOrders(purchaseOrders) {
                //Construct Table
                for (var purchaseOrder in purchaseOrders) {
                    pushPurchaseOrderDetails(purchaseOrders[purchaseOrder]);
                }
            }

            function pushPurchaseOrderDetails(purchaseOrders) {
                var purchaseOrder = new App.Models.Common.PurchaseOrder();
                purchaseOrder.poNumber = ko.observable(purchaseOrders.poNumber);
                purchaseOrder.poType = ko.observable(purchaseOrders.poType);
                purchaseOrder.buNumber = ko.observable(purchaseOrders.buNumber);
                purchaseOrder.departmentNumber = ko.observable(purchaseOrders.departmentNumber);
                purchaseOrder.receiverNumber = ko.observable(purchaseOrders.receiverNumber);
                purchaseOrder.countryCode = ko.observable(purchaseOrders.countryCode);
                viewDeliveryModel.delivery.purchaseOrders.push(purchaseOrder);
            }
        },

        render: function () {
            this.$el.html(this.viewDelivery);
            $("#delivery").html(this.delivery);
            $("#dateTime").html(this.dateTime);
            $("#printOptions").html(this.printOptions);
            $("#purchaseOrderOptions").html(this.purchaseOrderDetails);
        }

    });

    return App.Views.ViewDeliveryView;

});