<%@ Import Namespace="System.Data"%>
<%@ Import Namespace="System.Data.SqlClient"%>
<%@ import namespace="NetReusables"%>
<%@ Import namespace="Unionsoft.Workflow.Items"%>
<%@ Import namespace="Unionsoft.Workflow.Platform"%>
<%@ Import namespace="Unionsoft.Workflow.Engine"%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.UserPageBase" validateRequest="false"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<Html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
	<title>外出申请表</title>
	<script type="text/javascript" src="scripts/DataConvert.js"></script>
	<script type="text/javascript" src="scripts/FormValidate.js"></script>
	<script type="text/javascript" src="scripts/DateCalendar.js"></script>
	
	
	
	<!--弹出层js文件--------->

    <script type="text/javascript" language="JavaScript" src="../scripts/dragDiv.js"></script>

    <!--验证页面文本框必填及类型 js文件----------->

    <script type="text/javascript" language="JavaScript" src="../scripts/FormValidate.js"></script>

    <!--日期控件-->

    <script type="text/javascript" language="javascript" src="../scripts/DateCalendar.js"></script>

    <!--金额大写js文件------------->

    <script type="text/javascript" language="javascript" src="../scripts/DataConvert.js"></script>
    <link href="css/YYTys.css" rel="stylesheet" type="text/css" />
</head>
<body>
<!--#include file="../includes/WorkflowUtilies.aspx"-->
 
<script language="vb" runat="server">
Private ActionKey As String
Private WorkflowId As String 
 Dim hstFValues As New Hashtable()
Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load	
        
	If ViewState.Item("_Id") Is Nothing  Then ViewState("_Id")=TimeId.CurrentMilliseconds(30)
	If Request.Form("action_value") <> "" Then ActionKey = Request.Form("action_value")
	If Request("WorkflowId") <>"" Then WorkflowId = Request("WorkflowId")
    
	If Not Me.IsPostBack Then Return
	If ActionKey = "" Then Return	
    Dim hst As New Hashtable 
	hst.Add("ID",ViewState("_Id")) 
        hst.Add("RESID", 435756716220)
	hst.Add("CRTTIME",DateTime.Now)
	hst.Add("VisitNo",Request.Form("VisitNo"))
	
        hst.Add("Years", Request.Form("Years"))
	hst.Add("StartDate",Request.Form("StartDate"))
	hst.Add("EmpName",Request.Form("EmpName"))
	hst.Add("EmpCode",CurrentUser.Code)
	
	hst.Add("Visittime",Request.Form("Visittime"))
	hst.Add("StartHour",Request.Form("StartHour"))
	hst.Add("StartMinute",Request.Form("StartMinute"))
	hst.Add("EndDate",Request.Form("EndDate"))
	hst.Add("EndHour",Request.Form("EndHour"))
	hst.Add("EndMinute",Request.Form("EndMinute"))
	hst.Add("Explain",Request.Form("Explain"))
        hst.Add("State", "审核中") '审核状态
	 hst.Add("Date", DateTime.Now.ToString("yyyy-MM-dd"))
	
	hst.Add("ProjectName", Request.Form("ProjectName")) '项目名称
    hst.Add("ProjectCode", Request.Form("ProjectCode")) '项目编号
    

        SDbStatement.InsertRow(hst, "HR_Visitation")
	

    Dim oWorklistItem As WorklistItem = CreateWorkflowInstance(Convert.ToInt64(WorkflowId),Convert.ToInt64(ViewState("_Id")),ActionKey,hst,"")
		hst.Add("WorkflowInstId",oWorklistItem.ActivityInstance.WorkflowInstance.Key)
		StartWorkflowInstance(oWorklistItem)
		    
End Sub 
</script>



<script LANGUAGE=javascript type="text/javascript"> 
function sumNum()
{
    var rad=document.getElementsByName("Overtimetype");
    var radValue="";
    for(var i=0;i<rad.length;i++)
    {
        if(rad[i].checked)
        {
             radValue=rad[i].value;
        }
    }
    
    //开始日期 时 分
        var StartDate=Form1.StartDate.value;	
        var StartHour=Form1.StartHour.value;
        var StartMinute=Form1.StartMinute.value;
        //结束日期 时 分
        var EndDate=Form1.EndDate.value;
        var EndHour=Form1.EndHour.value;
        var EndMinute=Form1.EndMinute.value;
        //计算开始
        if(Form1.Visittime.value=="NaN")
        {
	        Form1.Visittime.value=0;
        }
        var a=EndHour-StartHour;
        if(StartMinute==30)
        {
	        a=a-0.5
        }
        if(EndMinute==30)
        {
	        a=a+0.5
        }
        if(DateDiff(EndDate,StartDate)>0)
        {
	        a=a+(DateDiff(EndDate,StartDate)*24)
        }
        Form1.Visittime.value=a
}
function DateDiff(d1,d2)
{
    var day = 24 * 60 * 60 *1000;
	try{    
		var dateArr = d1.split("-");
  		var checkDate = new Date();
        checkDate.setFullYear(dateArr[0], dateArr[1]-1, dateArr[2]);
   		var checkTime = checkDate.getTime();  
   		var dateArr2 = d2.split("-");
   		var checkDate2 = new Date();
        checkDate2.setFullYear(dateArr2[0], dateArr2[1]-1, dateArr2[2]);
   		var checkTime2 = checkDate2.getTime();    
   		var cha = (checkTime - checkTime2)/day;  
        return cha;
    }catch(e){
   		return false;
	}
}

