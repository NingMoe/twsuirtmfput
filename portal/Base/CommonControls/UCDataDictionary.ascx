<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCDataDictionary.ascx.cs" Inherits="Base_CommonControls_UCDataDictionary" %> 

<img src="../../images/shu.gif"  onclick="openDictionary('../CommonDictionary/DataDictionary.aspx?ResourceID=<%=ResourceID %>&ResourceColumn=<%=Server.UrlEncode(ResourceColumn) %>&ChildIndex=<%=ChildIndex %>&IsMultiselect=<%=IsMultiselect %>&IsAppend=<%=IsAppend %>',<%=DicWidth %>,<%=DicHeight %>,'数据字典');" />
 