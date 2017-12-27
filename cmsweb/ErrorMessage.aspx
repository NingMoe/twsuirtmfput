<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ErrorMessage.aspx.vb" Inherits="ErrorMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<style>
body {font-family:Verdana;FONT-SIZE: 12px;MARGIN: 0;color: #000000;background: #F7F7F7;}
#messagebox {
    margin-top: 150px;
    padding: 1px;
    width: 500px;
    border: solid 1px #739ACE;
    background: #faf8ed;
}
#messagebox-title {
    color: #fff;
    text-align: left;
    font-weight: bold;
    background-color: #739ACE;
    padding: 5px;
}
#messagebox-content {
    color: #227fb7;
    background-color: #fff;
    padding: 10px;
    height:80px;
    padding-bottom: 20px;
    text-align: left;
}
#messagebox-bottom {
    color: #fff;
    background-color: #CED4E8;
    padding: 3px;
    text-align: center;
}

</style>
<title>提 示 信 息</title>
</head>

<body>
<form id="form1" runat=server>
<div align="center">
<div id="messagebox">
  <div id="messagebox-title" align="center">提示信息</div>
  <div id="messagebox-content">
    <span id="spanMessage" runat=server></span>
    
  </div>
  <div id="messagebox-bottom"><a href="javascript: window.history.back();">返回</a></div>
</div>
</div>
</form>
</body>
</html>
