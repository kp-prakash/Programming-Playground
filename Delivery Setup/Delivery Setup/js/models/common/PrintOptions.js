/*
* File: js\models\common\PrintOptions.js
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
    //Backbone model representing a single PrintOption.
    App.Models.Common.PrintOption = Backbone.Model.extend({

        //Initializes Backbone Model.
        initialize: function () {
            this.isSelected = ko.observable(false);
            this.isEnabled = ko.observable(true);
        }

    });

    //Backbone model representing PrintOptions.
    App.Models.Common.PrintOptions = Backbone.Model.extend({

        //Initializes Backbone Model.
        initialize: function () {
            this.entireDelivery = new App.Models.Common.PrintOption();
            this.unloaderReferenceSheet = new App.Models.Common.PrintOption();
            this.receiver = new App.Models.Common.PrintOption();
            this.allPOLines = new App.Models.Common.PrintOption();
            this.asnPOLines = new App.Models.Common.PrintOption();
            this.receivingManifest = new App.Models.Common.PrintOption();
        }

    });

    return App.Models.Common.PrintOptions;
});