﻿$(document).ready(function() {
    var $validator = $("#MainForm").validate({
        rules: {
            emailfield: {
                required: true,
                email: true,
                minlength: 3
            },
            optradio: {
                required: true
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
            if (!v) {
                $validator.focusInvalid();
            }
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
        if ($current == 1) {
            $('#rootwizard').find('.pager .next').show();
            $('#rootwizard').find('.pager .previous').hide();
            $('#btnSubmit').hide();
        }
        else if ($current == $total) {
            $('#rootwizard').find('.pager .next').hide();
            $('#rootwizard').find('.pager .previous').show();
            $("#btnSubmit").show();
        } else {
            $('#rootwizard').find('.pager .next').show();
            $('#rootwizard').find('.pager .previous').show();
            $('#btnSubmit').hide();
        }
    };
    $('#rootwizard').bootstrapWizard({
        onNext: wizardOnTabChange,
        onTabShow: wizardOnTabShow
    });

    $('#rootwizard input[type=file]').change(function (event) {
        if ($validator.element(event.target))
            $validator.focusInvalid();
    });

    $('#btnSubmit').click(function () {
            $("#ajaxLoader").show();
       
    });
});