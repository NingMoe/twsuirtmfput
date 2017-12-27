 
function SetFieldStyle(ReportKey, value, row, index, IsChild)
{
    var s = ""
    switch (ReportKey)
    {
        case "BBXMTZXX":
            {
                if (row.计划结束日期 < GetTodayStr() && row.计划结束日期 != undefined) {
                    s = "background-color:#FFFF33;"
                }
            }
            break;
        case "XMXX":
            {
                if (row.项目状态 == '已完成') {
                    s = "background-color:#66FF66;"
                }
            }
            break;
    }
     
    return s
}


function SetFieldformatter(ReportKey, value, row, index, IsChild, Field) {
    var s = ""
    
    switch (ReportKey) {
        case "Report_NBSJ":
            {
                switch (Field) {
                    case "文件名":
                        {
                            s = "<span title='" + value + "'>" + value + "<span>";//实现鼠标放到单元格上时的提示
                        }
                        break;
                    case "操作":
                        {
                            if (row.审核状态 != '1')
                                s = "<a href='javascript:void(0)' class='easyui-linkbutton' style='margin-left:0px;color:#800080' data-options=\"iconCls:'icon-edit'," + "" + "plain:true\"   onclick=\"audit('" + row.项目名称 + "','" + row.项目编号 + "','" + row.员工账号 + "','" + row.BaseRecid + "')\">审批</a> ";
                        }
                        break;
                }
            }
            break;
        case "CommonProcessList":
            {
                switch (Field) {
                    case "操作":
                        {

                            if (row.是否完成 == 0) {
                                s = "<a href='javascript:void(0)' class='easyui-linkbutton'  data-options=\"iconCls:'icon-tip'," + "" + "plain:true\"  onclick=\"TipExecution('','','','" + row.流程实例ID + "')\">催办</a> ";

                                //s += "<a href='javascript:void(0)' class='easyui-linkbutton'  style='margin-left:10px;'  data-options=\"iconCls:'icon-cancel_Two'," + "" + "plain:true\"  onclick=\"StopExecution('','','','" + row.流程实例ID + "')\">结束</a> ";
                                if (row.是否锁定) {
                                    s += "<a href='javascript:void(0)' class='easyui-linkbutton'  style='margin-left:10px;'  data-options=\"iconCls:'icon-unlock'," + "" + "plain:true\"  onclick=\"TipExecution('','','','" + row.流程实例ID + "')\">解锁</a> ";
                                }
                                else {
                                    s += "<a href='javascript:void(0)' class='easyui-linkbutton'  style='margin-left:10px;'  data-options=\"iconCls:'icon-lock'," + "" + "plain:true\"  onclick=\"TipExecution('','','','" + row.流程实例ID + "')\">锁定</a> ";
                                }

                                //s += "<a href='javascript:void(0)' class='easyui-linkbutton'  style='margin-left:10px;'  data-options=\"iconCls:'icon-ok'," + "" + "plain:true\"  onclick=\"StopExecution('','','','" + row.流程实例ID + "')\">结束</a> ";

                                s += "<a href='javascript:void(0)' class='easyui-linkbutton'  style='margin-left:10px;'  data-options=\"iconCls:'icon-file-delete'," + "" + "plain:true\"  onclick=\"DelExecution('','','','" + row.流程实例ID + "')\">删除</a> ";

                            }
                            else {
                                s = "<a href='javascript:void(0)' class='easyui-linkbutton'  data-options=\"iconCls:'icon-file-preview'," + "" + "plain:true\"  onclick=\"GetExecutionUrl('" + row.RecordID + "')\">查阅</a> ";

                                s += "<a href='javascript:void(0)' class='easyui-linkbutton'  style='margin-left:10px;'  data-options=\"iconCls:'icon-file-delete'," + "" + "plain:true\"  onclick=\"DelExecution('','','','" + row.流程实例ID + "')\">删除</a> ";
                            }
                            break;
                        }
                }
            }
            break;
        case "XMXX":
        case "NDWHXM":
            {
                switch (Field) {
                    case "操作":
                        {
                            if (row.项目经理 == _UserName || _UserID=='0040') {
                                s += " <a   href=\"#\"  onclick=\"fnLink('Project/AddOrEditorXMPG.aspx?XMID=" + row.ID + "&PNumber=" + row.项目编号 + "&IsUpdate=true&IsDelete=true',1000,550)\"  style=\"text-decoration: none;color: #800080;\"  >" + "派工" + "</a>";

                                s += " | <a   href=\"#\"  onclick=\"fnLink('Project/AddOrEditorJJFP.aspx?XMID=" + row.ID + "&PNumber=" + row.项目编号 + "&IsUpdate=true&IsDelete=true',1000,550)\"  style=\"text-decoration: none;color: #800080;\"  >" + "奖金分配" + "</a>";
                            }
                        }
                        break;
                }
            }
            break;
    }
    return s
}


