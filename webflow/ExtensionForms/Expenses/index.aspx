<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="System.Data.SqlClient"%>
<%@ import namespace="NetReusables"%>
<%@ Import namespace="Unionsoft.Workflow.Items"%>
<%@ Import namespace="Unionsoft.Workflow.Platform"%>
<%@ Import namespace="Unionsoft.Workflow.Engine"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.UserPageBase" validateRequest="false"  %>
<HTML>
	<HEAD>
		<title>报销申请单</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<!--弹出层js文件--------->
		<SCRIPT type="text/javascript" LANGUAGE="JavaScript" src="../scripts/dragDiv.js"></SCRIPT>
		<!--验证页面文本框必填及类型 js文件----------->
		<SCRIPT type="text/javascript" LANGUAGE="JavaScript" src="../scripts/FormValidate.js"></SCRIPT>
		<!--日期控件-->
		<script type="text/javascript" language="javascript" src="../scripts/DateCalendar.js"></script>
		<!--金额大写js文件------------->
		<script type="text/javascript" language="javascript" src="../scripts/DataConvert.js"></script>
		<script type="text/javascript" src="../scripts/jquery.min.js"></script>
		<link   href="../css/YYTys.css" rel="stylesheet" type="text/css" />
	<script>
	function selectProject(code,name)
	{
		document.getElementById("txtEmpName").value=name;
	}
	</script>
	
	
	<script type="text/javascript" language="javascript">
        var rows=3;
		var checkSum=3;
        function addRow(tableName)
		  {		 
		  	var obj=document.getElementById(tableName)
		    var row=obj.insertRow(obj.rows.length-1);			
		    rows++;
			checkSum++;
		    document.getElementById("txtRowCount").value=rows;		   	
		    var col;
		    if(tableName=="tbDetails")
		    {	    	     
		      col=row.insertCell(0);
			  col.innerHTML=rows;
			  col=row.insertCell(1);
		      col.innerHTML="<input type='text' id='ExpDate"+rows+"' name='ExpDate"+rows+"' class='noborder' style='width:70px' fieldtype='date' onFocus='Cal_dropdown(this);'/><input name='ExpID"+rows+"' id='ExpID"+rows+"' type='hidden' class='noborder' value='<%=GetCode()%>'>";
			  col=row.insertCell(2);
		      col.innerHTML="<textarea name='Explain"+rows+"' class='noborder' id='Explain"+rows+"' style='width:380px'></textarea>";
			  col=row.insertCell(3);
		      col.innerHTML="<input name='Price"+rows+"'  type='text' class='noborder' id='Price"+rows+"' style='width:70px' onKeyUp='Chenfa("+rows+");' onKeyPress='if((event.keyCode<48||event.keyCode>57)&amp;&amp; event.keyCode!=46)event.returnValue=false;' onpaste='return false'/>";
			   col=row.insertCell(4);
		      col.innerHTML="<input name='Number"+rows+"' value='1' type='text' class='noborder' id='Number"+rows+"' style='width:70px' onKeyUp='Chenfa("+rows+");' onKeyPress='if((event.keyCode<48||event.keyCode>57)&amp;&amp; event.keyCode!=46)event.returnValue=false;' onpaste='return false'/>";  
			  col=row.insertCell(5);
		      col.innerHTML="<input name='Amount"+rows+"' type='text' class='noborder' id='Amount"+rows+"' style='width:70px' onKeyUp='CountSum();' onblur='fnBlur("+rows+")' onKeyPress='if((event.keyCode<48||event.keyCode>57)&amp;&amp; event.keyCode!=46)event.returnValue=false;' onpaste='return false'/>";
			  col=row.insertCell(6);
		      col.innerHTML="<img src='../../images/del.gif' name='img"+rows+"' alt='删除' onclick='delRow(this);'/>";      
		    }
		  }
		    function delRow(o)
		    {		 
		      var objTR =o.parentNode.parentNode; 
			  var currRowIndex= objTR.rowIndex; 
			  if(confirm("您确认要删除?")) 
			  { 
			   objTR.parentNode.deleteRow(currRowIndex); 
			   CountSum();
			   checkSum--;
			  }		    
		    }
			
		
	</script>
	
