$(document).ready(function () {
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
                }

            }
        });
    var wizardOnTabChange = function(tab, navigation, index) {
        var $valid = $("#commentForm").valid();
        if (!$valid) {
            $validator.focusInvalid();
            return false;
        }
        return true;
    };
    var wizardOnTabShow = function(tab, navigation, index) {
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
    var fileName = "";
    var fileContent = "";
    var maxFileSize = 1024 * 1024 * 100;
    $('#rootwizard').bootstrapWizard({
        onNext:wizardOnTabChange,
        onTabShow:wizardOnTabShow
    });

    $('#rootwizard .finish').click(function () {
        $("#ajaxLoader").show();
        $("#wizardBtn").hide();
        $.post("/home/SumitUserInfoWithFile"
            ,
            {
                name: $("#namefield").val(),
                email: $("#emailfield").val(),
                sex: $('input:radio[name="sexradio"]:checked', '#commentForm').val(),
                company: $("#companyfield").val(),
                data: fileContent,
                fileName: fileName

            }).done(function (data) {
                if (data.IsSuccess) {

                }
            }).fail(function (error) {
                alert("error");
                console.log(error);
            }).always(function () {
                $("#ajaxLoader").hide();
                $("#wizardBtn").show();
            });
    });
    function handleFileSelect(evt) {
        var files = evt.target.files; // FileList object

        if (files && files[0]) {
            var f = files[0];
            // Only process files less than or equal 100MB
            if (f.size > maxFileSize) {
                $("#uploadSizeError").show();
                return;
            } else {
                $("#uploadSizeError").hide();
            }

            var reader = new FileReader();
            // Closure to capture the file information.
            reader.onload = (function (theFile) {
                return function (e) {
                    // stroe file content
                    fileName = theFile.name;
                    fileContent = e.target.result;
                    $('#rootwizard .finish').show();
                };
            })(f);
            $('#rootwizard .finish').hide();
            reader.readAsDataURL(f);
        }
    }
    document.getElementById('fileUpload').addEventListener('change', handleFileSelect, false);

});