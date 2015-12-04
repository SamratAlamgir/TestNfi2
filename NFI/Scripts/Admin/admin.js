$(function () {
    var eventSubscribed = false;

    var setGridData = function (data) {
        var table = $('#applicationListTable').DataTable({
            "iDisplayLength": 25,
            "aaData": data,
            //"aaSorting": [],
            "order": [[ 2, "desc" ]],
            "bDestroy": true,
            "aoColumns": [
                { "mDataProp": "ProduksjonsforetaketsNavn" },
                { "mDataProp": "OrganisasjonsNummer" },
                {
                    "mDataProp": "CreateTime",
                    "fnCreatedCell": function (nTd, sData, oData) {
                        var date = new Date(parseInt(sData.substr(6)));
                        $(nTd).html((date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear());
                    }
                },

                { "mDataProp": "OrganisasjonsPostnummer" },
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

        if (!eventSubscribed) {
            $('#applicationListTable tbody').on('click', 'button', function () {
                var appId = this.getAttribute("data-appid");
                markAsArchive(appId);
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