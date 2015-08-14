/*
* File: js\models\create\CreateDelivery.js
* Authors: Srihari.Sridharan@cognizant.com; Renga.PrasadV@cognizant.com
* Reviewer: Ganesh.Kondal@cognizant.com
* Copyrights: Cognizant 2013
*/

'use strict';

//Namespace Definitions.
var App = App || {};
App.Models = App.Models || {};
App.Models.Create = App.Models.Create || {};

define([
    'underscore',
    'backbone',
    'knockout',
    '../../models/common/Delivery',
    '../../models/create/CreateMetaData'
], function (_, Backbone, ko, Delivery, CreateMetaData) {

    //Backbone view model for CreateDelivery screen.
    App.Models.Create.CreateDelivery = Backbone.Model.extend({

        //Initializes Backbone Model.
        initialize: function () {
            this.delivery = new App.Models.Common.Delivery();
            this.createMetaData = new App.Models.Create.CreateMetaData();
        }
    });

    return App.Models.Create.CreateDelivery;
});