<script language="javascript">
function CountSum()
{
    var Sum=0;
    var Price=0;
	var inputList=document.getElementById("tbDetails").getElementsByTagName("input");
 
 
	    for(var j=0;j<inputList.length;j++)
	    {
	        if(inputList[j].id.indexOf("Amount")>=0)
	        {
	            if(inputList[j].value!=""){ Amount=inputList[j].value;}
	            else{ Amount=0;}
	            Sum=parseFloat(Amount)+parseFloat(Sum);
	        }
	    }
  
	
	document.getElementById("Sum").value=ForDight(Sum,2);
	document.getElementById("CapitalSum").value=ConvertToCnMoney(document.getElementById("Sum").value);
	Allsum();
}

function Chenfa(Column)
			{
			var sum=document.getElementById("Price"+Column).value*document.getElementById("Number"+Column).value;
			document.getElementById("Amount"+Column).value=ForDight(sum,2);
			CountSum();
			}
			
//function CountSum1()
//{
//    var Sum=0;
//    var Price=0;
//	var inputList=document.getElementById("ZanZ").getElementsByTagName("input");
// 
// 
//	    for(var j=0;j<inputList.length;j++)
//	    { 
//	        if(inputList[j].id.indexOf("Price")>=0)
//	        {//alert(inputList[j].id.replace("Price","ZZ_Offset"));
//				if(document.getElementById(inputList[j].id.replace("Price","ZZ_Offset")).checked)
//				{
//					if(inputList[j].value!="") Price=inputList[j].value;
//					else Price=0;
//					Sum=parseFloat(Price)+parseFloat(Sum);
//				}
//	        }
//			
//	    }
//  
//	document.getElementById("ZZ_Allsum").value=ForDight(Sum,2);
//	Allsum();
//}

function Allsum()
{
		var 	tts=isNaN(parseFloat(Form1.Sum.value,10))?0:parseFloat(Form1.Sum.value,10);
		var 	tbs=isNaN(parseFloat(Form1.ZZ_Allsum.value,10))?0:parseFloat(Form1.ZZ_Allsum.value,10);	
		Form1.Margin.value=tts-tbs;
}

function ForDight(Dight,How) //控制小写位数
{
	Dight=Math.round(Dight*Math.pow(10,How))/Math.pow(10,How);
	return  Dight;
}
</script>



	</head>
	<!--#include file="../includes/WorkflowUtilies.aspx"-->
	<!--#include file="../includes/WorkflowAttachment.aspx"-->

	<script language="vb" runat="server">
	Private ActionKey As String
	Private WorkflowId As String
	Public cmd As SqlCommand
    Public conn As SqlConnection
   Dim hstFValues As New Hashtable()
	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
	        If ViewState.Item("_Id") Is Nothing Then ViewState("_Id") = TimeId.CurrentMilliseconds(30)
		If Request.Form("action_value") <> "" Then ActionKey = Request.Form("action_value")
		If Request("WorkflowId") <>"" Then WorkflowId = Request("WorkflowId")
		If Not Me.IsPostBack Then Return
		If ActionKey = "" Then Return
		
        '定义
		Dim hstFormValues As New Hashtable()
		'取表单数据
		hstFormValues.Add("Id",ViewState("_Id"))
	        hstFormValues.Add("RESID", 425383682998)
	        hstFormValues.Add("ProjectName", Request.Form("ProjectName")) '项目名称
	        hstFormValues.Add("ProjectCode", Request.Form("ProjectCode")) '项目编号
	        hstFormValues.Add("ExpensesID", Request.Form("ExpensesID"))  '报销编号
	        hstFormValues.Add("Month", Request.Form("Month"))  '报销编号
	        hstFormValues.Add("EmpCode", Request.Form("EmpCode"))
	        hstFormValues.Add("EmpName", Request.Form("EmpName"))
	        hstFormValues.Add("Department", Request.Form("Department"))
	        hstFormValues.Add("Position", Request.Form("Position"))
	        hstFormValues.Add("AppDate", Request.Form("AppDate"))  '申请日期
	        hstFormValues.Add("Sum", Request.Form("Sum")) '合计
	        hstFormValues.Add("CapitalSum", Request.Form("CapitalSum")) '合计大写
	        hstFormValues.Add("Remarks", Request.Form("Remarks"))
	'写入 新建的流程表WORKFLOW_FORM_ProjectChange
	        SDbStatement.InsertRow(hstFormValues, "PM_Expenses")
		
	        Dim rowCount As Int32 = Convert.ToInt32(Me.txtRowCount.Value)
	      
        For i As Integer = 1 To rowCount
		 If (Request.Form("Explain" & i)<>"") Then
		Dim hstFormfybx As New Hashtable()
        hstFormfybx.Add("ID",TimeId.CurrentMilliseconds(30)) 
	                hstFormfybx.Add("RESID", 425387278782)
	                hstFormfybx.Add("ExpID", Request.Form("ExpID" & i)) '报销编号
	                hstFormfybx.Add("ExpDate", Request.Form("ExpDate" & i)) '使用日期
	                hstFormfybx.Add("Explain", Request.Form("Explain" & i)) '说明
	                hstFormfybx.Add("Price", Request.Form("Price" & i)) '单价
	                hstFormfybx.Add("Number", Request.Form("Number" & i)) '数量
	                hstFormfybx.Add("Amount", Request.Form("Amount" & i)) '金额
	                hstFormfybx.Add("EmpName", Request.Form("EmpName"))
	                hstFormfybx.Add("EmpCode", Request.Form("EmpCode"))
	                SDbStatement.InsertRow(hstFormfybx, "PM_ExpDetail")
	                  
		End If
		Next
		
		Dim oWorklistItem As WorklistItem = CreateWorkflowInstance(Convert.ToInt64(WorkflowId),Convert.ToInt64(ViewState("_Id")),ActionKey,hstFormValues,"")
		hstFormValues.Add("WorkflowInstId",oWorklistItem.ActivityInstance.WorkflowInstance.Key)
		StartWorkflowInstance(oWorklistItem)
		
		
	End Sub
	
	private Function GetCode() as string
	        Dim dt As DataTable = SDbStatement.Query("select IsNull(Max(ExpensesID),0) Code from PM_Expenses where ExpensesID like '" + Date.Now.ToString("yyyyMMdd") + "%'").Tables(0)
		if dt.Rows.Count>0 then
			if DbField.GetLng(dt.Rows(0),"Code")=0 then
				return Date.Now.ToString("yyyyMMdd")+"0001"
			else
				return Date.Now.ToString("yyyyMMdd")+ Convert.ToInt32(Convert.ToInt32(DbField.GetStr(dt.Rows(0),"Code").SubString(DbField.GetStr(dt.Rows(0),"Code").length-4))+1).ToString("0000")
			end if
		else
			return Date.Now.ToString("yyyyMMdd")+"0001"
		end if
	end Function
	</script>
	 
