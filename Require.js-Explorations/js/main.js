require.config({
    
    baseUrl: 'js',
    
    paths: {
        backbone: 'dev/backbone',
        radio: 'dev/backbone.radio', 
        jquery: 'dev/jquery-2.1.3',
        marionette: 'dev/backbone.marionette',
        text: 'common/text',
        underscore: 'dev/underscore'
    },
    
    shim:{        
        underscore:{
            exports: '_'
        },
        
        backbone: {
            deps: ['jquery', 'underscore'],
            exports: 'Backbone'
        },

        marionette: {
            deps: ['backbone'],
            exports: 'Marionette'
        },

        radio:{
            deps: ['backbone'],
            exports: 'Radio'
        }
    }
});

require(['app',
    'jquery',
    'modules/Sample/SampleView',
    'modules/Sample/SampleModel'],
    function(app, $, SampleView, SampleModel) {
    
    app.Channel.on('start', function(){
        alert('Started!!!');
    });

    app.start();
    var sampleModel = new SampleModel();
    var sampleView = new SampleView({ model:sampleModel });
    app.mainRegion.show(sampleView);
});