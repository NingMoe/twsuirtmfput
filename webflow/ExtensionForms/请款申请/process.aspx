<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="System.Data.SqlClient"%>
<%@ import namespace="NetReusables"%>
<%@ Import namespace="Unionsoft.Workflow.Items"%>
<%@ Import namespace="Unionsoft.Workflow.Platform"%>
<%@ Import namespace="Unionsoft.Workflow.Engine"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.UserPageBase" validateRequest="false"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
	<title>请款申请</title>
	<script type="text/javascript" src="../scripts/DataConvert.js"></script>
	<script type="text/javascript" src="../scripts/FormValidate.js"></script>
	<script type="text/javascript" src="../scripts/DateCalendar.js"></script>
	<script type="text/javascript" src="../scripts/dragDiv.js"></script>
    <link href="../css/YYTys.css" rel="stylesheet" type="text/css" />
</head>
<body>
	<!--#include file="../includes/WorkflowUtilies.aspx"-->
        <!--#include file="../includes/WorkflowAttachment.aspx"-->
<script language="vb" runat="server">
Private ActionKey As String
Private WorkflowInstId As String
Private WorklistItemId As String
Private hstFormValues As Hashtable
Private table As DataTable

Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
	If Request.Form("action_value") <> "" Then ActionKey = Request.Form("action_value")
	If Request("WorkflowInstId") <>"" Then WorkflowInstId = Request("WorkflowInstId")
	If Request("WorklistItemId") <>"" Then WorklistItemId = Request("WorklistItemId")
	
	Dim oWorklistItem As WorklistItem = Worklist.GetWorklistItem(WorklistItemId)
	ViewState.Item("_Id") = oWorklistItem.ActivityInstance.WorkflowInstance.RecordId
    hstFormValues = GetFormValues(oWorklistItem.ActivityInstance.WorkflowInstance.RecordID, "CT282994819609")

	If Not Me.IsPostBack Then Return
	If ActionKey="" Then Return  
    
	ProcessWorklistItem(oWorklistItem,ActionKey,hstFormValues,Request.Form("memo"))
 End Sub
</script>
    <link rel="stylesheet" type="text/css" href="../easyUI/jquery-easyui-1.4.3/themes/gray/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../easyUI/jquery-easyui-1.4.3/themes/icon.css"/> 
    <script type="text/javascript" src="../easyUI/jquery-easyui-1.4.3/jquery.min.js"></script>
    <script type="text/javascript" src="../easyUI/jquery-easyui-1.4.3/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../easyUI/easyui-lang-zh_CN.js" ></script>
    <script language="javascript" type="text/javascript">
    $(function () {
       // loadCenterGrid();
    });

    function loadCenterGrid() {
        $('#CenterGrid').datagrid({
            toolbar: [{
                iconCls: 'icon-add',
                text: "查看",
                handler: function () {
                    var rec = $('#CenterGrid').datagrid('getSelected');
                    if (rec == null) {
                        alert("请选择一条记录!")
                    } else {
                        fnFormListDialog("Add.aspx?RecID=" + rec.ID, 600, 400, "查看信息");
                    }
                }
            }],
            nowrap: true,
            border: true,
            striped: true,
            singleSelect: true,
            loadFilter: function (data) {
                if (data == null) {
                    $(this).datagrid("load");
                    return $(this).datagrid("getData");
                } else {
                    return data;
                }
            },
            url: '../common/AjaxRequest.aspx?typeValue=GetCGMX',
            queryParams: {
                QKBH: '<%=hstFormValues("C3_282994956906") %>'
            },
            columns: [[
	            { field: '物品名称', title: '物品名称', width: 80, align: 'center' },
	            { field: '数量', title: '数量', width: 60, align: 'center' },
                { field: '单价', title: '单价', width: 60, align: 'center' },
                { field: '用途', title: '用途', width: 260, align: 'center' },
                { field: '金额', title: '金额', width: 60, align: 'center'}]],
            fitColumns: true,
            fit: true,
            rownumbers: true
        });
    }


    function fnFormListDialog(url, DialogWidth, DialogHeight, title) {
        $('#divParentContent').append($("<iframe scrolling='no' id='FromInfo' frameborder='0' marginwidth='0' marginheight='0' style='width:100%;height:100%;'></iframe")).dialog({
            title: title,
            width: DialogWidth,
            height: DialogHeight,
            cache: false,
            closed: true,
            shadow: false,
            closable: true,
            draggable: false,
            resizable: false,
            modal: true
        });
        var index = url.indexOf("?");
        if (index != "-1") {
            url = url + "&height=" + DialogHeight;
        } else {
            url = url + "?height=" + DialogHeight;
        }
        $('#FromInfo')[0].src = encodeURI(url);
        $('#divParentContent').dialog('open');
    }

    function ParentCloseWindow() {
        $('#divParentContent').dialog('close');
        //$('#CenterGrid').dialog('loadData');
    }