<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		<!--显示按钮-->
<%GenerateCommand(Convert.ToInt64(WorkflowId), "return CheckValue(Form1) && Validate() && ValidateNum();")%>

<h1 align="center">报销申请单</h1>
<table width="900" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td>&nbsp;</td>
	<td align="center" style="text-align:center">
	    <div align="center">
	          <input name="ExpensesID" id="ExpensesID" type="text" class="noborder" style="width:100px" value="<%=GetCode()%>" readonly/>
      </div></td>
  </tr>
</table>
<table width="900" border="0" align="center" cellpadding="0" cellspacing="0" class="ziti">
  <tr>
    <td width="49" style="font-size: 12px;line-height: 15px;color: #666666;text-decoration: none;">报销人:</td>
    <td width="80" style="font-size: 12px;line-height: 15px;color: #666666;text-decoration: none;"><input   type="hidden"  name="EmpCode" id="EmpCode" value="<%=CurrentUser.Code%>"/><input name="EmpName" id="EmpName" type="text" class="noborder" style="width:70px" value="<%=CurrentUser.Name%>"/></td>
    <td width="32" style="font-size: 12px;line-height: 15px;color: #666666;text-decoration: none;">部门:</td>
    <td width="105" style="font-size: 12px;line-height: 15px;color: #666666;text-decoration: none;"><input name="Department" id="Department" type="text" class="noborder" style="width:100px" value="<%=GetDepartment(CurrentUser.Code)%>"/></td>
	<td width="33" style="font-size: 12px;line-height: 15px;color: #666666;text-decoration: none;">职务:</td>
    <td width="119" style="font-size: 12px;line-height: 15px;color: #666666;text-decoration: none;"><input name="Position" id="Position" type="text" class="noborder" style="width:100px" value="<%=CurrentUser.HeadShip%>"/></td>
	<td width="58" style="font-size: 12px;line-height: 15px;color: #666666;text-decoration: none;">申请日期:</td>
    <td width="85" style="font-size: 12px;line-height: 15px;color: #666666;text-decoration: none;"><input type="hidden" id="Month" name='Month',class="noborder", value="<%=DateTime.Now.month%>"/><input name="AppDate" id="AppDate" type="text" class="noborder" style="width:121px"  value='<%=DateTime.Now.ToString("yyyy-MM-dd")%>'/></td>
  </tr>
