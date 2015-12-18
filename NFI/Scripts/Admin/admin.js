$(function () {
    var eventSubscribed = false;

    var setGridData = function (data) {
        var table = $('#applicationListTable').DataTable({
            "iDisplayLength": 25,
            "aaData": data,
            //"aaSorting": [],
            "order": [[3, "desc"]],
            "bDestroy": true,
            "aoColumns": [
                { "mDataProp": "AppType", "sTitle": "Application Type" },
                { "mDataProp": "ApplicantName", "sTitle": "Name" },
                { "mDataProp": "Email", "sTitle": "Email" },
                {
                    "mDataProp": "CreateTime", "sTitle": "Create Date",
                    "fnCreatedCell": function (nTd, sData, oData) {
                        var date = new Date(parseInt(sData.substr(6)));
                        $(nTd).html(date.getDate() + "." + (date.getMonth() + 1) + "." + date.getFullYear());
                    }
                },
                {
                    "mDataProp": "AppId", "sTitle": "View",
                    "fnCreatedCell": function (nTd, sData, oData) {
                        $(nTd).html("<a data-action='view' data-appType='" + oData.AppTypeId + "' data-appId='" + oData.AppId + "'>View</a>");
                    }
                },
                {
                    "mDataProp": "AppId", "sTitle": "Download",
                    "fnCreatedCell": function (nTd, sData, oData) {
                        $(nTd).html("<Button data-action='download' data-appType='" + oData.AppTypeId + "' data-appId='" + oData.AppId + "'>Download</Button>");
                    }
                },
                {
                    "mDataProp": "IsArchived", "sTitle": "Archive",
                    "fnCreatedCell": function (nTd, sData, oData) {

                        if (!oData.IsArchived) {
                            $(nTd).html("<Button data-action='archive' data-appType='" + oData.AppTypeId + "' data-appId='" + oData.AppId + "'>Archive</Button>");
                        } else {
                            $(nTd).html("<h4><span class='label label-warning'>Archived</span></h4>");
                        }
                    }
                }
            ],
            "columnDefs": [{ "targets": [4, 5], "orderable": false, "searchable": false }]
        });

        if (!eventSubscribed) {
            $('#applicationListTable tbody').on('click', 'button,a', function () {
                var actionType = this.getAttribute("data-action");
                var appType = this.getAttribute("data-appType");
                var appId = this.getAttribute("data-appId");

                if (actionType === 'download') {
                    downloadZipFile(appType, appId);
                }
                else if (actionType === 'archive') {
                    markAsArchive(appType, appId);
                }
                else if (actionType === 'view') {
                    showDetailView(appType, appId);
                }


            });

            eventSubscribed = true;
        }
    }

    var loadGridData = function () {
        var appType = $("#appType").val();
        var includeArchive = $("#includeArchive").prop("checked");

        $.getJSON("GetApplications", { "appType": appType, "includeArchive": includeArchive })
            .done(function (data) {
                setGridData(data);
            })
            .fail(function () {
                alert("Fail");
            });
    }

    var downloadZipFile = function (appType, appId) {
        document.location = "DownloadZipFile/" + appType + "/" + appId;
    }
    var showDetailView = function (appType, appId) {
        window.open("ShowDetail/" + appType + "/" + appId, "_blank");
    }
    var markAsArchive = function (appType, appId) {

        bootbox.confirm("Are you sure you want to move this application to Archive?", function (result) {
            if (!result) return; // do nothing

            $.post("MarkAsArchive/" + appType + "/" + appId)
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