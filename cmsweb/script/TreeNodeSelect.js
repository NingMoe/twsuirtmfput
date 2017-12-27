//oncheck事件
function tree_oncheck(tree)
{
 var node=tree.getTreeNode(tree.clickedNodeIndex);
 var Pchecked=tree.getTreeNode(tree.clickedNodeIndex).getAttribute("checked");
 setcheck(node,Pchecked);
 setpnodecheck(node);
 setpnodenocheck(node);
}
//设置子节点选中
function setcheck(node,isCheck)
{
node.setAttribute("Expanded",true);
 var i;
 var ChildNode=new Array();
 ChildNode=node.getChildren();
 if(parseInt(ChildNode.length)==0)
  return;
 else
 {
  for(i=0;i<ChildNode.length;i++)
  {
   var cNode;
   cNode=ChildNode[i];
   if(parseInt(cNode.getChildren().length)!=0)
    setcheck(cNode,isCheck);
   cNode.setAttribute("Checked",isCheck);
  }
 }
}
//设置父节点选定
function setpnodecheck(node)
{
	var pnode = node.getParent();
	if(pnode!=null)
	{
		pnode.setAttribute("Checked",true);
		setpnodecheck(pnode,true);
	}
}
//设置父节点不选定
function setpnodenocheck(node)
{
	var pnode = node.getParent();
	if(pnode!=null)
	{
		var childNodes=pnode.getChildren();
		var childCount=childNodes.length;
		if(childNodes.length>0)
		{
			for(var i=0;i<childCount;i++)
			{
				if(childNodes[i].getAttribute("checked"))
				{
					return;
				}
			}
		}
		pnode.setAttribute("checked",false);
		setpnodenocheck(pnode);
	}
}
//设置选取的人员
function ReturnSelectedValue(node) {
	window.opener.document.all(GetQueryvalue("ctlName")).value = GetCheckedNode(node);
	window.close();
}
function GetCheckedNode(treeView)
{
    //var treeView=document.getElementById('tvwDeptEmp');
    var children=treeView.getChildren();
    var checkedIds=GetCheckedNodeIds(children[0]);
    return checkedIds.substr(1,checkedIds.length);
}
        
function GetCheckedNodeIds(node)
{
    var ids='';
    if(node.getAttribute("checked")==true&&node.getAttribute("NodeData")=="emp")
    {
        ids+=','+node.getAttribute("Text");
    }
    var childNodes=node.getChildren();
    var childCount=childNodes.length;
    if(childNodes.length>0)
    {
        for(var i=0;i<childCount;i++)
        {
            ids+=GetCheckedNodeIds(childNodes[i]);
        }
    }
    return ids;
}
function GetQueryvalue(panStr) 
{ 
	var sorStr = document.location.search;
	var vStr=""; 
	panStr = panStr.toLowerCase();
	if (sorStr==null || sorStr=="" || panStr==null || panStr=="") return vStr; 
		sorStr = sorStr.toLowerCase(); 
		panStr += "="; 
		var itmp=sorStr.indexOf(panStr); 
	if (itmp<0){return vStr;} 
	sorStr = sorStr.substr(itmp + panStr.length); 
	itmp=sorStr.indexOf("&"); 
	if (itmp<0) 
	{ 
		return sorStr;
	} 
	else 
	{ 
		sorStr=sorStr.substr(0,itmp); 
		return sorStr; 
	} 
} 