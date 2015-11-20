$(document).ready(function () {
    $.validator.addMethod("fileUploadSize", function (value, element, param) {
        var maxFileSize = 1024 * 1024 * 100;
        var files = element.files;
        for (var i = 0; i < files.length; ++i) {
            var size = files[i];
            if (size > maxFileSize) // checks the file more than 100 MB
            {
                return false;
            }
        }
        return true;
    }, "File size must be within 100 MB");
    var $validator = $("#commentForm").validate({
        rules: {
            emailfield: {
                required: true,
                email: true,
                minlength: 3
            },
            namefield: {
                required: true,
                minlength: 3
            },
            fileUpload: {
                required: true,
                fileUploadSize: true
            }

        }
    });

    var wizardOnTabChange = function (tab, navigation, index) {
        var $valid = $("#commentForm").valid();
        if (!$valid) {
            $validator.focusInvalid();
            return false;
        }
        return true;
    };
    var wizardOnTabShow = function (tab, navigation, index) {

        var $total = navigation.find('li').length;
        var $current = index + 1;
        var $percent = ($current / $total) * 100;
        $('#rootwizard').find('.bar').css({ width: $percent + '%' });

        // If it's the last tab then hide the last button and show the finish instead
        if ($current >= $total) {
            $('#rootwizard').find('.pager .next').hide();
            $('#rootwizard').find('.pager .finish').show();
            $('#rootwizard').find('.pager .finish').removeClass('disabled');
        } else {
            $('#rootwizard').find('.pager .next').show();
            $('#rootwizard').find('.pager .finish').hide();
        }

    };

    //var filesToUpload = new Array();

    $('#rootwizard').bootstrapWizard({
        onNext: wizardOnTabChange,
        onTabShow: wizardOnTabShow
    });

    $("#file2").on('change', function () {
        debugger;
        var input = $(this),
            numFiles = input.get(0).files ? input.get(0).files.length : 1,
            label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
        input.trigger('fileselect', [numFiles, label]);
    });

    $('.btn-file :file').on('fileselect', function (event, numFiles, label) {
        debugger;
        if (numFiles >= 0 && typeof label !== 'undefined') {
       
          $("#btnSubmit").show();
      }
    });
});