function SetFieldformatterByCom(ReportKey, value, row, index, IsChild, Field, DialogWidth, DialogHeight) {
    var s = ""
    var D = 0
    var H = 0
    
    if (DialogWidth == "")
        D = 700;
    else
        D = DialogWidth;

    if (DialogHeight == "")
        H = 400;
    else
        H = DialogHeight;

     
    switch (ReportKey) {
        case "XMXX":
            {
                D = 1000;
                switch (Field) {
                    case "操作":
                        {
                            if (row.项目经理 == _UserName || _UserID == "0001" || _UserID=='0040') {
                                s += " <a  href='#'  onclick=showWindow('派工信息','Project/AddorEditPGXX.aspx?ProjectCode=" + row.项目编号 + "&ProjectName=" + row.项目名称 + "'," +700 + "," + 400 + ")  style='text-decoration: none;color: #800080;'  >" + "派工" + "</a>";
                              
                                s += "<span style=\"margin-left:10px;\" >| </span> &nbsp;  <a  href='#'  onclick=showWindow('派工信息','Project/AddorEditXMWBGL.aspx?ProjectCode=" + row.项目编号 + "&ProjectName=" + row.项目名称 + "'," + 700 + "," + 360 + ")  style='text-decoration: none;color: #800080;'  >" + "外包" + "</a>";

                                s += " <span style=\"margin-left:10px;\" > | </span> &nbsp; <a  href='#'  onclick=showWindow('项目跟进记录','Project/AddorEditXMGJJL.aspx?ProjectCode=" + row.项目编号 + "&ProjectName=" + row.项目名称 + "'," + 700 + "," + 360 + ")  style='text-decoration: none;color: #800080;'  >" + "添加跟进记录" + "</a>";
                            }
                            else
                            {
                                s += " <span style=\"margin-left:10px;color: #CCCCCC ;\" >无此权限</span>";
                            }
                        }
                        break;
                }
            }
            break;
    }
    return s
} 
function TipExecution(checkedObj, PopUpPage, ThisGridID, r) {
    var tip = "催办流程"
    
    if (r == "") {
        var strWorkflowInstIds = "";
        if (checkedObj.length == 0) return;
        for (var i = 0; i < checkedObj.length; i++) {
            strWorkflowInstIds += "," + checkedObj[i].流程实例ID;
        }
        ProcessInstance("TipWorkflowInstance", strWorkflowInstIds, tip)
    }
    else {
        ProcessInstance("TipWorkflowInstance", r, tip)
    }

}

function StopExecution(checkedObj, PopUpPage, ThisGridID, r) {
    var tip = "结束流程"
    if (!confirm("是否要结束所选流程？")) {
        return
    }
    if (r == "") {
        var strWorkflowInstIds = "";
        if (checkedObj.length == 0) return;
        for (var i = 0; i < checkedObj.length; i++) {
            strWorkflowInstIds += "," + checkedObj[i].流程实例ID;
        }
        ProcessInstance("FinishWorkflowInstance", strWorkflowInstIds, tip)
    }
    else {
        ProcessInstance("FinishWorkflowInstance", r, tip)
    }
}

function DelExecution(checkedObj, PopUpPage, ThisGridID, r) {
    var tip = "删除流程"
    if (!confirm("是否要删除所选流程？")) {
        return
    }
    if (r == "") {
        var strWorkflowInstIds = "";
        if (checkedObj.length == 0) return;
        for (var i = 0; i < checkedObj.length; i++) {
            strWorkflowInstIds += "," + checkedObj[i].流程实例ID;
        }
        ProcessInstance("DeleteWorkflowInstance", strWorkflowInstIds, tip)
    }
    else {
        ProcessInstance("DeleteWorkflowInstance", r, tip)
    }
}

function RollBackExecution(checkedObj, PopUpPage, ThisGridID, r) {
    var tip = "回退流程"
    if (r == "") {
        var strWorkflowInstIds = "";
        if (checkedObj.length == 0) return;
        for (var i = 0; i < checkedObj.length; i++) {
            strWorkflowInstIds += "," + checkedObj[i].流程实例ID;
        }
        ProcessInstance("RollBackInstance", strWorkflowInstIds, tip)
    }
    else {
        ProcessInstance("RollBackInstance", r, tip)
    }
}

function RedirectEmployeeSelect(checkedObj, PopUpPage, ThisGridID, r) {
    var tip = "重选处理人"
    if (r == "") {
        var strWorkflowInstIds = "";
        if (checkedObj.length == 0) return;
        for (var i = 0; i < checkedObj.length; i++) {
            strWorkflowInstIds += "," + checkedObj[i].流程实例ID;
        }
        ProcessInstance("RedirectEmployeeSelect", strWorkflowInstIds, tip)
    }
    else {
        ProcessInstance("RedirectEmployeeSelect", r, tip)
    }
}


function ProcessInstance(typeValue, strWorkflowInstIds, tip) {
    var index = layer.load(1); //换了种风格
    $.ajax({
        type: "POST",
        url: window.location.protocol + "//" + window.location.host + "/webflow/admin/WorkflowManager_ajax.aspx",
        dataType: "json",
        data: {
            "typeValue": typeValue,
            "strWorkflowInstIds": strWorkflowInstIds
        },
        success: function (centerJson) {
            if (centerJson[0] != null) {
                if (centerJson[0].success) {
                    alert(tip + " 成功 " + centerJson[0].SucessCount + "个，失败" + (centerJson[0].Count - centerJson[0].SucessCount) + "个！");
                }
                else {
                    alert(tip + " 成功 " + centerJson[0].SucessCount + "个，失败" + (centerJson[0].Count - centerJson[0].SucessCount) + "个！\n\n" + centerJson[0].ErrorStr);
                }
            }
            layer.close(index);
            ReloadBaseGrid();
        },
        error: function (p) {
            layer.close(index);
            ReloadBaseGrid();
            debugger
        }
    });

}

function GetExecutionUrl(r) {
    window.open(CommonPath + "Execution2.aspx?RowID=" + r + "&Type=view&Hgt=2000")
}




function audit(r,x,BSUserName, BaseRecid) {
    
    window.open(CommonPath + "../ReportAuditProjectDetails/audit.aspx?CustomerNumber=" + r + "&ProjectNo=" + x + "&BSUserName=" + BSUserName + "&BaseRecid=" + BaseRecid)
}