</table>
<table width="900" border="0" align="center" cellpadding="0" cellspacing="0" class="Bold_box" id="tbDetails" style="border-bottom-width:0px">
 <tr style="height: 30px;">
                <td class="F_center" colspan="2">
                    项目名称</td>
                <td>&nbsp;<input name="ProjectName" id="ProjectName" type="text" class="noborder" style="width:300px" FieldTitle="项目名称" minput="true" value="<%=hstFValues("ProName")%>"  /><img style="cursor: hand; border: 0;"src="../../images/query1.gif" onclick="showMessageBox(800,300,'ProjectChoose.aspx','项目列表');"alt="项目列表" />
                </td>
                <td class="F_center">
                    项目编号</td>
                <td colspan="4"> &nbsp;<input name="ProjectCode" id="ProjectCode" type="text" class="noborder" style="width:120px"
                       value="<%=hstFValues("ProCode")%>" readonly /></td>
            </tr>
  <tr>
    <td width="47" class="F_center">序号</td>
    <td width="99" class="F_center">用款日期</td>
    <td width="392" class="F_center">说明</td>
    <td width="80" class="F_center">单价</td>
    <td width="76" class="F_center">数量</td>
    <td width="80" class="F_center">金额</td>
	<td width="43" class="F_center">操作</td>
  </tr>
  <%For i As Integer = 1 To 3%>
  <tr>
    <td><%=i%></td>
    <td><input name="ExpDate<%=i%>" id="ExpDate<%=i%>" type="text" class="noborder" style="width:70px" fieldtype="date" onFocus="Cal_dropdown(this);"/><input name="ExpID<%=i%>" id="ExpID<%=i%>" type="hidden" class="noborder" value="<%=GetCode()%>"/>
    <td><textarea name="Explain<%=i%>" class="noborder" id="Explain<%=i%>" style="width:380px"></textarea></td>
    <td><input name="price<%=i%>" id="price<%=i%>" type="text" class="noborder" style="width:70px" onKeyUp="Chenfa(<%=i%>)" onKeyPress="if((event.keyCode<48||event.keyCode>57)&amp;&amp; event.keyCode!=46)event.returnValue=false;" onpaste="return false"/></td>
    <td><input name="Number<%=i%>" id="Number<%=i %>" type="text" class="noborder" style="width:70px" onKeyUp="Chenfa(<%=i%>)" onKeyPress="if((event.keyCode<48||event.keyCode>57)&amp;&amp; event.keyCode!=46)event.returnValue=false;" onpaste="return false" value="1"/></td>
	<td><input name="Amount<%=i%>" id="Amount<%=i%>" type="text" class="noborder" style="width:70px" onKeyPress="if((event.keyCode<48||event.keyCode>57)&amp;&amp; event.keyCode!=46)event.returnValue=false;" onpaste="return false"/ readonly></td>
	<td><img src="../images/del.gif" id="img<%=i%>" name="img<%=i%>" alt="删除" onClick="delRow(this);" ></td>
  </tr>
  <% Next%>
  <tr>
    <td colspan="2" class="F_center">合计</td>
	<td><input name="Sum" class="noborder" id="Sum" style="width:100px" value="0"></td>
    <td class="F_center">合计大写</td>
	<td colspan="3"><input name="CapitalSum" class="noborder" id="CapitalSum" style="width:140px"></td>
	<td width="39" align="center" style="padding: 0px;"><img src="../images/add.png" onClick="addRow('tbDetails');" border="0" width="20" align="absmiddle"></td>
  </tr>
 
  </table>
<table width="900" border="0" align="center" cellpadding="0" cellspacing="0" class="Bold_box" style="border-top-width:0px; border-bottom-width:0px">
  <tr>
	<td width="152" class="F_center">备注</td>
	<td><textarea name="Remarks" class="noborder" id="Remarks" style="width:740px"></textarea></td>
  </tr>
</table>

<br>
<input type="hidden" id="txtRowCount" value="3" runat="server" size="4" name="txtRowCount">
<input type="hidden" id="txtNO" value='<%=Request.QueryString("NO")%>'/>
</form>
	</body>
</HTML>

