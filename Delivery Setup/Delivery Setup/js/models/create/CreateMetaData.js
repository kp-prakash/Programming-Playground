/*
* File: js\models\create\CreateMetaData.js
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
    'knockout'
], function (_, Backbone, ko) {

    //Backbone model for CreateMetaData to be populated in the CreateDelivery screen.
    App.Models.Create.CreateMetaData = Backbone.Model.extend({

        //Initializes Backbone Model.
        initialize: function () {
            this.subCenters = new App.Models.Create.SubCenters();
            this.loadType = new App.Models.Create.LoadType();
            this.wareHouseArea = new App.Models.Create.WarehouseArea();
            this.trailerType = new App.Models.Create.TrailerType();
        }

    });

    /*
    * As of now this meta data is hard coded, this is supposed to be returned by
    * service call, as in Search Delivery Screen.
    */

    App.Models.Create.SubCenters = Backbone.Model.extend({

        //Initializes Backbone Model.
        initialize: function () {
            this.subCenterCodes = ko.observableArray([
                { name: 'Sub Center1', value: '1' },
                { name: 'Sub Center2', value: '2' },
                { name: 'Sub Center3', value: '3' },
                { name: 'Sub Center4', value: '4' },
                { name: 'Sub Center5', value: '5' }
            ]);
        }

    });

    App.Models.Create.LoadType = Backbone.Model.extend({

        //Initializes Backbone Model.
        initialize: function () {
            this.loadTypes = ko.observableArray([
                { name: '1 - Slip Sheet', value: '1' },
                { name: '2 - Slip Sheet', value: '2' },
                { name: '3 - Slip Sheet', value: '3' },
                { name: '4 - Slip Sheet', value: '4' },
                { name: '5 - Slip Sheet', value: '5' }

            ]);
        }
    });

    App.Models.Create.WarehouseArea = Backbone.Model.extend({

        //Initializes Backbone Model.
        initialize: function () {
            this.wareHouseCodes = ko.observableArray([
                { name: '1 - Dry-Dc', value: '1' },
                { name: '2 - Dry-Dc', value: '2' },
                { name: '3 - Dry-Dc', value: '3' },
                { name: '4 - Dry-Dc', value: '4' },
                { name: '5 - Dry-Dc', value: '5' }
            ]);
        }
    });

    App.Models.Create.TrailerType = Backbone.Model.extend({

        //Initializes Backbone Model.
        initialize: function () {
            this.trailerTypes = ko.observableArray([
                { name: '135 - 40 Cont Dry', value: '1' },
                { name: '136 - 40 Cont Dry', value: '2' },
                { name: '137 - 40 Cont Dry', value: '3' }
           ]);
        }
    });

    return App.Models.Create.CreateMetaData;
});