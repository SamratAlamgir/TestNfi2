$(function () {
    var eventSubscribed = false;

    var setGridData = function (data) {
        var table = $('#applicationListTable').DataTable({
            "iDisplayLength": 25,
            "aaData": data,
            //"aaSorting": [],
            "order": [[ 3, "desc" ]],
            "bDestroy": true,
            "aoColumns": [
                { "mDataProp": "AppType" },
                { "mDataProp": "ApplicantName" },
                { "mDataProp": "Email" },
                {
                    "mDataProp": "CreateTime",
                    "fnCreatedCell": function (nTd, sData, oData) {
                        var date = new Date(parseInt(sData.substr(6)));
                        $(nTd).html(date.getDate() + "." + (date.getMonth() + 1) + "." + date.getFullYear());
                    }
                },
                
                {
                    "mDataProp": "AppId",
                    "fnCreatedCell": function (nTd, sData, oData) {
                        //$(nTd).html("<a href='/" + oData.ZipFilePath + "'>Download</a>");
                        $(nTd).html("<Button data-action='download' data-appType='" + oData.AppTypeId + "' data-appId='" + oData.AppId + "'>Download</Button>");
                    }
                },
                {
                    "mDataProp": "IsArchived",
                    "fnCreatedCell": function (nTd, sData, oData) {

                        if (!oData.IsArchived) {
                            $(nTd).html("<Button data-action='archive' data-appType='" + oData.AppTypeId + "' data-appId='" + oData.AppId + "'>Archive</Button>");
                        } else {
                            $(nTd).html("<h4><span class='label label-warning'>Archived</span></h4>");
                        }
                    }
                }
            ],
            "columnDefs": [{ "targets": 4, "orderable": false, "searchable": false }]
        });

        if (!eventSubscribed) {
            $('#applicationListTable tbody').on('click', 'button', function () {
                var actionType = this.getAttribute("data-action");
                var appType = this.getAttribute("data-appType");
                var appId = this.getAttribute("data-appId");
                //markAsArchive(appId);

                if (actionType === 'download') {
                    downloadZipFile(appType, appId);
                }
                else if (actionType === 'archive') {
                    markAsArchive(appType, appId);
                }

            });

            eventSubscribed = true;
        }
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

    var downloadZipFile = function(appType, appId) {
        //$.get("/admin/DownloadZipFile/" + appType + "/" + appId);
        document.location = "/admin/DownloadZipFile/" + appType + "/" + appId;
    }

    var markAsArchive = function (appType, appId) {

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