$(function () {

    var setGridData = function (data) {
        var table = $('#applicationListTable').DataTable({
            "iDisplayLength": 25,
            "aaData": data,
            "bDestroy": true,
            "aoColumns": [
                { "mDataProp": "Name" },
                { "mDataProp": "Email" },
                { "mDataProp": "Sex" },
                { "mDataProp": "Company" },
                {
                    "mDataProp": "ZipFilePath",
                    "fnCreatedCell": function (nTd, sData, oData) {
                        $(nTd).html("<a href='/" + oData.ZipFilePath + "'>Download</a>");
                    }
                },
                {
                    "mDataProp": "IsArchived",
                    "fnCreatedCell": function (nTd, sData, oData) {

                        if (!oData.IsArchived) {
                            $(nTd).html("<Button>Archive</Button>");
                        } else {
                            $(nTd).html("<h4><span class='label label-warning'>Archived</span></h4>");
                        }
                    }
                }
            ],
            "columnDefs": [{ "targets": 4, "orderable": false, "searchable": false }]
        });

        $('#applicationListTable tbody').on('click', 'button', function () {
            var data = table.row($(this).parents('tr')).data();
            markAsArchive(data.AppId);
        });
    }

    var loadGridData = function () {
        var appType = $("#appType").val();
        var includeArchive = $("#includeArchive").prop("checked");

        $.getJSON("/admin/GetApplications", { "appType": appType, "includeArchive": includeArchive })
            .done(function (data) {
                setGridData(data);
            })
            .fail(function () {
                alert("Fail");
            });
    }

    var markAsArchive = function (appId) {

        if (!confirm("Are you sure to mark this application as Archive?"))
            return;

        $.post("/admin/MarkAsArchive", { "appId": appId })
       .done(function () {
           loadGridData();
       })
        .fail(function () {
            alert("Fail");
        });
    };

    $("#searchButton").click(function () {
        loadGridData();
    });

    loadGridData();
});