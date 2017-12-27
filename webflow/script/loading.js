document.write('<div id="loader_container" style="text-align:center; position:absolute; top:40%; width:100%; left: 0;"><div id="loader" style="font-family:Tahoma, Helvetica, sans; font-size:11.5px; color:#000000; background-color:#FFFFFF; padding:10px 0 16px 0; margin:0 auto; display:block; width:230px; border:1px solid #5a667b; text-align:left; z-index:2;"><div align="center">页面正在用加载中……请稍候</div><div id="loader_bg" style="background-color:#e4e7eb; position:relative; top:8px; left:8px; height:7px; width:213px; font-size:1px; "><div id="progress" style="height:5px; font-size:1px; width:1px; position:relative; top:1px; left:0px; background-color:#77A9E0;"> </div></div></div></div>'); 
var t_id = setInterval(animate,20);
var pos=0; var dir=2; var len=0; 
function animate() {
	var elem = document.getElementById('progress'); 
	if(elem != null) { 
		if (pos==0) len += dir; 
		if (len>32 || pos>179) pos += dir; 
		if (pos>179) len -= dir; 
		if (pos>179 && len==0) pos=0; 
		elem.style.left = pos; 
		elem.style.width = len; 
	} 
} 
function remove_loading() { 
	this.clearInterval(t_id); 
	var targelem = document.getElementById('loader_container'); 
	targelem.style.display='none';
	targelem.style.visibility='hidden';
} 

function window.onload(){ 
	remove_loading();
}

