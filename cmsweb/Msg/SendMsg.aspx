<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMsg.aspx.cs" Inherits="Msg_SendMsg" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
      <script type="text/javascript" language="JavaScript" src="../script/dragDiv.js"></script>
<title>短信发送</title>
<style type="text/CSS">  
.roundedRectangle{  
    height:40px;   
    width: 99%;  
    margin-top: 8px;  
    background: #F0F8FF;   
    border-width: 3px;   
    border-style: solid;  
    border-radius: 15px;     
    border-color: #F0F8FF #F0F8FF #F0F8FF #F0F8FF;    
}  
  
.roundedRectangle p{  
    margin: 0px auto;  
    padding: 10px;  
    text-align:left;  
    font-size: 20px;  
    letter-spacing:10px;  
}  
  
</style> 
    <script type="text/javascript">
        var msgLength = 300;
        var smslong = 67;
        if (smslong <= 0) {
            smslong = 70;
        }
        var longsms_long = 64;//长短信长度
        function checkContentLength() {
            var content = document.getElementById(arguments[0]);
            var length = msgLength;

            if (arguments.length == 3) {
                length = msgLength;
                var message = document.getElementById(arguments[2]);

                var l = content.value.length;
                l = l + document.getElementById("<%=title.ClientID%>").value.length;
                var useSign = true;


                var b = msgLength - l;
                if (l < 0) {
                    l = 0;
                }
                if (b < 0) {
                    b = 0;
                }
                var phonelength = mobilecount(l, smslong);
                var firstMsg = "";
                if (useSign) {
                    firstMsg = "包含签名";
                }
                message.innerHTML = firstMsg + "已输入<font color='red'>" + l + "</font>个字，还剩<font color='red'>" + b + "</font>字，最多<font color='red'>" + msgLength + "</font>个字，手机<font color='red'>" + phonelength + "</font>条";
            } else if (arguments.length == 2) {
                length = msgLength;
            }
            if (l > length) {
                alert("您输入的文字长度达到最多" + msgLength + "个字，请酌情删减！");
            }
        }

        //计算座机的数量
        function telcount(contentLength) {
            var totalcount = 1;
            if (contentLength > 60 && contentLength <= (60 + 54)) {
                totalcount = 2;
            }
            else if (contentLength > (60 + 54) && contentLength <= (60 + 54 * 2)) {
                totalcount = 3;
            }
            else if (contentLength > (60 + 54 * 2) && contentLength <= (60 + 54 * 3)) {
                totalcount = 4;
            }
            else if (contentLength > (60 + 54 * 3)) {
                totalcount = 5;
            }
            return totalcount;
        }
        //计算手机的数量
        function mobilecount(contentLength, firstLength) {

            var totalcount = 1;
            if (contentLength <= firstLength) {
                totalcount = 1;
            } else {
                totalcount = Math.ceil(contentLength / longsms_long);
            }

            return totalcount;
        }
        //鼠标按下的事件
        function keydown() {
            var content = document.getElementById("<%=content.ClientID%>");
            var length = msgLength;
            var message = document.getElementById("message");


            var l = content.value.length;
            l = l + document.getElementById("<%=title.ClientID%>").value.length;

            var b = msgLength - l;

            if (((event.ctrlKey) && (event.keyCode == 86 || event.keyCode == 88)) || event.keyCode == 8 || event.keyCode == 32 || event.keyCode == 46 || event.keyCode == 13 || (event.keyCode >= 48 && event.keyCode <= 90)) {              //屏蔽 Ctrl+n
                if (l < 0) {
                    l = 0;
                }
                if (b < 0) {
                    b = 0;
                }

                var phonelength = mobilecount(l, smslong);

                var useSign = true;
                var firstMsg = "";
                if (useSign) {
                    firstMsg = "包含签名";
                }

                message.innerHTML = firstMsg + "已输入<font color='red'>" + l + "</font>个字，还剩<font color='red'>" + b + "</font>字，最多<font color='red'>" + msgLength + "</font>个字，手机<font color='red'>" + phonelength + "</font>条";
                if (l > length) {
                    alert("您输入的文字长度达到最多" + msgLength + "个字，请酌情删减！");
                }
            }
        }
        //去除空格方法
        function trim(str) { return str.replace(/^\s+|\s+$/g, ''); }

        //黏贴的事件
        function paste() {
            var content = document.getElementById("<%=content.ClientID%>");
            var length = msgLength;
            var message = document.getElementById("message");

            //去掉最后空格
            content.value = trim(content.value);
            //


            var l = content.value.length;  //加上签名的长度
            l = l + document.getElementById("<%=title.ClientID%>").value.length;
            var useSign = true;

            var b = msgLength - l;

            if (l < 0) {
                l = 0;
            }
            if (b < 0) {
                b = 0;
            }

            var tellength = telcount(l);
            var phonelength = mobilecount(l, smslong);

            var firstMsg = "";
            if (useSign) {
                firstMsg = "包含签名";
            }
            message.innerHTML = firstMsg + "已输入<font color='red'>" + l + "</font>个字，还剩<font color='red'>" + b + "</font>字，最多<font color='red'>" + msgLength + "</font>个字，手机<font color='red'>" + phonelength + "</font>条";
            if (l > length) {
                alert("您输入的文字长度达到最多" + msgLength + "个字，请酌情删减！");
            }

        }
 

        //清除格式错误的手机号
        function clearErr() {
            if (document.getElementById("<%=mobile.ClientID%>").value == "")
                return;
            setTimeout("doFilter()", 200);

        }
        
        function checkPhone() {
            var arrData = new Array();
            var ObjM = document.getElementById("<%=mobile.ClientID%>");
            var mob = ObjM.value;
            if (mob.length < 9) {
                ObjM.value = "";
                document.getElementById("mobiletip").innerHTML = "共计号码：0个";
                return;
            }
            arrData = mob.match(/\d{11}/g);
            if (arrData == "" || arrData == null) {
                ObjM.value = "";
                document.getElementById("mobiletip").innerHTML = "共计号码：0个";
                return;
            }

            if (arrData.length > 0) {
                var t = 0, m = 0, e = 0;
                for (var i = 0; i < arrData.length; i++) {
                    if (isTel(arrData[i])) {
                        t++;
                    }
                    else if (isMobile(arrData[i])) {
                        m++;
                    }
                    else {
                        e++;
                    }
                }

                re = /(\,)+/g;
                document.getElementById("telephonenumber").value = t;
                document.getElementById("mobilenumber").value = m + e;
                document.getElementById("countnumber").value = m + t + e;

                var content = document.getElementById("<%=content.ClientID%>").value;
                var totalcount = mobileallcount(content.length, smslong, (m + e));
                totalcount += telallcount(content.length, t);

                var mobiles = arrData.join(',');//转换为字符串
                mobiles = mobiles.replace(re, ",");
                document.getElementById("<%=mobile.ClientID%>").value = mobiles;
                // alert("共计号码："+(m+t+e)+"个");
                document.getElementById("mobiletip").innerHTML = "共计号码：" + (m + t + e) + "个,计费条数：" + totalcount + "条";

            }
        }

        function isTel(value) {
            if (/^0(([1-2]\d)|([3-9]\d{2}))\d{8}$/g.test(value)) {
                return true;
            }
            else {
                return false;
            }
        }

        function isMobile(value) {
            if (/^13\d{9}$/g.test(value) || (/^14\d{9}$/g.test(value)) || (/^15\d{9}$/g.test(value)) || (/^16\d{9}$/g.test(value)) || (/^18\d{9}$/g.test(value))) {
                return true;
            }
            else {
                return false;
            }
        }

        function CountNum() {
            if (document.getElementById("<%=mobile.ClientID%>").value == "")
                return;
            setTimeout("doCount()", 200);

        }

        function doCount() {
            var arrData = new Array();
            var ObjM = document.getElementById("<%=mobile.ClientID%>");
            var mob = ObjM.value;

            arrData = mob.match(/\d+/g);
            if (arrData == "" || arrData == null) {
                ObjM.value = "";
                document.getElementById("mobiletip").innerHTML = "共计号码：0个";
                return;
            }

            if (arrData.length > 0) {
                var j = 0, u = 0, t = 0, m = 0, n = 0;

                var re = /(\,)+/g;

                var content = document.getElementById("<%=content.ClientID%>").value;
                var totalcount = mobileallcount(content.length, smslong, (arrData.length));
                //totalcount += telallcount(content.length, t);

                var mobiles = arrData.join(',');//转换为字符串
                mobiles = mobiles.replace(re, ",");
                document.getElementById("<%=mobile.ClientID%>").value = mobiles;
                document.getElementById("telephonenumber").value = 0;
                document.getElementById("mobilenumber").value = arrData.length;
                document.getElementById("mobiletip").innerHTML = "共计号码：" + (arrData.length) + "个,计费条数：" + totalcount + "条";

            }

        }

        function doFilter() {
            var arrData = new Array();
            var ObjM = document.getElementById("<%=mobile.ClientID%>");
            var mob = ObjM.value;
            if (mob.length < 9) {
                ObjM.value = "";
                document.getElementById("mobiletip").innerHTML = "共计号码：0个";
                return;
            }
            arrData = mob.match(/\d+/g);
            if (arrData == "" || arrData == null) {
                ObjM.value = "";
                document.getElementById("mobiletip").innerHTML = "共计号码：0个";
                return;
            }

            if (arrData.length > 0) {
                var j = 0, u = 0, t = 0, m = 0, n = 0;
                var arrDatam = new Array();
                for (var i = 0; i < arrData.length; i++) {
                    if (isTel(arrData[i])) {

                        t++;
                        arrDatam[n] = arrData[i];
                        n++;
                    }
                    else if (isMobile(arrData[i])) {

                        m++;
                        arrDatam[n] = arrData[i];
                        n++;
                    }
                }

                re = /(\,)+/g;

                var content = document.getElementById("<%=content.ClientID%>").value;
                var totalcount = mobileallcount(content.length, smslong, (m));
                totalcount += telallcount(content.length, t);

                var mobiles = arrDatam.join(',');//转换为字符串
                mobiles = mobiles.replace(re, ",");
                document.getElementById("<%=mobile.ClientID%>").value = mobiles;
                document.getElementById("telephonenumber").value = t;
                document.getElementById("mobilenumber").value = m;
                document.getElementById("mobiletip").innerHTML = "共计号码：" + (n) + "个,计费条数：" + totalcount + "条";
            }

        }


        function telallcount(contentLength, telNum) {

            contentLength = contentLength + document.getElementById("<%=title.ClientID%>").value.length;

            var totalcount = telNum;
            if (contentLength > 60 && contentLength <= (60 + 54)) {
                totalcount = telNum * 2;
            }
            else if (contentLength > (60 + 54) && contentLength <= (60 + 54 * 2)) {
                totalcount = telNum * 3;
            }
            else if (contentLength > (60 + 54 * 2) && contentLength <= (60 + 54 * 3)) {
                totalcount = telNum * 4;
            }
            else if (contentLength > (60 + 54 * 3)) {
                totalcount = telNum * 5;
            }
            return totalcount;
        }

        function mobileallcount(contentLength, firstLength, phonelength) {

            contentLength = contentLength + document.getElementById("<%=title.ClientID%>").value.length;

            var totalcount = phonelength;

            if (contentLength <= firstLength) {
                totalcount = phonelength;
            } else {
                totalcount = phonelength * Math.ceil(contentLength / longsms_long);
            }
            return totalcount;
        }

