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
                            $(nTd).html("<Button data-appId='" + oData.AppId + "'>Archive</Button>");
                        } else {
                            $(nTd).html("<h4><span class='label label-warning'>Archived</span></h4>");
                        }
                    }
                }
            ],
            "columnDefs": [{ "targets": 4, "orderable": false, "searchable": false }]
        });

        $('#applicationListTable tbody').on('click', 'button', function () {
            var appId = this.getAttribute("data-appid");
            markAsArchive(appId);
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

        bootbox.confirm("Are you sure you want to move this application to Archive?", function (result) {
            if (!result) return; // do nothing

            $.post("/admin/MarkAsArchive", { "appId": appId })
                .done(function () {
                    loadGridData();
                })
                .fail(function () {
                    alert("Fail");
                });
        });
    };

    $("#searchButton").click(function () {
        loadGridData();
    });

    loadGridData();
});