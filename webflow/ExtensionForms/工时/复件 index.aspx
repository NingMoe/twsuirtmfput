<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="System.Data.SqlClient"%>
<%@ import namespace="NetReusables"%>
<%@ Import namespace="Unionsoft.Workflow.Items"%>
<%@ Import namespace="Unionsoft.Workflow.Platform"%>
<%@ Import namespace="Unionsoft.Workflow.Engine"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.UserPageBase" validateRequest="false"  %>
<HTML>
	<HEAD>
		<title>ҵ��Ԫ���¼��</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<!--������js�ļ�--------->
		<SCRIPT type="text/javascript" LANGUAGE="JavaScript" src="../scripts/dragDiv.js"></SCRIPT>
		<!--��֤ҳ���ı��������� js�ļ�----------->
		<SCRIPT type="text/javascript" LANGUAGE="JavaScript" src="../scripts/FormValidate.js"></SCRIPT>
		<!--���ڿؼ�-->
		<script type="text/javascript" language="javascript" src="../scripts/DateCalendar.js"></script>
		<!--����дjs�ļ�------------->
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
		      col.innerHTML="<input name='ProjectName"+rows+"' id='ProjectName"+rows+"' type='text' class='noborder' style='width:180px' readonly/><input id='ProjectCode"+rows+"' name='ProjectCode"+rows+"' type='hidden' class='noborder'  style='width:1px' /><img style='cursor: hand; border: 0;' src='../../images/query1.gif' onclick=showMessageBox(800,300,'Choose.aspx?NO="+rows+"','��Ŀ�б�'); alt='��Ŀ�б�' />";
		       col=row.insertCell(1);
		      col.innerHTML="<input name='Remark"+rows+"' id='Remark"+rows+"' type='text' class='noborder' style='width:130px' />";
		      col=row.insertCell(2);
		      col.innerHTML="<input name='Y_sum"+rows+"' id='Y_sum"+rows+"' type='text' class='noborder' style='width:90px' FieldType='num' FieldTitle='Ӧ�����ܶ�' onkeyup='getTotalSum();Chenfa("+rows+")'/>";
              col=row.insertCell(3);
		      col.innerHTML="<input name='J_sum"+rows+"' id='J_sum"+rows+"' type='text' class='noborder' style='width:90px' FieldType='num' FieldTitle='����' onkeyup='getTotalAward();Chenfa("+rows+")'/>";
			  col=row.insertCell(4);
		      col.innerHTML="<input id='K_sum"+rows+"' name='K_sum"+rows+"' type='text' class='noborder'  style='width:90px' FieldType='num' FieldTitle='����' onkeyup='getTotalCheck();Chenfa("+rows+")'/>";	
			  col=row.insertCell(5);
		      col.innerHTML="<input id='A_Sum"+rows+"' name='A_Sum"+rows+"' type='text' class='noborder' style='width:90px' readonly />";
		      col=row.insertCell(6);
		      col.innerHTML="<img src='../images/del.gif' name='img"+rows+"' alt='ɾ��' onclick='delRow(this);'/>";      
		    }
		  }
		    function delRow(o)
		    {		 
		      var objTR =o.parentNode.parentNode; 
			  var currRowIndex= objTR.rowIndex; 
			  if(confirm("��ȷ��Ҫɾ��?")) 
			  { 
			   objTR.parentNode.deleteRow(currRowIndex); 
			   getTotalSum();
			   getTotalAward();
			   getTotalCheck();
			   getAttriSum();
			   checkSum--;
			  }		    
		    }

			</script>	  
	</head>
	<!--#include file="../includes/WorkflowUtilies.aspx"-->
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
		
        '����
		Dim hstFormValues As New Hashtable()
		'ȡ������
	        hstFormValues.Add("Id", ViewState("_Id"))
	        hstFormValues.Add("RELID", 0)
	        hstFormValues.Add("CRTID", CurrentUser.Name)
	        hstFormValues.Add("CRTTIME", DateTime.Now)
	        hstFormValues.Add("EDTID", CurrentUser.Name)
	        hstFormValues.Add("EDTTIME", DateTime.Now)
	        hstFormValues.Add("RESID", 425054103832)
	        hstFormValues.Add("ItemNum", Request.Form("ItemNum")) '���
	        hstFormValues.Add("EMPName", CurrentUser.Name)
	        hstFormValues.Add("EMPCode", CurrentUser.Code)
	        hstFormValues.Add("SubmitDate", Request.Form("SubmitDate"))
	        hstFormValues.Add("ProjectName", Request.Form("ProjectName"))
	        hstFormValues.Add("ProjectCode", Request.Form("ProjectCode"))
	        hstFormValues.Add("WorkTime", Request.Form("WorkTime"))
	        hstFormValues.Add("content", Request.Form("content"))
	  
	'д�� �½������̱�WORKFLOW_FORM_ProjectChange
	        SDbStatement.InsertRow(hstFormValues, "HR_WorkManege")
	                
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
	                hstFormfybx.Add("ItemNum", Request.Form("ItemNum")) '���
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
		<!--��ʾ��ť-->
