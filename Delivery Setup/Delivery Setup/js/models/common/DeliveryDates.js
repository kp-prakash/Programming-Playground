/*
* File: js\models\common\DeliveryDates.js
* Authors: Srihari.Sridharan@cognizant.com; Renga.PrasadV@cognizant.com
* Reviewer: Ganesh.Kondal@cognizant.com
* Copyrights: Cognizant 2013
*/

'use strict';

//Namespace Definitions
var App = App || {};
App.Models = App.Models || {};
App.Models.Common = App.Models.Common || {};

define([
    'underscore',
    'backbone',
    'knockout'
], function (_, Backbone, ko) {

    // Backbone model representing DeliveryDates. 
    // Acts as the container for various dates in delivery appointment.
    App.Models.Common.DeliveryDates = Backbone.Model.extend({

        //Initializes Backbone Model.
        initialize: function () {
            this.scheduledDate = ko.observable(new Date());
            this.releaseDate = ko.observable(new Date());
            this.notifyDate = ko.observable(new Date());
            this.requestDate = ko.observable(new Date());
            this.arrivedDate = ko.observable(new Date());
            this.yardCheckDate = ko.observable(new Date());
            this.emptyDate = ko.observable(new Date());
            this.exitDate = ko.observable(new Date());
        }

    });

    return App.Models.Common.DeliveryDates;
});