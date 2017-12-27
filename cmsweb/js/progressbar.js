// JScript 文件
var pb_pos=0; var pb_dir=2; var pb_len=0;
var pb_animate = setInterval(progressbar_animate,20);
function progressbar_animate() {
	var elem = document.getElementById('progressbar_progress'); 
	if(elem != null) { 
		if (pb_pos==0) pb_len += pb_dir; 
		if (pb_len>32 || pb_pos>179) pb_pos += pb_dir; 
		if (pb_pos>179) pb_len -= pb_dir; 
		if (pb_pos>179 && pb_len==0) pb_pos=0; 
		elem.style.left = pb_pos; 
		elem.style.width = pb_len;
	}
}
function remove_progressbar_loading() {
	this.clearInterval(pb_animate);
	var targelem = document.getElementById('progressbar_loader_container');
	targelem.style.display='none';
	targelem.style.visibility='hidden';
}
