<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NorthModel.aspx.cs" Inherits="Base_CommonPage_NorthModel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server"> 
<title>顶端布局</title>
    <link href="../../CSS/changeStyle/cupertino.css" rel="stylesheet" />
</head>
<body>
    <script type="text/javascript">
        $(function () {
            
            changeColor('Navigation<%=resourceInfoID %>');
            $("#hiddenNavigationId").val('Navigation<%=resourceInfoID %>');
        });

        function changeColor(id) {
            $("#" + id).css("background-image", "url('images/bannerBg.jpg')")
            $("#" + id).css("background-repeat", "no-repeat");
            $("#" + id).css("background-size", "100%");
        }

        function removeColor() {
            $("#NorthHeadTableID").find("div[id^='Navigation']").css("background-image", "none");
            $("#NorthHeadTableID").find("div[id^='Navigation']").css("background-repeat", "none");
            $("#NorthHeadTableID").find("div[id^='Navigation']").css("background-size", "100%");
           
            if ($("#hiddenNavigationId").val() != "") {
                $("#" + $("#hiddenNavigationId").val()).css("background-image", "url('images/bannerBg.jpg')")
                $("#" + $("#hiddenNavigationId").val()).css("background-repeat", "repeat");
                $("#NorthHeadTableID").find("div[id^='Navigation']").css("background-size", "100%");
            }
        }              
    </script>
  <form id="Form1" runat="server">
        <div  id="northChageWidthDiv" style=" border-bottom:0px; width:100%;height:55px;" >
            <table width="100%" style="height:45px;"  border="0" cellspacing="0" cellpadding="0" >
                <tr style="height:22px;">
                    <td style="width:309px" rowspan="2" valign="top"><img alt="" src="images/loogo.jpg" /></td>
                    <td style="width:532px" rowspan="2" valign="top"><img alt="" src="images/logorightnews.jpg" /></td>
                    
                    <td align="right" style="width:200px; vertical-align:middle; padding-right:3px;" valign="middle">
                        <span style="width:25px;">   <img alt="" src="images/time.jpg"  style=" vertical-align:middle;"/></span>
                        日期：<span id="spanNowDate" style="color:Red;"><%=NowDate%></span>
                         
                    </td>
                </tr>
                <tr> 
                    <td align="right"  valign="middle" style=" padding-right:3px;">
                        <img alt="" src="images/nowUser.jpg" style=" vertical-align:middle;" />
                        当前用户：<span id="span2" style="color:Red;"><%=UserName %></span>&nbsp;&nbsp;
                         <span style="width:25px;">
                        <a href="javascript:" onclick="if(window.confirm('您确定要退出？')){window.location.href='login.aspx';}" ><img alt="" src="images/exit.jpg" style="border:0px; vertical-align:bottom; " /></a>
                        
                        <a href="javascript:" onclick="if(window.confirm('您确定要退出？')){window.location.href='login.aspx';}"  >退出</a>
                        </span>
                     </td>
                </tr> 
                <tr style="height:22px; display:none;">
                <td  valign="middle"  align="right" ><%=MyTaskCountStr%></td>
                <td  align="right" style="height:22px;vertical-align:middle;" >
                     
                </td>
                </tr>
                 <tr style="height:22px;"></tr>
            </table>
        </div>
        <div style="border-top :1px solid #A4A4A4;width:100%;height:1px;">&nbsp;</div>
        <div class="gradient"  style="width:100%;height:45px; background-color:#E0ECFF;border-bottom: 1px solid #A4A4A4; ">
            <asp:HiddenField id="hidType" value="0" runat="server" />
            <input id="hiddenNavigationId" type="hidden" />
            <input id="hidCheckResID" value="<%=resourceInfoID %>" type="hidden" />
            <div style="width:100%; height:35px;" id="NorthHeadTableID" >
            <span style=" width:40px;float:left; text-align:left; font-size:14px;padding-top:3px; ">
            &nbsp;
            </span> 
            <span style=" width:140px; float:left; text-align:center; font-size:14px;padding-top:3px; ">
                <div id="Navigation<%=CommonProperty.PortalResourceID  %>" onclick='setWestModelUrl(<%=CommonProperty.PortalResourceID  %>,"Base/SystemConfig/ResourceTree.aspx?ResID=<%=CommonProperty.PortalResourceID  %>&PID=")' onmouseout="removeColor()"  onmouseover="changeColor('Navigation<%=CommonProperty.PortalResourceID  %>')"  style="text-align:center;height:35px;width:140px; line-height:35px;"  >
                    <a  href="javascript:"  style="text-decoration:none;text-align:center; color:#000000;" >Portal菜单配置</a>
                </div>
            </span> 
            <span style=" width:140px; float:left; text-align:left; font-size:14px;padding-top:3px;padding-bottom:3px;  ">
                <div id="Navigation1" onclick='setWestModelUrl(1,"Base/SystemConfig/ResourceTree.aspx?ResID=&PID=-1")' onmouseout="removeColor()"  onmouseover="changeColor('Navigation1')"  style="text-align:center;height:35px;width:140px; line-height:35px;"  >
                    <a  href="javascript:"  style="text-decoration:none; color:#000000;" >资源配置管理</a>
                </div>
            </span> 
            </div>
            </div> 
        </form>
</body>
</html>
