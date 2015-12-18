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
    //functions from unobtrusive:
    function setValidationValues(options, ruleName, value) {
        options.rules[ruleName] = value;
        if (options.message) {
            options.messages[ruleName] = options.message;
        }
    }
    var formEls;
    function getUniqueFormId(form) {
        if (typeof (formEls === 'undefined')) {
            formEls = document.getElementsByTagName('form');
        }
        return 'form' + Array.prototype.indexOf.call(formEls, form);
    }
    //from jQuery validation
    function findByName(name, form) {
        // select by name and filter by form for performance over form.find("[name=...]")
        return jQuery(document.getElementsByName(name)).map(function (index, element) {
            return element.form == form && element.name == name && element || null;
        });
    }
    jQuery.validator.addMethod('requiredgroup', function (value, element, params) {
        for (var i = 0; i < params.length; i++) {
            if (params[i].checked) { return true; }
        }
        return false;
    });
    var valGroups = [];
    jQuery.validator.unobtrusive.adapters.add('requiredgroup', function (options) {
        var groupName = options.element.name,
            uniqueGroup = getUniqueFormId(options.form) + groupName;
        if (!valGroups[uniqueGroup]) {
            valGroups[uniqueGroup] = findByName(groupName, options.form);
        }
        //jQuery Validation Plugin 1.9.0 only ever validates first chekcbox in a list
        //could probably work around setting this for every element:
        setValidationValues(options, 'requiredgroup', valGroups[uniqueGroup]);
    });
}(jQuery));