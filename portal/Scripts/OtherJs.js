


function LedgerChildKey(ResID, RSResID, ChildKey)
{
    var ResIDKey = ""
    var RSResIDKey = ""
    if (ChildKey.indexOf(',') == -1) {
        if (ChildKey.indexOf('=') != -1) {
            ResIDKey = ChildKey.split('=')[0];
            RSResIDKey = ChildKey.split('=')[1];
            $("#" + ResID + '_' + ResIDKey).val($("#" + +RSResID + '_' + RSResIDKey).val());
            $("#" + ResID + '_' + ResIDKey).attr("readonly", "readonly");
        }
    }
    else {
        var s = ChildKey.split(',');
        for (var i = 0; i < s.length; i++) {
            if (ChildKey.indexOf('=') != -1) {
                ResIDKey = s[i].split('=')[0];
                RSResIDKey = s[i].split('=')[1];
                $("#" + ResID + '_' + ResIDKey).val($("#" + +RSResID + '_' + RSResIDKey).val());
                $("#" + ResID + '_' + ResIDKey).attr("readonly", "readonly");
            }
        }
    }
}

function QueryLedgerChildKeyJson(ResID, ReferenceFields, IsReadonly, UserID, FatherResID, QueryRecID, AssociatedFields)
{
    var CommonPath = GetApplicationPath() + "Base/"
    var QueryStr = "";

    if (QueryRecID == "" && AssociatedFields != "") {
        if (AssociatedFields.indexOf(',') == -1) {
            AssociatedKey = AssociatedFields.split('=')[0];
            RSAssociatedKey = AssociatedFields.split('=')[1];
        }
        else {
            var s = AssociatedFields.split(',');
            for (var j = 0; j < s.length; j++) {
                if (s[j].indexOf('=') != -1) {
                    AssociatedKey = s[j].split('=')[0];
                    RSAssociatedKey = s[j].split('=')[1];
                }
            }
        }
    }
    $.ajax({
        type: "POST",
        url: CommonPath + "Common/CommonAjax_Request.aspx?typeValue=GetOneRowByRecID&ResID=" + FatherResID + "&RecID=" + QueryRecID ,
        success: function (centerJson) {
            var jsonList = eval("(" + centerJson + ")");
            if (jsonList.length > 0) {
                LedgerChildKeyByJson(ResID, ReferenceFields, jsonList, IsReadonly);
            }
        }
    });
}



function LedgerChildKeyByJson(ResID, ChildKey, jsonList, IsReadonly) {
    var ResIDKey = ""
    var RSResIDKey = ""
    if (ChildKey.indexOf(',') == -1) {
        if (ChildKey.indexOf('=') != -1) {
            ResIDKey = ChildKey.split('=')[0];
            RSResIDKey = ChildKey.split('=')[1];
            for (var i = 0; i < jsonList.length; i++) {
                for (var key in jsonList[i] ) {
                    //$("#<%=ResID %>_" + "流程编号").val(jsonList[i]["报销ID"]);
                    //$("#<%=ResID %>_" + "付款金额").val((parseFloat(jsonList[i]["合计"]) - parseFloat(jsonList[i]["金额"])).toFixed(2));
                    if (key == RSResIDKey) {
                        $("#" + ResID + '_' + ResIDKey).val(jsonList[i][RSResIDKey]);
                        if (IsReadonly) $("#" + ResID + '_' + ResIDKey).attr("readonly", "readonly");
                    }
                }
            }
        }
    }
    else {
        var s = ChildKey.split(',');
        for (var j = 0; j < s.length; j++) {
            if (ChildKey.indexOf('=') != -1) {
                ResIDKey = s[j].split('=')[0];
                RSResIDKey = s[j].split('=')[1];
                for (var i = 0; i < jsonList.length; i++) {
                    for (var key in jsonList[i]) {
                        if (key == RSResIDKey) {
                            $("#" + ResID + '_' + ResIDKey).val(jsonList[i][RSResIDKey]);
                            if (IsReadonly) $("#" + ResID + '_' + ResIDKey).attr("readonly", "readonly");
                        }
                    }
                }
            }
        }
    }
}


