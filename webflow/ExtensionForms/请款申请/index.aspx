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
    <title>�������</title>
    <link href="../css/YYTys.css" rel="stylesheet" type="text/css" />
</head>
<body>
 <!--#include file="../includes/WorkflowUtilies.aspx"-->
    <script language="vb" runat="server">
        Private ActionKey As String
        Private WorkflowId As String
        Private QKSQID As String
        
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim bxkmDt As DataTable = SDbStatement.Query("select KeyValue from DataDictionary WHERE ParentId='545845512082' ORDER BY KeySort ASC").Tables(0)
            BXKMRep.DataSource = bxkmDt
            BXKMRep.DataBind()
            Dim FLOWCODE As String
            Dim MaxFLOWCODEDt As DataTable = SDbStatement.Query("select max(Convert(float,FLOWCODE))+1 from CT282994819609  ").Tables(0)
            FLOWCODE = Convert.ToInt32(MaxFLOWCODEDt.Rows(0)(0)).ToString()
            
            If ViewState.Item("_Id") Is Nothing Then ViewState("_Id") = TimeId.CurrentMilliseconds(30)
            QKSQID=ViewState("_Id").ToString()
            If Request.Form("action_value") <> "" Then ActionKey = Request.Form("action_value")
            If Request("WorkflowId") <> "" Then WorkflowId = Request("WorkflowId")
            If Not Me.IsPostBack Then Return
            If ActionKey = "" Then Return
            Dim hst As New Hashtable
            hst.Add("ID", QKSQID)
            hst.Add("RESID", 282994819609)
            hst.Add("RELID", 0)
            hst.Add("CRTID", CurrentUser.Code)
            hst.Add("CRTTIME", DateTime.Now)
            hst.Add("EDTID", CurrentUser.Code)
            hst.Add("EDTTIME", DateTime.Now)
            hst.Add("FLOWCODE", FLOWCODE) '��� �����ϵ�
            hst.Add("C3_282994956906", "QK-" + FLOWCODE) '(���̱��)
            hst.Add("C3_282994853625", Request("C3_282994853625").ToString()) '��������   
            hst.Add("C3_282994864593", Request("C3_282994864593").ToString()) '������   
            hst.Add("C3_282994932921", Request("C3_282994932921").ToString()) '�����   
            hst.Add("C3_282994883125", Request("C3_282994883125").ToString()) '�ɹ����ϼ�
            hst.Add("C3_282994921125", Request("C3_282994921125").ToString()) '������� 
            hst.Add("C3_384347781708", Request("C3_384347781708").ToString()) '������Ŀ���� 
            hst.Add("C3_386163735265", Request("C3_386163735265")) '�����Ŀ��� 
            hst.Add("C3_284725263953", Request("C3_284725263953").ToString()) '�Ƿ��ѻ��   
            hst.Add("C3_284725355500", Request("C3_284725355500").ToString()) '�Ƿ���תΪ����
            hst.Add("C3_284725426953", Request("C3_284725426953")) '������������� 
            Dim isHave As Integer = SDbStatement.InsertRow(hst, "CT282994819609")
            Dim oWorklistItem As WorklistItem = CreateWorkflowInstance(Convert.ToInt64(WorkflowId), Convert.ToInt64(QKSQID), ActionKey, hst, "")
            hst.Add("WorkflowInstId", oWorklistItem.ActivityInstance.WorkflowInstance.Key)
            StartWorkflowInstance(oWorklistItem)
        End Sub
    </script>
    <link rel="stylesheet" type="text/css" href="../easyUI/jquery-easyui-1.4.3/themes/gray/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../easyUI/jquery-easyui-1.4.3/themes/icon.css"/> 
       <!--��֤ҳ���ı��������� js�ļ�----------->
    <script type="text/javascript" language="JavaScript" src="../scripts/FormValidate.js"></script>
    <script type="text/javascript" src="../easyUI/jquery-easyui-1.4.3/jquery.min.js"></script>
    <script type="text/javascript" src="../easyUI/jquery-easyui-1.4.3/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../easyUI/easyui-lang-zh_CN.js" ></script>
    <script src="../OverTime/scripts/FormValidate.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">


    $(function () {
        $('#C3_386163735265').combogrid({
            panelWidth: 700,
            height: 30,
            width: 590,
            checkbox: true,
            rownumbers: true,
            selectOnCheck: true,
            checkOnSelect: true,
            singleSelect: true,
            multiple: true,
            showFooter: true,
            fitColumns: true,
            pagination: true,
            idField: '�����Ŀ���',
            textField: '�����Ŀ���',
            url: '../common/AjaxRequest.aspx?typeValue=GetWBXMBH',
            columns: [[
                { field: 'ck', checkbox: true },
	            { field: '��Ŀ���', title: '��Ŀ���', width: 80, align: 'center' },
	            { field: '�����Ŀ���', title: '�����Ŀ���', width: 80, align: 'center' },
	            { field: '��������', title: '��������', width: 60, align: 'center' },
                { field: '�������', title: '�������', width: 60, align: 'center' },
                { field: '��������', title: '��������', width: 30, align: 'center' },
                { field: '�ܷ���', title: '�ܷ���', width: 30, align: 'center' },
                { field: '����', title: '����', width: 30, align: 'center' }
            ]]
        });

        $('#C3_284725426953').combogrid({
            panelWidth: 700,
            height: 30,
            width: 590,
            checkbox: true,
            rownumbers: true,
            selectOnCheck: true,
            checkOnSelect: true,
            singleSelect: true,
            multiple: true,
            showFooter: true,
            fitColumns: true,
            pagination: true,
            idField: '���������',
            textField: '���������',
            url: '../common/AjaxRequest.aspx?typeValue=GetBXDBH',
            columns: [[
                { field: 'ck', checkbox: true },
	            { field: '���������', title: '���������', width: 80, align: 'center' },
	            { field: '������', title: '������', width: 80, align: 'center' },
	            { field: '�������', title: '�������', width: 60, align: 'center' },
                { field: '��ע', title: '��ע', width: 330, align: 'center' }
            ]]
        });
//        loadCenterGrid();
    });

