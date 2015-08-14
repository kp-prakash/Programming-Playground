define([
    'marionette', 'radio'], function (Marionette, Radio) {

    var app = new Marionette.Application();

    app.addRegions({
        //Header
        headerRegion: "#header-region",
        //Main
        mainRegion: "#main-region",
        //Footer
        footerRegion: "#footer-region"
    });

    app.navigate = function (route, options){
        options|| (options = {});
        Backbone.history.navigate(route, options);
    };

    app.getCurrentRoute = function(){
        return Backbone.history.fragment;
    };

    //Validate and create a module and the start the same.
    app.startSubModule = function (moduleName, arguments){
        var currentModule = moduleName ? app.module(moduleName) : null;

        if(app.currentModule === currentModule){
            return;
        }

        if(app.currentModule){
            app.currentModule.stop();
        }

        app.currentModule = currentModule;
        if(currentModule){
            currentModule.start(arguments);
        }
    };

    // Refer to backbone marionette's documentation: 
    // http://marionettejs.com/docs/v2.3.2/marionette.application.html#the-application-channel
    // Recreating global Channel and assigning the same into app.channel
    var globalChannel = new Backbone.Radio.Channel('global');
    app.Channel = globalChannel;

    app.on('start', function() {
        if(Backbone.history){
            Backbone.history.start();
            if(app.getCurrentRoute() === ''){
                // Load home page!
            }
            app.Channel.trigger('start');
        }
    });

    return app;
});