function DeleteDept()
{
    //var ParamsList = getParams();
    //var Index = GetParamsListIndex(ParamsList, "DeptID");
    var o = $(_GridID).datagrid('getSelected');
    var DeptID = o.ID


        if (DeptID == "")
            return;

        $.ajax({
            type: "POST",
            dataType: "json",
            url: "Common/Ajax_Request.aspx?typeValue=GetDataBySql",
            data: {
                "sqlData": "SELECT (SELECT NAME AS Name FROM CMS_DEPARTMENT WHERE ID='" + DeptID + "') Name, (SELECT COUNT(*) FROM dbo.CMS_DEPARTMENT WHERE PID='" + DeptID + "') ZBM, ( SELECT  COUNT(*)  FROM dbo.CMS_EMPLOYEE  WHERE HOST_ID='" + DeptID + "') XT, ( SELECT COUNT(*) FROM EmployeeInfo WHERE DeparmentID='" + DeptID + "') YG ",
                "Condition": " "
            },
            success: function (o) {
                var obj=o.rows
                if (obj.length > 0) {

                    if (obj[0].ZBM > 0) {
                        alert("该部门下有子部门，不能删除！")
                    } else if (obj[0].XT > 0 || obj[0].YG > 0) {
                        alert("该部门下还有员工，不能删除！")
                    }
                    else {
                        if (confirm("是否需要删除【" + obj[0].Name + "】部门？"))
                            DeleteDataBySQL(" DELETE FROM dbo.CMS_DEPARTMENT WHERE id=" + DeptID, "【" + obj[0].Name + "】部门")
                    }
                } else {
                    //alert("保存失败,请刷新页面！");
                }
            },
            error: function (obj) {
                debugger
            }
        });
    
}


function DeleteDataBySQL(DelSql,tip) {
 
    $.ajax({
        type: "POST",
        dataType: "json",
        data: {
            "sql": DelSql
        },
        url: "Organization/ZTH_GetInfo_ajax.aspx?typeValue=ExecSql",
        success: function (obj) {
            if (obj > 0) {
                alert(tip + "删除成功，共删除" + obj + "条记录！");
            } else {
                alert(tip + "删除失败,请刷新页面！");
            }
        },
        error: function (obj) {
            debugger
        }
    });
}
 
function getParams()
{
    if (_Params == "")
        return "";

    var ParamsList = [];
    if (_Params.indexOf("&") == -1)
    {
        if (_Params.indexOf("=") > -1) {
            ParamsList.push({
                Name: _Params.split("=")[0],
                Value: _Params.split("=")[1]
            })
        }
    }
    else
    {
        var paras= ParamsList.split("&");
        for (var i = 0; i < paras.length; i++) {
            var ps = paras[i]
            if (ps.indexOf("=") > -1) {
                ParamsList.push({
                    Name: ps.split("=")[0],
                    Value: ps.split("=")[1]
                })
            }
        }
    }
    return ParamsList
}
  
function GetParamsListIndex(ParamsList,name)
{
    for (var i = 0; i < ParamsList.length; i++) {
        if ( ParamsList[i].Name==name)
          return i
    }
    return -1
}


function GetDataListJson(obj)
{
    var json = [];
    for(var k in obj)
    {
        json.push({
            text: k
        })
    }
    return json
}


function SetReadOnly(ResID) {
    var textInput = $("#div" + ResID + "FormTable").find("input:text");
    var radioInput = $("#div" + ResID + "FormTable").find("input:radio");
    var textarea = $("#div" + ResID + "FormTable").find("textarea");
    var select = $("#div" + ResID + "FormTable").find("select");
    var checkboxInput = $("#div" + ResID + "FormTable").find("input:checkbox");
    checkboxInput.attr("disabled", "disabled");
    radioInput.attr("disabled", "disabled");
    textInput.attr("readonly", "readonly");
    textarea.attr("readonly", "readonly");
    select.attr("disabled", "disabled");
    $("#fnChildSave").hide();
}
 
function GetUserDefinedSqlBykeyWordValue(keyWordValue) {
    var sql = "";
    switch (keyWordValue)
    {
        case "WDKQ":
            break;
    }
    return "";
}
 
function GetUserCondition(keyWordValue) {
    var c = "";
    switch (keyWordValue) {
        case "WDKQ":
            c = " DATEPART(YEAR, DATEADD(MONTH,1,日期))=  DATEPART(YEAR,GETDATE()) AND  DATEPART(month, DATEADD(MONTH,1,日期))=  DATEPART(month,GETDATE()) ";
            break;
    }
    return c == "" ? "" : " and " + c;
}
 