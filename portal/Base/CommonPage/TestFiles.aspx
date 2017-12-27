<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestFiles.aspx.cs" Inherits="Base_CommonPage_TestFiles" %>

<%@ Register Src="~/Base/CommonControls/UploadFile.ascx" TagPrefix="uc1" TagName="UploadFile" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <div class="con" id="div<%=ResID %>FormTable" style="overflow-x: hidden; overflow-y: hidden; position: relative; border: none;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table2" id="Table1<%=ResID %>">
                <tr>
                    <td colspan="8" valign="middle">&nbsp;<a style="border: 0px;" href="#"><input type="image" id="fnChildSave" src="../../images/bar_save.gif"
                        style="padding: 3px 0px 0px 0px; border: 0px;" onclick="fnChildSave(); return false;" /></a>
                        <a style="border: 0px;" href="#" onclick="window.parent.ParentCloseWindow();">
                            <input type="image" src="../../images/bar_out.gif" style="padding: 3px 0px 0px 0px; border: 0px;"
                                onclick="return false;" /></a>
                    </td>
                </tr>
            </table>
            <div title="" class="easyui-panel" style="overflow: hidden; padding: 5px; border-bottom: none; margin: 0px;">
                <div class="easyui-panel" border="true" style="border-bottom: none;">
                    <table border="0" class="table2" cellspacing="0" cellpadding="0" id="Table3" style="width: 100%;"> 
                        <tr>
                            <th style="width: 11%">文件编号</th>
                            <td style="width: 22%">
                                <input id="<%=ResID %>_文件编号" type="text" class="box3" style="width: 90%;" />
                                <span style="color: red;">*</span></td>
                            <th style="width: 11%">文件名</th>
                            <td colspan="3">
                                <input id="<%=ResID %>_文件名" type="text" class="box3" style="width: 96%;" />
                                <span style="color: red;">*</span></td>
                        </tr>
                    </table>

                    <uc1:UploadFile runat="server" ID="UploadFile1" />

                </div>
            </div>
    </div>
    </form>
</body>
</html>
