<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="NetReusables" %>
<%@ Import Namespace="Unionsoft.Workflow.Items" %>
<%@ Import Namespace="Unionsoft.Workflow.Platform" %>
<%@ Import Namespace="Unionsoft.Workflow.Engine" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Unionsoft.Workflow.Web.UserPageBase"
    ValidateRequest="false" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>请假表</title>
    <!--弹出层js文件--------->

    <script type="text/javascript" language="JavaScript" src="../scripts/dragDiv.js"></script>

    <!--验证页面文本框必填及类型 js文件----------->

    <script type="text/javascript" language="JavaScript" src="../scripts/FormValidate.js"></script>

    <!--日期控件-->

    <script type="text/javascript" language="javascript" src="../scripts/DateCalendar.js"></script>

    <!--金额大写js文件------------->

    <script type="text/javascript" language="javascript" src="../scripts/DataConvert.js"></script>

    <link href="../css/YYTys.css" rel="stylesheet" type="text/css" />
</head>
<body>
 <!--#include file="../includes/WorkflowUtilies.aspx"-->
      
    

    <script language="vb" runat="server">
        Private ActionKey As String
        Private WorkflowId As String
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ''获取剩余年假天数
            'Dim dt As DataTable = SDbStatement.Query("select 姓名,年假天数,isnull(已用年假天数,0) 已用年假天数,剩余年假=年假天数-isnull(已用年假天数,0) from(select EmpName as 姓名 ,isnull(Annual,0) 年假天数 from HR_Employee ) a left join (select EmpName,isnull(SUM(LeaveDays),0)已用年假天数 from HR_Leave WHERE Leavetype='年假' GROUP BY EmpName ) b on a.姓名=b.EmpName where 姓名='" + CurrentUser.Name + "'").Tables(0)
          
            
            'If dt.Rows.Count > 0 Then
            '    DayNum.Value = dt.Rows(0)("剩余年假").ToString
            'Else
            '    DayNum.Value = "0"
            'End If
            
            If ViewState.Item("_Id") Is Nothing Then ViewState("_Id") = TimeId.CurrentMilliseconds(30)
            If Request.Form("action_value") <> "" Then ActionKey = Request.Form("action_value")
            If Request("WorkflowId") <> "" Then WorkflowId = Request("WorkflowId")
            If Not Me.IsPostBack Then Return
            If ActionKey = "" Then Return
            Dim LeaveNo As String = TimeId.CurrentMilliseconds(30).ToString
            Dim hst As New Hashtable
            hst.Add("ID", ViewState("_Id"))
            hst.Add("RESID", 425064036957)
            hst.Add("RELID", 0)
            hst.Add("CRTID", CurrentUser.Code)
            hst.Add("CRTTIME", DateTime.Now)
            hst.Add("EDTID", CurrentUser.Code)
            hst.Add("EDTTIME", DateTime.Now)
            hst.Add("LeaveNo", LeaveNo) '请假单编号
            hst.Add("EmpName", Request.Form("EmpName")) '请假人姓名
            hst.Add("EmpCode", CurrentUser.Code)
            hst.Add("Deparment", GetDepartment(CurrentUser.Code)) '部门
            'hst.Add("Company", GetEmployeeAttr(CurrentUser.Code, "C3_426789855468")) '加班人所在公司
            hst.Add("Askdate", Request.Form("Askdate")) '请假日期
            hst.Add("Leavetype", Request.Form("Leavetype")) '假期类型
            hst.Add("Begindate", Request.Form("Begindate")) '开始日期
            hst.Add("StartHour", Request.Form("StartHour")) '开始时
            hst.Add("StartMinute", Request.Form("StartMinute")) '开始分
            hst.Add("EndDate", Request.Form("Enddate")) '结束日期
            hst.Add("EndHour", Request.Form("EndHour")) '结束时
            hst.Add("EndMinute", Request.Form("EndMinute")) '结束分
            hst.Add("Hours", Request.Form("Hours")) '请假小时数
            hst.Add("LeaveDays", Request.Form("LeaveDays")) '请假天数
            hst.Add("Leavedesc", Request.Form("Leavedesc")) '事由
            hst.Add("Department", Request.Form("Department"))
            hst.Add("Job", Request.Form("Job"))
            hst.Add("State", "审核中") '审核状态
            hst.Add("Years", Request.Form("Years"))
            SDbStatement.InsertRow(hst, "HR_Leave")
            Dim oWorklistItem As WorklistItem = CreateWorkflowInstance(Convert.ToInt64(WorkflowId), Convert.ToInt64(ViewState("_Id")), ActionKey, hst, "")
            hst.Add("WorkflowInstId", oWorklistItem.ActivityInstance.WorkflowInstance.Key)
            StartWorkflowInstance(oWorklistItem)
		    
        End Sub
    </script>

    <script language="javascript" type="text/javascript"> 
    function sumNum()
    {
    //开始日期 时 分
	var StartDate=Form1.Begindate.value;	
	var StartHour=Form1.StartHour.value;
	var StartMinute=Form1.StartMinute.value;
	//结束日期 时 分
	var EndDate=Form1.Enddate.value;
	var EndHour=Form1.EndHour.value;
	var EndMinute=Form1.EndMinute.value;
	//计算开始
	if(Form1.Hours.value=="NaN")
	{
		Form1.Hours.value=0;
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
	if(StartHour<12 && EndHour>12)//中午午休12：00-13：00  不算在请假内
	{
		a=a-1
	}
	if(a>=7) a=8;
	if(DateDiff(EndDate,StartDate)>0)
	{   
		a=a+(DateDiff(EndDate,StartDate)*8)
	}
	Form1.Hours.value=a
    Form1.LeaveDays.value=ForDight(a/8,1);
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
        return ForDight(cha,1);
    }catch(e){
   		return false;
	}
}
//四舍五入
function  ForDight(Dight,How)
{
	Dight  =  Math.round(Dight*Math.pow(10,How))/Math.pow(10,How);
	return  Dight;
}

