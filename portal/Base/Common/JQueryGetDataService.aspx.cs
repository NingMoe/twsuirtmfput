using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Common_JQueryGetDataService : UserPagebase
{
    public string UserID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        UserID = CurrentUser.ID;
        //这个参数是用来判断接下来是要进行做什么操作的。
        string typeValue = Request.QueryString["typeValue"];

        if (typeValue != null&&typeValue.Equals("GetEditorData"))
        {
            GetEditorData();
        }else if(typeValue != null&&typeValue.Equals("GetEJYT")){
            GetEJYT();
        }
     
        else if (typeValue != null && typeValue.Equals("SaveInfo"))
        {
            SaveInfo();//保存信息，单表保存
        }
        else if (typeValue != null && typeValue.Equals("GetPPXTMCCombo"))
        {
            GetPPXTMCCombo();
        } 
         
        else if (typeValue != null && typeValue.Equals("deleteGridRow"))
        {
            deleteGridRow();
        }
        else if (typeValue != null && typeValue.Equals("deleteGridRowAndFile"))
        {

            string FilePath = Request.QueryString["hrefUrl"];
           // FilePath = "../../cmsweb/UploadFile/432746741276/460656429330.jpg";
            if (!string.IsNullOrEmpty(FilePath))
            {
                deleteGridRowAndFile(FilePath);
                deleteGridRow();
            }
        }
       else if(typeValue!=null&& typeValue.Equals("fileupload")){
            fileUpload();
        }
        else if (typeValue != null && typeValue.Equals("SavePPXTPJL"))
        {
            SavePPXTPJL();
        }else if(typeValue!=null&& typeValue.Equals("GetTPJLData")){
            GetTPJLData();
        }
     
        else if (typeValue != null && typeValue.Equals("SaveRWPFInfo"))
        {
            SaveRWPFInfo();
        }
        else if (typeValue != null && typeValue.Equals("SaveRWPFUploadInfo"))
        {
            SaveRWPFUploadInfo();
        }
        else if (typeValue != null && typeValue.Equals("SaveJLRCInfo"))
        {
            SaveJLRCInfo();
        }
        else if (typeValue != null && typeValue.Equals("SaveDCLYGYInfo"))
        {
            SaveDCLYGYInfo();
        }
    }




    protected void deleteGridRowAndFile(string  FilePath )
    {
        //string pSavedPath1 = Server.MapPath("../../images/ProductImages/6114413/绿.jpg");
        string pSavedPath1 = Server.MapPath(FilePath);         
        if (File.Exists(pSavedPath1))
        {
            FileInfo fi = new FileInfo(pSavedPath1);
            if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                fi.Attributes = FileAttributes.Normal;
            File.Delete(pSavedPath1);
        }
    }

    protected void GetEditorData()
    {
        String ResID = Request.QueryString["ResID"];
        String RecID = Request.QueryString["RecID"];
        string searchstr = "";
        WebServices.Services Resource = new WebServices.Services();

        if (ResID == "460222389874")
        {
            searchstr = " and DOCID ='" + RecID + "'";
        }
        else
        {
            searchstr = " and id=" + RecID;
        }

        DataTable ppDt = Resource.GetDataListByResourceID(ResID, false, searchstr).Tables[0];
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
        String str = Newtonsoft.Json.JsonConvert.SerializeObject(ppDt, timeConverter);
        
        //方案系统编号，加载方案扩展表的时候用 430590206015//方案扩展表的ResID
        String FAXTBH = Request.QueryString["FAXTBH"];
        if (FAXTBH != null && FAXTBH != "")
        {
            DataTable sunDt = Resource.GetDataListByResourceID("430590206015", false, " and 方案系统编号='" + FAXTBH + "'").Tables[0];
            String str1 = Newtonsoft.Json.JsonConvert.SerializeObject(sunDt, timeConverter);
            str = str + "[#]" + str1;
            String lxrxtbh = sunDt.Rows[0]["联系人系统编号"].ToString();
            if (lxrxtbh != null && lxrxtbh!="")
            {
                //查询品牌方案关联的联系人系统编号
                DataTable lxrDt = Resource.GetDataListByResourceID("379264068953", false, " and 联系人系统编号='" + lxrxtbh + "'").Tables[0];
                String str2 = Newtonsoft.Json.JsonConvert.SerializeObject(lxrDt, timeConverter);
                str = str + "[#]" + str2;
            }
        }
        Response.Write(str);
    }
    //通过业态名称来查询子业态名称，并返回字符串"
    protected void GetEJYT()
    {
        WebServices.Services Resource = new WebServices.Services();
        String ytValue = "";
        String ChildYT = "";
        if (Request.QueryString["ytValue"] != null)
        {
            ytValue = Request.QueryString["ytValue"];
        }
        if (ytValue != null&&ytValue!="")
        {
            DataTable ejYTDt = Resource.GetDataListByResourceID("423743051439", false, " and 上级业态名称='" + ytValue + "'").Tables[0];
            if (ejYTDt.Rows.Count > 0)
            {
                for(int i=0;i<ejYTDt.Rows.Count;i++){
                    ChildYT += ejYTDt.Rows[i]["业态名称"].ToString();
                    if (i < ejYTDt.Rows.Count - 1 ){
                        ChildYT += ",";
                    }
                }
            }
        }
        Response.Write(ChildYT);
    }
  
    //保存信息，单表保存
    protected void SaveInfo()
    {
        String str = "true";
        String ResID = "";
        if (Request["ResID"] != null)
        {
            ResID = Request["ResID"];
            String RecID = Request["RecID"];
            String lxrJson = "";
            if (Request["lxrJson"] != null)
            {
                lxrJson = Request["lxrJson"];
            }
            else
            {
                str = "false";
            }
            bool isSuccess = CommonMethod.AddOrEidt(lxrJson, ResID, RecID, UserID);
            if (!isSuccess)
            {
                str = "false";
            }
        }
        else
        {
            str = "false";
        }
        Response.Write(str);
    }
    //获取下拉grid的json
    protected void GetPPXTMCCombo()
    {
        String ResID=Request["ResID"];
        String dispname = Request["dispname"];
        String str=CommonMethod.GetDictionaryColName(ResID, dispname);
    }
 
    

    //删除grid的行数据
    protected void deleteGridRow()
    {
        String ResID = Request["ResID"];
        String RecID = Request["RecID"];
        if (ResID != null && ResID != "")
        {
            WebServices.Services Resource = new WebServices.Services();
            Resource.Delete(ResID, RecID, UserID);
        }
    }
    
    protected void fileUpload()
    {
      
        Response.ContentType = "text/plain";
        Response.Charset = "utf-8";
        HttpPostedFile file = Request.Files["Filedata"];
        string ResID = Request["ResID"];
        string RecID = Request["RecID"];
        string jsonStr = Request["jsonStr"];
        string uploadPath =HttpContext.Current.Server.MapPath(Request["folder"]) + "\\";
        DataTable dt = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(jsonStr);
        WebServices.FieldInfo[] FieldListInfo = new WebServices.FieldInfo[dt.Rows.Count];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WebServices.FieldInfo fi = new WebServices.FieldInfo();
                fi.FieldDescription = dt.Rows[i]["Description"].ToString();
                fi.FieldValue = dt.Rows[i]["Value"].ToString();
                FieldListInfo[i] = fi;
            }
        }
        if (file != null)
        {
            string fileName = System.IO.Path.GetFileName(file.FileName);
            int FileLen = file.ContentLength;
            byte[] FileData = new byte[FileLen];
            System.IO.Stream sr = file.InputStream;//创建数据流对象 
            sr.Read(FileData, 0, FileLen);
            sr.Close();
            WebServices.Services Resource = new WebServices.Services();
            Resource.UploadFile(ResID, UserID, fileName, FileData, FieldListInfo);
            //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
            Response.Write("1");
        }
        else
        {
            WebServices.Services Resource = new WebServices.Services();
            Resource.UploadFile(ResID, UserID, "", null, FieldListInfo);
            Response.Write("1");
        }  

    }

    protected void SavePPXTPJL() {
        Response.ContentType = "text/plain";
        Response.Charset = "utf-8";
        HttpPostedFile file = Request.Files["Filedata"];
        string TPJLResID ="";
        string RecID = "";
        string TPJLWDResID = "";
        string jsonStr = "";
        string jsonStr1 = "";
        if (Request["RecID"] != null)
        {
            RecID = Request["RecID"];
        }
        if (Request["ResID"] != null)
        {
            TPJLResID = Request["ResID"];
        }
        if (Request["TPJLWDResID"] != null)
        {
            TPJLWDResID = Request["TPJLWDResID"];
        }
        if (Request["jsonStr"] != null)
        {
            jsonStr = Request["jsonStr"];
        }
        if (Request["jsonStr1"] != null)
        {
            jsonStr1 = Request["jsonStr1"];
        }
        bool isSuccess = CommonMethod.AddOrEidt(jsonStr1, TPJLResID, RecID, UserID);
        if (isSuccess)
        {
            string uploadPath = HttpContext.Current.Server.MapPath(Request["folder"]) + "\\";
            DataTable dt = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(jsonStr);
            WebServices.FieldInfo[] FieldListInfo = new WebServices.FieldInfo[dt.Rows.Count];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    WebServices.FieldInfo fi = new WebServices.FieldInfo();
                    fi.FieldDescription = dt.Rows[i]["Description"].ToString();
                    fi.FieldValue = dt.Rows[i]["Value"].ToString();
                    FieldListInfo[i] = fi;
                }
            }
            WebServices.Services Resource = new WebServices.Services();

            if (file != null)
            {
                string fileName = System.IO.Path.GetFileName(file.FileName);
                int FileLen = file.ContentLength;
                byte[] FileData = new byte[FileLen];
                System.IO.Stream sr = file.InputStream;//创建数据流对象 
                sr.Read(FileData, 0, FileLen);
                sr.Close();
                Resource.UploadFile(TPJLWDResID, UserID, fileName, FileData, FieldListInfo);
                //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
                Response.Write("1");
            }
            else
            {
                Resource.UploadFile(TPJLWDResID, UserID, "", null, FieldListInfo);
                Response.Write("1");
            }
        }
        else
        {
            Response.Write("0");
        }
    }
    //编辑谈判记录的时候进来
    protected void GetTPJLData() {
        string RecID = "";
        string ResID = "";
        if (Request["RecID"] != null)
        {
            RecID = Request["RecID"];
        }
        if (Request["ResID"] != null)
        {
            ResID = Request["ResID"];
        }
        WebServices.Services Resource = new WebServices.Services();
        DataTable ppDt = Resource.GetDataListByResourceID(ResID, false, " and id=" + RecID).Tables[0];
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
        String str = Newtonsoft.Json.JsonConvert.SerializeObject(ppDt, timeConverter);
        if (ppDt.Rows.Count > 0)
        {
            DataTable sunDt = Resource.GetDataListByResourceID("383153238947", false, " and 谈判系统编号='" + ppDt.Rows[0]["谈判系统编号"]+"'").Tables[0];
            str = str.Substring(0, str.Length - 1);
            String str1 = Newtonsoft.Json.JsonConvert.SerializeObject(sunDt, timeConverter);
            str = str + "," + str1.Substring(1, str1.Length - 1);
        }
        Response.Write(str);
    }


    protected void SaveRWPFInfo()
    {
       
        string RecID = "0";
        string jsonStr1 = "";
        string jsonStr2 = "";
        string ygyStr = "";
        string rwType = "";
       
        if (Request["RecID"] != null)
        {
            RecID = Request["RecID"];
        }
        if (Request["jsonStr1"] != null)
        {
            jsonStr1 = Request["jsonStr1"];
        }
        if (Request["jsonStr2"] != null)
        {
            jsonStr2 = Request["jsonStr2"];
        }
        if (Request["ygyStr"] != null)
        {
            ygyStr = Request["ygyStr"];
        }
        if (Request["rwType"] != null)
        {
            rwType = Request["rwType"];
        }
       
        String returnId = CommonMethod.AddRWPF(RecID, jsonStr1, jsonStr2, ygyStr, rwType, UserID);
        Response.Write(returnId);
    }

    protected void SaveRWPFUploadInfo()
    {
        string jsonStr3 = "";
        string rcswwdResID = "393296902171";
        if (Request["jsonStr3"] != null)
        {
            jsonStr3 = Request["jsonStr3"];
        }
        Response.ContentType = "text/plain";
        Response.Charset = "utf-8";
        HttpPostedFile file = Request.Files["Filedata"];
        WebServices.Services Resource = new WebServices.Services();

        string uploadPath = HttpContext.Current.Server.MapPath(Request["folder"]) + "\\";

        DataTable dt = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(jsonStr3);
        WebServices.FieldInfo[] FieldListInfo = new WebServices.FieldInfo[dt.Rows.Count];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WebServices.FieldInfo fi = new WebServices.FieldInfo();
                fi.FieldDescription = dt.Rows[i]["Description"].ToString();
                fi.FieldValue = dt.Rows[i]["Value"].ToString();
                FieldListInfo[i] = fi;
            }
        }
        if (file != null)
        {
            string fileName = System.IO.Path.GetFileName(file.FileName);
            int FileLen = file.ContentLength;
            byte[] FileData = new byte[FileLen];
            System.IO.Stream sr = file.InputStream;//创建数据流对象 
            sr.Read(FileData, 0, FileLen);
            sr.Close();
            Resource.UploadFile(rcswwdResID, UserID, fileName, FileData, FieldListInfo);
            //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
            Response.Write("1");
        }
        else
        {
            Resource.UploadFile(rcswwdResID, UserID, "", null, FieldListInfo);
            Response.Write("1");
        }
    }

    protected void SaveJLRCInfo()
    {
        string RecID = "0";
        string jsonStr1 = "";
        string jsonStr2 = "";

        if (Request["RecID"] != null)
        {
            RecID = Request["RecID"];
        }
        if (Request["jsonStr1"] != null)
        {
            jsonStr1 = Request["jsonStr1"];
        }
        if (Request["jsonStr2"] != null)
        {
            jsonStr2 = Request["jsonStr2"];
        }
     
        String returnId = CommonMethod.AddJLRC(RecID, jsonStr1, jsonStr2, UserID);
        Response.Write(returnId);
    }

    protected void SaveDCLYGYInfo(){
        string RecID = "";
        string jsonStr1 = "";
        string jsonStr2 = "";
        if (Request["RecID"] != null)
        {
            RecID = Request["RecID"];
        }
        if (Request["jsonStr1"] != null)
        {
            jsonStr1 = Request["jsonStr1"];
        }
        if (Request["jsonStr2"] != null)
        {
            jsonStr2 = Request["jsonStr2"];
        }
        String returnId = CommonMethod.RWPFCLOfYGY(RecID, jsonStr1, jsonStr2, UserID);
        Response.Write(returnId);
    }
    
}