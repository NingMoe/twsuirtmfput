// JScript 文件

/*必须的CSS*/
document.write("<style>");
document.write(".mesWindow{border:#666 0px solid;background:#fff;border:0px solid #C4D9F9;}");
document.write(".mesWindowTop{margin:0px;font-size:15px;text-align:right;vertical-align:middle;height:31px;background-image:url(../../images/bg0_06.jpg);}");
document.write(".mesWindowContent{margin:0px;background:#FFFFFF;}");
document.write(".mesWindowclose{margin:0px;height:15px;width:28px;border:none;cursor:pointer;text-align:center;vertical-align:middle;}");
document.write(".close{color:#1c4c75;height:10px;width:10px;border:0px solid white;cursor:hand;font-size:11px;}");
document.write(".p{font-family:Arial,Helvetica,sans-serif;font-size:12px;font-weight:normal;color:#1c4c75;}");
document.write("</style>");

var isIe=(document.all)?true:false;
var move=false;

//主调函数  参数:（wWidth:显示窗体的宽度；hHeight:显示窗体的高度；Url:显示窗体的URL地址；title;标题；center:是否在窗体中间显示；IsTitleBar:是否显示标题栏(无标题栏时，不可拖动、不可手动关闭)）
function showMessageBox(Width,Height,Url,title,center,IsTitleBar,IsShield)
{   //debugger;
    if (center==null) center = true;
    if(IsTitleBar==null) IsTitleBar=true;
    if(IsShield==null) IsShield=true;
    closeWindow1();   
    FirstDiv(Width,Height,Url,title,center,IsTitleBar);    
}

//最上层的操作层
function FirstDiv(divWidth,divHeight,Url,title,center,IsTitleBar)
{
    var clientWidth=document.documentElement.scrollWidth;
    var clientHeight=document.documentElement.scrollHeight;
    var pos;
    if (center == false)
        pos = mousePosition(event,event.srcElement);
    else
       pos = {x:(document.body.clientWidth-divWidth)/2, y:(document.body.clientHeight-divHeight)/2, w:0,h:0}
    
    var divHtml=document.createElement("div");
    divHtml.id="mesWindow";
    divHtml.className="mesWindow";
    divHtml.innerHTML = IsTitleBar==true ? "<div class=\"mesWindowTop\"><table cellpadding='0' cellspacing='0' border='0' width='100%'><tr onmousedown=\"StartDrag(this);\" onmouseup=\"StopDrag(this);\" onmousemove='Drag(this,event);'><td width='50%' height='31' class='p'>&nbsp;&nbsp;"+title+"</td><td width='50%' align='right'><a onclick='closeNotReload();' class=\"close\"><img src=\"../../images/close.gif\" style=\"'width:13px; height:13px; border:0px;\" /></a>&nbsp;</td></tr></table></div>" : "";
    divHtml.innerHTML = divHtml.innerHTML + "<div class='mesWindowContent' id='mesWindowContent'><iframe id='ifDisplay' frameborder=0 style='width:"+divWidth+"px; height:"+divHeight+"px' src='" + Url + "'></iframe></div>";
    var style="left:"+(((clientWidth-pos.x-divWidth)<=0)?(pos.x-divWidth):pos.x)+"px;top:"+(((clientHeight-pos.y-(divHeight-20))<=0)?(pos.y-(divHeight-20)-(2*pos.h)):pos.y+(2*pos.h))+"px;position:absolute;width:"+divWidth+"px;";
    style += "background:#E2EBF4;border:1px solid #a6d0e7;" 
    divHtml.style.cssText=style;
    document.body.appendChild(divHtml);
}

//获取单击控件的坐标及高宽度 
function mousePosition(ev,srcElement)
{
    return {x:ev.clientX ,y:ev.clientY - srcElement.offsetHeight,w:srcElement.offsetWidth,h:srcElement.offsetHeight};
}

//拖动div
function Drag(obj,ev)              
{
     if(move)
     {
          var oldwin=obj.parentElement.parentElement.parentElement.parentNode;
          var bWidth=parseInt(document.documentElement.scrollWidth);
          var bHeight=parseInt(document.documentElement.scrollHeight);
          var Left=0;
          var Top =0;
          if(parseInt(ev.clientX)-parseInt(oldwin.style.width.split('px')[0])/2<=0) Left=0;
          else if(parseInt(ev.clientX)-parseInt(oldwin.style.width.split('px')[0])/2>=parseInt(bWidth)-parseInt(oldwin.style.width.split('px')[0])-2)
            Left=parseInt(bWidth)-parseInt(oldwin.style.width.split('px')[0])-2;    
          else Left=parseInt(ev.clientX)-parseInt(oldwin.style.width.split('px')[0])/2;
          
          if(ev.clientY-10<=0) Top=0;
          else if(ev.clientY-10>=parseInt(bHeight)-parseInt(obj.offsetHeight))
            Top=parseInt(bHeight)-parseInt(obj.offsetHeight);
          else Top=ev.clientY-10;
            
        oldwin.style.top=Top;
        oldwin.style.left=Left;
    }
}

function StopDrag(obj)
{
    obj.style.cursor="auto";
    obj.releaseCapture();
    move=false;
}

function StartDrag(obj)                       
{
    if(event.srcElement.parentElement.tagName.toUpperCase()=="TR")
    {
        obj.setCapture();
        obj.style.cursor="move";
        move=true;
    } 
}

//关闭窗口
function closeWindow1()
{
    if(document.getElementById('mesWindow')!=null)
    {
       document.getElementById('mesWindow').parentNode.removeChild(document.getElementById('mesWindow'));
       //window.location.reload();
    } 
    document.body.style.overflow ="auto";
    //if(document.getElementById("ctl00_ContentPlaceHolder1_txtCode")!=null) document.getElementById("ctl00_ContentPlaceHolder1_txtCode").focus();
    
}

//关闭窗口
function closeNotReload()
{
    
    if(document.getElementById('mesWindow')!=null)
    {
        document.getElementById('mesWindow').parentNode.removeChild(document.getElementById('mesWindow'));      
    } 
    document.body.style.overflow ="auto";
    if(document.getElementById("ctl00_ContentPlaceHolder1_txtCode")!=null) document.getElementById("ctl00_ContentPlaceHolder1_txtCode").focus();
    
}
    function closeWindow()
    {
        if(document.getElementById('mesWindow1')!=null)
        {
            document.getElementById('mesWindow1').parentNode.removeChild(document.getElementById('mesWindow1'));
        }       
            
        document.body.style.overflow ="auto";
    }

