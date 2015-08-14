var Contact = Backbone.Model.extend({
    defaults: {
        name: 'Srihari',
        city: 'Chennai'
    }
});

var Contacts = Backbone.Collection.extend({
    model: Contact
});
