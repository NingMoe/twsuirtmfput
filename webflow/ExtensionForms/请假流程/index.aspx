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
    <title>��ٱ�</title>
    <!--������js�ļ�--------->

    <script type="text/javascript" language="JavaScript" src="../scripts/dragDiv.js"></script>

    <!--��֤ҳ���ı��������� js�ļ�----------->

    <script type="text/javascript" language="JavaScript" src="../scripts/FormValidate.js"></script>

    <!--���ڿؼ�-->

    <script type="text/javascript" language="javascript" src="../scripts/DateCalendar.js"></script>

    <!--����дjs�ļ�------------->

    <script type="text/javascript" language="javascript" src="../scripts/DataConvert.js"></script>

    <link href="../css/YYTys.css" rel="stylesheet" type="text/css" />
</head>
<body>
 <!--#include file="../includes/WorkflowUtilies.aspx"-->
      
    

    <script language="vb" runat="server">
        Private ActionKey As String
        Private WorkflowId As String
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ''��ȡʣ���������
            'Dim dt As DataTable = SDbStatement.Query("select ����,�������,isnull(�����������,0) �����������,ʣ�����=�������-isnull(�����������,0) from(select EmpName as ���� ,isnull(Annual,0) ������� from HR_Employee ) a left join (select EmpName,isnull(SUM(LeaveDays),0)����������� from HR_Leave WHERE Leavetype='���' GROUP BY EmpName ) b on a.����=b.EmpName where ����='" + CurrentUser.Name + "'").Tables(0)
          
            
            'If dt.Rows.Count > 0 Then
            '    DayNum.Value = dt.Rows(0)("ʣ�����").ToString
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
            hst.Add("LeaveNo", LeaveNo) '��ٵ����
            hst.Add("EmpName", Request.Form("EmpName")) '���������
            hst.Add("EmpCode", CurrentUser.Code)
            hst.Add("Deparment", GetDepartment(CurrentUser.Code)) '����
            'hst.Add("Company", GetEmployeeAttr(CurrentUser.Code, "C3_426789855468")) '�Ӱ������ڹ�˾
            hst.Add("Askdate", Request.Form("Askdate")) '�������
            hst.Add("Leavetype", Request.Form("Leavetype")) '��������
            hst.Add("Begindate", Request.Form("Begindate")) '��ʼ����
            hst.Add("StartHour", Request.Form("StartHour")) '��ʼʱ
            hst.Add("StartMinute", Request.Form("StartMinute")) '��ʼ��
            hst.Add("EndDate", Request.Form("Enddate")) '��������
            hst.Add("EndHour", Request.Form("EndHour")) '����ʱ
            hst.Add("EndMinute", Request.Form("EndMinute")) '������
            hst.Add("Hours", Request.Form("Hours")) '���Сʱ��
            hst.Add("LeaveDays", Request.Form("LeaveDays")) '�������
            hst.Add("Leavedesc", Request.Form("Leavedesc")) '����
            hst.Add("Department", Request.Form("Department"))
            hst.Add("Job", Request.Form("Job"))
            hst.Add("State", "�����") '���״̬
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
    //��ʼ���� ʱ ��
	var StartDate=Form1.Begindate.value;	
	var StartHour=Form1.StartHour.value;
	var StartMinute=Form1.StartMinute.value;
	//�������� ʱ ��
	var EndDate=Form1.Enddate.value;
	var EndHour=Form1.EndHour.value;
	var EndMinute=Form1.EndMinute.value;
	//���㿪ʼ
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
	if(StartHour<12 && EndHour>12)//��������12��00-13��00  �����������
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
//��������
function  ForDight(Dight,How)
{
	Dight  =  Math.round(Dight*Math.pow(10,How))/Math.pow(10,How);
	return  Dight;
}