<%GenerateCommand(Convert.ToInt64(WorkflowId), "return CheckValue(Form1) && Validate() && ValidateNum();")%>
<h1 style="text-align: center" class="h1" >��ʱ�ύ</h1>
<table width="750"  border="0" style="text-align: center" cellpadding="0" cellspacing="0" class="noborder">
  <tr>
    <td  style="font-size: 12px; width:80px; line-height: 15px; color: #666666; text-decoration: none; height: 24px;">Ա��������</td>
    <td  style="width:120px; height: 24px;"><input class="noborder" style="width:120px" type="text" name="EmpName" value="<%=CurrentUser.Name%>" readonly /></td>
    <td  style="font-size: 12px; width:80px; line-height: 15px; color: #666666; text-decoration: none; height: 24px;">Ա���˺ţ�</td>
    <td  style="width:120px; height: 24px;"><input class="noborder" style="width:80px" name="EmpCode" type="text" id="EmpCode" value="<%=CurrentUser.Code%>" readonly /></td>
    <td  style="font-size: 12px; width:80px; line-height: 15px; color: #666666; text-decoration: none; height: 24px;">����:</td>
    <td  style="width:140px; height: 24px;"><input class="noborder" style="width:140px" name="date" type="text" id="date" value='<%=DateTime.Now.ToString("yyyy-MM-dd")%>' readonly /></td>
  </tr>
</table>


<table width="750" border="0" style="text-align: center" cellpadding="0" cellspacing="0" class="Bold_box">
  <tr>
    <td style="width:70px"; class="F_center">����</td>
    <td style="width:200px";  class="F_center">��Ŀ����</td>
    <td style="width:380px";  class="F_center">��������</td>
    <td style="width:50px";  class="F_center">��ʱ</td>
    <td style="width:50px";  class="F_center">����</td>
  </tr>
    <%For i As Integer = 1 To 3%>
  <tr>
    <td style="height: 25px"><input type="text" class="noborder" name="Date<%=i+1%>" id="Date<%=i+1%>" style="width:70px"   value="<%=datetime.now.ToString("yyyy-MM-dd")%>" onfocus="Cal_dropdown(this);" readonly /></td>
    <td style="height: 25px"><span style="font-size: 12px; line-height: 16px; color: #666666; text-decoration: none;">
      <select id="projectname" name="projectname" class="noborder"  style="width:190px;"  onchange="fnOnChange()"  >
      </select>
    </span></td>
    <td style="height: 25px"><input type="text" class="noborder" name="content<%=i+1%>" id="content<%=i+1%>" style="width:380px"  /></td>
    <td style="height: 25px"><input type="text" class="noborder" name="WorkTime<%=i+1%>" id="WorkTime<%=i+1%>" style="width:50px"  /></td>
    <td style="height: 25px"><img class="noborder" src="../images/del.gif" id="img<%=i%>"  alt="ɾ��" onclick="delRow(this);" /></td>
  </tr>
  <%Next%>
  <tr>
    <td colspan="3" class="F_center">�ϼ�</td>
    <td></td>
    <td><img id="img"  alt="���" src="../images/add.png" onclick="addRow('tbDetails');" /></td>
  </tr>
</table>
<input id="ItemNum" name="ItemNum" type="hidden" value="<%=GetCode()%>" />
<input type="hidden" id="txtRowCount" value="3" runat="server" style="width:50px" name="txtRowCount" />
</form>
	</body>
</HTML>