<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EidtPwd.aspx.cs" Inherits="Base_CommonPage_EidtPwd" %>

<!DOCTYPE html>
<html>
<head id="Head2" runat="server">
    <title>Pass</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link href="../../CSS/UpdPwd.css" rel="stylesheet" />
    <script src="../../Scripts/jquery1.11.3.js" rel="stylesheet" ></script>
    <script src="../../Scripts/js/FormValidate.js" type="text/javascript"></script>  
    <script type="text/javascript" language="javascript">
        function fnParentSave() {
            if (CheckValue("loginform")) {
                var newPwd = $('#NewPwd').val();
                var newPwdTo = $('#NewPwdAgain').val();
                if (newPwd == newPwdTo) {
                    $.ajax({
                        type: "POST",
                        dataType: "json",
                        data: { userId: $('#loginId').val(), oldPwd: $('#OldPwd').val(), newPwd: $('#NewPwd').val() },
                        url: "EidtPwd.aspx?typeValue=Updpass",
                        success: function (obj) {                          
                            if (obj.success || obj.success == "true") {                               
                                alert('修改成功，请退出重新登录！');
                            } else {
                                alert('修改失败，原密码不正确！');
                            }
                        }
                    });
                }
                else {
                    $.messager.alert('系统提示', '两次输入的新密码不一致！', 'warning');
                    clearTxt();
                    return false;
                }

            }

        }

        function clearTxt() {
            $('#OldPwd').val("");
            $('#NewPwd').val("");
            $('#NewPwdAgain').val("");
        }

    </script>

</head>
<body>
    <div style="width:550px;margin-right:90px; margin-top:0px;" class="theme-popbod dform">
           <form class="theme-signin" id="loginform" name="loginform" action="" method="post">
                 <ol>
                    <li><strong>用户名：</strong><input class="ipt" type="text" id="loginId" readonly="readonly" name="loginId" value="<%=UserID %>" size="30" /></li>
                    <li><strong>原密码：</strong><input class="ipt" type="password" name="OldPwd" id="OldPwd" minput="true" fieldtitle="原密码" size="30" /><span style=" color:red; font-size:12px;">*原密码</span></li>
                    <li><strong>新密码：</strong><input class="ipt" fieldtype="username" minput="true" fieldtitle="新密码" type="password" name="NewPwd" id="NewPwd" size="30" /><span style=" color:red; font-size:12px;">*输入新密码!</span></li>
                    <li><strong>密码确认：</strong><input class="ipt" fieldtype="username" minput="true" fieldtitle="密码确认" type="password" name="NewPwdAgain" id="NewPwdAgain" size="30" /><span style=" color:red; font-size:12px;">*再次确认新密码!</span></li>
                    <li><input class="btn btn-primary" onclick="fnParentSave();"  type="button" name="submit" id="submit" value=" 保  存 " />
                        &nbsp;&nbsp;&nbsp;
                        <input class="btn btn-primary" onclick="clearTxt();"  type="button" name="cancel" id="cancel" value=" 清  空 " />
                    </li>
                </ol>
           </form>
     </div> 
</body>
</html>