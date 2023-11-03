
let insTypeDic = {
    "tp": "شخص ثالث",
    "life": "زندگی",
    "cb": "بدنه اتومبیل",
    "fire": "آتش سوزی",
    "lia": "مسئولیت",
    "travel": "مسافرتی"
};


function fileValidation() {
    var fileInput =
        document.getElementById('file');
    var filePath = fileInput.value;
    // Allowing file type
    var allowedExtensions = /(\.jpg|\.jpeg|\.gif|\.svg|\.png|\.webp|\.avif|\.pdf)$/i;

    if (!allowedExtensions.exec(filePath)) {

        Swal.fire({
            icon: "success",
            title: 'لطفا فایل از نوع تصویر یا pdf انتخاب کنید !',
            icon: "danger"
        });
        fileInput.value = '';
        return false;
    }
}
function Submit_InsertStatus(refreshId) {
    
    const inpObj = document.getElementById("InsNo");
    const inpPayObj = document.getElementById("PayVal");
    
    if (inpObj !== null && inpObj != "undefined") {
        if (inpObj.value == "") {
            var sp = document.getElementById("insval");  
            if (sp !== null && sp !== "ubdefined") {
                sp.innerText = "لطفا شماره بیمه نامه را وارد کنید";
                return false;
            }            
        }
    }
    if (inpPayObj !== null && inpPayObj != "undefined") {
        if (inpPayObj.value == "") {
            //alert("لطفا مبلغ حق بیمه را وارد کنید !");
            var spa = document.getElementById("sppayval");            
            if (spa !== null && spa !== "ubdefined") {
                spa.innerText = "لطفا مبلغ حق بیمه را وارد کنید";
                return false;
            }          
        }
    }
    if ($("#insertStatusForm").valid()) {
        var insertData = $("#insertStatusForm").serialize();
        $.ajax({
            url: "/UsersPanel/Reports/InsertStatus",
            data: insertData,
            type: "POST",
        }).done(function (data) {            
            if (data.isSuccess) {
                if (data.refreshPage == "no") {
                    var refreshEl = "#" + data.refreshElId;
                    var zurl = "/UsersPanel/Reports/ShowInsStatuses";

                    $.ajax({
                        url: zurl,
                        data: { insId: data.insId, res: data.isSuccess, insType: data.insType, refreshElId: data.refreshElId },
                        type: "GET",
                    }).done(function (result) {
                        Swal.fire({
                            toast: true,
                            icon: "success",
                            title: "عملیات با موفقیت انجام شد",
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                            didOpen: (toast) => {
                                toast.addEventListener('mouseenter', Swal.stopTimer)
                                toast.addEventListener('mouseleave', Swal.resumeTimer)
                            }
                        });
                        $(refreshEl).html(result);
                        document.getElementById("close-modal").click();
                    });
                }
                if (data.refreshPage == "yes") {                   
                    location.reload();
                }
            }
        });
    }
}
function Submit_InsertFinanceStatus(refreshId) {
    if ($("#insertFinanceStatusForm").valid()) {
        var insertData = $("#insertFinanceStatusForm").serialize();
        $.ajax({
            async: true,
            cache: false,
            url: "/UsersPanel/Reports/InsertFinancialStatus",
            data: insertData,
            type: "POST",
        }).done(function (data) {
            if (data.isSuccess) {
                var refreshEl = "#" + data.refreshElId;
                var zurl = "/UsersPanel/Reports/ShowInsStatuses";
                if (data.statusType == "fins") {
                    zurl = "/UsersPanel/Reports/ShowInsFinanceStatus";
                }
                $.ajax({
                    url: zurl,
                    data: { insId: data.insId, res: data.isSuccess, insType: data.insType, refreshElId: data.refreshElId },
                    type: "GET",
                }).done(function (result) {
                    Swal.fire({
                        toast: true,
                        icon: "success",
                        title: "عملیات با موفقیت انجام شد",
                        showConfirmButton: false,
                        timer: 2000,
                        timerProgressBar: true,
                        didOpen: (toast) => {
                            toast.addEventListener('mouseenter', Swal.stopTimer)
                            toast.addEventListener('mouseleave', Swal.resumeTimer)
                        }
                    });
                    $(refreshEl).html(result);
                    document.getElementById("close-modal").click();
                });

            }
        });
    }

}
function Submit_InsertComment(refreshId) {
    if ($("#insertCommentForm").valid()) {
        var insertData = $("#insertCommentForm").serialize();
        if (insertData)
            $.ajax({
                async: true,
                cashe: false,
                url: "/UsersPanel/Reports/InsertStatusComment",
                data: insertData,
                type: "POST",

            }).done(function (data) {
                var refreshEl = "#" + data.refreshElId;
                var zurl = "/UsersPanel/Reports/ShowInsStatuses";
                if (data.statusType == "fins") {
                    zurl = "/UsersPanel/Reports/ShowInsFinanceStatus";
                }
                $.ajax({
                    url: zurl,
                    data: { insId: data.insId, res: data.isSuccess, insType: data.insType, refreshElId: data.refreshElId },
                    type: "GET",
                }).done(function (result) {
                    Swal.fire({
                        toast: true,
                        icon: "success",
                        title: "عملیات با موفقیت انجام شد",
                        showConfirmButton: false,
                        timer: 2000,
                        timerProgressBar: true,
                        didOpen: (toast) => {
                            toast.addEventListener('mouseenter', Swal.stopTimer)
                            toast.addEventListener('mouseleave', Swal.resumeTimer)
                        }
                    });
                    $(refreshEl).html(result);
                    document.getElementById("close-modal").click();
                });
            });
    }

}
function Submit_EditComment(refreshId) {
    if ($("#editCommentForm").valid()) {
        var editData = $("#editCommentForm").serialize();
        var refreshId = "#" + $(this).attr("data-refid");
        $.ajax({
            url: "/UsersPanel/Reports/EditStatusComment",
            data: editData,
            type: "POST",

        }).done(function (data) {
            if (data.isSuccess) {
                var refreshEl = "#" + data.refreshElId;
                var zurl = "/UsersPanel/Reports/ShowInsStatuses";
                if (data.statusType == "fins") {
                    zurl = "/UsersPanel/Reports/ShowInsFinanceStatus";
                }
                $.ajax({
                    url: zurl,
                    data: { insId: data.insId, res: data.isSuccess, insType: data.insType, refreshElId: data.refreshElId },
                    type: "GET",
                }).done(function (result) {
                    Swal.fire({
                        toast: true,
                        icon: "success",
                        title: "عملیات با موفقیت انجام شد",
                        showConfirmButton: false,
                        timer: 2000,
                        timerProgressBar: true,
                        didOpen: (toast) => {
                            toast.addEventListener('mouseenter', Swal.stopTimer)
                            toast.addEventListener('mouseleave', Swal.resumeTimer)
                        }
                    });
                    $(refreshEl).html(result);
                    document.getElementById("close-modal").click();
                });
            }
        });
    }
}
function Submit_InsertInsSupp(refreshId) {
    if ($("#InsertInsSupplementForm").valid()) {
        var data = new FormData();
        var form_data = $("#InsertInsSupplementForm").serializeArray();
        $.each(form_data, function (key, input) {
            data.append(input.name, input.value);
        });

        //File data
        var file_data = $('input[name="File"]')[0].files;
        data.append("File", file_data[0]);
        $.ajax({
            url: "/UsersPanel/Reports/InsertInsSupplement",
            data: data,
            type: "POST",
            cache: false,
            contentType: false,
            processData: false,

        }).done(function (data) {
            if (data.isSuccess) {
                var refreshEl = "#" + data.refreshElId;
                var zurl = "/UsersPanel/Reports/ShowInsSupplements";

                $.ajax({
                    url: zurl,
                    data: { insId: data.insId, res: data.isSuccess, insType: data.insType, refreshElId: data.refreshElId },
                    type: "GET",
                }).done(function (result) {
                    Swal.fire({
                        toast: true,
                        icon: "success",
                        title: "عملیات با موفقیت انجام شد",
                        showConfirmButton: false,
                        timer: 2000,
                        timerProgressBar: true,
                        didOpen: (toast) => {
                            toast.addEventListener('mouseenter', Swal.stopTimer)
                            toast.addEventListener('mouseleave', Swal.resumeTimer)
                        }
                    });
                    $(refreshEl).html(result);
                    document.getElementById("close-modal").click();
                });
            }
        });
    }
}
function Submit_RemoveSupp() {
    var data = $("#removeSuppForm").serialize();
    Swal.fire({
        title: "<span class='text-danger'>آیا مطمئن به حذف فایل پیوست می باشید؟</span>",
        icon: 'question',
        showDenyButton: true,
        showCancelButton: false,
        confirmButtonText: 'بله',
        denyButtonText: 'خیر',
        confirmButtonColor: '#d33',
        denyButtonColor: '#3085d6'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'POST',
                url: '/UsersPanel/Reports/RemoveInsSupp',
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8', // when we use .serialize() this generates the data in query string format. this needs the default contentType (default content type is: contentType: 'application/x-www-form-urlencoded; charset=UTF-8') so it is optional, you can remove it
                data: data,

            }).done(function (data) {
                if (data.isSuccess) {
                    var refreshEl = "#" + data.refreshElId;
                    var zurl = "/UsersPanel/Reports/ShowInsSupplements";
                    $.ajax({
                        url: zurl,
                        data: { insId: data.insId, res: data.isSuccess, insType: data.insType, refreshElId: data.refreshElId },
                        type: "GET",
                    }).done(function (result) {
                        Swal.fire({
                            toast: true,
                            icon: "success",
                            title: "عملیات با موفقیت انجام شد",
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                            didOpen: (toast) => {
                                toast.addEventListener('mouseenter', Swal.stopTimer)
                                toast.addEventListener('mouseleave', Swal.resumeTimer)
                            }
                        });
                        $(refreshEl).html(result);
                        document.getElementById("close-modal").click();
                    });
                }
            });
        }
    });
}

