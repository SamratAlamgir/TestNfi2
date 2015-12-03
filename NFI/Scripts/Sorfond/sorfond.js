$(document).ready(function () {
    var $validator = $("#commentForm").validate();
    function validateActiveTab() {
        var $valid = true;
        $('#rootwizard .tab-pane.active :input').not(':button,:hidden').each(function (index, element) {
            var v = $validator.element(element);
            $valid = $valid && v;
            if (!v)
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

    $('#rootwizard input[type=file]').change(function (event) {
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

    function revalidateForm(divSelector) {
        var form = $(divSelector).closest("form");
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);
        $validator = $("#commentForm").validate();
    }
    function addRowOnClick(divId) {
        var divSelector = "#" + divId;
        var itemCount = $(divSelector + " > div").length;
        var templateHtml = $(divSelector + " > div").not($(".text-danger.field-validation-error > span")).last().html();
        var updatedHtml = templateHtml.replace(/\[\d+\]/g, "[" + itemCount + "]");
        $("#" + divId).append("<div class='well'> " + updatedHtml + "</div>");
        revalidateForm(divSelector);
    }
    function subtractOnClick(divId) {
        var itemCount = $("#" + divId + " > div").length;
        if (itemCount > 1) {
            $("#" + divId + " > div").last().remove();
            revalidateForm("#" + divId);
        }
    }
    $("#rootwizard [name='addVisualItemButton']").click(function () {
        var divId = $(this).parents().eq(1).siblings().attr('id');
        addRowOnClick(divId);
    });
    $("#rootwizard [name='removeVisualItemButton']").click(function () {
        var divId = $(this).parents().eq(1).siblings().attr('id');
        subtractOnClick(divId);
    });
});