//验证
function Validate()
{
    if(!CheckValue(Form1)) return false;
    var IsChecked=false;
    var rad=document.getElementsByName("leavetype");
    for(var i=0;i<rad.length;i++)
    {
        if(rad[i].checked) IsChecked=true;
    }
    
    if(!IsChecked)
    {
        alert("假期类型是必须的");
        return false;
    } 
    if(parseFloat(document.getElementById("Hours").value)<=0)
    {
        alert("请假小时数必须大于零");
        document.getElementById("Hours").focus();
        return false;
    }
    return true;
    
}
//验证
function ValidateLeaveDay()
{
    if(!Validate()) return false;
    var rad=document.getElementsByName("leavetype");
    for(var i=0;i<rad.length;i++)
    {
        if(rad[i].checked)
        {
            if(rad[i].value=="婚假" || rad[i].value=="产假" || rad[i].value=="产假陪护")
            {
               if(!window.confirm('婚假、产假、产假陪护属于一次性假期，您确定本次休假天数？'))
                 {
                    return false;
                 }           
            }
            else if(rad[i].value=="年假")
            {
               var re = /^[1-9]+[0-9]*]*$/;
               if(ForDight(parseFloat(document.getElementById("LeaveDays").value),1)>ForDight(parseFloat(document.getElementById("DayNum").value),1))
               {
                    alert('请假天数不能大于剩余天数');
                    return false;
               } 
            }
        }
    }
    return true;
}

function ValidateWorkDay(StartDate,EndDate)
{   
    var DayNum=0; 
    var c=DateDiff(EndDate,StartDate);
    
     var oDate=StartDate.split("-");
    
    var Years="";
    var Months="";
    var Days="";
    
    Days=ForDight(oDate[2],1);
    Months=ForDight(oDate[1],1);
    Years=ForDight(oDate[0],1);
    
    var strDate="";
    var strWorkDay=document.getElementById("WorkDay").value.split(",");
    var strNotWorkDay=document.getElementById("NotWorkDay").value.split(",");
    var CurrDay=new Date(Years, Months, 0).getDate();
  // debugger;
    for(var i=0;i<=c;i++)
    {
        var NewDays=parseInt(Days)+i;
       // var NewDate=parseDate(GetNewDate(Years+"-"+Months+"-"+NewDays));
        var NewDate=GetNewDate(Years+"-"+Months+"-"+NewDays);
        var weeks=parseDate(NewDate).getDay();
        var IsBe=false;
        if(parseInt(weeks)==0 || parseInt(weeks)==6) //判断是否为工作日
        { //debugger;
            IsBe=false
            for(var j=0;j<strWorkDay.length;j++)
            {
                Date.prototype.DateDiff
                if(parseDate(NewDate)>parseDate(strWorkDay[j]) || parseDate(NewDate)<parseDate(strWorkDay[j]))
                {
                    //IsBe=false;
                }
                else IsBe=true;
            }
            if(!IsBe) DayNum=parseInt(DayNum)+1;
        }
        else
        {
            
            var IsBe=false;
            for(var j=0;j<strNotWorkDay.length;j++)
            {
                if(parseDate(NewDate)>parseDate(strNotWorkDay[j]) || parseDate(NewDate)<parseDate(strNotWorkDay[j]))
                {
                    //IsBe=false;
                }
                else IsBe=true;
            }
            if(IsBe) DayNum=parseInt(DayNum)+1;
        }
    }
   // alert(DayNum);
    return DayNum;
}