//    function loadCenterGrid() {
//        $('#CenterGrid').datagrid({
//            toolbar: [{
//                iconCls: 'icon-add',
//                text: "���",
//                handler: function () {
//                    fnFormListDialog("Add.aspx", 600, 400, "�����Ϣ");
//                }
//            }, '-', {
//                iconCls: 'icon-add',
//                text: "ɾ��",
//                handler: function () {
//                    var rec = $('#CenterGrid').datagrid('getSelected');
//                    if (rec == null) {
//                        alert("��ѡ��һ����¼!")
//                    } else {
//                        if (confirm("ȷ��Ҫɾ��������¼��")) {
//                            var rowIndex = $('#CenterGrid').datagrid('getRowIndex', rec);
//                            $('#CenterGrid').datagrid('deleteRow', rowIndex);
//                        }
//                    }
//                }
//            }],
//            nowrap: true,
//            border: true,
//            striped: true,
//            singleSelect: true,
//            loadFilter: function (data) {
//                if (data == null) {
//                    $(this).datagrid("load");
//                    return $(this).datagrid("getData");
//                } else {
//                    return data;
//                }
//            },
//            url: '../common/AjaxRequest.aspx?typeValue=GetCGMX',
//            queryParams: {
//                QKBH: ""
//            },
//            columns: [[
//	            { field: '��Ʒ����', title: '��Ʒ����', width: 80, align: 'center' },
//	            { field: '����', title: '����', width: 60, align: 'center' },
//                { field: '����', title: '����', width: 60, align: 'center' },
//                { field: '��;', title: '��;', width: 260, align: 'center' },
//                { field: '���', title: '���', width: 60, align: 'center'}]],
//            fitColumns: true,
//            fit: true,
//            rownumbers: true
//        });
//    }
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

//    function ParentCloseWindow() {
//        $('#divParentContent').dialog('close');
//        $('#CenterGrid').dialog('loadData');
//    }

    //��֤
    function SaveQKSQ() {
        if ($("#C3_282994932921").val() == "") {
            alert("������Ϊ�գ�");
            return false;
        }
//        var Json1 = getJson1();
//        var Json2 = getJson2();
//        
//        $.ajax({
//            type: "POST",
//            dataType: "json",
//            async: false,
//            data: { "Json1": "" + Json1 + "", "Json2": "" + Json2 + "" },
//            url: "../common/AjaxRequest.aspx?typeValue=SaveQKSQ&QKSQID=<%=QKSQID %>&Code=<%=CurrentUser.Code %>",
//            async: false,
//            success: function (obj) {
//                if (obj == "1") {
//                    alert("�ύ�ɹ���");
//                    return true;
//                } else {
//                    alert("�ύʧ�ܣ�");
//                    return false;
//                }
//            }
//        });
    }
//    
//    //��ȡ�������JSON
//    function getJson1() {
//        var s1 = $("#C3_282994853625").val(); //��������
//        var s2 = $("#C3_282994864593").val(); //������
//        var s3 = $("#C3_282994932921").val(); //�����
//        var s4 = $("#C3_282994883125").val(); //�ɹ����ϼ�
//        var s5 = $("#C3_282994921125").val(); //�������
//        var s6 = $("#C3_384347781708").val();  //������Ŀ����
//        var s7=$("#C3_386163735265").val();  //�����Ŀ���
//        var s8=$("#C3_284725263953").val();  //�Ƿ��ѻ��
//        var s9=$("#C3_284725355500").val();  //�Ƿ���תΪ����
//        var s10 = $("#C3_284725426953").val();  // �������������
//        var Json1 = "[{";
//        Json1 += "'C3_282994853625':'" + s1 + "',";
//        Json1 += "'C3_282994864593':'" + s2 + "',";
//        Json1 += "'C3_282994932921':'" + s3 + "',";
//        Json1 += "'C3_282994883125':'" + s4 + "',";
//        Json1 += "'C3_282994921125':'" + s5 + "',";
//        Json1 += "'C3_384347781708':'" + s6 + "',";
//        Json1 += "'C3_386163735265':'" + s7 + "',";
//        Json1 += "'C3_284725263953':'" + s8 + "',";
//        Json1 += "'C3_284725355500':'" + s9 + "',";
//        Json1 += "'C3_284725426953':'" + s10 + "'";
//        Json1 += "}]";
//        return Json1;
//    }