</script> 
</head>

<body>
<form runat="server" id="clientForm">
<div class="roundedRectangle">  
    <strong><p>发短信</p></strong>  
</div> 
<div class="content-box role">

	<div class="content-box-content">
		<div class="tab-content default-tab" id="form">
               
                <table cellspacing="0" cellpadding="0" style="font-size:13px;" width="100%" border="0">
                    <tr>
                    <td width="10%" align="right" valign="top" class="left" style="line-height:30px;padding-top:30px;">
                       <div style="line-height:30px;margin-bottom:60px;"> 短信内容：</div></td>
                    <td style="line-height:40px;padding-top:6px;" align="left" class="auto-style1">
                          <asp:TextBox ID="content" Rows="5"  style="border:1px solid #6E9FDE;width:420px;height:85px;"  runat="server" TextMode="MultiLine" onKeyPress="checkContentLength('content',300,'message')" onKeyUp="keydown();" onblur="paste();" onchange="paste();" ></asp:TextBox>
                        
                        <div style="line-height:20px;clear:both;margin-top:5px;width:380px;"><span>使用签名：</span><asp:TextBox ID="title" runat="server"></asp:TextBox></div>
                        <div style="line-height:20px;clear:both;margin-top:5px;width:380px;"><span id="message">已输入<font color="red">0</font>个字，还剩<font color="red">300</font>字，最多<font color="red">300</font>个字</span></div>
                        </td>
					<td rowspan="10" valign="top" width="420" style="border-left:#E6E6E6 solid 1px;line-height:25px;color:#FF6600; padding-left:8px;padding-top:0px;">
					<strong>注意：</strong>
					<br />
					1、单次发送最好不超过20个号码。 <br />
					2、手动输入号码请用英文逗号","分隔。 <br />
					3、内容编辑完成请先检查敏感词再发送，否则会被封号。 <br />				
                    4、汉字、数字、英文和标点符号都表示1个长度。<br/>
                    5、一条短信内容长度为64个字，超过64字则算为两条。<br />                  
                    6、短信内容实际长度=短信签名+短信内容。<br />                  
                    
                    </td>
                    </tr>
                    
                     <tr>
						<td height="28" style="line-height:28px;"  width="10%" align="right" class="left">
							手机号码：						</td>
						<td valign="top" style="padding-top:8px;">
                <div id="mobilelibPanel" style="border:1px solid #6E9FDE;width:380px; margin-bottom:5px; display:none;"></div>
                              <asp:TextBox ID="mobile" Rows="6"  style="border:1px solid #6E9FDE;width:420px;height:85px;" onblur="CountNum();"  runat="server" TextMode="MultiLine" ></asp:TextBox>						    
						    <span id="showerrmobile" style="display:none;"><input name="error" id="error" value="" /><input name="telephone" id="telephone" value="" /><input name="union" id="union" value="" /><input name="countnumber" id="countnumber" value="" /><input name="mobilenumber" id="mobilenumber" value="" /><input name="telephonenumber" id="telephonenumber" value="" /><input name="phone" id="phone" value="" /><input name="cdma" id="cdma" value="" /></span> 
                            <div id="mobiletip" style="color:red;margin-left:7px;line-height:24px;height:24px;margin-top:0;margin-bottom:0;">共计号码：0 个
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                             <div style="margin-top:0;margin-bottom:0;"><input name="chooseKmobile" id="chooseKmobile" class="button" style="margin-bottom:3px;margin-left:5px;" onclick="showMessageBox(800, 400, 'ChooseN.aspx', '企业通讯录');" type="button" title="企业通讯录" value="企业通讯录" />
                            <input name="chooseNmobile" id="chooseNmobile" class="button" style="margin-bottom:3px;margin-left:5px;" onclick="showMessageBox(800, 400, 'ChooseK.aspx', '内部通讯录');" type="button" title="内部通讯录" value="内部通讯录" />
                                 <input name="checkmobile" id="checkmobile" class="button" style="margin-bottom:3px;margin-left:5px;" onclick="clearErr()" type="button" title="过滤错误号码" value="过滤错号" />
                                 <asp:Button ID="btnAdd" class="button" CssClass ="button" style="margin-bottom:3px;margin-left:5px;" runat="server" Text="发送短信" OnClick="btnAdd_Click" />
                             </div>
						</td>
					</tr>
                    
                </table>
               
		</div>
	</div>
</div>
</form>
</body>
</html>