function GetNewDate(strDate)
{
    var oDate=strDate.split("-");
    Days=ForDight(oDate[2],1);
    Months=ForDight(oDate[1],1);
    Years=ForDight(oDate[0],1);
    var currDays= new Date(Years, Months, 0).getDate();
    if(parseInt(currDays)<parseInt(Days))
    {
        Days=parseInt(Days)-parseInt(currDays);
        Months=parseInt(Months)+1;
        if(Months>12)
        {
            Months=1;
            Years=parseInt(Years)+1;
        }
        currDays=new Date(Years, Months, 0).getDate();
        if(parseInt(currDays)<parseInt(Days)) GetNewDate(Years+"-"+Months+"-"+Days);
    }
    return Years+"-"+Months+"-"+Days;
}
function parseDate(str){      
  if(typeof str == 'string'){      
    var results = str.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) *$/);      
    if(results && results.length>3)      
      return new Date(parseInt(results[1]),parseInt(results[2]) -1,parseInt(results[3]));       
    results = str.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) +(\d{1,2}):(\d{1,2}):(\d{1,2}) *$/);      
    if(results && results.length>6)      
      return new Date(parseInt(results[1]),parseInt(results[2]) -1,parseInt(results[3]),parseInt(results[4]),parseInt(results[5]),parseInt(results[6]));       
    results = str.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) +(\d{1,2}):(\d{1,2}):(\d{1,2})\.(\d{1,9}) *$/);      
    if(results && results.length>7)      
      return new Date(parseInt(results[1]),parseInt(results[2]) -1,parseInt(results[3]),parseInt(results[4]),parseInt(results[5]),parseInt(results[6]),parseInt(results[7]));       
  }      
  return null;      
} 



