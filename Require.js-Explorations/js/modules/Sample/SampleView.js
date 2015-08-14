define(['app',
    'marionette',
    'text!modules/sample/templates/SampleView.html'], 
    function (app, Marionette, sampleTemplate){
    if(app.SampleView){
        return app.SampleView;
    }

    app.SampleView = Marionette.ItemView.extend({
        ui:{
            'button': '#btnClickMe' 
        },

        initialize: function(){
            var view = this;
            view.model.on('change', function(){
                view.render();
            });
        },

        events: {
            'click #btnClickMe': 'click'
        },

        template : _.template(sampleTemplate),

        click: function(){
            this.model.set('message', Math.random());
        }
    });

    return app.SampleView;
});