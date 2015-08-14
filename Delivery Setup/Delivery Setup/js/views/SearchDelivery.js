/*
* File: js\views\SearchDelivery.js
* Authors: Srihari.Sridharan@cognizant.com; Renga.PrasadV@cognizant.com
* Reviewer: Ganesh.Kondal@cognizant.com
* Copyrights: Cognizant 2013
*/

'use strict';

//Namespace Definitions
var App = App || {};
App.Views = App.Views || {};
App.Models = App.Models || {};
App.Models.Search = App.Models.Search || {};

define([
	'jquery',
	'underscore',
	'backbone',
	'text!templates/searchdelivery/SearchDelivery.html',
    'text!templates/searchdelivery/SearchOptions.html',
    'text!templates/common/PrintOptions.html',
    'text!templates/searchdelivery/DeliveryDetails.html',
    'knockout',
    '../models/search/SearchDelivery',
], function ($, _, Backbone, searchDeliveryTemplate, searchOptionsTemplate, printOptionsTemplate,
        deliveryDetailsTemplate, ko, SearchDelivery) {

    //Backbone view for Search Delivery screen.
    App.Views.SearchDeliveryView = Backbone.View.extend({
        el: '#parentContainer',
        searchDelivery: _.template(searchDeliveryTemplate),
        searchOptions: _.template(searchOptionsTemplate),
        printOptions: _.template(printOptionsTemplate),
        deliveryDetails: _.template(deliveryDetailsTemplate),

        //Backbone events for Search Delivery screen
        events: {
            'click #btnRetrieve': 'btnRetrieveClicked',
            'click #btnClear': 'btnClearClicked',
            'click #btnCreateDelivery': 'btnCreateDeliveryClicked'
        },

        //btnRetrieveClicked event to retrive delivery details.
        btnRetrieveClicked: function () {
            var searchCriteria = this.searchDeliveryModel.searchCriteria;
            var searchCriteriaJSON = {
                "searchCriteria": {
                    "deliveryNumber": searchCriteria.deliveryNumber(),
                    "trailerNumber": searchCriteria.trailerNumber(),
                    "bpoNumber": searchCriteria.bpoNumber(),
                    "poNumber": searchCriteria.poNumber(),
                    "trailerControlRecordId": searchCriteria.trailerControlRecordId()
                }
            };

            var searchDeliveryModel = this.searchDeliveryModel;

            //REST call to fetch delivery details
            searchDeliveryModel.searchCriteria.save(searchCriteriaJSON,
            {
                success: function (deliveryDetailsFetch) {
                    //Clear Table
                    searchDeliveryModel.deliveries.removeAll();
                    if (deliveryDetailsFetch.attributes.deliveryListContainer) {
                        var deliveries = deliveryDetailsFetch.attributes.deliveryListContainer.deliveries;
                        //Construct Table
                        for (var returnedDelivery in deliveries) {
                            pushNewDelivery(deliveries[returnedDelivery]);
                        }
                    }
                   
                    //Clear the attributes, to ensure they are not sent back on subsequent request.
                    deliveryDetailsFetch.attributes = {};
                }, 
                error: function () {
                    alert('Error! Delivery Data Load Failed');
                }
            });

            //To push delivery detail data to the table
            function pushNewDelivery(deliveryData) {
                var newDelivery = new App.Models.Common.Delivery(deliveryData.deliveryNumber);
                newDelivery.subCenter = ko.observable(deliveryData.subCenter);
                newDelivery.deliveryNumber = ko.observable(deliveryData.deliveryNumber);
                newDelivery.id = ko.observable(deliveryData.deliveryNumber);
                newDelivery.createdBy = ko.observable(deliveryData.createdBy);
                newDelivery.mdseType = ko.observable(deliveryData.mdseType);
                newDelivery.trailerNumber = ko.observable(deliveryData.trailerNumber);
                newDelivery.carrierCode = ko.observable(deliveryData.carrierCode);
                newDelivery.deliveryDates.scheduledDate = ko.observable(deliveryData.deliveryDates.scheduledDate.toString('yyyy-MM-dd'));
                newDelivery.fbq = ko.observable('');
                newDelivery.deliveryStatus = ko.observable(deliveryData.deliveryStatus);
                searchDeliveryModel.deliveries.push(newDelivery);
            }
        },

        //btnClearClicked event to clear search delivery screen
        btnClearClicked: function () {
            this.searchDeliveryModel.destroy();
            this.initialize();
        },

        //btnCreateDeliveryClicked event to go to create delivery screen
        btnCreateDeliveryClicked: function () {
            var router = new App.Routers.Routes();
            router.navigate('#/CreateDelivery', { trigger: true });
        },

        //Initializes Backbone view and renders data.
        initialize: function () {
            this.render();
            this.searchDeliveryModel = new App.Models.Search.SearchDelivery();
            var searchDeliveryModel = this.searchDeliveryModel;
            var that = this.searchDeliveryModel.searchCriteria;
            this.searchDeliveryModel.searchCriteria.searchMetaDataModel.fetch({
                success: function (searchMetaDataFetch) {
                    var currentSearchMetaData = searchMetaDataFetch.attributes.searchMetaData;
                    that.searchMetaData = {};
                    that.searchMetaData.deliveryStatus = ko.observableArray(currentSearchMetaData.deliveryStatus);
                    that.searchMetaData.handlingTypes = ko.observableArray(currentSearchMetaData.handlingTypes);
                    that.searchMetaData.typeOfLabels = ko.observableArray(currentSearchMetaData.typeOfLabels);
                    that.searchMetaData.mdseTypes = ko.observableArray(currentSearchMetaData.mdseTypes);
                    that.searchMetaData.subCenters = ko.observableArray(currentSearchMetaData.subCenters);
                    that.searchMetaData.trailerStatus = ko.observableArray(currentSearchMetaData.trailerStatus);
                    ko.applyBindings(searchDeliveryModel);
                },
                error: function () {
                    alert('Error! Search Meta Data Load Failed');
                }
            });
        },

        render: function () {
            this.$el.html(this.searchDelivery);
            $("#searchOptions").html(this.searchOptions);
            $("#printOptions").html(this.printOptions);
            $("#deliveryDetails").html(this.deliveryDetails);
            
        }
               
    });

    return App.Views.SearchDeliveryView;
});
