
function WriteExcel(OptionModel, argTableResid, isPrint, DC_Params, vPath, IsUserPortal, isOnlineEdit) {

    var baseURL = window.location.protocol + "//" + window.location.host + "/webflow/Document/ToExcel/DoEventByExcel_ajax.aspx"
    var PortalPath = "";
    if (IsUserPortal) {
        PortalPath = "portal"
        baseURL = "/portal/Base/Common/DoEventByExcel_ajax.aspx"
    }

    if (DC_Params == undefined || DC_Params == "") {
        DC_Params = {
            GetExcelHelperOptionModelStr: JSON.stringify(OptionModel),
            argUserID: _UserID,
            argTableResid: argTableResid
        }
    }
    //alert(argTableResid)
    var index = layer.load(1); //换了种风格
    $.ajax({
        type: "POST",
        data: DC_Params,
        url: encodeURI(baseURL),
        success: function (o) {

            layer.close(index);

            var Result = eval('[' + o + ']')[0];
            if (Result.ErrorStr != "") {
                alert(Result.ErrorStr)
            }
            else {
                if (isPrint && Result.DownUrl != "") {
                    var IsBackfillStr = ""
                    if (isOnlineEdit)
                        IsBackfillStr = "&IsBackfill=IsBackfill";
                    window.open(window.location.protocol + "//" + window.location.host + "/webflow/Document/NewOfficeEdit/NewOfficeEditor.aspx?IsCheckOut=2&ResourceID=" + argTableResid + "&DocumentPath=" + encodeURI(Result.DownUrl) + "&IsHideBtnSave=1&IsShowBtnPrint=1&UserId=" + _UserID + "&PortalPath=" + PortalPath + IsBackfillStr + "&keyWord=" + DC_Params.keyWord)
                } else if (Result.DownUrl != "" && Result.DownUrlName != "") {
                    var MyUrl = window.location.protocol + "//" + window.location.host + "/webflow/" + vPath;
                    if (IsUserPortal)
                        MyUrl = window.location.protocol + "//" + window.location.host + "/portal/" + vPath;
                    alert("文件导出成功，稍后将自动下载！")
                    window.open(MyUrl)
                }
                else {
                    alert(tip + "，共导入 " + Result.Result + " 条数据！")
                }
            }
        },
        error: function (o) {
            debugger
        }
    });
}

 
function GetExcelModel(SelectReportField, OptionModel, SheetName, argQueryType, argTableResid, UserID, UpdateField) {
  
    var vPath = "DYMB/PrintTemp/" + SheetName + "_" + UserID + ".xls";
    var NewSavePath = "../../" + vPath;

    //var OptionModel = eval('[' + _ExcelOptionModelStr + ']')[0]
      
    OptionModel.KeyType = "GetExcelModel";
    OptionModel.ReadSheetStr = "Sheet1";
    OptionModel.StartRowNum = 1;
    OptionModel.StartColumnNum = 1;
    OptionModel.FilePath = NewSavePath;
    OptionModel.HasTitle = true;
    OptionModel.GetAjaxType = -1;
    OptionModel.IsReplaceWorkBook = true;
    OptionModel.IsSetColumnWidth = false;
    OptionModel.IsPortalPath = true;

    var DC_Params = {
        GetExcelHelperOptionModelStr: JSON.stringify(OptionModel),
        argUserID: UserID,
        argQueryType: argQueryType,
        argTableResid: argTableResid,
        SelectReportField: SelectReportField,
        UpdateField: UpdateField
    }
     
    WriteExcel(OptionModel, "", false, DC_Params, vPath,false,false);
}