//    //��ȡ�ɹ���ϸ��JSON
//    function getJson2() {
//        var rows = $('#CenterGrid').datagrid('getRows');
//        var Json2 = "[";
//        for (var i = 0; i < rows.length; i++) {
//            Json2 += "{";
//            Json2 += "'C3_282995029781':'" + rows[i].��Ʒ���� + "',";
//            Json2 += "'C3_282995087546':'" + rows[i].���� + "',";
//            Json2 += "'C3_282995098390':'" + rows[i].���� + "',";
//            Json2 += "'C3_282995108593':'" + rows[i].��; + "',";
//            Json2 += "'C3_282995628468':'" + rows[i].��� + "'";
//            Json2 += "},";
//        }
//        if (rows.length>0) {
//            Json2 = Json2.substring(0, Json2.length - 1);
//        }
//        Json2 += "]";
//        return Json2;
//    }
    </script>
    <form id="Form1" method="post" runat="server">
        <%GenerateCommand(Convert.ToInt64(WorkflowId), "return SaveQKSQ();")%>
        <h1 align="center">
            �������</h1>
        <table width="700" border="0" align="center" cellpadding="0" cellspacing="0" class="Bold_box">
            <tr>
                <td width="100" class="F_center">
                    ������</td>
                <td width="250" align="left" valign="middle">
                    <input name="C3_282994864593" type="text" class="noborder" id="C3_282994864593" style="width:95%"
                        value="<%=CurrentUser.Name%>"  /></td>
                <td width="100" class="F_center" style="height:30px;">
                    ��������</td>
                <td width="250" align="left" valign="middle">
                    <input name="C3_282994853625" type="text" class="easyui-datebox" id="C3_282994853625" style="width:95%"
                        value="<%=datetime.now.ToString("yyyy-MM-dd")%>"  /></td>
            </tr>
             <tr>
                <td width="100" class="F_center">
                    �����</td>
                <td width="250" align="left" valign="middle">
                    <input name="C3_282994932921" type="text" class="noborder" id="C3_282994932921" mInput="true" FieldTitle="�����" style="width: 95%" /></td>
                <td width="100" class="F_center" style="height:30px;">
                    �ɹ����ϼ�</td>
                <td width="250" align="left" valign="middle">
                    <input name="C3_282994883125" type="text" class="noborder" id="C3_282994883125"  mInput="true" FieldTitle="�����" style="width: 95%" /></td>
            </tr>
            <tr>
                <td class="F_center" >�������</td>
                <td align="left" colspan="3" >
                 <textarea name="C3_282994921125" class="noborder" id="C3_282994921125" style="width:98%; height:90px"  mInput="true"  FieldTitle="�������"></textarea>
                </td>
            </tr>
            <tr>
                <td class="F_center"  style="height:40px;">������Ŀ����</td>  
                <td align="left" colspan="3"  valign="center" >
                    <select style="width: 98%;height:30px;" id="C3_384347781708" name="C3_384347781708" >
                        <asp:Repeater ID="BXKMRep" runat="server">
                        <ItemTemplate>
                            <option value="<%#Eval("KeyValue") %>"><%#Eval("KeyValue") %></option>
                        </ItemTemplate>
                        </asp:Repeater>
                    </select>
                </td>  
            </tr>
            <tr> 
                <td class="F_center" style="height: 40px;">�����Ŀ���</td>
                <td align="left" colspan="3"  valign="center" >
                     <input name="C3_386163735265" id="C3_386163735265" class="easyui-combogrid" />
                </td>
            </tr>
         <%--   <tr><td colspan="4" style="height:150px;">
                <table id="CenterGrid"></table>
            </td></tr>--%>
             <tr style="height: 30px;">
                <td class="F_center" colspan="4"  style="height: 30px;">����״̬</td>
            </tr> 
           <tr style="height: 40px;">
               <td class="F_center" >�Ƿ��ѻ��</td>
               <td align="left"  valign="middle" ><select id="C3_284725263953" name="C3_284725263953" style="width:95%;height:30px;"><option></option><option value="��">��</option><option value="��">��</option></select></td>
            <td class="F_center" >�Ƿ���תΪ����</td>
                <td align="left" valign="middle" ><select id="C3_284725355500" name="C3_284725355500" style="width:95%;height:30px;"><option></option><option value="��">��</option><option value="��">��</option></select></td>
           </tr> 
            <tr style="height: 40px;">
                <td class="F_center"  style="height: 30px;">�������������</td>
                <td  colspan="3" ><input id="C3_284725426953" name="C3_284725426953" class="easyui-combogrid" /></td>
            </tr> 
        </table>
        <div closed="true"  class="easyui-window"  id="divParentContent" style="overflow:hidden;"/>
    </form>
</body>
</html>

