//<!--
//		ASP TreeView
//	
//		Copyright obout inc      http://www.obout.com

var ob_tree_js_version="2.0.10",ob_tb2,ob_last,ob_url2,ob_sids=null,ob_ivl="",ob_iar=0,ob_op2,tree_selected_id,ob_prev_selected,tree_parent_id,tree_selected_path,ob_xmlhttp2,ob_alert2; /*@cc_on @*//*@if (@_jscript_version >= 5);try{ob_xmlhttp2=new ActiveXObject("Msxml2.XMLHTTP")}catch(e){try {ob_xmlhttp2=new ActiveXObject("Microsoft.XMLHTTP")}catch(E){}};@else;ob_xmlhttp2=false;ob_alert2=true;@end @*/if (!ob_xmlhttp2 && !ob_alert2){try {ob_xmlhttp2=new XMLHttpRequest();}catch(e){}}function ob_rsc() {if (ob_xmlhttp2.readyState==4){ob_tb2.className="none";ob_tb2.innerHTML=ob_xmlhttp2.responseText;}}function ob_t24(){if(ob_xmlhttp2){ob_xmlhttp2.open("GET",ob_url2,true);ob_xmlhttp2.onreadystatechange=ob_rsc;ob_xmlhttp2.send(null);}}function ob_t26(){ob_ivl=window.setInterval("ob_t28()",200);}function ob_t28(){if(ob_sids==null){window.clearInterval(ob_ivl);ob_t25(document.getElementById(ob_last));return;}if(ob_xmlhttp2.readyState==4 || ob_xmlhttp2.readyState==0){if(ob_iar==ob_sids.length){window.clearInterval(ob_ivl);ob_t25(document.getElementById(ob_last));return;}ob_t25(document.getElementById(ob_sids[ob_iar]));ob_iar=ob_iar+1;}}


function ob_t21(os, url) {
	// Switch plus-minus images when clicked.
	// If dynamic loading URL is supplied, call loading function.
	
	// ob_node_id is ID of exapanded/collapsed node.
	var ob_node_id = os.parentNode.parentNode.firstChild.nextSibling.nextSibling.id;
	
    var ot = os.parentNode.parentNode.parentNode.parentNode.nextSibling;
    var lensrc = (os.src.length - 8);
    var s = os.src.substr(lensrc, 8);
    if ((s == "inus.gif")||(s == "us_l.gif")) {
		
		// EVENT. Node expanded.
		
		if (s == "inus.gif") {
			ot.style.display = "none";
			os.src = ob_style + "/plusik.gif";
			}
		else {
			ot.style.display = "none";
			os.src = ob_style + "/plusik_l.gif";
		}
    }
    
    if ((s == "usik.gif")||(s == "ik_l.gif")) {
    
		// EVENT. Node collapsed.
    
		if (s == "usik.gif") {
			ot.style.display = "block";
			os.src = ob_style + "/minus.gif";
		}
		else {
			ot.style.display = "block";
			os.src = ob_style + "/minus_l.gif";
			if (url != "") {
			var s = os.parentNode.parentNode.parentNode.parentNode.nextSibling.firstChild.firstChild.firstChild.nextSibling.innerHTML;
				if (s != "Loading ...") {
					return;
				}
				ob_url2 = url;
				ob_tb2 = os.parentNode.parentNode.parentNode.parentNode.nextSibling.firstChild.firstChild.firstChild.nextSibling;
				window.setTimeout("ob_t24()", 100);
			}
		}
    }
	// select collapsed node if its child was previously selected
    if(tree_selected_path!=null){
		var s = os.src.substr((os.src.length - 8), 8);
		if ((s=="ik_l.gif")||(s=="usik.gif")){
			//if tree_selected_path contains collapsed node id.
			var e = os.parentNode.parentNode.firstChild.nextSibling.nextSibling;
			var a = tree_selected_path.split(",");
			for (i=0; i<a.length; i++) {
				if(a[i]==e.id){
					ob_t22(e);
					return;
				}
			}
		}
	}
	
	// EVENT. Node collapsed OR expanded.
}

