<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default2.aspx.vb" Inherits="report_Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>个人信息查询</title>
    <script src="../My97DatePicker/WdatePicker.js" type="text/javascript"></script>

   
    <script src="../script/jquery-1.4.1.min.js" type="text/javascript"></script>
    <style type="text/css">
        body{width:1150px; height:auto; margin:auto; padding:0px; text-align:center; font:"宋体"; font-size:12px; }
        img{border:none;}
        .liebiao{width:1140px; height:auto; float:left; margin-left:10px;}
        .list_fy{width:1000px; height:auto; float:left;}
        .list_fy  span{ color:#3399FF}
        .list_nr{width:1140px; height:auto; float:left; border:1px solid #c1d9f5; background-color:#FFF; padding-bottom:10px; }

        .list_bt{ height:20px; float:left; border:1px solid #FFF;  background-color:#3399FF; text-align:left; text-indent:10px; padding-top:10px; color:#FFF;}
        .list_fy{width:1000px; height:auto; float:left; text-align:center; padding-top:35px;}
        .list_fy a:link{color:#3399FF; text-decoration:none;}
        .list_fy a:visited{color:#3399FF; text-decoration:none;}
        .list_fy a:hover{color:#ff0000; text-decoration:none;}
        .list_fy ul li span{ color :#3399FF}
        
        .list_mian{width:1140px; height:15px; float:left; color:#3399FF; text-align:left; padding-top:9px; }
        .list_mian td{ padding-left :10px;}
        .list_mian div{ height:auto; float:left; text-indent:10px;}
        .list_mian div a:link{color:#3399FF; text-decoration:none;}
        .list_mian div a:visited{color:#3399FF; text-decoration:none;}
        .list_mian div a:hover{color:#FF0000; text-decoration:none;}
        th{line-height:18px;background:#E3EFFC;text-align: center;}
        table{font-size: 12px;
	line-height: 18px;
	color: #666666;
	text-decoration: none;
	border-top-width: 2px;
	border-right-width: 1px;
	border-bottom-width: 1px;
	border-left-width: 2px;
	border-top-style: solid;
	border-right-style: solid;
	border-bottom-style: solid;
	border-left-style: solid;
	border-top-color: #67b2ec;
	border-right-color: #67b2ec;
	border-bottom-color: #67b2ec;
	border-left-color: #67b2ec;}
    </style>
    <script type="text/javascript">
        function Check() {
            var proVal = "";
            $('input[type="checkbox"][name="chk"]:checked').each(
                function () {
                    proVal = proVal + "|" + $(this).val();
                }
            );
            $("#txtHid").val(proVal);
        }
  
    </script>



</head>
<body>
    <form id="form1" runat="server">
    

    <div style="float:left;">
        <table style="width:1000px;" align="left">
            <tr>
                <td colspan="2">
                </td>
            </tr>
                        <tr>
                <td colspan="2">
                <table border="0.5"  align="left" cellspacing="0">
            <tr align="center">
                <th style="width: 100px">
                    账号
                </th>
                <th style="width: 100px">
                    姓名
                </th>
                <th style="width: 100px">
                    入职日期
                </th>
                <th style="width: 100px">
                    部门
                </th>
                <th style="width: 100px">
                   邮箱
                </th>
                
            </tr>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr align="center">
                        <td>
                            <%#Eval("EmpCode")%>&nbsp;
                        </td>
                        <td>
                            <%#Eval("EmpName")%>&nbsp;
                        </td>
                        
                        <td>
                           
                             <%#String.Format("{0:yyyy-MM-dd}", Eval("EntryDate"))%>&nbsp;
                        </td>
                       
                       <td>
                            <%#Eval("Department")%>&nbsp;
                        </td>
                       
                        <td>
                            <%#Eval("Email")%>&nbsp;
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
                
                </td>
            </tr>
                       <tr>
                <td align="left"><div id="divHidPrint" style="float:left;">
        年:<input type="text" class="box3" runat="server" style="width: 70px;" onfocus="WdatePicker({dateFmt:'yyyy'})" id="seYear" />
        月:<input type="text" class="box3" runat="server" style="width: 70px;" onfocus="WdatePicker({dateFmt:'MM'})" id="seMonth" />
        <asp:Button ID="btnSearch" runat="server" Text="搜索" />
        &nbsp;&nbsp;
        <asp:Button ID="btnPrint" OnClientClick="btn()" runat="server" Text="打印" />
    </div>
                </td>
                <td >
                
                </td>   
            </tr>
            <tr>
            <td style="width: 500px; text-align: center;" align='left'>
                <span style="font-size: 12pt; font-family: System"><strong>请假信息</strong></span></td>
             <td style="width: 500px; text-align: center;"> <span style="font-size: 12pt; font-family: System"><strong>加班信息</strong></span></td>
            </tr>
                        <tr>
                <td valign="top"> <div style="float:left;">

         <table border="0"  cellspacing="0">
         
            <tr align="center">
                <th style="width: 100px">
                    账号
                </th>
                <th style="width: 100px">
                    姓名
                </th>
             <th style="width: 100px">
                    请假日期
                </th>
                <th style="width: 100px">
                    请假类型
                </th>
                <th style="width: 100px">
                   请假天数
                </th>
                
            </tr>
            <asp:Repeater ID="Repeater2" runat="server">
                <ItemTemplate>
                    <tr align="center">
                        <td>
                            <%#Eval("EmpCode")%>&nbsp;
                        </td>
                        <td>
                            <%#Eval("EmpName")%>&nbsp;
                        </td>
                        
                      <td>
                             <%#String.Format("{0:yyyy-MM-dd}", Eval("Askdate"))%>&nbsp;
                          
                        </td>
                       
                       <td>
                            <%#Eval("Leavetype")%>&nbsp;
                        </td>
                       
                        <td>
                            <%#Eval("LeaveDays")%>&nbsp;
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
       
       </table>
    </div>
                </td>
                <td valign="top" > <div style="float:left;">

         <table border="0"  cellspacing="0">
         
            <tr align="center">
                <th style="width: 100px">
                    账号
                </th>
                <th style="width: 100px">
                    姓名
                </th>
             <th style="width: 100px">
                   加班日期
                </th>
                <th style="width: 100px">
                   加班小时
                </th>
                <th style="width: 100px">
                   事由
                </th>
                
            </tr>
            <asp:Repeater ID="Repeater3" runat="server">
                <ItemTemplate>
                    <tr align="center">
                        <td>
                            <%#Eval("EmpCode")%>&nbsp;
                        </td>
                        <td>
                            <%#Eval("EmpName")%>&nbsp;
                        </td>
                        
                      <td>
                
                           <%#String.Format("{0:yyyy-MM-dd}", Eval("StartDate"))%>&nbsp;
                        </td>
                       
                       
                       
                        <td>
                            <%#Eval("Overtime")%>&nbsp;
                        </td>
                        <td>
                            <%#Eval("Explain")%>&nbsp;
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
       
       </table>
    </div>
                </td>   
            </tr>
            
        </table>

    
    </div>
      
    
   
    </form>
</body>
</html>
