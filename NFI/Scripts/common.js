$(function () {
    jQuery.validator.unobtrusive.adapters.add('filesize', ['maxbytes'], function (options) {
        // Set up test parameters
        var params = {
            maxbytes: options.params.maxbytes
        };

        // Match parameters to the method to execute
        options.rules['filesize'] = params;
        if (options.message) {
            // If there is a message, set it for the rule
            options.messages['filesize'] = options.message;
        }
    });

    jQuery.validator.addMethod("filesize", function (value, element, param) {
        if (value === "") {
            // no file supplied
            return true;
        }

        var maxBytes = parseInt(param.maxbytes);

        // use HTML5 File API to check selected file size
        // https://developer.mozilla.org/en-US/docs/Using_files_from_web_applications
        // http://caniuse.com/#feat=fileapi
        if (element.files != undefined) {
            var files = element.files;
            for (var i = 0; i < files.length; ++i) {
                var size = parseInt(files[i].size);
                if (size > maxBytes) // checks the file more than 100 MB
                {
                    return false;
                }
            }
        }
        return true;
    });
}(jQuery));