//��֤
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
        alert("���������Ǳ����");
        return false;
    } 
    if(parseFloat(document.getElementById("Hours").value)<=0)
    {
        alert("���Сʱ�����������");
        document.getElementById("Hours").focus();
        return false;
    }
    return true;
    
}
//��֤
function ValidateLeaveDay()
{
    if(!Validate()) return false;
    var rad=document.getElementsByName("leavetype");
    for(var i=0;i<rad.length;i++)
    {
        if(rad[i].checked)
        {
            if(rad[i].value=="���" || rad[i].value=="����" || rad[i].value=="�����㻤")
            {
               if(!window.confirm('��١����١������㻤����һ���Լ��ڣ���ȷ�������ݼ�������'))
                 {
                    return false;
                 }           
            }
            else if(rad[i].value=="���")
            {
               var re = /^[1-9]+[0-9]*]*$/;
               if(ForDight(parseFloat(document.getElementById("LeaveDays").value),1)>ForDight(parseFloat(document.getElementById("DayNum").value),1))
               {
                    alert('����������ܴ���ʣ������');
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
        if(parseInt(weeks)==0 || parseInt(weeks)==6) //�ж��Ƿ�Ϊ������
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
            ��ٵ�</h1>
        <table width="700" border="0" align="center" cellpadding="0" cellspacing="0" class="Bold_box">
            <tr>
                <td width="100" class="F_center">
                    �������</td>
                <td width="250" align="left" valign="middle">
                    <input name="Askdate" type="text" class="noborder" id="Askdate" style="width: 70px"
                        value="<%=datetime.now.ToString("yyyy-MM-dd")%>" /></td>
                <td width="100" class="F_center">
                    �����</td>
                <td width="250" align="left" valign="middle">
                    <input name="EmpName" type="text" class="noborder" id="EmpName" style="width: 100px"
                        value="<%=CurrentUser.Name%>" readonly /><input name="Department" type="hidden" class="noborder" id="Department" style="width:100px" value="<%=GetDepartment(CurrentUser.Code)%>" readonly/><input name="Job" type="hidden" class="noborder" id="Job" style="width:100px" value="<%=CurrentUser.HeadShip%>" readonly/><input name="Years"  class="noborder" id="Years" type="hidden" style="width:40px; height:17px" readonly value="<%=datetime.now.year%>"/></td>
            </tr>
            <tr>
                <td class="F_center">
                    ��ʼ����</td>
                <td align="left" valign="middle">
                    <input name="Begindate" type="text" value="<%=datetime.now.ToString("yyyy-MM-dd")%>"
                        onfocus="Cal_dropdown(this);" readonly class="noborder" id="Begindate" style="width: 98%"
                        onpropertychange="sumNum();" /></td>
                <td class="F_center">
                    ��ʼʱ��</td>
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
                    ʱ
                    <select id="StartMinute" name="StartMinute" onchange="sumNum()">
                        <option value="00">00</option>
                        <option value="30">30</option>
                    </select>
                    ��</td>
            </tr>
            <tr>
                <td class="F_center">
                    ��������</td>
                <td align="left" valign="middle">
                    <input name="Enddate" onfocus="Cal_dropdown(this);" value="<%=datetime.now.ToString("yyyy-MM-dd")%>"
                        readonly type="text" class="noborder" id="Enddate" style="width: 98%" onpropertychange="sumNum()" /></td>
                <td class="F_center">
                    ����ʱ��</td>
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
                    ʱ
                    <select id="EndMinute" name="EndMinute" onchange="sumNum()">
                        <option value="00">00</option>
                        <option value="30">30</option>
                    </select>
                    ��</td>
            </tr>
            <tr>
                <td class="F_center">
                    ���Сʱ��</td>
                <td align="left" valign="middle">
                    <input name="Hours" type="text" class="noborder" id="Hours" style="width: 100px"
                          mInput="true" fieldtype="num" FieldTitle="���Сʱ��" />
                </td>
                <td class="F_center">
                    �������</td>
                <td align="left" valign="middle">
                    <input name="LeaveDays" type="text" class="noborder" id="LeaveDays" style="width: 100px"
                         /></td>
            </tr>
            <tr>
                <td height="32" class="F_center">
                    ��������</td>
                <td colspan="4" align="left" valign="middle">
                    &nbsp;&nbsp;&nbsp;&nbsp;�¼�<input name="leavetype" type="radio" id="" value="�¼�" onclick='document.getElementById("trDayNum").style.display="none";sumNum();' />

                    &nbsp;&nbsp;&nbsp;&nbsp;����<input name="leavetype" type="radio" id="" value="����" onclick='document.getElementById("trDayNum").style.display="none";sumNum();' />
                    &nbsp;&nbsp;&nbsp;&nbsp;����<input name="leavetype" type="radio" id="" value="����" onclick='document.getElementById("trDayNum").style.display="none";sumNum();' />
                    &nbsp;&nbsp;&nbsp;&nbsp;���<input name="leavetype" type="radio" id="" value="���" onclick='document.getElementById("trDayNum").style.display="none";sumNum();' />
                    &nbsp;&nbsp;&nbsp;&nbsp;����<input name="leavetype" type="radio" id="" value="����" onclick='document.getElementById("trDayNum").style.display="none";sumNum();' />
                    &nbsp;&nbsp;&nbsp;&nbsp;�����㻤<input name="leavetype" type="radio" id="" value="�����㻤"onclick='document.getElementById("trDayNum").style.display="none";sumNum();' />
                    &nbsp;&nbsp;&nbsp;&nbsp;ɥ��<input name="leavetype" type="radio" id="" value="ɥ��" onclick='document.getElementById("trDayNum").style.display="none";sumNum();' />
                    &nbsp;&nbsp;&nbsp;&nbsp;���<input name="leavetype" type="radio" id="" value="���" onclick='document.getElementById("trDayNum").style.display="none";sumNum();' />
                    &nbsp;&nbsp;&nbsp;&nbsp;�ڼҰ칫<input name="leavetype" type="radio" id="" value="�ڼҰ칫" onclick='document.getElementById("trDayNum").style.display="none";sumNum();' />
                </td>
            </tr>
           <tr style="display: none;" id="trDayNum">
                <td class="F_center">
                    ��������</td>
                <td colspan="4" align="left">
                    <input type="text" id="DayNum" name="DayNum" class="noborder" runat ="server" readonly />
                </td>
            </tr>
            <tr>
                <td class="F_center">
                    ����</td>
                <td height="98" colspan="4" align="left" valign="top">
                    <textarea name="Leavedesc" class="noborder" id="Leavedesc" style="width: 99%; height: 90px"
                        mInput="true" FieldTitle="����"></textarea>
                </td>
            </tr>
            <tr><td style="font-size: 12px; color: Red;" colspan="5">
                    ע�����һ�찴8Сʱ����;�����������������Ϣ��¼�롣
                </td>
            </tr>
 <%--             <tr style="height: 30px;">
    <td colspan="5"><%DisplayAttachment(Convert.ToInt64(ViewState.Item("_Id")))%> </td>
  </tr>--%>
        </table>
    </form>
</body>
</html>

