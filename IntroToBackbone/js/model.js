var Person = Backbone.Model.extend({
    defaults: {
        name: 'Srihari',
        nationality: 'Indian'
    },

    initialize: function(options) {
        if(options) {
            this.set('name', options.name);
            this.set('nationality', options.nationality);
        }

        this.on('change:name', function (model) {
            alert('Name changed to : ' + model.get('name'));
        });

        this.on('change:nationality', function (model) {
            alert(model.get('name') + ' nationality changed to : ' + model.get('nationality'));
        });
    },

    getName: function() {
        return 'Name: ' + this.get('name');
    },

    getNationality: function () {
        return 'Nationality: ' + this.get('nationality');
    }
});