/*
* File: js\collections\common\Deliveries.js
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
    '../../models/common/Delivery'
], function (_, Backbone, Delivery) {

    //Backbone Collection for Deliveries.
    App.Collections.Common.Deliveries = Backbone.Collection.extend({
        model: App.Models.Common.Delivery
    });

    return App.Collections.Common.Deliveries;
});