function LedgerChildKeyByRowdata(ResID, ChildKey, rowData, IsReadonly, ResID_Key) {
 
    var ResIDKey = ""
    var RSResIDKey = ""
    if (ChildKey.indexOf(',') == -1) {
        if (ChildKey.indexOf('=') != -1) {
            ResIDKey = ChildKey.split('=')[0];
            RSResIDKey = ChildKey.split('=')[1];
            SetValueLedgerChildKeyByRowdata(ResID,ResIDKey, RSResIDKey, rowData, IsReadonly, ResID_Key)
            //for (var key in rowData) {
            //    if (key == RSResIDKey) {
            //        $("#" + ResID + '_' + ResIDKey).val(rowData[RSResIDKey]);
            //        if (IsReadonly) $("#" + ResID + '_' + ResIDKey).attr("readonly", "readonly");
            //    }
            //}
        }
    }
    else {
        var s = ChildKey.split(',');
        for (var j = 0; j < s.length; j++) {
            if (s[j].indexOf('=') != -1) {
                ResIDKey = s[j].split('=')[0];
                RSResIDKey = s[j].split('=')[1];
                SetValueLedgerChildKeyByRowdata(ResID,ResIDKey, RSResIDKey, rowData, IsReadonly, ResID_Key)
                //for (var key in rowData) {
                //    if (key == RSResIDKey) {
                //        $("#" + ResID + '_' + ResIDKey).val(rowData[RSResIDKey]);
                //        if (IsReadonly) $("#" + ResID + '_' + ResIDKey).attr("readonly", "readonly");
                //    }
                //}
            }
        }
    }

}



function SetValueLedgerChildKeyByRowdata(ResID,ResIDKey, RSResIDKey, rowData, IsReadonly, ResID_Key) {
    if (ResID_Key == "" || ResID_Key == undefined) {
        for (var key in rowData) {
            if (key == RSResIDKey) {
                if ($("#" + ResID + '_' + ResIDKey).length > 0) {
                    $("#" + ResID + '_' + ResIDKey).val(rowData[RSResIDKey]);
                    if (IsReadonly) $("#" + ResID + '_' + ResIDKey).attr("readonly", "readonly");
                }
                else if ($("#" + ResIDKey).length > 0) {

                    $("#" + ResIDKey).val(rowData[RSResIDKey]);
                    if (IsReadonly) $("#" + ResIDKey).attr("readonly", "readonly");
                }
            }
        }
    }
    else {
       // debugger
       // $('#' + ResID_Key).combogrid('grid').datagrid('getSelections');
        //var checkedObj = $('#' + ResID_Key).combogrid('grid').datagrid('getChecked');
        var checkedObj = $('#' + ResID_Key).combogrid('grid').datagrid('getSelections');
        for (var i = 0; i < checkedObj.length; i++) {
            for (var key in checkedObj[i]) {
                if (key == RSResIDKey) {
                    if (i==0)
                        $("#" + ResID + '_' + ResIDKey).val(rowData[RSResIDKey]);
                    else
                        $("#" + ResID + '_' + ResIDKey).val(  $("#" + ResID + '_' + ResIDKey).val() + "," + rowData[RSResIDKey]);
                    if (IsReadonly) $("#" + ResID + '_' + ResIDKey).attr("readonly", "readonly");
                }
            }
        }
    }
}


function getRootPath() {//返回虚拟应用程序跟路径
    var strFullPath = window.document.location.href;
    var strPath = window.document.location.pathname;
    var pos = strFullPath.indexOf(strPath);
    var prePath = strFullPath.substring(0, pos);
    var postPath = strPath.substring(0, strPath.substr(1).indexOf('/') + 1);
    return (prePath + postPath);
}

function GetApplicationPath() {   //返回虚拟应用程序跟目录 
    var strPath = window.document.location.pathname;
    var postPath = strPath.substring(0, strPath.substr(1).indexOf('/') + 1)+"/";
    //if (postPath.indexOf("/Base/") >= 0)
    //    postPath = postPath.substring(0, postPath.indexOf("/Base/"))+"/";
        return postPath ;
}