</script>

<form id="Form1" method="post" runat="server">
        <%GenerateActInstCommand(Convert.ToInt64(WorklistItemId),"")%>
       <h1 align="center">请款申请</h1>
       <table width="700" border="0" align="center" cellpadding="0" cellspacing="0" class="Bold_box">
            <tr>
                <td width="100" class="F_center">
                    申请人</td>
                <td width="250" align="left" valign="middle">
                    <input name="C3_282994864593" type="text" class="noborder" id="C3_282994864593" style="width: 100px"
                       value='<%=hstFormValues("C3_282994864593") %>' readonly /></td>
                <td width="100" class="F_center" style="height:30px;">
                    申请日期</td>
                <td width="250" align="left" valign="middle">
                    <input name="C3_282994853625" type="text" class="noborder" id="C3_282994853625" style="width: 70px"
                        value='<%=hstFormValues("C3_282994853625") %>' readonly  /></td>
            </tr>
             <tr>
                <td width="100" class="F_center">
                    请款金额</td>
                <td width="250" align="left" valign="middle">
                    <input name="C3_282994932921" type="text" class="noborder" id="C3_282994932921"  value='<%=hstFormValues("C3_282994932921") %>' readonly style="width: 100px" /></td>
                <td width="100" class="F_center" style="height:30px;">
                    采购金额合计</td>
                <td width="250" align="left" valign="middle">
                    <input name="C3_282994883125" type="text" class="noborder" id="C3_282994883125"  value='<%=hstFormValues("C3_282994883125") %>' readonly style="width: 70px" /></td>
            </tr>
            <tr>
                <td class="F_center" >请款事由</td>
                <td height="98" colspan="3"  align="left" valign="top">
			        <%=hstFormValues("C3_282994921125") %></td>
            </tr>
            <tr>
                <td class="F_center"  style="height:40px;">报销科目名称</td>  
                <td align="left" colspan="3"  valign="center" >
                    <input style="width: 98%;height:30px;" id="C3_384347781708"  value='<%=hstFormValues("C3_384347781708") %>' readonly="readonly" type="text"  name="C3_384347781708" />
                </td>  
            </tr>
            <tr> 
                <td class="F_center" style="height: 40px;">外包项目编号</td>
                <td align="left" colspan="3"  valign="center" >
                     <input name="C3_386163735265" id="C3_386163735265" style="height:30px;width:98%"  value='<%=hstFormValues("C3_386163735265") %>' readonly="readonly" />
                </td>
            </tr>
         <%--   <tr><td colspan="4" style="height:150px;">
                <table id="CenterGrid"></table>
            </td></tr>--%>
             <tr style="height: 30px;">
                <td class="F_center" colspan="4"  style="height: 30px;">财务状态</td>
            </tr> 
           <tr style="height: 40px;">
               <td class="F_center" >是否已汇款</td>
               <td align="left"  valign="middle" ><input type="text" id="C3_284725263953" name="C3_284725263953"  value='<%=hstFormValues("C3_284725263953") %>' readonly="readonly" style="width:95%;height:30px;" /></td>
            <td class="F_center" >是否已转为报销</td>
                <td align="left" valign="middle" ><input type="text" id="C3_284725355500" name="C3_284725355500"  value='<%=hstFormValues("C3_284725355500") %>' readonly="readonly" style="width:95%;height:30px;" /></td>
           </tr> 
            <tr style="height: 40px;">
                <td class="F_center"  style="height: 30px;">关联报销单编号</td>
                <td  colspan="3" ><input id="C3_284725426953" name="C3_284725426953" style="height:30px;width:98%" value='<%=hstFormValues("C3_284725426953") %>' readonly="readonly" /></td>
            </tr> 
        </table>
    <div closed="true"  class="easyui-window"  id="divParentContent" style="overflow:hidden;"/>
</form>
</body>
</html>
