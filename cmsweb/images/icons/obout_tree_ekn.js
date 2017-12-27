//		Copyright obout inc      http://www.obout.com
//	
//		obout_ASP_TreeView_2      version 2.0.9 

// string containing previous node content
var prevNodeContent;
var tree_edit_id = "";
var tree_node_editing = false;

document.onkeydown = function(e){ob_nodeKeyDown(e)};
if (document.layers) try {document.registerEvents(Event.KEYDOWN)} catch (e) {};

var inited = false;

function ob_nodeKeyDown(e)
{
	if (!e) e = window.event;
	
	if (tree_node_editing) return;
	if (document.all)
	{
		e.cancelBubble = true;
		e.returnValue = false;
		if (e.stopPropagation) e.stopPropagation();
	}
	
	// get the selected node
	currentlySelected = document.getElementById(tree_selected_id);
	
	if (currentlySelected != null)
	{
		// a small little artifice to make the page not scroll in Mozilla
		if (!inited)
		{
			tmptxt = document.createElement("input");
			currentlySelected.appendChild(tmptxt);
			tmptxt.focus();
			currentlySelected.removeChild(tmptxt);
			inited = true;

			e.cancelBubble = true;
			e.returnValue = false;
			if (e.stopPropagation) e.stopPropagation();
		}

		if (typeof(ob_tree_keynav_enable) != 'undefined' && ob_tree_keynav_enable)
		{
			// arrow up
			if (e.keyCode == 38)
			{
				prevSibling = ob_getPrevSibling(currentlySelected);
				if (prevSibling != null) ob_t22(prevSibling);
			}
			// arrow down
			else if (e.keyCode == 40)
			{
				nxtSibling = ob_getNextSibling (currentlySelected);
				if (nxtSibling != null) ob_t22(nxtSibling);
			}
			// arrow left
			else if (e.keyCode == 37)
			{
				// if the node has children and is expanded, we collapse it
				if (ob_hasChildren(currentlySelected) && ob_isExpanded(currentlySelected))
				{
					currentlySelected.parentNode.firstChild.firstChild.onclick();
				}
				// otherwise we select the parent node
				else
				{
					parentNode = ob_getParentOfNode(currentlySelected);
					if (parentNode != null) ob_t22 (parentNode);
				}
			}
			// arrow right
			else if (e.keyCode == 39)
			{
				// if the node has children and is collapsed, we expand it
				if (ob_hasChildren(currentlySelected))
				{
					// if the node is colapsed, we expand it
					if(!ob_isExpanded(currentlySelected))
					{
						currentlySelected.parentNode.firstChild.firstChild.onclick();
					}
					// otherwise we select the first child node
					else
					{
						firstChild = ob_getFirstChildOfNode(currentlySelected);
						if (firstChild != null) ob_t22 (firstChild);
					}
				}
			}
		}
		if (typeof(ob_tree_editnode_enable) != 'undefined' && ob_tree_editnode_enable)
		{
			// enter or f2 (enter edit mode)
			if (e.keyCode == 13 || e.keyCode == 113)
			{
				// call the selection function again
				ob_t22 (currentlySelected);
			}
		}
	}
}

function ob_textBoxKeyDown(e)
{
	if (!e) e = window.event;

	// get the selected node
	currentlySelected = document.getElementById(tree_selected_id);
	if (currentlySelected != null)
	{
		// if enter was pressed we need to remove the textbox (exit edit mode)
		if (e.keyCode == 13)
		{
			if (currentlySelected.childNodes.length > 0)
			{
				if (currentlySelected.childNodes[0].nodeName.toLowerCase() == "input")
				{
					var name = currentlySelected.childNodes[0].value;
					
					if (name.length == 0 || name.indexOf(':') != -1 || name.indexOf('|') != -1 || name.indexOf(',') != -1 || name.indexOf('<') != -1 || name.indexOf('>') != -1)
					{
						// detach the blur event since upon showing the alert dialog, the blur event will be fired
						currentlySelected.childNodes[0].onblur = null;
						
						// the node name can't be empty or contain invalid characters
						alert('The node name cannot be empty\nand\nIt cannot contain the following characters : | , < >');
						// put the initial name
						currentlySelected.childNodes[0].value = prevNodeContent;
						// focus the textbox
						currentlySelected.childNodes[0].focus();
						// and select the text
						try
						{
							oRange = currentlySelected.childNodes[0].ownerDocument.selection.createRange().duplicate();
							oRange.moveStart("textedit", -1)
							oRange.moveEnd("textedit");
							oRange.select();
						}
						catch (e) {}
						
						// reattach blur event
						currentlySelected.childNodes[0].onblur = function(){ob_textBoxExit(true)};
						
						// mark that we're editing a tree node
						tree_node_editing = true;
					}
					else
					{
						currentlySelected.removeChild(prevSelected.childNodes[0]);
						currentlySelected.innerHTML = name;
						currentlySelected.className = "ob_t3";

						// if it's different from the initial text, add it to the string containing the modifications
						if (name != prevNodeContent) tree_edit_id += tree_selected_id + ":" + name + "|";
						// mark that we're not editing any tree node
						tree_node_editing = false;
					}
				}
			}

			// stop the propagation of the event (prevent postback and who knows what other events :P)
			e.cancelBubble = true;
			e.returnValue = false;
			if (e.stopPropagation) e.stopPropagation();
		}
		// if escape was pressed we need to remove the textbox (exit edit mode) and restore initial value
		if (e.keyCode == 27)
		{
			ob_textBoxExit(false);
		}
		else
		{
			//alert(e.keyCode);
		}
	}
}

