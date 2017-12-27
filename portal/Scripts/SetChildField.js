


Date.prototype.pattern = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份      
        "d+": this.getDate(), //日      
        "h+": this.getHours() % 12 == 0 ? 12 : this.getHours() % 12, //小时      
        "H+": this.getHours(), //小时      
        "m+": this.getMinutes(), //分      
        "s+": this.getSeconds(), //秒      
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度      
        "S": this.getMilliseconds() //毫秒      
    };
    var week = {
        "0": "\u65e5",
        "1": "\u4e00",
        "2": "\u4e8c",
        "3": "\u4e09",
        "4": "\u56db",
        "5": "\u4e94",
        "6": "\u516d"
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    if (/(E+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "\u661f\u671f" : "\u5468") : "") + week[this.getDay() + ""]);
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
}

//批量保存

//批量保存
function CommonBatchSaveWDInfo(SaveResID, SaveRecIDStr, json, tip, UserID, NodeID, OpenDiv, gridID) {
     
    var jsonStr1 = "[";
    jsonStr1 += json;
    jsonStr1 = jsonStr1.substring(0, jsonStr1.length - 1);
    jsonStr1 += "]";
    $.ajax({
        type: "POST",
        dataType: "json",
        data: { "Json": "" + jsonStr1 + "" },
        url: "../../../Base/Common/CommonGetInfo_ajax.aspx?typeValue=BatchSaveWDInfo&ResID=" + SaveResID + "&RecidStr=" + SaveRecIDStr + "&UserID=" + UserID,
        success: function (obj) {
            var s = eval(obj)[0]
            if (s.success || s.success == "true") {
                alert(tip + " 保存成功 " + s.SucessCount + "个，失败" + (s.Count - s.SucessCount) + "个！");
               // CommonClose(OpenDiv, "", "")
                window.parent.ParentCloseWindow();
                window.parent.RefreshGrid(gridID);

            } else {
                alert("保存失败,请刷新页面！");
            }
        },
        error: function (dd) {
            debugger
        }
    });
}

 
function SetChildField(FatherID, FatherResID, ResID, UserID, ChildFieldStr) {
    
    if (FatherID == '' || FatherResID == "" || ResID == '' || ChildFieldStr == '') return
    $.ajax({
        type: "POST",
        url: "../Common/CommonAjax_Request.aspx?typeValue=GetOneRowByRecID&ResID=" + FatherResID + "&RecID=" + FatherID + "&UserID=" + UserID,
        success: function (centerJson) {
            //alert(centerJson)
            var jsonList = eval("(" + centerJson + ")");
            LedgerChildKeyByRowdata(ResID, ChildFieldStr, jsonList[0], true);
        }
    });
}


