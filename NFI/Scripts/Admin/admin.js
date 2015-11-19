$(function () {

    $.getJSON("/admin/GetApplications")
        .done(function (data) {

            var table = $('#applicationListTable').DataTable({
                "iDisplayLength": 25,
                "aaData": data,
                "aoColumns": [
                    { "mDataProp": "Name" },
                    { "mDataProp": "Email" },
                    { "mDataProp": "Sex" },
                    { "mDataProp": "Company" },
                    {
                        "mDataProp": "FilePath",
                        "fnCreatedCell": function (nTd, sData, oData) {
                            $(nTd).html("<a href='/" + oData.FilePath + "'>Download</a>");
                        }
                    }
                ],
                "columnDefs": [{ "targets": 4, "orderable": false, "searchable": false }]
            });
        })
    .fail(function () {
        alert("Fail");
    });
});