function Validate()
{
    if(parseFloat(document.getElementById("Visittime").value)<=0)
    {
        alert("外出时间必须大于零");
        document.getElementById("Visittime").focus();
        return false;
    }
    return true;
    
}
</script>
<form id="Form1" method="post" runat="server">
<%GenerateCommand(Convert.ToInt64(WorkflowId), "return CheckValue(Form1) && Validate() && ValidateNum();")%>	
<h1 align="center">外出表</h1>
<table width="594" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr> 
	  <td width="594" align="right" style="font-size:12px;">年度：<input name="Years" type="text" class="noborder" id="Years" style="width:35px; height:17px" readonly value="<%=datetime.now.year%>"/></td>
  </tr>
</table>
<table width="594" border="0" align="center" cellspacing="0" class="Bold_box">
<tr >
                <td width="73" class="F_center">
                    项目名称</td>
                <td><input name="ProjectCode" type="hidden" id="ProjectCode"  class="noborder" style="width:120px" value="<%=hstFValues("ProCode")%>"/><input name="ProjectName" id="ProjectName" type="text" class="noborder" style="width:180px " mInput="true" FieldTitle="项目名称" value="<%=hstFValues("ProName") %>" readonly /><img style="cursor: hand; border: 0;"src="../../images/query1.gif" onclick="showMessageBox(800,400,'Choose.aspx','项目列表');"alt="项目列表" />
                </td>
                <td width="69" class="F_center">外出人</td>
    <td width="220" colspan="2" align="left" valign="middle">　　
      <input name="EmpName" type="text" class="noborder" id="Text1" style="width:90px" value="<%=CurrentUser.Name%>" readonly="readonly"/></td>
            </tr>
  <tr>
    <td height="33" class="F_center">外出日期</td>
    <td align="left" valign="middle">
      <input name="StartDate" type="text" class="box1" style="width:70px" onFocus="Cal_dropdown(this);" value="<%=datetime.now.ToString("yyyy-MM-dd")%>" onpropertychange="sumNum()"  mInput="true" FieldType="date" FieldTitle="加班日期" readonly/>日
      <select id="StartHour" name="StartHour" onChange="sumNum()">
        <option value="0">00</option>
        <option value="1">01</option>
        <option value="2">02</option>
        <option value="3">03</option>
        <option value="4">04</option>
        <option value="5">05</option>
        <option value="6">06</option>
        <option value="7">07</option>
        <option value="8">08</option>
        <option value="9">09</option>
        <option value="10">10</option>
        <option value="11">11</option>
        <option value="12">12</option>
        <option value="13">13</option>
        <option value="14">14</option>
        <option value="15">15</option>
        <option value="16">16</option>
        <option value="17">17</option>
        <option value="18">18</option>
        <option value="19">19</option>
        <option value="20">20</option>
        <option value="21">21</option>
        <option value="22">22</option>
        <option value="23">23</option>
        <option value="24">24</option>
      </select>
      时
      <select id="StartMinute" name="StartMinute" onChange="sumNum()">
        <option value="0">00</option>
        <option value="30">30</option>
      </select>
      分</td>
	  <td class="F_center">结束时间</td>
	  <td colspan="2"><input name="EndDate" type="text" class="box1" id="EndDate" onFocus="Cal_dropdown(this);" style="width:70px" value="<%=datetime.now.ToString("yyyy-MM-dd")%>" onpropertychange="sumNum()"  minput="true" fieldtype="date" fieldtitle="结束时间"  readonly/>
      日
      <select id="EndHour" name="EndHour" onChange="sumNum()">
        <option value="0">00</option>
        <option value="1">01</option>
        <option value="2">02</option>
        <option value="3">03</option>
        <option value="4">04</option>
        <option value="5">05</option>
        <option value="6">06</option>
        <option value="7">07</option>
        <option value="8">08</option>
        <option value="9">09</option>
        <option value="10">10</option>
        <option value="11">11</option>
        <option value="12">12</option>
        <option value="13">13</option>
        <option value="14">14</option>
        <option value="15">15</option>
        <option value="16">16</option>
        <option value="17">17</option>
        <option value="18">18</option>
        <option value="19">19</option>
        <option value="20">20</option>
        <option value="21">21</option>
        <option value="22">22</option>
        <option value="23">23</option>
        <option value="24">24</option>
      </select>
      时
      <select id="EndMinute" name="EndMinute" onChange="sumNum()">
        <option value="0">00</option>
        <option value="30">30</option>
      </select>
分</td>
    </tr>
  <tr>
    <td class="F_center">外出时间</td>
    <td align="left" valign="middle" colspan="4">　
      <input type="text" name="Visittime" id="Visittime"  style="width:50px" class="noborder"  mInput="true" FieldType="num" FieldTitle="外出时间" readonly/> 小时</td>


  </tr>
  <tr>
    <td class="F_center">外出事由</td>
    <td colspan="4" align="left" valign="top">　
       <textarea name="Explain" class="noborder" id="Explain" style="width:90%; height:90px" mInput="true" FieldTitle="外出事由"></textarea>   </td>
  </tr>
</table> 

</form>
</body>
</html>