function ob_t22(ob_od) {
	// Highlight selected node
	// Change class name and assign id to variable tree_selected_id
	
	if (ob_od.id == "") return;
	
	// EVENT. Before node is selected.

	// the previously selected node
	prevSelected = document.getElementById(tree_selected_id);
	// for edit mode only
	if ((prevSelected != null)&&typeof(ob_tree_editnode_enable) != 'undefined'&&(ob_tree_editnode_enable==true))
	{
		ob_attemptEndEditing(ob_od);
	}
	if(ob_prev_selected!=null)
	{
		ob_prev_selected.className = "ob_t2";
	}
	ob_prev_selected = ob_od;
	ob_od.className = "ob_t3";
		
	// for edit mode only
	if (typeof(ob_tree_editnode_enable) != 'undefined'&&ob_tree_editnode_enable==true){ob_attemptStartEditing(ob_od)};
	
	// store the current node id as the last selected node
	tree_selected_id = ob_od.id;	
    
    // Find path to selected and extend the path
    var sel_id;
	if(ob_od.parentNode.parentNode.parentNode.parentNode.parentNode.className=="ob_di2"){return};
	ob_od = ob_od.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.firstChild.firstChild.firstChild.firstChild.nextSibling.nextSibling;
	tree_parent_id = ob_od.id;
	tree_selected_path = tree_parent_id+","+tree_selected_id;
	ob_t20(ob_od);
	
	// EVENT. After node is selected.
}

function ob_t20(e) {
	// Extend all parents up and populate tree_selected_path
	
    var os = e.parentNode.firstChild.firstChild;
	//e
    if (os != null) {
		if ((typeof os != "undefined") && (os.tagName == "IMG")) {
			var lensrc = (os.src.length - 8);
			var s = os.src.substr(lensrc, 8);
			if ((s == "usik.gif") || (s == "ik_l.gif")) {
				os.onclick();
			}
			if(e.parentNode.parentNode.parentNode.parentNode.parentNode.className=="ob_di2"){return};
			e=e.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.firstChild.firstChild.firstChild.firstChild.nextSibling.nextSibling
			tree_selected_path=e.id+","+tree_selected_path;
			ob_t20(e);
		}
	}
}

function ob_t23(e){
	// To expand-collapse node by clicking on node text.
	// Find first image with plus-minus and call onclick()

    var os = e.parentNode.parentNode.firstChild.firstChild;

    if (os != null) {
		if ((typeof os != "undefined") && (os.tagName == "IMG")) {
			var lensrc = (os.src.length - 8);
			var s = os.src.substr(lensrc, 8);
			if ((s == "inus.gif") || (s == "usik.gif") || (s == "us_l.gif") || (s == "ik_l.gif")) {
				os.onclick();
			}
		}
		else {
			ob_t23(e.parentNode);
		}
	}
}

function ob_t25(ob_od) {
	// When tree is first loaded - Highlight and Extend selected node.

	if(ob_od==null) {
		//alert("Selected node does not exists or its id is not unique.");
		return
	};
	// Extend.
	var e, lensrc, s;
    e = ob_od.parentNode.firstChild.firstChild;
	if ((typeof e.src != "undefined") && (e.tagName == "IMG")) {
		s = e.src.substr((e.src.length - 8), 8);
		if ((s == "usik.gif") || (s == "ik_l.gif")) {
			e.onclick();
		}
	}
	// Highlight and populate path.
	ob_t22(ob_od);
}

function ob_tall(exp) {
	// To expand all nodes ob_tall(1)
	// To collapse all nodes ob_tall(0)
	
	var i, e, lensrc, s
	for (i=0; i<document.images.length; i++) {
		e = document.images[i];
		lensrc = (e.src.length - 8);
		s = e.src.substr(lensrc, 8);
		if (exp == 1) {
			if ((s == "usik.gif") || (s == "ik_l.gif")) {
				e.onclick();
			}
		}
		else
			if ((s == "inus.gif") || (s == "us_l.gif")) {
				e.onclick();
			}
	}
}

function ob_t2c(e) {
	// To check/uncheck checkboxes in children.
	
	var ob_t2in=e.parentNode.parentNode.parentNode.parentNode.nextSibling.getElementsByTagName("input");
	for (var i=0; i<ob_t2in.length; i++) {
		if (e.checked==true) {
			ob_t2in[i].checked=true;
		}
		else {
			ob_t2in[i].checked=false;
		}
	}
}


