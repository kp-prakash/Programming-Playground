define(['app', 'backbone'], function (app, Backbone){
    if(app.SampleModel){
        return app.SampleModel;
    }

    app.SampleModel = Backbone.Model.extend({
        defaults:{
            'message': Math.random()
        }
    });

    return app.SampleModel;
});