</script>

    <form id="Form1" method="post" runat="server">
        <%GenerateCommand(Convert.ToInt64(WorkflowId), "return ValidateLeaveDay();")%>
        <h1 align="center">
            请假单</h1>
        <table width="700" border="0" align="center" cellpadding="0" cellspacing="0" class="Bold_box">
            <tr>
                <td width="100" class="F_center">
                    请假日期</td>
                <td width="250" align="left" valign="middle">
                    <input name="Askdate" type="text" class="noborder" id="Askdate" style="width: 70px"
                        value="<%=datetime.now.ToString("yyyy-MM-dd")%>" /></td>
                <td width="100" class="F_center">
                    请假人</td>
                <td width="250" align="left" valign="middle">
                    <input name="EmpName" type="text" class="noborder" id="EmpName" style="width: 100px"
                        value="<%=CurrentUser.Name%>" readonly /><input name="Department" type="hidden" class="noborder" id="Department" style="width:100px" value="<%=GetDepartment(CurrentUser.Code)%>" readonly/><input name="Job" type="hidden" class="noborder" id="Job" style="width:100px" value="<%=CurrentUser.HeadShip%>" readonly/><input name="Years"  class="noborder" id="Years" type="hidden" style="width:40px; height:17px" readonly value="<%=datetime.now.year%>"/></td>
            </tr>
            <tr>
                <td class="F_center">
                    开始日期</td>
                <td align="left" valign="middle">
                    <input name="Begindate" type="text" value="<%=datetime.now.ToString("yyyy-MM-dd")%>"
                        onfocus="Cal_dropdown(this);" readonly class="noborder" id="Begindate" style="width: 98%"
                        onpropertychange="sumNum();" /></td>
                <td class="F_center">
                    开始时间</td>
                <td align="left" valign="middle">
                    <select id="StartHour" name="StartHour" onchange="sumNum()">
                        <option value="8">8</option>
                        <option value="9">9</option>
                        <option value="10">10</option>
                        <option value="11">11</option>
                        <option value="12">12</option>
                        <option value="13">13</option>
                        <option value="14">14</option>
                        <option value="15">15</option>
                        <option value="16">16</option>
                        <option value="17">17</option>
                        <option value="17">18</option>
                    </select>
                    时
                    <select id="StartMinute" name="StartMinute" onchange="sumNum()">
                        <option value="00">00</option>
                        <option value="30">30</option>
                    </select>
                    分</td>
            </tr>
            <tr>
                <td class="F_center">
                    结束日期</td>
                <td align="left" valign="middle">
                    <input name="Enddate" onfocus="Cal_dropdown(this);" value="<%=datetime.now.ToString("yyyy-MM-dd")%>"
                        readonly type="text" class="noborder" id="Enddate" style="width: 98%" onpropertychange="sumNum()" /></td>
                <td class="F_center">
                    结束时间</td>
                <td align="left" valign="middle">
                    <select id="EndHour" name="EndHour" onchange="sumNum()">
                        <option value="8">8</option>
                        <option value="9">9</option>
                        <option value="10">10</option>
                        <option value="11">11</option>
                        <option value="12">12</option>
                        <option value="13">13</option>
                        <option value="14">14</option>
                        <option value="15">15</option>
                        <option value="16">16</option>
                        <option value="17">17</option>
                         <option value="17">18</option>
                    </select>
                    时
                    <select id="EndMinute" name="EndMinute" onchange="sumNum()">
                        <option value="00">00</option>
                        <option value="30">30</option>
                    </select>
                    分</td>
            </tr>
            <tr>
                <td class="F_center">
                    请假小时数</td>
                <td align="left" valign="middle">
                    <input name="Hours" type="text" class="noborder" id="Hours" style="width: 100px"
                          mInput="true" fieldtype="num" FieldTitle="请假小时数" />
                </td>
                <td class="F_center">
                    请假天数</td>
                <td align="left" valign="middle">
                    <input name="LeaveDays" type="text" class="noborder" id="LeaveDays" style="width: 100px"
                         /></td>
            </tr>
            <tr>
                <td height="32" class="F_center">
                    假期类型</td>
                <td colspan="4" align="left" valign="middle">
                    &nbsp;&nbsp;&nbsp;&nbsp;事假<input name="leavetype" type="radio" id="" value="事假" onclick='document.getElementById("trDayNum").style.display="none";sumNum();' />

                    &nbsp;&nbsp;&nbsp;&nbsp;调休<input name="leavetype" type="radio" id="" value="调休" onclick='document.getElementById("trDayNum").style.display="none";sumNum();' />
                    &nbsp;&nbsp;&nbsp;&nbsp;病假<input name="leavetype" type="radio" id="" value="病假" onclick='document.getElementById("trDayNum").style.display="none";sumNum();' />
                    &nbsp;&nbsp;&nbsp;&nbsp;婚假<input name="leavetype" type="radio" id="" value="婚假" onclick='document.getElementById("trDayNum").style.display="none";sumNum();' />
                    &nbsp;&nbsp;&nbsp;&nbsp;产假<input name="leavetype" type="radio" id="" value="产假" onclick='document.getElementById("trDayNum").style.display="none";sumNum();' />
                    &nbsp;&nbsp;&nbsp;&nbsp;产假陪护<input name="leavetype" type="radio" id="" value="产假陪护"onclick='document.getElementById("trDayNum").style.display="none";sumNum();' />
                    &nbsp;&nbsp;&nbsp;&nbsp;丧假<input name="leavetype" type="radio" id="" value="丧假" onclick='document.getElementById("trDayNum").style.display="none";sumNum();' />
                    &nbsp;&nbsp;&nbsp;&nbsp;年假<input name="leavetype" type="radio" id="" value="年假" onclick='document.getElementById("trDayNum").style.display="none";sumNum();' />
                    &nbsp;&nbsp;&nbsp;&nbsp;在家办公<input name="leavetype" type="radio" id="" value="在家办公" onclick='document.getElementById("trDayNum").style.display="none";sumNum();' />
                </td>
            </tr>
           <tr style="display: none;" id="trDayNum">
                <td class="F_center">
                    可用天数</td>
                <td colspan="4" align="left">
                    <input type="text" id="DayNum" name="DayNum" class="noborder" runat ="server" readonly />
                </td>
            </tr>
            <tr>
                <td class="F_center">
                    事由</td>
                <td height="98" colspan="4" align="left" valign="top">
                    <textarea name="Leavedesc" class="noborder" id="Leavedesc" style="width: 99%; height: 90px"
                        mInput="true" FieldTitle="事由"></textarea>
                </td>
            </tr>
            <tr><td style="font-size: 12px; color: Red;" colspan="5">
                    注：请假一天按8小时计算;年假天数请在人事信息中录入。
                </td>
            </tr>
 <%--             <tr style="height: 30px;">
    <td colspan="5"><%DisplayAttachment(Convert.ToInt64(ViewState.Item("_Id")))%> </td>
  </tr>--%>
        </table>
    </form>
</body>
</html>