function LedgerChildKeyByRowdata(ResID, ChildKey, rowData, IsReadonly) {
    var ResIDKey = ""
    var RSResIDKey = ""
    if (ChildKey.indexOf(',') == -1) {
        if (ChildKey.indexOf('=') != -1) {
            ResIDKey = ChildKey.split('=')[0];
            RSResIDKey = ChildKey.split('=')[1];
            for (var key in rowData) {
                if (key == RSResIDKey) {
                    $("#" + ResID + '_' + ResIDKey).val(rowData[RSResIDKey]);
                    if (IsReadonly) $("#" + ResID + '_' + ResIDKey).attr("readonly", "readonly");
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
                for (var key in rowData) {
                    if (key == RSResIDKey) {
                        $("#" + ResID + '_' + ResIDKey).val(rowData[RSResIDKey]);
                        if (IsReadonly) $("#" + ResID + '_' + ResIDKey).attr("readonly", "readonly");
                    }
                }
            }
        }
    }
}



function NewCheckValue(objID) {
    var flag = true;
    $(".MustInput").removeClass("MustInput");

    $("#" + objID + " input,#" + objID + " select,#" + objID + " textarea").each(function () {
        var id = $(this).attr('id');
        var fieldtitle = $(this).attr('fieldtitle');
        if (id != "" && fieldtitle != "" && id != undefined && fieldtitle != undefined) {
            var obj = document.getElementById(id);
            if (obj == null) {
                // alert(id);
            } else {
                if (NewCheckValueType(obj) == false) {
                    flag = false;
                    //return flag;
                    $(obj).parent().addClass("MustInput");
                    //$(obj).addClass("MustInput")
                }
            }
        }
    });
    if (!flag)
        alert("表单验证未通过，请检查红色方框内的信息是否填写正确！")
    return flag;
}



/********************************************/
function NewCheckValueType(o) {
    var id = o.id;
    //检查是否为必输项;
    if ($("#" + id).attr("minput") == "true") {
        if (o.value.trim() == "") {
            if ($("#" + id).attr("fieldtype") == "file") {
                //alert('请先选择要上传的' + $("#" + id).attr("fieldtitle"));
                //o.focus();
            } else {
                try {
                    //alert($("#" + id).attr("fieldtitle") + '不能为空！');
                    //if (o.type != "hidden") {

                    //    o.focus();
                    //    o.select();
                    //}
                } catch (e) {

                }

            }
            return false;
        }
    }
    //类型判断;
    switch ($("#" + id).attr("fieldtype")) {
        case "int":
            if (o.value != "") {
                var s = o.value
                var re = /^\d+$/;
                if (!re.test(s)) {
                    alert($("#" + id).attr("fieldtitle") + '必须为整数！');
                    if (o.type != "hidden") {

                        o.focus();
                        o.select();
                    }
                    return false;
                }
            }
            break;
        case "num":
            if (isNaN(o.value)) {
                alert($("#" + id).attr("fieldtitle") + '必须为数字！');
                if (o.type != "hidden") {

                    o.focus();
                    o.select();
                }
                return false;
            }
            break;
        case "num1":
            if (isNaN(o.value)) {
                alert($("#" + id).attr("fieldtitle") + '必须为小于等于1的数字！');
                if (o.type != "hidden") {

                    o.focus();
                    o.select();
                }
                return false;
            } else {
                if (parseFloat(o.value) > 1) {
                    alert($("#" + id).attr("fieldtitle") + '必须为小于等于1的数字！');
                    if (o.type != "hidden") {

                        o.focus();
                        o.select();
                    }
                    return false;
                }
            }
            break;
        case "username":
            if (!Verifyname(o.value)) {
                alert($("#" + id).attr("fieldtitle") + '只能是字母，数字，－＿');
                if (o.type != "hidden") {

                    o.focus();
                    o.select();
                }
                return false;
            }
            break;

        case "email":

            if (o.value != "") {

                var s = o.value;
                var temp = s.indexOf("@");
                if (temp == "-1" || temp == s.length - 1) {
                    alert($("#" + id).attr("fieldtitle") + '格式不正确！');
                    if (o.type != "hidden") {

                        o.focus();
                        o.select();
                    }
                    return false;
                }


            }

            break;
        case "date":
            if (o.value != "") {
                if (!isDate(o.value)) {
                    alert($("#" + id).attr("fieldtitle") + '必须为日期格式！\n 如:2004-12-12 或者 2004/12/12');
                    if (o.type != "hidden") {

                        o.focus();
                        o.select();
                    }
                    return false;
                }
            }
            break;
        case "file":
            if (o.value != "") {
                if (!isDate(o.value)) {
                    alert('请先选择要上传的' + $("#" + id).attr("fieldtitle"));
                    if (o.type != "hidden") {

                        o.focus();
                        o.select();
                    }
                    return false;
                }
            }
            break;
        case "time":
            if (o.value != "") {
                if (!isTime(o.value)) {
                    alert($("#" + id).attr("fieldtitle") + '必须为时间格式！\n 如:18:30:60 或者 8:25');
                    if (o.type != "hidden") {

                        o.focus();
                        o.select();
                    }
                    return false;
                }
            }
            break;
        case "datetime":
            if (o.value != "") {
                if (!isDateTime(o.value)) {
                    alert($("#" + id).attr("fieldtitle") + '必须为日期时间格式格式！\n 如:2004-12-12 或者 2004-12-12 15:30 或者 2004/12/12 15:30:28');
                    if (o.type != "hidden") {
                        o.focus();
                        o.select();
                    }
                    return false;
                }
            }
            break;
        case "ph":
            if (o.value != "") {
                var str = o.value
                var re1 = /^(([0\+]\d{2,3}-)?(0\d{2,3})-)?(\d{7,8})(-(\d{3,}))?$/
                var re2 = /^[1]+[3,5]+\d{9}?$/
                if (re1.exec(str) || re2.exec(str)) {
                    break;
                }
                else {
                    alert($("#" + id).attr("fieldtitle") + "的格式不正确.ex:139********或021-58******或021-58******-分机号");
                    if (o.type != "hidden") {

                        o.focus();
                        o.select();
                    }
                    return false;
                }
            }
            break;
        default:
            break;

    }
    return true;
}

/********************************************/
function NewCheckOptionValueType(o) {
    //选择项不能为空

    if ($("#" + id).attr("minput") == "true") {
        if (o.options(o.selectedIndex).text.trim() == "") {
            //alert($("#" + id).attr("fieldtitle") + '不能为空！');
            //if (o.type != "hidden") {
            //    o.focus();
            //    o.select();
            //}
            return false;
        }
    }
    return true;
}