function NewGetExcelModel(SelectReportField, OptionModel, SheetName, argQueryType, argTableResid, UserID, UpdateField, UserTableName, SaveTableName, KeyType, Condition) {

    var vPath = "PrintTemp/" + SheetName + ".xls";
    var NewSavePath = "../../" + vPath;
    //alert(argQueryType)
    OptionModel.KeyType = KeyType;
    OptionModel.ReadSheetStr = "Sheet1";
    OptionModel.StartRowNum = 1;
    OptionModel.StartColumnNum = 1;
    OptionModel.FilePath = NewSavePath;
    OptionModel.HasTitle = true;
    OptionModel.GetAjaxType = -1;
    OptionModel.IsReplaceWorkBook = true;
    OptionModel.IsSetColumnWidth = false;
    OptionModel.IsPortalPath = true;

    var DC_Params = {
        GetExcelHelperOptionModelStr: JSON.stringify(OptionModel),
        argUserID: UserID,
        argQueryType: argQueryType,
        argTableResid: argTableResid,
        SelectReportField: SelectReportField,
        UpdateField: UpdateField,
        UserTableName: UserTableName,
        SaveTableName: SaveTableName,
        Condition: Condition,
        keyWord: argQueryType
    }

    WriteExcel(OptionModel, argTableResid, (KeyType == "OnlineEdit" ? true : false), DC_Params, vPath, true, (KeyType == "OnlineEdit" ? true : false));
}




function GetResid(keyWord) {
    
    $.ajax({
        type: "POST",
        url: "../Base/Common/CommonGetInfo_ajax.aspx",   //(檔案名稱/方法名稱)
        data: {
            typeValue: "GetDataByUserDefinedSql",
            page: 1,
            rows: 10,
            UserDefinedSql: "437496952875",
            Condition: " and 参数关键字='" + keyWord + "' ",
            ROW_NUMBER_ORDER: " order by id"
        },
        success: function (data) {
            var obj = eval('[' + data + ']')[0]
            if (obj.total > 0) {
                //alert(obj.rows[0].Value)
               // OnlineEdit(keyWord, obj.rows[0].Value, obj.rows[0].Value)
                OnlineEditSelectField(keyWord, obj.rows[0].Value, obj.rows[0].ShowTitle)
            }
        },
        error: function (err) {
            debugger;
        }
    });
}


function OnlineEdit(keyWord,Resid) {
    
    var vPath = "../PrintTemp/" + "KCFP" + "_" + _UserID + ".xls";
    var NewSavePath = vPath;

    var OptionModel = _ExcelOptionModelStr

    OptionModel.KeyType = "OnlineEdit";
    OptionModel.ReadSheetStr = "Sheet1";
    OptionModel.StartRowNum = 1;
    OptionModel.StartColumnNum = 1;
    OptionModel.FilePath = "../" + NewSavePath;
    OptionModel.HasTitle = true;
    OptionModel.GetAjaxType = -1;
    OptionModel.IsReplaceWorkBook = true;
    OptionModel.IsSetColumnWidth = false;
    OptionModel.IsPortalPath = true;

    var DC_Params = {
        GetExcelHelperOptionModelStr: JSON.stringify(OptionModel),
        argUserID: _UserID,
        Condition: $('#hidSearchCondition' + Resid).val(),
        argTableResid: Resid,
        SelectReportField: "",
        keyWord: keyWord
    }

    WriteExcel(OptionModel, Resid, true, DC_Params, vPath, true, true);
}


function OnlineEditSelectField(KeyWord, ResID, ShowTitle) {


    $('#hidExpCondition').val($('#hidSearchCondition' + ResID).val());
    // alert(_DefaultCondition)
    parent.parent._hidExpCondition = $('#hidExpCondition').val();
    if (_DefaultCondition != "")
        parent.parent._hidExpCondition += _DefaultCondition
    var Title = "【" + ShowTitle + "】";

    parent.parent.OpenLayerByUrl(Title + ' - 选择字段页面', 'Base/CommonPage/SelectReportField.aspx?HideQueryBox=1&BaseResid=' + ResID + '&SearchTitle=' + Title + '&BaseKeyWordValue=' + KeyWord + '&key=OnlineEdit', '900px', '500px', "CommonSearch")
}




 