$.ajaxPrefilter(function (options, original_Options, jqXHR) {
    options.async = true;
});
$(document).ready(function () {
    $('.zoom').loupe({
        width: 400, // width of magnifier
        height: 600, // height of magnifier
        loupe: 'loupe' // css class for magnifier
    });
    $(document).on("click", ".btnInsertState", function () {
        var id = $(this).attr("data-insid");
        var refid = $(this).attr("data-refreshid");
        var instyp = $(this).attr("data-instype");
        var txt_insType = insTypeDic[instyp];
        
        $.ajax({
            async: true,
            url: "/UsersPanel/Reports/InsertStatus",
            data: { InsId: id, Instype: instyp, refreshDivId: refid },
            type: "GET",
        }).done(function (result) {
            $("#adminmodal #modal-dialog").removeClass("modal-lg");
            $("#adminmodal .modal-body").html(result);
            var html = "<h4 class='text-xs-center alert alert-success'>" + "افزودن وضعیت صدور به درخواست بیمه نامه" + " " + txt_insType + "</h4>";
            $("#adminmodal .modal-title").html(html);
            $("#adminmodal").modal('show');
        });
    });
    $(document).on("click", ".FinbtnInsertState", function () {
        var id = $(this).attr("data-insid");
        var refid = $(this).attr("data-refreshid");
        var instyp = $(this).attr("data-instype");
        var txt_insType = insTypeDic[instyp];
        $.ajax({
            asyns: true,
            url: "/UsersPanel/Reports/InsertFinancialStatus",
            data: { InsId: id, Instype: instyp, refreshDivId: refid },
            type: "GET",
        })
            .done(function (result) {
                $("#adminmodal #modal-dialog").removeClass("modal-lg");
                $("#adminmodal .modal-body").html(result);
                var html = "<h4 class='text-xs-center alert alert-success'>" + "افزودن وضعیت مالی به درخواست بیمه نامه" + " " + txt_insType + "</h4>";
                $("#adminmodal .modal-title").html(html);
                $("#adminmodal").modal('show');
            });
    });
    $(document).on("click", ".btnInsertSupp", function () {
        var refId = $(this).attr("data-refreshid");
        var insid = $(this).attr("data-insid");
        var instype = $(this).attr("data-instype");
        $.ajax({
            url: "/UsersPanel/Reports/InsertInsSupplement",
            data: { insid: insid, refreshDivId: refId, insType: instype },
            type: "GET",

        }).done(function (result) {
            $("#adminmodal #modal-dialog").removeClass("modal-lg");
            $("#adminmodal .modal-body").html(result);
            var text = "<h3 >افزودن پیوست به بیمه نامه</h3>";
            $("#adminmodal .modal-title").html(text);
            $("#adminmodal").modal('show');
        });
    });
    $(document).on("click", ".btnInsertStatusComment", function () {
        var insstid = $(this).attr("data-insstid");
        var refId = $(this).attr("data-refreshid");
        var insId = $(this).attr("data-insid");
        var instype = $(this).attr("data-instype");
        var sttp = $(this).attr("data-sttype");
        var statustext = $(this).attr("data-status-text");
        var rad = $(this).attr("data-radif");
        $.ajax({
            url: "/UsersPanel/Reports/InsertStatusComment",
            data: { insStatusId: insstid, insid: insId, refreshDivId: refId, insType: instype, statusType: sttp },
            type: "GET",
            async: true,
            cache: false,
        }).done(function (result) {
            $("#adminmodal #modal-dialog").removeClass("modal-lg");
            $("#adminmodal .modal-body").html(result);
            var text = "<h3 >" + "افزودن توضیحات به وضعیت" + " <span class='text-danger'>" + statustext + "</span>" + " در ردیف" + " " + rad + "</h3>";
            $("#adminmodal .modal-title").html(text);
            $("#adminmodal").modal();
        });

    });
    $(document).on('click', ".btnEditStatusCom", function (event) {
        var refId = $(this).attr("data-refreshid");
        var sttp = $(this).attr("data-sttype");
        var id = $(this).attr("data-scid");
        var statustext = $(this).attr("data-status-text");
        var rad = $(this).attr("data-radif");
        var instype = $(this).attr("data-instype");
        $.ajax({
            url: "/UsersPanel/Reports/EditStatusComment",
            data: { scId: id, refreshDivId: refId, insType: instype, statusType: sttp },
            type: "GET",
            async: true,
            cache: false,
        }).done(function (result) {
            $("#adminmodal #modal-dialog").removeClass("modal-lg");
            $("#adminmodal .modal-body").html(result);
            var text = "<h3 >" + "ویرایش یادداشت مربوط به وضعیت " + " <span class='text-danger'>" + statustext + "</span>" + " در ردیف" + " " + rad + "</h3>";
            $("#adminmodal .modal-title").html(text);
            $("#adminmodal").modal();

        });
    });
    $(document).on("click", ".btnRemoveStatusCom", function () {
        var refId = $(this).attr("data-refreshid");
        var sttp = $(this).attr("data-sttype");
        var id = $(this).attr("data-scid");
        var com = $(this).attr("data-com-text");
        var instype = $(this).attr("data-instype");

        Swal.fire({
            title: 'آیا مطمئن به حذف یادداشت می باشید؟',
            icon: 'question',
            showDenyButton: true,
            showCancelButton: false,
            confirmButtonText: 'بله',
            denyButtonText: 'خیر',
            confirmButtonColor: '#d33',
            denyButtonColor: '#3085d6'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: "/UsersPanel/Reports/RemoveStatusComment",
                    data: { scId: id, insType: instype, RefreshElId: refId, statusType: sttp },
                    type: "POST",
                }).done(function (result) {
                    $("#" + refId).html(result);
                });
            }
        });
    });
    $(document).on("click", ".btnInsertSupp", function () {
        var refId = $(this).attr("data-refreshid");
        var insid = $(this).attr("data-insid");
        var instype = $(this).attr("data-instype");
        $.ajax({
            url: "/UsersPanel/Reports/InsertInsSupplement",
            data: { insid: insid, refreshDivId: refId, insType: instype },
            type: "GET",
            cache: false,
            success: function (result) {
                $("#adminmodal #modal-dialog").removeClass("modal-lg");
                $("#adminmodal .modal-body").html(result);
                var text = "<h3 >افزودن پیوست به بیمه نامه</h3>";
                $("#adminmodal .modal-title").html(text);
                $("#adminmodal").modal('show');
            }
        });
    });



});//last_of_document_ready

