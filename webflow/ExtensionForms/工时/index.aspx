<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="System.Data.SqlClient"%>
<%@ import namespace="NetReusables"%>
<%@ Import namespace="Unionsoft.Workflow.Items"%>
<%@ Import namespace="Unionsoft.Workflow.Platform"%>
<%@ Import namespace="Unionsoft.Workflow.Engine"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.UserPageBase" validateRequest="false"  %>
<html xmlns="http://www.w3.org/1999/xhtml">

	<head>
		<title>业务单元余额录入</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
		<meta name="vs_defaultClientScript" content="JavaScript" />
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<!--弹出层js文件--------->
		<script type="text/javascript" language="JavaScript" src="../scripts/dragDiv.js"></script>
		<!--验证页面文本框必填及类型 js文件----------->
		<script type="text/javascript" language="JavaScript" src="../scripts/FormValidate.js"></script>
		<!--日期控件-->
		<script type="text/javascript" language="javascript" src="../scripts/DateCalendar.js"></script>
		<!--金额大写js文件------------->
		<script type="text/javascript" language="javascript" src="../scripts/DataConvert.js"></script>
		<link   href="../css/YYTys.css" rel="stylesheet" type="text/css" />
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
		    var col
		    if(tableName=="tbDetails")
		    {	
			  row.style.textAlign="center";    
		      col=row.insertCell(0);
		      col.innerHTML="<input name='ProjectName"+rows+"' id='ProjectName"+rows+"' type='text' class='noborder' style='width:180px' readonly/><input id='ProjectCode"+rows+"' name='ProjectCode"+rows+"' type='hidden' class='noborder'  style='width:1px' /><img style='cursor: hand; border: 0;' src='../../images/query1.gif' onclick=showMessageBox(800,300,'Choose.aspx?NO="+rows+"','项目列表'); alt='项目列表' />";
		      col=row.insertCell(1);
		      col.innerHTML="<input name='Remark"+rows+"' id='Remark"+rows+"' type='text' class='noborder' style='width:130px' />";
		      col=row.insertCell(2);
		      col.innerHTML="<input name='Y_sum"+rows+"' id='Y_sum"+rows+"' type='text' class='noborder' style='width:90px' FieldType='num' FieldTitle='应分配总额' onkeyup='getTotalSum();Chenfa("+rows+")'/>";
              col=row.insertCell(3);
		      col.innerHTML="<input name='J_sum"+rows+"' id='J_sum"+rows+"' type='text' class='noborder' style='width:90px' FieldType='num' FieldTitle='奖励' onkeyup='getTotalAward();Chenfa("+rows+")'/>";
			  col=row.insertCell(4);
		      col.innerHTML="<input id='K_sum"+rows+"' name='K_sum"+rows+"' type='text' class='noborder'  style='width:90px' FieldType='num' FieldTitle='考核' onkeyup='getTotalCheck();Chenfa("+rows+")'/>";	
			  col=row.insertCell(5);
		      col.innerHTML="<input id='A_Sum"+rows+"' name='A_Sum"+rows+"' type='text' class='noborder' style='width:90px' readonly />";
		      col=row.insertCell(6);
		      col.innerHTML="<img src='../images/del.gif' name='img"+rows+"' alt='删除' onclick='delRow(this);'/>";      
		    }
		  }
		    function delRow(o)
		    {		 
		      var objTR =o.parentNode.parentNode; 
			  var currRowIndex= objTR.rowIndex; 
			  if(confirm("您确认要删除?")) 
			  { 
			   objTR.parentNode.deleteRow(currRowIndex); 
			    getTotalSum();
			    getTotalAward();
			    getTotalCheck();
			    getAttriSum();
			    checkSum--;
			  }		    
		    }
           //选取项目类型
           function getList(){ 
		        var listStr=AjaxClass.getAttriType().value;		    
				var listValue = listStr.split("|");
				var arg=document.documentElement.getElementsByTagName("select");
				for(var i=0;i<arg.length;i++)
                {
                    document.getElementById(arg[i].id).options.length=0;
				    document.getElementById(arg[i].id).options[0] = new Option("");
				    for (j=0;j<listValue.length ;j++ )    
				    {
					    document.getElementById(arg[i].id).options[j+1] = new Option(listValue[j].split(",")[0],listValue[j].split(",")[1]);
				    }
                }
           }
		 function fnOnChangeName(No){  			    
				var objSelect = document.getElementById("ProjectName"+No);
				for(var i=0;i<objSelect.options.length;i++)
				{
					if(objSelect.options[i].selected == true)
					{
					    document.getElementById("ProjectName"+No).value=objSelect.options[i].text;						 						 
					}
				}
				//添加行加载select
		function getselectValue(id){ 
		        var listStr=AjaxClass.getAttriType().value;
				var listValue = listStr.split("|");
				document.getElementById("ProjectName"+id).options.length=0;
				document.getElementById("ProjectName"+id).options[0] = new Option("");  
				for (i=0;i<listValue.length ;i++ )    
				{				
					document.getElementById("ProjectName"+id).options[i+1] = new Option(listValue[i].split(",")[0],listValue[i].split(",")[1]);
				}
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

	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If ViewState.Item("_Id") Is Nothing  Then ViewState("_Id")=TimeId.CurrentMilliseconds(30)
		If Request.Form("action_value") <> "" Then ActionKey = Request.Form("action_value")
		If Request("WorkflowId") <>"" Then WorkflowId = Request("WorkflowId")
		
		If Not Me.IsPostBack Then Return
		If ActionKey = "" Then Return
		
        '定义
		Dim hstFormValues As New Hashtable()
		'取表单数据
	        hstFormValues.Add("Id", ViewState("_Id"))
	        hstFormValues.Add("RELID", 0)
	        hstFormValues.Add("CRTID", CurrentUser.Name)
	        hstFormValues.Add("CRTTIME", DateTime.Now)
	        hstFormValues.Add("EDTID", CurrentUser.Name)
	        hstFormValues.Add("EDTTIME", DateTime.Now)
	        hstFormValues.Add("RESID", 425054103832)
	        hstFormValues.Add("ItemNum", Request.Form("ItemNum")) '编号
	        hstFormValues.Add("EMPName", CurrentUser.Name)
	        hstFormValues.Add("EMPCode", CurrentUser.Code)
	        hstFormValues.Add("SubmitDate", Request.Form("SubmitDate"))
	        hstFormValues.Add("ProjectName", Request.Form("ProjectName"))
	        hstFormValues.Add("ProjectCode", Request.Form("ProjectCode"))
	        hstFormValues.Add("WorkTime", Request.Form("WorkTime"))
	        hstFormValues.Add("content", Request.Form("content"))
	  
	'写入 新建的流程表WORKFLOW_FORM_ProjectChange
	        SDbStatement.InsertRow(hstFormValues, "HR_WorkManege")
	        
	    Dim EmpCode as string=CurrentUser.Code
	    Dim EmpName as string=CurrentUser.Name           
		Dim rowCount As int32 = Convert.ToInt32(Me.txtRowCount.Value)
	        For i As Integer = 1 To rowCount
	            If (Request.Form("ProjectName" & i) <> "") Then
	                Dim hstFormfybx As New Hashtable()
	                hstFormfybx.Add("ID", TimeId.CurrentMilliseconds(30))
	                hstFormfybx.Add("RELID", 0)
	                hstFormfybx.Add("CRTID", CurrentUser.Name)
	                hstFormfybx.Add("CRTTIME", DateTime.Now)
	                hstFormfybx.Add("EDTID", CurrentUser.Name)
	                hstFormfybx.Add("EDTTIME", DateTime.Now)
	                hstFormfybx.Add("RESID", 425054103832)
	                hstFormfybx.Add("ItemNum", Request.Form("ItemNum")) '编号
	                hstFormfybx.Add("ProjectCode", Request.Form("ProjectCode" & i))
	                hstFormfybx.Add("ProjectName", Request.Form("ProjectName" & i))
	                hstFormfybx.Add("SubmitDate", Request.Form("SubmitDate" & i))
	                hstFormfybx.Add("WorkTime", Request.Form("WorkTime" & i))
	                hstFormfybx.Add("content", Request.Form("content" & i))
	                SDbStatement.InsertRow(hstFormfybx, "HR_WorkManege")
	            End If
	        Next
		Dim oWorklistItem As WorklistItem = CreateWorkflowInstance(Convert.ToInt64(WorkflowId),Convert.ToInt64(ViewState("_Id")),ActionKey,hstFormValues,"")
		hstFormValues.Add("WorkflowInstId",oWorklistItem.ActivityInstance.WorkflowInstance.Key)
		StartWorkflowInstance(oWorklistItem)
	End Sub
		private Function GetCode() as string
	        Dim dt As DataTable = SDbStatement.Query("select IsNull(Max(ItemNum),0) Code from HR_WorkManege where ItemNum like '" + Date.Now.ToString("yyyyMMdd") + "%'").Tables(0)
		if dt.Rows.Count>0 then
			if DbField.GetLng(dt.Rows(0),"Code")=0 then
				return Date.Now.ToString("yyyyMMdd")+"0001"
			else
				return Date.Now.ToString("yyyyMMdd")+ Convert.ToInt32(Convert.ToInt32(DbField.GetStr(dt.Rows(0),"Code").SubString(DbField.GetStr(dt.Rows(0),"Code").length-4))+1).ToString("0000")
			end if
		else
			return Date.Now.ToString("yyyyMMdd")+"0001"
		end if
	    End Function
	    
	    
	</script>
	 
<body >
<form id="Form1" method="post" runat="server">
<!--显示按钮-->
<%GenerateCommand(Convert.ToInt64(WorkflowId), "return CheckValue(Form1) && Validate() && ValidateNum();")%>
<h1 style="text-align: center" class="h1" >工时提交</h1>



<table width="750" border="0" style="text-align: center" cellpadding="0" cellspacing="0" class="Bold_box">
  <tr>
    <td style="width:80px; height: 31px;"; class="F_center">日期</td>
    <td style="width:200px; height: 31px;";  class="F_center">项目名称</td>
    <td style="width:380px; height: 31px;";  class="F_center">工作内容</td>
    <td style="width:40px; height: 31px;";  class="F_center">工时</td>
    <td style="width:50px; height: 31px;";  class="F_center">操作</td>
  </tr>
    <%For i As Integer = 1 To 3%> 
  <tr>
    <td style="height: 25px"><input type="text" class="noborder" name="Date<%=i%>" id="Date<%Response.Write(i)%>" style="width:80px"   value="<%=datetime.now.ToString("yyyy-MM-dd")%>" onfocus="Cal_dropdown(this);" /></td>
    <td style="height: 25px">
      <select id="Projectname<%Response.Write(i)%>" name="Projectname<%=i%>"  style="width:200px;" onchange="fnOnChangeName(<%=i%>)" ></select><input type="hidden" name="ProjectName<%=i%>" id="%ProjectName<Response.Write(i)%>" style="width:1px" />
   </td>
    <td style="height: 25px"><input type="text" class="noborder" name="content<%=i%>" id="content<%Response.Write(i)%>" style="width:380px"  /></td>
    <td style="height: 25px"><input type="text" class="noborder" name="WorkTime<%=i%>" id="WorkTime<%Response.Write(i)%>" style="width:40px"  /></td>
    <td style="height: 25px"><img class="noborder" src="../images/del.gif" id="img<%=i%>" name="img<%=i%>" alt="删除" onclick="delRow(this);" /></td>
  </tr>
  <%Next%>
  <tr>
    <td colspan="3" class="F_center">合计</td>
    <td></td>
    <td><img id="img"  alt="添加" name="img" src="../images/add.png" onclick="addRow('tbDetails');" /></td>
  </tr>
</table>
<input id="ItemNum" name="ItemNum" type="hidden" value="<%Response.Write(GetCode())%>" />
<input type="hidden" id="txtRowCount" value="3" runat="server" style="width:50px" name="txtRowCount" />
</form>
	</body>
</html>