function openDictionary(url, DialogWidth, DialogHeight, title) { 
    if (DialogHeight == 0) DialogHeight = document.documentElement.clientHeight - 10;
    if (DialogWidth == 0) {
        DialogWidth = document.documentElement.clientWidth - 50;
        if (DialogWidth > 1200) DialogWidth = 1200;
    }
    $('#divDictionary').append($("<iframe scrolling='no' id='FromPage' frameborder='0' marginwidth='0' marginheight='0' style='width:100%;height:100%;'></iframe")).dialog({
        title: title,
        width: DialogWidth,
        height: DialogHeight,
        cache: false,
        closed: true,
        shadow: false,
        closable: true,
        draggable: true,
        resizable: false,
        onClose: function () {
            $('#FromPage:gt(0)').remove();
            // RefreshMessageGrid();

            if ($("#childForm").val() != "") {
                $("#childForm").val("");

                if (callajax) {
                    Beforcloseajax(infoofchild[0]["ajaxurl"], infoofchild[0]["successinfo"]);
                }
                else {
                    if (IsHomebool) {

                    }
                    IsHomebool = false
                }
                // RefreshMessageGrid();
            }
        },
        onBeforeClose: function (select) {
            infoofchild = $("#childForm").val();
            infoofchild = eval(infoofchild);
            RefreshNodeID = "";
            IsHomeMessage = false;
            RefreshGridDiv = "";
            if ("undefined" != typeof (infoofchild)) {
                if (infoofchild.length > 0) {
                    if ("undefined" != typeof (infoofchild[0]["IsHome"])) {
                        if (infoofchild[0]["IsHome"] == 1) IsHomebool = true;
                    }

                    if ("undefined" != typeof (infoofchild[0]["IsHomeMessage"])) {
                        if (infoofchild[0]["IsHomeMessage"] == 1) IsHomeMessage = true;
                    }

                    if ("undefined" != typeof (infoofchild[0]["RefreshNodeID"])) {
                        RefreshNodeID = infoofchild[0]["RefreshNodeID"];
                    }

                    if ("undefined" != typeof (infoofchild[0]["RefreshGridDiv"])) {
                        RefreshGridDiv = infoofchild[0]["RefreshGridDiv"];
                    }

                    if (infoofchild[0]["closewindow"] == 0) {
                        if ("undefined" != typeof (infoofchild[0]["IsReply"])) {
                            if (infoofchild[0]["IsReply"] == 1) {
                                return;
                            }
                        }
                        if ("undefined" != typeof (infoofchild[0]["mustdo"])) {
                            if (infoofchild[0]["mustdo"] == 1) {
                                alert(infoofchild[0]["reminderinfo"])
                                return false;
                            }
                        }

                        if ("undefined" != typeof (infoofchild[0]["confrm"])) {
                            var confrmresult = true;
                            if (infoofchild[0]["confrm"] == 1) {
                                if (!confirm(infoofchild[0]["reminderinfo"])) {
                                    confrmresult = false;
                                }
                            }
                            if (confrmresult) {
                                if ("undefined" != typeof (infoofchild[0]["ajaxurl"])) {
                                    callajax = true;
                                    //   Beforcloseajax(infoofchild[0]["ajaxurl"], infoofchild[0]["successinfo"]);
                                }
                            }
                            else {
                                if (infoofchild[0]["noclose"] == 1) return false;
                                // if (confirm(infoofchild[0]["noclose"]) == 1) return false;
                            }
                        }
                    }
                }
            }

        },
        modal: true
    });
    var index = url.indexOf("?");
    if (index != "-1") {
        url = url + "&height=" + DialogHeight;
    } else {
        url = url + "?height=" + DialogHeight;
    }
    $('#FromPage')[0].src = encodeURI(url);
    openwindows = $('#divDictionary').dialog('open');

}
 

function CloseDictionary() {
    $('#divDictionary').dialog('close');
}