function ob_t2send() {
	// Make string with checked checkboxes IDs.
	
	var ob_t2in,ob_t2s,ob_t2send="";
	ob_t2in=document.getElementsByTagName("input");
	for (var i=0; i<ob_t2in.length; i++) {
		ob_t2s=ob_t2in[i].id;
		if ((ob_t2s.substr(0,4)=="chk_") && (ob_t2in[i].checked)) {
			ob_t2send=ob_t2send+ob_t2s+",";
		}
	}
	document.getElementById('txtSend').value=ob_t2send;
}

function ob_hasChildren(ob_od)
{
	try
	{
		return (ob_od.parentNode.parentNode.parentNode.parentNode.tagName.toLowerCase() == 'div' && ob_od.parentNode.parentNode.parentNode.parentNode.className.toLowerCase() == 'ob_t2b');
	}
	catch (e)
	{
		return false;
	}
}

function ob_isExpanded(ob_od)
{
	try
	{
		imgSrc = ob_od.parentNode.firstChild.firstChild.src;
		lenSrc = imgSrc.length - 8;
		imgSrcLast = imgSrc.substr(lenSrc, 8);
		return (imgSrcLast == 'inus.gif' || imgSrcLast == 'us_l.gif');
	}
	catch (e)
	{
		return false;
	}
}

function ob_getParentOfNode (ob_od)
{
	try
	{
		if(ob_od.parentNode.parentNode.parentNode.parentNode.parentNode.className=="ob_di2") { return null }
		else return ob_od.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.firstChild.firstChild.firstChild.firstChild.nextSibling.nextSibling;
	}
	catch (e)
	{
		return null;
	}
}

function ob_getLastChildOfNode (ob_od)
{
	try
	{
		if (ob_hasChildren(ob_od))
		{
			lastChild = ob_od.parentNode.parentNode.parentNode.parentNode.firstChild.nextSibling.firstChild.firstChild.firstChild.nextSibling.lastChild.firstChild.firstChild.firstChild.firstChild.nextSibling.nextSibling;
		}
		
		return lastChild;
	}
	catch (e)
	{
		return null;
	}
}

function ob_getFirstChildOfNode (ob_od)
{
	try
	{
		if (ob_hasChildren(ob_od))
		{
			firstChild = ob_od.parentNode.parentNode.parentNode.parentNode.firstChild.nextSibling.firstChild.firstChild.firstChild.nextSibling.firstChild.firstChild.firstChild.firstChild.firstChild.nextSibling.nextSibling;
		}
		
		return firstChild;
	}
	catch (e)
	{
		return null;
	}
}

function ob_getNextSiblingOfNode (ob_od)
{
	try
	{
		nxtSibling = ob_od.parentNode.parentNode.parentNode.parentNode.nextSibling;
		if (nxtSibling != null)
		{
			nxtSibling = nxtSibling.firstChild.firstChild.firstChild.firstChild.nextSibling.nextSibling;
		}
		
		return nxtSibling;
	}
	catch (e)
	{
		return null;
	}
}

function ob_getPrevSiblingOfNode (ob_od)
{
	try
	{
		prvSibling = ob_od.parentNode.parentNode.parentNode.parentNode.previousSibling;
		if (prvSibling != null)
		{
			prvSibling = prvSibling.firstChild.firstChild.firstChild.firstChild.nextSibling.nextSibling;
		}
		
		return prvSibling;
	}
	catch (e)
	{
		return null;
	}
}

function ob_getFurthestChildOfNode (ob_od)
{
	fChild = ob_od;

	while (true)
	{
		if (ob_hasChildren(fChild) && ob_isExpanded(fChild))
		{
			tmpChild = ob_getLastChildOfNode(fChild);
			if (tmpChild == null) break;
			else fChild = tmpChild;
		}
		else break;
	}
	
	return fChild;
}

function ob_elementBelongsToTree (ob_od)
{
	try
	{
		if (ob_od.tagName.toLowerCase() == "body") return false;
		if (ob_od.className.toLowerCase() == "ob_tree") return true;

		while (ob_od.parentNode.tagName.toLowerCase() != "body")
		{
			if (ob_od.parentNode.className.toLowerCase() == "ob_tree") return true;
			ob_od = ob_od.parentNode;
		}
		
		return false;
	}
	catch (e)
	{
		return false;
	}
}

//-->