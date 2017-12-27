
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


//通用导出方法
function fastCommonReport(checkedObj, PopUpPage, ThisGridID, keyWordValue, ReportKey, ResID, ExcelName, SearchCondition, AutomaticFilteringColumn, CustomQueryStr,Type) {

    if (ReportKey == "") {
        if (keyWordValue == "") {
            keyWordValue = _keyWordValue;
            ResID = _ResID;
        }
    }

    if (Type == "导前一天工时")
    {

    }


    var CommonGetDataUrl = CommonPath + "Common/CommonGetInfo_ajax.aspx?typeValue=GetDataByUserDefinedSql_FormList";

    if (CustomQueryStr == "")
        CommonGetDataUrl = CommonPath + "Common/CommonGetInfo_ajax.aspx?typeValue=GetDataListByResIDForReport";


    var index = layer.load(1); //换了种风格
    $.ajax({
        type: "POST",
        url: CommonGetDataUrl,
        dataType: "json",
        data: {
            keyWordValue: keyWordValue,
            ResID: ResID,
            UserID: _UserID,
            Condition: SearchCondition,
            CustomQueryStr: CustomQueryStr,
            argIsExport: "1"
           // sort: _sort,
            //order: _order
        },
        success: function (centerJson) {

            $.ajax({
                type: "POST",
                url: CommonPath + "Common/GetDataForReport_ajax.aspx?typeValue=ReportToExcel",
                data: {
                    "keyWordValue": keyWordValue,
                    "ReportKey": ReportKey,
                    "UserID": _UserID,
                    "JsonData": JSON.stringify(centerJson),
                    "ExcelName": ExcelName,
                    "AutomaticFilteringColumn": 1
                },
                success: function (Result) {

                    if (Result == "") {
                        //关闭
                        layer.close(index);
                        alert("导出失败，可能的原因是数据集为空！")
                    }
                    else {
                        //关闭
                        layer.close(index);
                        alert("导出成功，稍后会自动下载！")
                        window.location = Result
                    }

                }, error: function (i) {
                    //关闭
                    layer.close(index);
                    debugger
                }
            });
        },
        error: function (i) {
            //关闭
            layer.close(index);
            debugger
        }
    });
}


 
function ImportWork() {
    //fnFormListDialog(CommonPath + editUrl + "?RecID=" + selected.ID + "&FatherResID=" + FatherResID + "&FatherID=" + FatherID + "&FatherKey=" + FatherKey, DialogWidth, DialogHeight, "修改信息");
    debugger
    fnFormListDialog("UploadFile_DQFL.aspx?key=1111" , 400, 250, "修改信息");
}


