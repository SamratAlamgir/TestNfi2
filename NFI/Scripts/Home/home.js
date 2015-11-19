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

    var filesToUpload = new Array();

    $('#rootwizard').bootstrapWizard({
        onNext: wizardOnTabChange,
        onTabShow: wizardOnTabShow
    });

    //$('#rootwizard .finish').click(function () {
    //    var $valid = $("#commentForm").valid();
    //    if (!$valid) {
    //        $validator.focusInvalid();
    //        return false;
    //    }
    //    $("#ajaxLoader").show();
    //    $("#wizardBtn").hide();
    //    $.post("/home/SumitUserInfoWithFile"
    //        ,
    //        {
    //            name: $("#namefield").val(),
    //            email: $("#emailfield").val(),
    //            sex: $('input:radio[name="sexradio"]:checked', '#commentForm').val(),
    //            company: $("#companyfield").val(),
    //            fileDtos: filesToUpload,
    //        }).done(function (data) {
    //            if (data.IsSuccess) {
    //                clear_form_elements("tab-pane");
    //                $('#rootwizard').find("a[href*='tab1']").trigger('click');
    //                filesToUpload = new Array();
    //                $('#fileList ol').empty();
    //                $('#fileList').addClass('hide');
    //            }
    //        }).fail(function (error) {
    //            alert("error");
    //            console.log(error);
    //        }).always(function () {
    //            $("#ajaxLoader").hide();
    //            $("#wizardBtn").show();
    //        });
    //});

    

    //$('#fileUpload').on('change', function (sender) {

    //    var files = sender.target.files;

    //    $.each(files, function (index, file) {
    //        var reader = new FileReader();
    //        reader.onload = function (event) {
    //            $('.file-input-name').hide();
    //           var  object = {};
    //            object.Name = file.name;
    //            object.Content = event.target.result;
    //            filesToUpload.push(object);
    //            $('#fileList ol').append("<li>" + file.name + "</li>");
    //            $('#fileList').removeClass('hide');

    //        };
    //        reader.readAsDataURL(file);
    //    });
    //});

  
    

    function clear_form_elements(class_name) {
        jQuery("." + class_name).find(':input').each(function () {
            switch (this.type) {
                case 'password':
                case 'text':
                case 'textarea':
                case 'file':
                case 'select-one':
                case 'select-multiple':
                    jQuery(this).val('');
                    break;
                case 'checkbox':
                    //case 'radio':
                    //    this.checked = false;
            }
        });
    }


});