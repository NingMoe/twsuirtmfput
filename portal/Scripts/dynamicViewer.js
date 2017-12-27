function GetDynamicViewer(BaseTableDiv, ResID, RecID, NoShowName, LastFun) {
    var html = ""
    if (NoShowName == "")
        NoShowName = "rownum,ID,RESID,";

    if ($("#" + BaseTableDiv).length == 0 || BaseTableDiv == "" || ResID == "" || RecID == "") {
        return
    }


    $.ajax({
        type: "POST",
        url: "../Organization/ZTH_GetInfo_ajax.aspx?typeValue=getSortDynamicFieldRecords&argResid=" + ResID + "&argRecID=" + RecID,
        success: function (centerJson) {
            var jsonList = eval("(" + centerJson + ")").rows;
            var ColIndex = 0
            var num = 0
            var KuaLieShu = 0
            var readonlystr = " readonly=\"readonly\" "
            for (var i = 0; i < jsonList.length; i++) {
                for (var key in jsonList[i]) {

                    //if (key == "文件类型")
                    //{
                    //    debugger
                    //}
                      
                    KuaLieShu = 0
                    if (jsonList[i][key] == "" || jsonList[i][key] == null || NoShowName.indexOf(key + ",") > -1) {
                        continue;
                    }

                    var showkey = key;

                    //if (key == "档案所属类型名称") {
                    //    showkey = "项目/公司名称"
                    //}

                    if (ColIndex % 3 == 0) {
                        if (num > 0)
                            html += "</tr><tr>"
                        else
                            html += "<tr>"

                        if (jsonList[i][key].length > 20) {
                            KuaLieShu = 3
                        } else if (jsonList[i][key].length > 50) {
                            KuaLieShu = 5
                        }
                        html += GetFiledTD(showkey, key, KuaLieShu, ResID)

                    } else if (ColIndex % 3 == 1) {
                        if (jsonList[i][key].length > 20) {
                            KuaLieShu = 3
                        }
                        html += GetFiledTD(showkey, key, KuaLieShu, ResID)
                    }
                    else if (ColIndex % 3 == 2) {
                        html += GetFiledTD(showkey, key, KuaLieShu, ResID)
                    }

                    num++;
                    if (KuaLieShu > 0)
                        ColIndex += (1 + Math.floor(KuaLieShu / 2))
                    else
                        ColIndex += 1

                }
            }

            $("#" + BaseTableDiv).html(html)
            LoadDynamicViewerValue(jsonList, ResID)
            if (LastFun != "")
                eval(LastFun())
        }
    });
}


function LoadDynamicViewerValue(jsonList, ResID) {
    for (var i = 0; i < jsonList.length; i++) {
        for (var key in jsonList[i]) {
            if ($("#" + ResID + "_" + key).attr("type") == "checkbox") {
                if (jsonList[i][key] == "1" || jsonList[i][key] == "是" || jsonList[i][key] == "True" || jsonList[i][key] == "true") $("#" + ResID + "_" + key).attr("checked", "true");
            }
            else $("#" + ResID + "_" + key).val(jsonList[i][key]);
        }
    }
}


function GetFiledTD(showkey, key, KuaLieShu, ResID) {
    var readonlystr = " readonly=\"readonly\" "
    var tdStyle = " style=\"width: 22%\" "
    var inputStyle = " style=\"width: 90%\" "
    var KuaLie = " "

    if (KuaLieShu > 0) {
        tdStyle = " "
        KuaLie = " colspan=\"" + KuaLieShu + "\" "
    }

    if (KuaLieShu == 3)
        inputStyle = " style=\"width: 92%\" "
    else if (KuaLieShu == 5)
        inputStyle = " style=\"width: 95%\" "

    var html = "<th style=\"width: 11%\">" + showkey + "</th>"
    html += "<td " + tdStyle + KuaLie + " >"
    html += "<input id=\"" + ResID + "_" + key + "\" name='InputKey' type=\"text\" class=\"box3\"  " + inputStyle + readonlystr + "/>"
    html += "</td>"
    return html
}
 

 

function MyDeleteRow(obj, ResID, RecID) {
    if (RecID == "")
        deleteRow(obj)
    else
        deleteData(obj, ResID, RecID)
}


function GetDynamicInput(Title, obj, ResID, index, readonly, AddTD, widthStr, IsHidden, IsDate,IsMinput,fieldtype) {
    if (ResID == ""  || Title == "") return ""
    var readonlystr = " readonly=\"readonly\" "
    var minputstr = " "
    var fieldtypeStr = " "
    if(IsMinput)
        minputstr = " minput='true' fieldtitle='" + Title + "'" 
    if(fieldtype !="")
        fieldtypeStr = " fieldtype='"  + fieldtype + "'"
    
    if (IsHidden) {
        s = "<input type='hidden' id='" + ResID + "_" + index + "_" + Title + "' value='" + (obj == undefined || obj == 'null' ? "" : obj) + "' " + (readonly ? readonlystr : "") + " />";
    }
    else {
        s = "<input type='text' id='" + ResID + "_" + index + "_" + Title + "' class='box3'  style='width: " + (widthStr == "" ? "70%" : widthStr) + ";' fieldtitle='" + Title + "'  value='" + (obj == undefined || obj == 'null' ? "" : obj) + "' " + (readonly ? readonlystr : "") + (IsDate && !readonly ? " onfocus=\"WdatePicker({dateFmt:'yyyy-MM-dd'})\" " : "") + minputstr + fieldtypeStr + " />";
    }

    return AddTD ? "<td>" + s + "</td>" : s;
}