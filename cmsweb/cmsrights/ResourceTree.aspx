<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ResourceTree.aspx.vb" Inherits="cmsrights_ResourceTree" %>
<%@ Import Namespace="Unionsoft.Cms.Web"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
	<LINK href="../css/cmsstyle.css" type="text/css" rel="stylesheet" /> 
    <link type="text/css" rel="stylesheet" href="../css/sys-menu2.css" /> 
    <script src="../js/MzTreeView10.js"></script> 
    <script src="../js/menu4.js" type="text/javascript"></script>
    <script src="../js/poslib.js" type="text/javascript"></script>
    <script src="../js/scrollbutton.js" type="text/javascript"></script>
     
</head>
<body>
   <form id="form1" runat="server">
    <div style="height:30px;margin-left:10px;"><asp:Label ID="lblName" runat="server"></asp:Label></div>
    <div id="treeviewarea" style="margin-left:10px;"></div>
     
    </form>
    
    <script language="javascript">
    <!--
    var tree = new MzTreeView("tree");
    tree.icons["Empty"] = "res_empty.gif";
    tree.icons["TWOD"] = "res_twod.gif";
    tree.icons["DOC"] = "res_doc.gif";
    tree.icons["TWOD_jc"] = "res_twod_jc.gif";
    tree.icons["DOC_jc"] = "res_doc_jc.gif";
    tree.icons["Flow"] = "res_workflow.gif";  
    tree.icons["VIEW"] = "view.jpg";  
     tree.icons["RESOURCE"] = "enterprise.gif"
    
    tree.setIconPath("../images/tree1/");
    
    <%=strTree %>
    
    document.getElementById('treeviewarea').innerHTML = tree.toString();
    
    function expand2lChieldren()
    {
        try{
            var root = tree.node["1"];
            if(root.hasChild)
            {
                for (var k=0; k<root.childNodes.length; k++) tree.expand(root.childNodes[k].id,true);
            }
        }
        catch(e){}
    }
    //setTimeout("expand2lChieldren();",1000);
    //-->
    
    </script> 
</body>
</html>
