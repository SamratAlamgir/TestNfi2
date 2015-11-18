$(function () {

    $.getJSON("/admin/GetApplications")
        .done(function (data) {

            $('#example').DataTable({
                "aaData": data,
                "aoColumns": [
                    { "mDataProp": "Name" },
                    { "mDataProp": "Email" },
                    { "mDataProp": "Sex" },
                    { "mDataProp": "Company" },
                    { "mDataProp": "FilePath" }
                ]
            });

        })
    .fail(function () {
        alert("Fail");
    });
});