using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebServices;
using System.IO;
//using Unionsoft.Base;

public partial class Common_UpLoadAjax : UserPagebase
{
    public string UserID = "";
    private static string UploadFileName = "";
    private static string UploadPathUrl = "";
    private static string FileSize = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        UserID = CurrentUser.ID;
        string typeValue = Request.QueryString["typeValue"];
        string Json = "";
        try
        { 
            if (typeValue == "SaveUploadInfo")
            {
                string ResID = "";
                if (Request["ResID"] != null) ResID = Request["ResID"].ToString();
                SaveUploadInfo(ResID);
            }
            else if (typeValue == "LoadUploadInfo")
            {
                Json = "[{'UploadFileName':'" + UploadFileName.Substring(0, UploadFileName.Length - 1) + "','UploadPathUrl':'" + UploadPathUrl + "','FileSize':'" + FileSize + "'}]";
                UploadFileName = "";
                FileSize = "";
            }
            else if (typeValue == "GetUploadInfo")
            {
                string ResID = Request.QueryString["ResID"];
                string ParentResID = Request.QueryString["ParentResID"];
                string ParentRecID = Request.QueryString["ParentRecID"];

                Json = GetFilesInfo(ResID, ParentResID, ParentRecID);
            }
            else if (typeValue == "LoadUploadInfo")
            {
                Json = "[{'UploadFileName':'" + UploadFileName.Substring(0, UploadFileName.Length - 1) + "','UploadPathUrl':'" + UploadPathUrl + "','FileSize':'" + FileSize + "'}]";
                UploadFileName = "";
                FileSize = ""; 
            }
            else if (typeValue == "DeleteDoc")
            {
               string ResID = Request["ResID"].ToString();
               string RecID = Request["RecID"].ToString();
               string FilePathUrl = Request["FilePathUrl"];

               Json = DeleteDoc(ResID, RecID, UserID);
            }
            else if (typeValue == "DeleteFile")
            {
                string FilePathUrl = Request["FilePathUrl"];
                if (DeleteFile(FilePathUrl))
                {
                    Json = "{\"success\":true}";
                }
                else Json = "{\"success\":false}";
            }
            else if (typeValue == "ImportFile")
            {
                string ResID = Request["ResID"];
                HttpFileCollection httpFileCollection = Request.Files;
                HttpPostedFile file = null;
                if (httpFileCollection.Count > 0)
                    file = httpFileCollection[0];
                if (file != null)
                {
                    //获取上传的文件，并转为文件流
                    Stream stream = file.InputStream;
                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    DoExcel doexcel = new DoExcel();
                    //Excel中的第一行将作为列名
                    DataTable dt = doexcel.ImportExcel(stream);
                    ExcelOperate.SetDaoRuAccess(dt, ResID);
                    Json = "{\"success\":true,\"msg\":'上传成功！'}";
                }
                else
                {
                    Json = "{\"success\":false,\"msg\":'上传文件失败！'}";
                }
            }
            Response.Write(Json);
        }
        catch (Exception ex)
        {
             Log.Error(ex.ToString());
            //throw;
        }
       
    } 
     

    protected string GetFilesInfo(string ResID, string ParentResID, string ParentRecID)
    {
        string strJson = "";
        WebServices.Services Resource = new Services();
        DataTable dtRelation = Resource.GetRelationTable(ParentResID).Tables[0];

        DataRow[] drs = dtRelation.Select("ChildResourceID=" + ResID);
        if (drs.Length > 0)
        {
            string strSql = "select ID,DocID,DocHostName,(DOC2_NAME+'.'+DOC2_EXT) DocName,cast(ROUND(DOC2_SIZE/1024,1) as decimal(18,1)) DocSize,DOC2_CRTTIME CreateDate from (select Res.ID, Res." + drs[0]["ChildColName"] + ",Res.DocID from " + drs[0]["ChildTableName"] + " Res where " + drs[0]["ChildColName"] + " in (select " + drs[0]["ParentColName"] + " from " + drs[0]["ParentTableName"] + " where ID='" + ParentRecID + "'))T ";
            strSql += " join Cms_DocumentCenter Doc on Doc.DOC2_ID=T.DocID ";
            string[] changePassWord = Common.getChangePassWord();
            DataTable dt = Resource.SelectData(strSql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0]; 
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter(); 
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
            strJson = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        } 
        return strJson;
    }

    protected void SaveUploadInfo(string ResID)
    {
        try
        {
            Response.ContentType = "text/plain";
            Response.Charset = "utf-8";
            HttpPostedFile file = Request.Files["Filedata"];
            string Savefolder = System.Web.Configuration.WebConfigurationManager.AppSettings["UploadFileUrl"].Replace("/", "\\");
            if (Savefolder.TrimStart().Substring(0, 2) != "\\") Savefolder = "\\" + Savefolder;
            if (Savefolder.TrimEnd().Substring(Savefolder.TrimEnd().Length - 2, 2) != "\\") Savefolder += "\\";

            Savefolder += DateTime.Now.ToString("yyyy") + "\\"+ ResID + "\\" + DateTime.Now.ToString("yyyyMM") + "\\";

              FileSize += file.ContentLength.ToString()+"|";

            string uploadPath = HttpContext.Current.Server.MapPath("~");
            uploadPath = uploadPath.Substring(0, uploadPath.LastIndexOf("\\")) + Savefolder;
              
            if (Directory.Exists(uploadPath) == false)
            {
                Directory.CreateDirectory(uploadPath);
            }
            string guid = System.Guid.NewGuid().ToString();
            string type = file.FileName.Substring(file.FileName.IndexOf('.'));
            
            file.SaveAs(uploadPath + file.FileName.Substring(0, file.FileName.IndexOf('.')).ToString() + "[_]" + guid + type); //保存图片
            UploadFileName += file.FileName.Substring(0, file.FileName.IndexOf('.')).ToString() + "[_]" + guid + type + "|";
            UploadPathUrl = Savefolder.Replace("\\", "/");
            Response.Write(UploadFileName);
        }
        catch (Exception ex)
        {
            //Log.Error(ex.ToString());
            //return ex;
        }
    }

    private bool  DeleteFile(string FilePathUrl)
    {
        string uploadPath = HttpContext.Current.Server.MapPath("~");
        uploadPath = uploadPath.Substring(0, uploadPath.LastIndexOf("\\")) + FilePathUrl.Replace("/", "\\");
        try
        {
            if (File.Exists(uploadPath))
            {
                File.Delete(uploadPath);
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private string DeleteDoc(string ResID, string RecID,string UserID)
    {
        string strJson = "";
        WebServices.Services Resource = new Services();
        if (Resource.Delete(ResID, RecID, UserID)) strJson = "{\"success\":true}";
        else strJson = "{\"success\":false}";
        return strJson;
    }

}