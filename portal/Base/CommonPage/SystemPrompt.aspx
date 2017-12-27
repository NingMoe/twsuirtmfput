<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemPrompt.aspx.cs" Inherits="Base_CommonPage_SystemPrompt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>数据列表</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <%=this.GetScript1_4_3  %>  
    <link href="../../CSS/THI/css/css.css" rel="stylesheet" /> 
    
</head>
<body>
    <form id="form1" runat="server">
        <script type="text/javascript">
            var centerHeight = $('#westModel_id', window.parent.document).height();//内容部分页面高度
            var centerWidth = $('#centerModel_id', window.parent.document).width();//内容部分页面宽度
            $(document).ready(function () { //初始化方法
                $(".rightlist").css({ height: centerHeight / 2 - 10 });
                $(".tyHeightForTH").css({ height: centerHeight / 2 - 55 });
            });

            function click(id) {
                $("#wcdj").css("display", "none");
                $("#qjdj").css("display", "none");
                $("#" + id).css("display", "block");
            }
        </script>
        <div class="rightmain">
            <div class="rightlist">
                <div class="menubg">
                    <img src="../../images/icon1.png" />&nbsp;&nbsp;待办事宜
                </div>
                <div class="content1">
                    <div style="display: block; overflow: auto;" class="tyHeightForTH">
                        <table id="DBLX" width="100%" class="table" cellpadding="0" cellspacing="0">
                            <asp:Repeater ID="RepeaterDBLX" runat="server">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr>
                                            <td width="30%" class="borderrigth1 borderbottom1ccc">流程名称</td>
                                            <td width="70%" class="borderrigth1 borderbottom1ccc">流程主题</td>
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tbody>
                                        <tr>
                                            <td class="borderbottom1ccctd" style="text-align: center;"><%#Eval("流程") %></td>
                                            <td class="borderbottom1ccctd" style="text-align: center;"><a href="Execution.aspx?Type=transtract&WorklistItemId=<%#Eval("流程ID")  %>&WorkflowInstId=<%#Eval("流程ID")  %>" target="_blank"><%#Eval("主题") %></a></td>
                                        </tr>
                                    </tbody>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </div>
            <div class="rightlist">
                <div class="menubg">
                    <img src="../../images/icon1.png" />&nbsp;&nbsp;通知公告
                </div>
                <div class="content1">
                    <div style="display: block; overflow: auto;" class="tyHeightForTH">
                        <table class="table" cellpadding="0" cellspacing="0">
                            <asp:Repeater ID="Repeater4" runat="server">
                                
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </div>
            <div class="rightlist">
            <div class="menubg">
                <img src="../../CSS/THI/images/icon1.png" />&nbsp;&nbsp;出勤记录
            </div>
            <div class="content1" id="wcdj">
                <div style="display: block; overflow: auto;" class="tyHeightForTH">
                    <table class="table" cellpadding="0" cellspacing="0">
                        <asp:Repeater ID="Repeater2" runat="server">
                            
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </div>

            <div class="rightlist">
            <div class="menubg">
                <img src="../../CSS/THI/images/icon1.png" />&nbsp;&nbsp;<span style="color:red;">我的提醒(未完成)</span>
            </div>
            <div class="content1" id="Div1">
                <div style="display: block; overflow: auto;" class="tyHeightForTH">
                    <table class="table" cellpadding="0" cellspacing="0">
                        <asp:Repeater ID="Repeater1" runat="server">
                            
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

