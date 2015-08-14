var PersonView = Backbone.View.extend({
    el: $("#person"),
    
    events: {
        "click p": "updateModel"  
    },

    personTemplate: _.template($('#person-template').html()),

    initialize: function() {
        this.model.on('change',this.render,this);
        this.render();
    },

    render: function() {
        this.$el.html(this.personTemplate(this.model.toJSON()));
        return this;
    },

    updateModel: function () {
        this.model.set('name', 'SRIHARI');
    }
});