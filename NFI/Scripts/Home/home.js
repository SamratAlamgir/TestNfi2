$(document).ready(function () {
    $.validator.addMethod("fileUploadSize", function (value, element, param) {
        var maxFileSize = 1024 * 1024 * 100;
        var files = element.files;
        for (var i = 0; i < files.length; ++i) {
            var size = files[i].size;
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
            file1: {
                required: true,
                fileUploadSize: true
            },
            file2: {
                required: true,
                fileUploadSize: true
            }

        }
    });

    function validateActiveTab() {
        var $valid = true;
        $('#rootwizard .tab-pane.active :input').not(':button,:hidden').each(function (index, element) {
            var v = $validator.element(element);
            $valid = $valid && v;
            if (v)
                $validator.focusInvalid();
        });
        return $valid;
    }
    var wizardOnTabChange = function (tab, navigation, index) {
        return validateActiveTab();
    };
    var wizardOnTabShow = function (tab, navigation, index) {

        var $total = navigation.find('li').length;
        var $current = index + 1;
        var $percent = ($current / $total) * 100;
        $('#rootwizard').find('.bar').css({ width: $percent + '%' });

        // If it's the last tab then hide the next button
        if ($current >= $total) {
            $('#rootwizard').find('.pager .next').hide();
            $('#rootwizard').find('.pager .prev').show();
            $("#btnSubmit").show();
        } else {
            $('#rootwizard').find('.pager .next').show();
            $('#rootwizard').find('.pager .prev').hide();
            $('#btnSubmit').hide();
        }

    };
    $('#rootwizard').bootstrapWizard({
        onNext: wizardOnTabChange,
        onTabShow: wizardOnTabShow
    });

     $('#rootwizard input[type=file]').change(function(event) {
         if ($validator.element(event.target))
             $validator.focusInvalid();
     });
     $('#btnSubmit').click(function () {
         var valid = validateActiveTab();
         if (valid) {
             $("#wizardBtn").hide();
             $("#ajaxLoader").show();
         }
         return valid;
     });
});