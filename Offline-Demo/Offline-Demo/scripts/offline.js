(function() {
    if(window.applicationCache) {
        window.applicationCache.onchecking = function (e) {
            $('#status').append('\nChecking');
        };

        window.applicationCache.oncached = function (e) {
            $('#status').append('\nCached');
        };

        window.applicationCache.onnoupdate = function (e) {
            $('#status').append('\nNo Update');
        };

        window.applicationCache.onobsolete = function (e) {
            $('#status').append('\nObsolete');
        };

        window.applicationCache.ondownloading = function (e) {
            $('#status').append('\nDownloading');
        };

        window.applicationCache.onerror = function (e) {
            $('#status').append('\nError');
        };

        ////updateready is fired only when all the files are in the application cache.
        //window.applicationCache.onupdateready = function(e) {
        //    $('#status').append('\nUpdate Ready');
        //    $('#status').append('\nSwapping Cache');
        //    //Even though files are downloaded, what is in the browser memory is still an older version
        //    applicationCache.swapCache();
        //    //In order to update automatically, call location.reload() immediately after swapping cache.
        //    confirm('Do you want to refresh the app now?');
        //    location.reload();
        //};

        if ('applicationCache' in window) {
            var appCache = window.applicationCache;
            appCache.addEventListener('updateready', function () {
                appCache.swapCache();
                if (confirm('App update is available. Update now?')) {
                    window.location.reload();
                }
            }, false);
        }
    }
})();