function ReportQQ()
{
  
    var weekDay = ["星期天", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"];
    var to = new Date();
    var s = new Date(to.setDate(to.getDate() - 1))
    var b = new Date(Date.parse(s.pattern("yyyy-MM-dd").replace(/\-/g, "/")));

    if (s.getDay() == 0) {
        s = new Date(to.setDate(to.getDate() - 2))
    }

    SearchCondition = " and 日期 ='" + s.pattern("yyyy-MM-dd") + "'"

    if (!confirm("是否导出 【" + s.pattern("yyyy-MM-dd") + "】（" + weekDay[s.getDay()] + "）的所有工时？")) {
        fnGridLoadByCondition('账号', ' asc ', SearchCondition)
        return
    }
   

    $.ajax({
        type: "POST",
        dataType: "json",
        url: CommonPath + "Common/NewAjax.aspx?typeValue=GetfDataByGSQQTJB",
        data: {
            "getEmpCode": '',
            "gettxt1": s.pattern("yyyy-MM-dd"),
            "gettxt2": s.pattern("yyyy-MM-dd"),
            "getWarm": 0,
            "Condition": '',
            "rows": 1000,
            "page": 1
        },
        success: function (Result) {
            
            if (Result.rows.length > 0) {
                if (!confirm("系统发现有 【" + Result.rows.length + "】人 未填写工时，是否继续导出？")) {
                    fnGridLoadByCondition('账号', ' asc ', SearchCondition)
                    return
                }
            }

            fastCommonReport('', '', '', '', '', '', '日常工时导出（' + s.pattern("yyyy-MM-dd") + "）" , SearchCondition, '', '', '导前一天工时')
        },
        error: function(d)
        {
            debugger
        }

    })
}


//批量保存
function BatchSaveWDInfo(SaveResID, SaveRecIDStr, json, tip, UserID, NodeID, OpenDiv, gridID)
{
    var jsonStr1 = "[";
    jsonStr1 += json;
    jsonStr1 = jsonStr1.substring(0, jsonStr1.length - 1);
    jsonStr1 += "]";
    $.ajax({
        type: "POST",
        dataType: "json",
        data: { "Json": "" + jsonStr1 + "" },
        url:CommonPath + "Common/CommonGetInfo_ajax.aspx?typeValue=BatchSaveWDInfo&ResID=" + SaveResID + "&RecidStr=" + SaveRecIDStr + "&UserID=" + UserID,
        success: function (obj) {
            var s=eval(obj)[0]
            if (s.success || s.success == "true") {
                alert(tip + " 保存成功 " + s.SucessCount + "个，失败" + (s.Count - s.SucessCount) + "个！");
                if ($.isFunction(window.CommonCloseWindow)) {
                    if (parent.OpenDiv != undefined && parent.OpenDiv != "")
                    {
                        CommonCloseWindow(parent.OpenDiv, parent.gridID, parent.ChildgridID)
                    }
                    else
                    {
                        CommonCloseWindow(NodeID, parent.gridID, parent.ChildgridID)
                    }
                }
                if ($.isFunction(window.afterEvent)) {
                    afterEvent()
                }
            } else {
                alert("保存失败,请刷新页面！");
            }
        },
        error: function (dd) {
            debugger
        }
    });
}


function FormListDialog_InCustom(url, DialogWidth, DialogHeight, title) {
    url = "Base/" + url  
    var index = url.indexOf("?");
    if (index != "-1") {
        url = url + "&SearchType=<%=SearchType %>&NodeID=<%=NodeID %>&keyWordValue=<%=keyWordValue %>"
    } else {
        url = url + "?SearchType=<%=SearchType %>&NodeID=<%=NodeID %>&keyWordValue=<%=keyWordValue %>"
    }
    window.parent.fnZSFADialog(url, DialogWidth, DialogHeight, title);
}



function BatchReportWork() {

    var SearchCondition = $("#CenterGrid" + _keyWordValue).val();

    alert($('#CommonSearch_EndDay').textbox('getText'))

    return

    if (!confirm("是否导出 【" + s.pattern("yyyy-MM-dd") + "】（" + weekDay[s.getDay()] + "）的所有工时？")) {
        fnGridLoadByCondition('账号', ' asc ', SearchCondition)
        return
    }


    $.ajax({
        type: "POST",
        dataType: "json",
        url: CommonPath + "Common/NewAjax.aspx?typeValue=GetfDataByGSQQTJB",
        data: {
            "getEmpCode": '',
            "gettxt1": s.pattern("yyyy-MM-dd"),
            "gettxt2": s.pattern("yyyy-MM-dd"),
            "getWarm": 0,
            "Condition": '',
            "rows": 1000,
            "page": 1
        },
        success: function (Result) {

            if (Result.rows.length > 0) {
                if (!confirm("系统发现有 【" + Result.rows.length + "】人 未填写工时，是否继续导出？")) {
                    fnGridLoadByCondition('账号', ' asc ', SearchCondition)
                    return
                }
            }

            fastCommonReport('', '', '', '', '', '', '日常工时导出（' + s.pattern("yyyy-MM-dd") + "）", SearchCondition, '', '', '导前一天工时')
        },
        error: function (d) {
            debugger
        }

    })
}


function FFJJ(PopUpPage) {
  
     
    var checkedObj = ChildGrid.datagrid('getSelections');
    if (checkedObj == null || checkedObj.length == 0) {
        alert('请至少选择一项！');
        return;
    }

    var SaveRecIDStr = "";
    var SaveJson = "";
    if (checkedObj.length == 0) return;
    for (var i = 0; i < checkedObj.length; i++) {

        if (checkedObj[i].绩效比例 > 0) {
            SaveRecIDStr += "," + checkedObj[i].ID;
            SaveJson += "[{";
            SaveJson += "'" + "奖金发放状态" + "':'" + '已发放' + "',";
            SaveJson += "'" + "奖金发放时间" + "':'" + GetTodayStr() + "'";
            SaveJson += "}],";
        }
        else
        {
            alert("【" + checkedObj[i].项目成员 + "】的绩效比例未设置，无法发放奖金！")
            return;
        }
    }
   
    BatchSaveWDInfo("439332828407", SaveRecIDStr, SaveJson, "奖金发放成功！", _UserID, "", "", _ChildGridID.split('#')[1])
}

function QXFFJJ(checkedObj, PopUpPage, ThisGridID) {

    var SaveRecIDStr = "";
    var SaveJson = "";
    if (checkedObj.length == 0) return;
    for (var i = 0; i < checkedObj.length; i++) {

        SaveRecIDStr += "," + checkedObj[i].ID;
        SaveJson += "[{";
        SaveJson += "'" + "奖金发放状态" + "':'" + '' + "',";
        SaveJson += "'" + "奖金发放时间" + "':'" + '' + "'";
        SaveJson += "}],";
    }
    alert(CommonPath)
    BatchSaveWDInfo("439332828407", SaveRecIDStr, SaveJson, "奖金取消发放成功！", _UserID, "", "", _ChildGridID.split('#')[1])

}


//--------------------------------------------------------------

function KPCZ(checkedObj, PopUpPage,ThisGridID) {
    var sum = 0;
    var KHBH = ""
    var XMBH = "";
    var t = true
    

    var rows = $('#' + ThisGridID).datagrid('getRows');
    
    $("input[name=ch_kp]").each(function () {
        if ($(this)[0].checked) {
            var xmid = $(this)[0].id.split('_')[3];
            var index = $(this)[0].id.split('_')[2];
            XMBH += "," + xmid;
            KHBH = rows[index].客户编号
        }
    });

    if (XMBH == "") {
        alert("请至少选择一项")
        return;
    }
  
    
    if (t && PopUpPage != "")
        FormListDialog_InCustom(PopUpPage + "?CustomerNumber=" + KHBH + "&XMNumber=" + XMBH + "&ProcessedType=KP", _DialogWidth, _DialogHeight, "添加信息");
}


function SKCZ(checkedObj, PopUpPage, ThisGridID) {
    var sum = 0;
    var KHBH = ""
    var XMBH = "";
    var t = true
    
    var rows = $('#' + ThisGridID).datagrid('getRows');

    $("input[name=ch_sk]").each(function () {
        if ($(this)[0].checked) {
            var xmid = $(this)[0].id.split('_')[3];
            var index = $(this)[0].id.split('_')[2];
            XMBH += "," + xmid;
            KHBH = rows[index].客户编号
        }
    });

    if (XMBH == "") {
        alert("请至少选择一项")
        return;
    }

    if (t && PopUpPage != "")
        FormListDialog_InCustom(PopUpPage + "?CustomerNumber=" + KHBH + "&XMNumber=" + XMBH + "&ProcessedType=SK", _DialogWidth, _DialogHeight, "添加信息");
}


////通用导出方法
//function CommonReport(checkedObj, PopUpPage, ThisGridID) {

//    var index = layer.load(1); //换了种风格
//    Export(encodeURI("123456"), $("#" + ThisGridID), "", "");
//    layer.close(index);
//}


//=============================

//托单开票方法
function TDKP(checkedObj, PopUpPage) {
    var sum = 0;
    var tuo = ""
    var tuodanhao = "";
    var t = true
    
    var SaveRecIDStr = "";
    var SaveJson = "";
    for (var i = 0; i < checkedObj.length; i++) {
        if (tuo == "") {
            tuodanhao = checkedObj[i].托单号;
            tuo = checkedObj[i].托运单位.trim();
        }
        else {
            if (checkedObj[i].托运单位.trim() != tuo) {
                alert("托运单位不一致，不能开票！")
                t = false;
                return 
            }

            tuodanhao += "," + checkedObj[i].托单号;
        }

        SaveRecIDStr += "," + checkedObj[i].ID;
        SaveJson += "[{";
        //SaveJson += "'" + "收款状态" + "':'" + '已收款' + "',";
        SaveJson += "'" + "开票日期" + "':'" + GetTodayStr() + "'";
        SaveJson += "}],";
        sum += parseFloat(checkedObj[i].营业收入);
    }

    //BatchSaveWDInfo("337257256365", SaveRecIDStr, SaveJson, "收款成功！")

    if (t && PopUpPage != "")
        fnFormListDialog(PopUpPage + "?sum=" + sum + "&tuo=" + tuo + "&tuodanhao=" + tuodanhao + "&SaveRecIDStr=" + SaveRecIDStr + "&SaveJson=" + SaveJson, _DialogWidth, _DialogHeight, "添加信息");
}

//托单收款方法
function TDSK(checkedObj) {
    var sum = 0;
    var tuo = ""
    var tuodanhao = "";

    var SaveRecIDStr = "";
    var SaveJson = "";
    if (checkedObj.length == 0) return;
    for (var i = 0; i < checkedObj.length; i++) {
        if (tuo == "") {
            tuodanhao = checkedObj[i].托单号;
            tuo = checkedObj[i].托运单位.trim();
            //SaveRecIDStr = checkedObj[i].ID;
            //SaveJson += "[{'" + "付款状态" + "':'" + '已付款' + "'}],";
            //SaveJson = "[{'" + "收款状态" + "':'" + '已收款' + "',";

            SaveRecIDStr += "," + checkedObj[i].ID;
            SaveJson += "[{";
            SaveJson += "'" + "收款状态" + "':'" + '已收款' + "',";
            SaveJson += "'" + "收款日期" + "':'" + GetTodayStr() + "'";
            SaveJson += "}],";
        }
        else {
            if (checkedObj[i].托运单位.trim() != tuo) {
                alert("托运单位不一致，不能付款")
                return
            }
            SaveRecIDStr += "," + checkedObj[i].ID;
            tuodanhao += "," + checkedObj[i].托单号;
            SaveJson += "[{'" + "收款状态" + "':'" + '已收款' + "'}],";
        }
        sum += parseFloat(checkedObj[i].营业收入);
    }
    BatchSaveWDInfo("337257256365", SaveRecIDStr, SaveJson, "收款成功！")

    //var json = "'" + "托单号" + "':'" + tuodanhao + "',";
    //json += "'" + "托运单位" + "':'" + tuo + "',";
    //json += "'" + "收款金额" + "':'" + sum + "',";
    //CommonChildSave("486743468596", json, "收款成功")

}

//获取当前时间 2015-10-15
function GetTodayStr()
{
    var today = new Date();
    return today.pattern("yyyy-MM-dd")
}


//外发付款方法
function WFSK(checkedObj) {
    var SaveRecIDStr = "";
    var SaveJson = "";
    if (checkedObj.length == 0) return;
    for (var i = 0; i < checkedObj.length; i++) {
        SaveRecIDStr += "," + checkedObj[i].ID;
        //SaveJson += "[{'" + "付款状态" + "':'" + '已付款' + "'}],";
        SaveJson += "[{";
        SaveJson += "'" + "付款状态" + "':'" + '已付款' + "',";
        SaveJson += "'" + "付款日期" + "':'" + GetTodayStr() + "'";
        SaveJson += "}],";
    }
    BatchSaveWDInfo("495567150099", SaveRecIDStr, SaveJson, "付款成功！")
}

//月结方法
function YJZT(checkedObj) {
    var SaveRecIDStr = "";
    var SaveJson = "";
    if (checkedObj.length == 0) return;
    for (var i = 0; i < checkedObj.length; i++) {
        SaveRecIDStr += "," + checkedObj[i].ID;
        //SaveJson += "[{'" + "月结状态" + "':'" + '已月结' + "'}],";
        SaveJson += "[{";
        SaveJson += "'" + "月结状态" + "':'" + '已月结' + "',";
        SaveJson += "'" + "月结日期" + "':'" + GetTodayStr() + "'";
        SaveJson += "}],";
    }
    BatchSaveWDInfo("337257256365", SaveRecIDStr, SaveJson, "月结成功！")
}


//取消月结方法
function QXYJZT(checkedObj) {
    var SaveRecIDStr = "";
    var SaveJson = "";
    if (checkedObj.length == 0) return;
    for (var i = 0; i < checkedObj.length; i++) {
        SaveRecIDStr += "," + checkedObj[i].ID;
        //SaveJson += "[{'" + "月结状态" + "':'" + '未月结' + "'}],";
        SaveJson += "[{";
        SaveJson += "'" + "月结状态" + "':'" + '未月结' + "',";
        SaveJson += "'" + "月结日期" + "':'" + "" + "'";
        SaveJson += "}],";
    }
    BatchSaveWDInfo("337257256365", SaveRecIDStr, SaveJson, "取消月结成功！")
}

//外发收票方法
function WFSP(checkedObj) {
    var SaveRecIDStr = "";
    var SaveJson = "";
    if (checkedObj.length == 0) return;
    for (var i = 0; i < checkedObj.length; i++) {
        SaveRecIDStr += "," + checkedObj[i].ID;
        //SaveJson += "[{'" + "收票状态" + "':'" + '已收票' + "'}],";
        SaveJson += "[{";
        SaveJson += "'" + "收票状态" + "':'" + '已收票' + "',";
        SaveJson += "'" + "收票日期" + "':'" + GetTodayStr() + "'";
        SaveJson += "}],";
    }
    BatchSaveWDInfo("495567150099", SaveRecIDStr, SaveJson, "收票成功！")

}