function ob_textBoxExit(keep)
{
	// get the selected node
	currentlySelected = document.getElementById(tree_selected_id);
	if (currentlySelected != null)
	{
		if (currentlySelected.childNodes.length > 0)
		{
			if (currentlySelected.childNodes[0].nodeName.toLowerCase() == "input")
			{
				var name = currentlySelected.childNodes[0].value;
				currentlySelected.removeChild(prevSelected.childNodes[0]);
				currentlySelected.innerHTML = keep ? name : prevNodeContent;;
				currentlySelected.className = "ob_t3";
				
				if (keep && (name != prevNodeContent)) tree_edit_id += tree_selected_id + ":" + name + "|";
				
				// mark that we're not editing any tree node
				tree_node_editing = false;
			}
		}		
	}
}

function ob_getPrevSibling (ob_od)
{
	prevSibling = ob_getPrevSiblingOfNode(ob_od);
	if (prevSibling != null) 
	{
		prevSibling = ob_getFurthestChildOfNode(prevSibling);
		return prevSibling;
	}
	else
	{
		nodeParent = ob_getParentOfNode(ob_od);
		if (nodeParent != null)
		{
			return nodeParent;
		}
	}
	return null;
}

function ob_getNextSibling (ob_od)
{	
	if (ob_hasChildren(ob_od) && ob_isExpanded(ob_od))
	{
		nxtSibling = ob_getFirstChildOfNode(ob_od);
		return nxtSibling;
	}
	else
	{
		nxtSibling = ob_getNextSiblingOfNode(ob_od);
		if (nxtSibling != null) return nxtSibling;
		
		nodeParent = ob_od;
		do
		{
			nodeParent = ob_getParentOfNode(nodeParent);
			if (nodeParent != null)
			{
				nxtSibling = ob_getNextSiblingOfNode(nodeParent);
				if (nxtSibling != null) return nxtSibling;
			}
		} while (nodeParent != null && nodeParent.className.toLowerCase() != "ob_tree")
	}
	
	return null;
}

function ob_attemptStartEditing(ob_od)
{
	if (typeof(tree_node_editing) != 'undefined' && typeof(ob_tree_editnode_enable) != 'undefined' && ob_tree_editnode_enable)
	{	
		// if the current node was clicked again
		if (ob_od.id == tree_selected_id)
		{
			// if it has children
			if (ob_od.childNodes.length > 0)
			{
				// if it's text (not a textbox)
				if (ob_od.childNodes[0].nodeName.toLowerCase() == "#text")
				{
					// get the current node text
					prevNodeContent = ob_od.childNodes[0].nodeValue;
					
					// create the textbox
					var textBox = document.createElement("input");

					textBox.setAttribute("type", "text");
					textBox.setAttribute("value", prevNodeContent);
					textBox.id = ob_od.id + "_txtBox";
					textBox.style.borderWidth = 0;
					textBox.style.width = ob_od.offsetWidth+30;
					textBox.style.backgroundColor = "transparent";
					textBox.className = ob_od.className;

					// remove the text currently inside the node
					ob_od.removeChild(ob_od.childNodes[0]);
					// add the textbox
					ob_od.appendChild(textBox);
					// wire the onkeydown event
					textBox.onkeydown = function(e){ob_textBoxKeyDown(e)};
					// wire the onblur event
					textBox.onblur = function(){ob_textBoxExit(true)};
					// focus the textbox
					textBox.focus();

					// select the text currently in the textbox
					try
					{
						oRange = textBox.ownerDocument.selection.createRange().duplicate();
						oRange.moveStart("textedit", -1)
						oRange.moveEnd("textedit");
						if (oRange.htmlText.toLowerCase().indexOf('body') == -1) oRange.select();
						else textBox.focus();
					}
					catch (e) {}

					// mark that we're editing a tree node
					tree_node_editing = true;
				}
			}
		}
	}
}

function ob_attemptEndEditing(ob_od)
{
	if (typeof(tree_node_editing) != 'undefined'  && typeof(ob_tree_editnode_enable) != 'undefined' && ob_tree_editnode_enable)
	{	
		// if it has children
		if (prevSelected.childNodes.length > 0)
		{
			// if the child is a textbox (was in edit mode)
			if (prevSelected.childNodes[0].nodeName.toLowerCase() == "input")
			{
				// if it's different than the curent node (was a click in the textbox or in another node)
				if (ob_od.id != tree_selected_id)
				{
					// get the current text in the textbox
					var name = prevSelected.childNodes[0].value;
					// the node name can't be empty or contain invalid characters
					if (name.length == 0 || name.indexOf(':') != -1 || name.indexOf('|') != -1 || name.indexOf(',') != -1 || name.indexOf('<') != -1 || name.indexOf('>') != -1)
					{
						alert('The node name cannot be empty\nand\nIt cannot contain the following characters : | , < >');
						// put the initial name
						prevSelected.childNodes[0].value = prevNodeContent;
						// focus the textbox
						prevSelected.childNodes[0].focus();
						// and select the text
						try
						{
							oRange = prevSelected.childNodes[0].ownerDocument.selection.createRange().duplicate();
							oRange.moveStart("textedit", -1)
							oRange.moveEnd("textedit");
							oRange.select();
						}
						catch(e) {}
						
						// mark that we're editing a tree node
						tree_node_editing = true;
						return;
					}
					// delete the textbox
					prevSelected.removeChild(prevSelected.childNodes[0]);
					// write the content to the node as text
					prevSelected.innerHTML = name;
					// if it's different from the initial text, add it to the string containing the modifications
					if (name != prevNodeContent) tree_edit_id += tree_selected_id + ":" + name + "|";
					// mark that we're not editing any tree node
					tree_node_editing = false;
				}
			}
		}
	}
}