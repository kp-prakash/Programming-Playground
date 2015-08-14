/*
* File: js\collections\common\PurchaseOrders.js
* Authors: Srihari.Sridharan@cognizant.com; Renga.PrasadV@cognizant.com
* Reviewer: Ganesh.Kondal@cognizant.com
* Copyrights: Cognizant 2013
*/

'use strict';

//Namespace Definitions
var App = App || {};
App.Collections = App.Collections || {};
App.Collections.Common = App.Collections.Common || {};
App.Models = App.Models || {};
App.Models.Common = App.Models.Common || {};

/* 
* As of now we are not using these collection classes. We have 
* implemented the collection using knockout's observableArray().
*/

define([
    'underscore',
    'backbone',
    '../../models/common/PurchaseOrder'
], function (_, Backbone, PurchaseOrder) {

    //Backbone Collection for PurchaseOrders.
    App.Collections.Common.PurchaseOrders = Backbone.Collection.extend({
        model: App.Models.Common.PurchaseOrder
    });

    return App.Collections.Common.PurchaseOrders;
});