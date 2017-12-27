<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintBXGL.aspx.cs" Inherits="PrintBXGL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>报销管理</title>
    <script src="../../Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
</head>
<body>
    <style type="text/css">
        table.gridtable {
            font-family: verdana,arial,sans-serif;
            font-size: 11px;
            color: #333333;
            border-width: 1px;
            border-color: #666666;
            border-collapse: collapse;
            width: 750px;
            margin-left: auto;
            margin-right: auto;
        }

        table.gridtable th {
            border-width: 1px;
            padding: 3px;
            border-style: solid;
            border-color: #666666;
            background-color: #dedede;
            width: 160px;
            height: 25px;
            text-align: left;
        }

        table.gridtable td {
            border-width: 1px;
            padding: 3px;
            border-style: solid;
            border-color: #666666;
            background-color: #ffffff;
            width: 200px;
            height: 25px;
            font-size: 12px;
        }

        table.gridtable1 {
            font-family: verdana,arial,sans-serif;
            font-size: 11px;
            color: #333333;
            border-width: 1px;
            border-color: #666666;
            border-collapse: collapse;
            width: 350px;
            margin-left:368px;
        }

        table.gridtable1 th {
            border-width: 0;
            padding: 3px;
            border-style: solid;
            border-color: #666666;
            background-color: #dedede;
            width: 200px;
            height: 25px;
            text-align:right;
        }

        table.gridtable1 td {
            border-width: 1px;
            padding: 3px;
            border-style: solid;
            border-color: #666666;
            background-color: #ffffff;
            width: 150px;
            height: 25px;
            font-size: 12px;
        }
    </style>
    <script type="text/javascript">
    </script>
    <form id="form1" runat="server">
        <div style="text-align: center; margin-top: 5px; margin-bottom: 10px; margin-left: 800px">
            <input type="button" value="打印" onclick="this.style.visibility = 'hidden'; window.print();" />
        </div>
        <div class="FormTable" style="width:750px;margin-left: auto;margin-right: auto;border:1px solid #D0D0D0">
            <table class="gridtable">
                <tr>
                    <td style="font-size: 24px; border: 0px;text-align:center" colspan="4">
                        <p><strong>报销管理</strong></p>
                    </td>
                </tr>
                <tr>
                    <th colspan="4">基本信息</th>
                </tr>
                <tr>
                    <th>日期</th>
                    <td><span ><%=RQ%></span></td>
                    <th>报销人</th>
                    <td><span ><%=BXR%></span></td>
                </tr>
                <tr>
                    <th>报销内容</th>
                    <td colspan="3"><span><%=BXNR%></span></td>
                </tr>
                <tr style="height:50px">
                    <th>报销说明</th>
                    <td colspan="3"><span ><%=BXSM%></span></td>                
                </tr>
                <tr>
                    <th>报销金额</th>
                    <td><span><%=BXJE%></span></td>
                    <th>凭证张数</th>
                    <td><span ><%=PZZS%></span></td>
                </tr>
                <tr>
                    <th>报销科目</th>
                    <td><span  ><%=BXKMMC%></span></td>
                    <th>是否已有请款</th>
                    <td><span ><%=SFYYQK%></span></td>
                </tr>
                <tr>
                    <th>请款编号</th>
                    <td><span ><%=QKBH%></span></td>
                    <th>请款金额</th>
                    <td><span ><%=QKJE%></span></td>
                </tr>
                <tr style="height:50px">
                    <th>备注</th>
                    <td colspan="3"><span ><%=BZ%></span></td>                
                </tr>
                <tr style="height:50px">
                    <th>外包项目编号</th>
                    <td colspan="3"><span  ><%=WBXMBH%></span></td>    
                </tr>
                <tr>
                <td colspan="4">
                    <table class="gridtable1" style="border:1px solid #666666">
                    <tr>
                        <th colspan="4" style="text-align:left">财务状态</th>
                    </tr>
                    <tr>
                        <th>是否已汇款</th>
                        <td><span ><%=SFYHK%></span></td>
                    </tr>
                    <tr>
                        <th>凭证是否已到财务</th>
                        <td><span ><%=PZSFYDCW%></span></td>
                    </tr>
                    </table>
                </td>
                </tr>
            </table>
            
        </div>
    </form>
</body>
</html>
