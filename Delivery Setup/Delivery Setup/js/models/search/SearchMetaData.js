/*
* File: js\models\search\SearchMetaData.js
* Authors: Srihari.Sridharan@cognizant.com; Renga.PrasadV@cognizant.com
* Reviewer: Ganesh.Kondal@cognizant.com
* Copyrights: Cognizant 2013
*/

'use strict';
var App = App || {};
App.Models = App.Models || {};
App.Models.Search = App.Models.Search || {};

define([
    'underscore',
    'backbone',
    'knockout'
], function (_, Backbone, ko) {

    App.Models.Search.SearchMetaData = Backbone.Model.extend({
        urlRoot: '/gls/metadata/search',
    });

    return App.Models.Search.SearchMetaData;
});