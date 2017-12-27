using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WebServices;
using System.Text;
using System.Collections;
using System.IO;
public partial class Common_CommonAjax_Request : UserPagebase
{
    string UserID = "";
    string UserName = "";
    UserInfo oEmployee = null;
    protected void Page_Load(object sender, EventArgs e)
    { 
        UserID = this.CurrentUser.ID;
        UserName = this.CurrentUser.Name;
        string json = "";
        string typeValue = Request["typeValue"];

        ///根据RecID查询附件信息
        if (typeValue == "GetAccessoryByRecID")
        {
            string RecID = Request["RecID"];
            json =GetOneRowByRecID(RecID);
        }  

        #region //单表单记录添加修改
        if (typeValue == "SaveInfo")
        {
            string RecID = "0";
            if (Request["RecID"] != null)
            {
                RecID = Request["RecID"];
            }
            string ResID = Request["ResID"];
            string dataJson = Request["Json"];
            json = SaveInfo(ResID, dataJson, RecID);
            if (ResID=="564794299148")
	        {
                DataTable dt = CommonMethod.JsonToDataTable(dataJson);
                if (dt.Columns.Contains("联单编号")&&dt.Columns.Contains("入库件数"))
                {
                    if (dt.Rows[0]["联单编号"].ToString()!=""&&dt.Rows[0]["入库件数"].ToString()!="")
                    {
                        Services Resource = new Services();
                        string LDBH = dt.Rows[0]["联单编号"].ToString();
                        int RKJS = Convert.ToInt32(dt.Rows[0]["入库件数"].ToString());
                        string delSql = "delete SW_Info_Detail where FormNumber='" + LDBH + "' and isnull(IsCheckOut,'')!='已出库'";
                        string[] changePassWord = Common.getChangePassWord();
                        Resource.ExecuteSql(delSql, changePassWord[0], changePassWord[1], changePassWord[2]);
                       // Resource.DeleteByTableName("SW_Info_Detail", "联单编号='" + LDBH + "'", UserID);
                        for (int i = 1; i < RKJS+1; i++)
                        {
                            string RKJson = "[{联单编号:'" + LDBH + "',是否出库:'已入库',序号:'" + LDBH+"_" + i + "'}]";
                            json = SaveInfo("565363204399", RKJson, "");
                        }
                    }
                }
	        }
        }

        if (typeValue == "SaveInfo_MultiLine")//单表多条数据
        {
            string ResID = Request["ResID"]; 
            string dataJson = Request["Json"];
            json = CommonMethod.SaveInfo_MultiLine(ResID, dataJson,UserID);
        }
        if (typeValue == "SaveInfoByParentAndChildDocument")// 单条主资源数据关联多条附件记录
        { 
            string  RecID = Request["RecID"];
            string ResID = Request["ResID"]; 
            string dataJson = Request["Json"];
            string ChildJson = Request["ChildJson"];
            string ChildResID = Request["ChildResID"];
            json = CommonMethod.SaveInfoByParentAndChildDocument(ResID, RecID, dataJson, ChildResID, ChildJson, UserID);
        }
        if (typeValue == "SaveInfoByParentAndChild")//  
        {
            string RecID = Request["RecID"];
            string ResID = Request["ResID"];
            string dataJson = Request["Json"];
            string ChildJson = Request["ChildJson"];
            string ChildResID = Request["ChildResID"];
            json = CommonMethod.SaveInfoByParentAndChild(ResID, RecID, dataJson, ChildResID, ChildJson, UserID);
        }
        if (typeValue == "SaveChildInfo")//  
        {
            string ParentRecID = Request["ParentRecID"];
            string ParentResID = Request["ParentResID"];
            string dataJson = Request["Json"];
            string ResID = Request["ResID"];
            json = CommonMethod.SaveChildInfo(ParentResID, ParentRecID, ResID, dataJson, UserID);
        }
        if (typeValue == "SaveInfoByMultiLineAndDocument")//单表多附件上传
        {
            string ResID = Request["ResID"];
            string ChildJson = Request["ChildJson"];
            json = CommonMethod.SaveInfoByMultiLineAndDocument(ResID, ChildJson, UserID);
        }
        #endregion
        #region //单表单记录查询
        if (typeValue == "GetOneRowByRecID")
        {
            string RecID = Request["RecID"];
            string ResID = Request["ResID"];
            json = CommonMethod.GetOneRowByRecID(ResID, RecID);
        }  
        // 记录查询
        if (typeValue == "GetDataByKeyWord")
        {
           string Condition = Request["Condition"];
           string KeyWord = Request["keyWordValue"];
            json = CommonMethod.GetDataByKeyWord(KeyWord, Condition);
        }
        #endregion

        if (typeValue == "UpdatePassword")
        {
            string UserAccount = CurrentUser.ID;
            string UserPassword = Request["Password"];
            Services oServices = new Services();
            if (oServices.ChangePassword(UserAccount, UserPassword))
            {
                json = "{\"success\":true}";
            }
            else
            {
                json = "{\"success\":false}";
            } 
        }
        if (typeValue == "GetDataDictionary")
        {
            string keyCode = Request["keyCode"];
            json = GetDataDictionary(keyCode);
        }
        if (typeValue == "SaveFPSQAndXMLB") {
            json = SaveFPSQAndXMLB();
        }
        if (typeValue == "GetfDataByYSKTJ")
        {
            json = GetfDataByYSKTJ();
        }
        if (typeValue == "GetfDataByYSKTJXS")
        {
            json = GetfDataByYSKTJXS();
        } 
        if (typeValue == "GetfDataByYSKTJKH")
        {
            json = GetfDataByYSKTJKH();
        }
        if (typeValue == "GetDataByXSMBDCJD")
        {
            json = GetDataByXSMBDCJD();
        } 
        if (typeValue == "GetDataByFXSXM")
        {
            json = GetDataByFXSXM();
        }
        if (typeValue == "GetDataByFDDXM")
        {
            json = GetDataByFDDXM();
        }
        if (typeValue == "GetDataByFKHXM")
        {
            json = GetDataByFKHXM();
        }
        if (typeValue == "GetDataByWGJKHTX")
        {
            json = GetDataByWGJKHTX();
        }
        if (typeValue == "GetDataByWGJKHTS")
        {
            json = GetDataByWGJKHTS();
        } 
        Response.Write(json);
    }
    #region 获取一条数据
    private string GetOneRowByRecID(string RecID)
    {
        Services Resource = new Services();
        string[] changePassWord = Common.getChangePassWord();
        string sqlStrID = "select Id,FileName,FileImage from dbo.WORKFLOW_FORM_ATTACHMENTS where RecID =(select ID from CT485950259084 where id='" + RecID + "')";
        DataTable dts = Resource.SelectData(sqlStrID, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        string str = "";
        for (int i = 0; i < dts.Rows.Count; i++)
        {
            Byte[] Files = (Byte[])dts.Rows[i]["FileImage"];
            string strUrl = HttpRuntime.AppDomainAppPath+"LoadFile/";
            BinaryWriter bw = new BinaryWriter(File.Open(strUrl + dts.Rows[i]["FileName"].ToString(), FileMode.OpenOrCreate));
            bw.Write(Files);
            bw.Close();
            str += dts.Rows[i]["FileName"].ToString() + "[,]";
        }
        return str;
    }
    #endregion

    #region 保存或更新记录
    private string SaveInfo(string ResID, string dataJson, string RecID)
    {

        if (CommonMethod.AddOrEidt(dataJson, ResID, RecID, UserID))
        {
            return "{\"success\": true}";
        }
        else
        {
            return "{\"success\": false}";
        }
    }
    #endregion

    #region 检查记录是否已经存在  ResID:资源ID  Condtion:条件
    private string CheckExist(string ResID, string Condtion)
    {
        if (CommonMethod.CheckExist(ResID, Condtion) > 0)
        {
            return "{\"success\": true}";
        }
        else
        {
            return "{\"success\": false}";
        }
    }
    #endregion

    #region  
    protected string GetDataListBySql(string sql)
    {
        string resultStr;
        WebServices.Services Resource = new WebServices.Services();
        string[] changePassWord = Common.getChangePassWord();

        DataTable dt = Resource.Query(sql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        // //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式     
        //timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss"
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm";
        resultStr = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        return resultStr;

    }
    #endregion

    #region//获取字典项
    protected string GetDataDictionary(string keyCode)
    {
        Services ws = new Services();       
        string strSql = "select KeyTitle id, KeyTitle text from dbo.DataDictionary where ParentId=(select ID from dbo.DataDictionary  where KeyCode='" + keyCode + "')";
        string[] changePassWord = Common.getChangePassWord();
        DataTable dt = ws.SelectData(strSql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];

        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
       
        return Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        
    }
    #endregion

    #region 保存发票申请和项目列表
    private string SaveFPSQAndXMLB() {
        string json1 = Request["Json1"];
        string json2 = Request["Json2"];
        string RecID = Request["RecID"];
        string ResID = Request["ResID"];
        Services Resource = new Services();
        ResourceInfo ResInfo = Resource.GetResourceInfoByID(ResID);
        bool isTrue = false;
        try
        {
            FieldInfo[] fiList = CommonMethod.GetFieldList(ResID, json1);
            if (RecID == "0" || RecID == "")
            {
                RecID= Resource.AddReturnID(ResID, UserID, fiList).ToString();
                if (RecID!=""&&RecID!="0")
                {
                    isTrue = true;
                }
                else
                {
                    isTrue = false;
                }
            }
            else
            {
                isTrue = Resource.Edit(ResID, RecID, UserID, fiList);
            }
            if (isTrue)
            {
                DataTable FPSQDt = Resource.GetDataListByResID(ResID, "", "and ID=" + RecID, UserID).Tables[0];
                string FPSQDH = "";
                if (FPSQDt.Rows.Count>0)
                {
                    FPSQDH = FPSQDt.Rows[0]["发票申请单号"].ToString();
                }
                DataTable oldDt=Resource.GetDataListByResID("401290344433","", "and  发票申请单号='" + FPSQDH + "'", UserID).Tables[0];
                DataTable dt = CommonMethod.JsonToDataTable(json2);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["发票申请单号"] = FPSQDH;
                    List<FieldInfo> fieldList = CommonMethod.GetFieldList(dt.Rows[i]);
                    if (dt.Rows[i]["ID"].ToString() == "0" || dt.Rows[i]["ID"].ToString() == "")
                    {
                        isTrue=Resource.Add("401290344433", UserID, fieldList.ToArray());
                    }
                    else
                    {
                        isTrue = Resource.Edit("401290344433", dt.Rows[i]["ID"].ToString(), UserID, fieldList.ToArray());
                        DataRow dr = oldDt.Select("ID=" + dt.Rows[i]["ID"].ToString())[0];
                        oldDt.Rows.Remove(dr);
                    }
                }
                for (int i = 0; i < oldDt.Rows.Count; i++)
                {
                    isTrue =Resource.Delete("401290344433", oldDt.Rows[i]["ID"].ToString(), UserID);
                }
                return "{\"success\": true}";
            }
            else
            {
                return "{\"success\": false}";
            }
        }
        catch (Exception)
        {
            return "{\"success\": false}";
        }
    }
    #endregion

    #region 获取应收款统计报表
    private string GetfDataByYSKTJ() {
        WebServices.Services Resource = new WebServices.Services();
        string str = "";
        if (Request["SKRQ"]!=null&&Request["SKRQ"]!="")
        {
            DateTime skrq =Convert.ToDateTime(Request["SKRQ"]);
            string sql ="select SUM(C3_401795702260) 本月回款 from CT401290354058  ";
            sql += " where Year(C3_475688833284)='" + skrq.Year + "' and MONTH(C3_475688833284)='" + skrq.Month + "';  ";
            sql +=" SELECT sum(C3_403033088714) 本月开票 FROM ct401290382901  ";
            sql += " where Year(C3_404405195524)='" + skrq.Year + "' and MONTH(C3_404405195524)='" + skrq.Month + "';  ";
            sql += " SELECT sum(C3_403007677105) 三个月内应收款 FROM ct401290382901   ";
            sql += " where C3_404405195524 between   dateadd(day,-90,'" + skrq + "')  and '" + skrq + "';";
            sql += " SELECT sum(C3_403007677105) 三个月以上应收款 FROM ct401290382901  ";
            sql += " where  C3_404405195524 <   dateadd(day,-90,'" + skrq + "') ;  ";
            sql += " SELECT sum(C3_403007677105) 应收款总额 FROM ct401290382901 where  C3_404405195524 < '" + skrq + "';";
            string[] changePassWord = Common.getChangePassWord();

            DataSet ds = Resource.Query(sql, changePassWord[0], changePassWord[1], changePassWord[2]);
            DataTable dt = new DataTable();
            dt.Columns.Add("本月回款");
            dt.Columns.Add("本月开票");
            dt.Columns.Add("三个月内合计");
            dt.Columns.Add("三个月内应收款");
            dt.Columns.Add("三个月以上应收款");
            dt.Columns.Add("应收款总额");
            dt.Rows.Add();
            dt.Rows[0]["本月回款"] = ds.Tables[0].Rows[0]["本月回款"].ToString();
            dt.Rows[0]["本月开票"] = ds.Tables[1].Rows[0]["本月开票"].ToString();
            dt.Rows[0]["三个月内应收款"] = ds.Tables[2].Rows[0]["三个月内应收款"].ToString();
            dt.Rows[0]["三个月以上应收款"] = ds.Tables[3].Rows[0]["三个月以上应收款"].ToString();
            dt.Rows[0]["应收款总额"] = ds.Tables[4].Rows[0]["应收款总额"].ToString();

            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
            str = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        }
        return "{\"total\":1 ,\"rows\":" + str + "}";
    }
    #endregion

    #region 获取应收款统计报表
    private string GetfDataByYSKTJXS()
    {
        WebServices.Services Resource = new WebServices.Services();
        string str = "";
        DataTable dt = new DataTable();
        if (Request["SKRQ"] != null && Request["SKRQ"] != "")
        {
            DateTime skrq = Convert.ToDateTime(Request["SKRQ"]);
            string sql = "select A.销售,SUM(A.C3_401795702260) 本月回款 from (select  C3_401795702260, ";
            sql += " (select max(C3_403007746042) from ct401290344433 where C3_403007758511=C3_401795684760) 销售  from CT401290354058 ";
            sql += " where Year(C3_475688833284)='" + skrq.Year + "' and MONTH(C3_475688833284)='" + skrq.Month + "') A group by A.销售;  ";
            sql += " SELECT sum(C3_403033088714) 本月开票,C3_403033098027 销售 FROM ct401290382901  ";
            sql += " where Year(C3_404405195524)='" + skrq.Year + "' and MONTH(C3_404405195524)='" + skrq.Month + "' group by C3_403033098027;  ";
            sql += " SELECT sum(C3_403007677105) 三个月内应收款,C3_403033098027 销售 FROM ct401290382901   ";
            sql += " where C3_404405195524 between   dateadd(day,-90,'" + skrq + "')  and '" + skrq + "' and isnull(C3_403007677105,0)!=0  group by C3_403033098027;";
            sql += " SELECT sum(C3_403007677105) 三个月以上应收款,C3_403033098027 销售 FROM ct401290382901  ";
            sql += " where  C3_404405195524 < dateadd(day,-90,'" + skrq + "') and isnull(C3_403007677105,0)!=0   group by C3_403033098027;  ";
            sql += " SELECT sum(C3_403007677105) 应收款总额,C3_403033098027 销售 FROM ct401290382901 where  C3_404405195524 < '" + skrq + "' and isnull(C3_403007677105,0)!=0 group by C3_403033098027;";
            string[] changePassWord = Common.getChangePassWord();

            DataSet ds = Resource.Query(sql, changePassWord[0], changePassWord[1], changePassWord[2]);
           
            dt.Columns.Add("销售");
            dt.Columns.Add("本月回款");
            dt.Columns.Add("本月开票");
            dt.Columns.Add("三个月内合计");
            dt.Columns.Add("三个月内应收款");
            dt.Columns.Add("三个月以上应收款");
            dt.Columns.Add("应收款总额");
            for (int i = 0; i <  ds.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[dt.Rows.Count - 1]["销售"] = ds.Tables[0].Rows[i]["销售"].ToString();
                dt.Rows[dt.Rows.Count - 1]["本月回款"] = ds.Tables[0].Rows[i]["本月回款"].ToString();
            }
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                int isHave = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["销售"].ToString()==ds.Tables[1].Rows[i]["销售"].ToString())
                    {
                        dt.Rows[j]["本月开票"] = ds.Tables[1].Rows[i]["本月开票"].ToString();
                        isHave = 1;
                    }
                }
                if (isHave==0)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["销售"] = ds.Tables[1].Rows[i]["销售"].ToString();
                    dt.Rows[dt.Rows.Count - 1]["本月开票"] = ds.Tables[1].Rows[i]["本月开票"].ToString();
                }
            }
            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                int isHave = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["销售"].ToString() == ds.Tables[2].Rows[i]["销售"].ToString())
                    {
                        dt.Rows[j]["三个月内应收款"] = ds.Tables[2].Rows[i]["三个月内应收款"].ToString();
                        isHave = 1;
                    }
                }
                if (isHave == 0)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["销售"] = ds.Tables[2].Rows[i]["销售"].ToString();
                    dt.Rows[dt.Rows.Count - 1]["三个月内应收款"] = ds.Tables[2].Rows[i]["三个月内应收款"].ToString();
                }
            }
            for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
            {
                int isHave = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["销售"].ToString() == ds.Tables[3].Rows[i]["销售"].ToString())
                    {
                        dt.Rows[j]["三个月以上应收款"] = ds.Tables[3].Rows[i]["三个月以上应收款"].ToString();
                        isHave = 1;
                    }
                }
                if (isHave == 0)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["销售"] = ds.Tables[3].Rows[i]["销售"].ToString();
                    dt.Rows[dt.Rows.Count - 1]["三个月以上应收款"] = ds.Tables[3].Rows[i]["三个月以上应收款"].ToString();
                }
            }
            for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
            {
                int isHave = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["销售"].ToString() == ds.Tables[4].Rows[i]["销售"].ToString())
                    {
                        dt.Rows[j]["应收款总额"] = ds.Tables[4].Rows[i]["应收款总额"].ToString();
                        isHave = 1;
                    }
                }
                if (isHave == 0)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["销售"] = ds.Tables[4].Rows[i]["销售"].ToString();
                    dt.Rows[dt.Rows.Count - 1]["应收款总额"] = ds.Tables[4].Rows[i]["应收款总额"].ToString();
                }
            }
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
            str = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        }
        return "{\"total\":" + dt.Rows.Count + " ,\"rows\":" + str + "}";
    }
    #endregion

    #region 获取应收款统计报表
    private string GetfDataByYSKTJKH()
    {
        WebServices.Services Resource = new WebServices.Services();
        string str = "";
        DataTable dt = new DataTable();
        if (Request["SKRQ"] != null && Request["SKRQ"] != "")
        {
            DateTime skrq = Convert.ToDateTime(Request["SKRQ"]);
            string sql = "select A.客户名称,SUM(A.C3_401795702260) 本月回款 from (select  C3_401795702260, ";
            sql += " (select max(C3_403007779574) from ct401290344433 where C3_403007758511=C3_401795684760) 客户名称  from CT401290354058 ";
            sql += " where Year(C3_475688833284)='" + skrq.Year + "' and MONTH(C3_475688833284)='" + skrq.Month + "') A group by A.客户名称;  ";
            sql += " SELECT sum(C3_403033088714) 本月开票,C3_403033049855 客户名称 FROM ct401290382901  ";
            sql += " where Year(C3_404405195524)='" + skrq.Year + "' and MONTH(C3_404405195524)='" + skrq.Month + "' group by C3_403033049855;  ";
            sql += " SELECT sum(C3_403007677105) 三个月内应收款,C3_403033049855 客户名称 FROM ct401290382901   ";
            sql += " where C3_404405195524 between   dateadd(day,-90,'" + skrq + "')  and '" + skrq + "' and isnull(C3_403007677105,0)!=0  group by C3_403033049855;";
            sql += " SELECT sum(C3_403007677105) 三个月以上应收款,C3_403033049855 客户名称 FROM ct401290382901  ";
            sql += " where  C3_404405195524 < dateadd(day,-90,'" + skrq + "') and isnull(C3_403007677105,0)!=0   group by C3_403033049855;  ";
            sql += " SELECT sum(C3_403007677105) 应收款总额,C3_403033049855 客户名称 FROM ct401290382901 where  C3_404405195524 < '" + skrq + "' and isnull(C3_403007677105,0)!=0 group by C3_403033049855;";
            string[] changePassWord = Common.getChangePassWord();
            DataSet ds = Resource.Query(sql, changePassWord[0], changePassWord[1], changePassWord[2]);

            dt.Columns.Add("客户名称");
            dt.Columns.Add("本月回款");
            dt.Columns.Add("本月开票");
            dt.Columns.Add("三个月内合计");
            dt.Columns.Add("三个月内应收款");
            dt.Columns.Add("三个月以上应收款");
            dt.Columns.Add("应收款总额");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[dt.Rows.Count - 1]["客户名称"] = ds.Tables[0].Rows[i]["客户名称"].ToString();
                dt.Rows[dt.Rows.Count - 1]["本月回款"] = ds.Tables[0].Rows[i]["本月回款"].ToString();
            }
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                int isHave = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["客户名称"].ToString() == ds.Tables[1].Rows[i]["客户名称"].ToString())
                    {
                        if (ds.Tables[1].Rows[i]["本月开票"].ToString() != "")
                        {
                            dt.Rows[j]["本月开票"] = Math.Round(Convert.ToDouble(ds.Tables[1].Rows[i]["本月开票"].ToString()), 2);
                        }
                        isHave = 1;
                    }
                }
                if (isHave == 0)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["客户名称"] = ds.Tables[1].Rows[i]["客户名称"].ToString();
                    if (ds.Tables[1].Rows[i]["本月开票"].ToString() != "")
                    {
                        dt.Rows[dt.Rows.Count - 1]["本月开票"] = Math.Round(Convert.ToDouble(ds.Tables[1].Rows[i]["本月开票"].ToString()), 2);
                    }
                }
            }
            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                int isHave = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["客户名称"].ToString() == ds.Tables[2].Rows[i]["客户名称"].ToString())
                    {
                        if (ds.Tables[2].Rows[i]["三个月内应收款"].ToString()!="")
                        {
                            dt.Rows[j]["三个月内应收款"] = Math.Round(Convert.ToDouble(ds.Tables[2].Rows[i]["三个月内应收款"].ToString()),2);
                        }
                        isHave = 1;
                    }
                }
                if (isHave == 0)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["客户名称"] = ds.Tables[2].Rows[i]["客户名称"].ToString();
                    if (ds.Tables[2].Rows[i]["三个月内应收款"].ToString() != "")
                    {
                        dt.Rows[dt.Rows.Count - 1]["三个月内应收款"] = Math.Round(Convert.ToDouble(ds.Tables[2].Rows[i]["三个月内应收款"].ToString()), 2);
                    }
                }
            }
            for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
            {
                int isHave = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["客户名称"].ToString() == ds.Tables[3].Rows[i]["客户名称"].ToString())
                    {
                        if (ds.Tables[3].Rows[i]["三个月以上应收款"].ToString() != "")
                        {
                            dt.Rows[j]["三个月以上应收款"] = Math.Round(Convert.ToDouble(ds.Tables[3].Rows[i]["三个月以上应收款"].ToString()), 2);;
                        }
                        isHave = 1;
                    }
                }
                if (isHave == 0)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["客户名称"] = ds.Tables[3].Rows[i]["客户名称"].ToString();
                    if (ds.Tables[3].Rows[i]["三个月以上应收款"].ToString() != "")
                    {
                        dt.Rows[dt.Rows.Count - 1]["三个月以上应收款"] = Math.Round(Convert.ToDouble(ds.Tables[3].Rows[i]["三个月以上应收款"].ToString()), 2); ;
                    }
                }
            }
            for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
            {
                int isHave = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["客户名称"].ToString() == ds.Tables[4].Rows[i]["客户名称"].ToString())
                    {
                        if (ds.Tables[4].Rows[i]["应收款总额"].ToString() != "")
                        {
                            dt.Rows[j]["应收款总额"] = Math.Round(Convert.ToDouble(ds.Tables[4].Rows[i]["应收款总额"].ToString()), 2); 
                        }
                        isHave = 1;
                    }
                }
                if (isHave == 0)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["客户名称"] = ds.Tables[4].Rows[i]["客户名称"].ToString();
                    if (ds.Tables[4].Rows[i]["应收款总额"].ToString() != "")
                    {
                        dt.Rows[dt.Rows.Count - 1]["应收款总额"] = Math.Round(Convert.ToDouble(ds.Tables[4].Rows[i]["应收款总额"].ToString()), 2);
                    }
                }
            }
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
            str = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        }
        return "{\"total\":" + dt.Rows.Count + " ,\"rows\":" + str + "}";
    }
    #endregion

    private string GetDataByXSMBDCJD() {
        WebServices.Services Resource = new WebServices.Services();
        string str = "";
        DataTable dt = new DataTable();
        string seaBegDate = Request["seaBegDate"];
        string seaEndDate = Request["seaEndDate"];
        if (seaBegDate != null && seaBegDate != "" && seaEndDate != null && seaEndDate != "")
        {
            string sql = "select C3_403033098027 销售  ,SUM(C3_403033088714 ) 开票金额  from CT401290382901 ";
            sql += " WHERE  C3_403033107511 BETWEEN '" + Convert.ToDateTime(seaBegDate).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(seaEndDate).ToString("yyyy-MM-dd") + "' GROUP BY C3_403033098027; ";
            sql += " select  C3_403033098027 销售   ,SUM(C3_401795702260 ) 项目收款额 from  CT401290354058 ";
            sql += " inner join CT401290382901 on C3_403271229261= C3_403193114292  ";
            sql += " WHERE C3_475688833284 BETWEEN '" + Convert.ToDateTime(seaBegDate).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(seaEndDate).ToString("yyyy-MM-dd") + "' group by C3_403033098027;  ";
            sql += " select C3_403033098027 销售  ,SUM(C3_403033088714 ) 开票金额  from CT401290382901 ";
            sql += " WHERE  C3_403033107511 BETWEEN '" + Convert.ToDateTime(seaBegDate).AddYears(-1).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(seaEndDate).AddYears(-1).ToString("yyyy-MM-dd") + "' GROUP BY C3_403033098027; ";
            sql += " select  C3_403033098027 销售   ,SUM(C3_401795702260 ) 项目收款额 from  CT401290354058 ";
            sql += " inner join CT401290382901 on C3_403271229261= C3_403193114292  ";
            sql += " WHERE C3_475688833284 BETWEEN '" + Convert.ToDateTime(seaBegDate).AddYears(-1).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(seaEndDate).AddYears(-1).ToString("yyyy-MM-dd") + "' group by C3_403033098027;  ";
            string[] changePassWord = Common.getChangePassWord();
            DataSet ds = Resource.SelectData(sql, changePassWord[0], changePassWord[1], changePassWord[2]);
            dt.Columns.Add("销售");
            dt.Columns.Add("当期开票金额");
            dt.Columns.Add("当期回款金额");
            dt.Columns.Add("上期开票金额");
            dt.Columns.Add("上期回款金额");
            dt.Columns.Add("开票金额同比增长率");
            dt.Columns.Add("回款金额同比增长率");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[dt.Rows.Count - 1]["销售"] = ds.Tables[0].Rows[i]["销售"].ToString();
                dt.Rows[dt.Rows.Count - 1]["当期开票金额"] = ds.Tables[0].Rows[i]["开票金额"].ToString();
            }
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                int isHave = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["销售"].ToString()== ds.Tables[1].Rows[i]["销售"].ToString())
                    {
                        dt.Rows[j]["当期回款金额"] = ds.Tables[1].Rows[i]["项目收款额"].ToString();
                        isHave = 1;
                    }   
                }
                if (isHave==0)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["销售"] = ds.Tables[1].Rows[i]["销售"].ToString();
                    dt.Rows[dt.Rows.Count - 1]["当期回款金额"] = ds.Tables[1].Rows[i]["项目收款额"].ToString();
                }
            }
            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                int isHave = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["销售"].ToString() == ds.Tables[2].Rows[i]["销售"].ToString())
                    {
                        dt.Rows[j]["上期开票金额"] = ds.Tables[2].Rows[i]["开票金额"].ToString();
                        isHave = 1;
                    }
                }
                if (isHave == 0)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["销售"] = ds.Tables[2].Rows[i]["销售"].ToString();
                    dt.Rows[dt.Rows.Count - 1]["上期开票金额"] = ds.Tables[2].Rows[i]["开票金额"].ToString();
                }
            }
            for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
            {
                int isHave = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["销售"].ToString() == ds.Tables[3].Rows[i]["销售"].ToString())
                    {
                        dt.Rows[j]["上期回款金额"] = ds.Tables[3].Rows[i]["项目收款额"].ToString();
                        isHave = 1;
                    }
                }
                if (isHave == 0)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["销售"] = ds.Tables[3].Rows[i]["销售"].ToString();
                    dt.Rows[dt.Rows.Count - 1]["上期回款金额"] = ds.Tables[3].Rows[i]["项目收款额"].ToString();
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["上期开票金额"].ToString()!=""&&dt.Rows[i]["当期开票金额"].ToString()!=""&&dt.Rows[i]["上期开票金额"].ToString()!="0")
	            {
                    dt.Rows[i]["开票金额同比增长率"] = Math.Round((Convert.ToDouble(dt.Rows[i]["当期开票金额"].ToString()) / Convert.ToDouble(dt.Rows[i]["上期开票金额"].ToString()) - 1) * 10000) / 100 + "%";
	            }
	            if (dt.Rows[i]["当期回款金额"].ToString()!=""&&dt.Rows[i]["上期回款金额"].ToString()!=""&&dt.Rows[i]["上期回款金额"].ToString()!="0")
	            {
                    dt.Rows[i]["回款金额同比增长率"] = Math.Round((Convert.ToDouble(dt.Rows[i]["当期回款金额"].ToString()) / Convert.ToDouble(dt.Rows[i]["上期回款金额"].ToString()) - 1) * 10000) / 100 + "%";
	            }
            }
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
            str = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        }
        return str;
    }

    private string GetDataByFXSXM()
    {
        WebServices.Services Resource = new WebServices.Services();
        string str = "";
        DataTable dt = new DataTable();
        string seaBegDate = Request["seaBegDate"];
        string seaEndDate = Request["seaEndDate"];
        if (seaBegDate != null && seaBegDate != "" && seaEndDate != null && seaEndDate != "")
        {
            string sql = "SELECT   C3_285435595578 销售, COUNT(C3_285435595640) 累计新开项目, SUM(CASE WHEN ISNULL(C3_285435594890,0)=0 THEN 1 ELSE 0 END) 累计未确认项目,";
            sql += " SUM(CASE WHEN ISNULL(C3_285435594890,0)<>0 AND ISNULL(C3_332437985389,0)=0 THEN 1 ELSE 0 END) 累计已确认未开票项目";
            sql += " from CT285435593984 WHERE C3_285435594453 BETWEEN '" + Convert.ToDateTime(seaBegDate).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(seaEndDate).ToString("yyyy-MM-dd") + "' GROUP BY C3_285435595578";
            string[] changePassWord = Common.getChangePassWord();
            dt = Resource.SelectData(sql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
            str = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        }
        return str;
    }

    private string GetDataByWGJKHTS()
    {
        WebServices.Services Resource = new WebServices.Services();
        string str = "";
        DataTable dt = new DataTable();

        string Condition = Request["Condition"];
        string Seaxs = Request["Seaxs"];
        
       
        string sql = "select C3_401291000526 客户全称 ,C3_551182758185 分配  from CT401290367151 ";
        sql += " WHERE  isnull(C3_551182775673,'1900-01-01') <dateadd(month,-2,GETDATE()) ";
        sql += " and isnull(C3_551182786094,'1900-01-01') <dateadd(month,-6,GETDATE())  ";
        if (Seaxs != "" && Seaxs!=null)
        {
            sql += " and C3_401291000526  like '%" + Seaxs + "%'";
        }
        if (Condition != "")
        {
            sql += Condition;
        }
        sql += " order by C3_551182758185 asc,C3_401291000526 asc";
        string[] changePassWord = Common.getChangePassWord();
        dt = Resource.SelectData(sql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
        str = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
      
        return str;
    }
    

    private string GetDataByFKHXM()
    {
        WebServices.Services Resource = new WebServices.Services();
        string str = "";
        DataTable dt = new DataTable();
        string seaBegDate = Request["seaBegDate"];
        string seaEndDate = Request["seaEndDate"];
        if (seaBegDate != null && seaBegDate != "" && seaEndDate != null && seaEndDate != "")
        {
            string sql = "SELECT   C3_403366466511 客户全称, COUNT(C3_285435595640) 累计新开项目, SUM(CASE WHEN ISNULL(C3_285435594890,0)=0 THEN 1 ELSE 0 END) 累计未确认项目,";
            sql += " SUM(CASE WHEN ISNULL(C3_285435594890,0)<>0 AND ISNULL(C3_332437985389,0)=0 THEN 1 ELSE 0 END) 累计已确认未开票项目";
            sql += " from CT285435593984 WHERE C3_285435594453 BETWEEN '" + Convert.ToDateTime(seaBegDate).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(seaEndDate).ToString("yyyy-MM-dd") + "' GROUP BY C3_403366466511";
            string[] changePassWord = Common.getChangePassWord();
            dt = Resource.SelectData(sql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
            str = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        }
        return str;
    }

    private string GetDataByFDDXM()
    {
        WebServices.Services Resource = new WebServices.Services();
        string str = "";
        DataTable dt = new DataTable();
        string seaBegDate = Request["seaBegDate"];
        string seaEndDate = Request["seaEndDate"];
        if (seaBegDate != null && seaBegDate != "" && seaEndDate != null && seaEndDate != "")
        {
            string sql = "SELECT   C3_316013848869 督导, COUNT(C3_285435595640) 累计新开项目, SUM(CASE WHEN ISNULL(C3_285435594890,0)=0 THEN 1 ELSE 0 END) 累计未确认项目,";
            sql += " SUM(CASE WHEN ISNULL(C3_285435594890,0)<>0 AND ISNULL(C3_332437985389,0)=0 THEN 1 ELSE 0 END) 累计已确认未开票项目";
            sql += " from CT285435593984 WHERE C3_285435594453 BETWEEN '" + Convert.ToDateTime(seaBegDate).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(seaEndDate).ToString("yyyy-MM-dd") + "' GROUP BY C3_316013848869";
            string[] changePassWord = Common.getChangePassWord();
            dt = Resource.SelectData(sql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
            Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
            str = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        }
        return str;
    }

    private string GetDataByWGJKHTX()
    {
        WebServices.Services Resource = new WebServices.Services();
        string str = "";
        DataTable dt = new DataTable();
        string sql = "select C3_404495328556 客户全称,C3_423050441017 销售   from CT401290331886  WHERE  C3_404495328556 NOT IN ( ";
        sql += " select C3_480524157728 from CT480524075049 WHERE C3_480524183775 BETWEEN '" + DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd") + "' AND GETDATE()) ";
        if (Request["Seaxs"]!=null&&Request["Seaxs"].ToString()!="")
        {
            sql += " and C3_423050441017 like '%" + Request["Seaxs"].ToString() + "%'";
        }
        sql +=" GROUP BY C3_404495328556 ,C3_423050441017  ORDER BY C3_423050441017 ";
        string[] changePassWord = Common.getChangePassWord();
        dt = Resource.SelectData(sql, changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];
        Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
        timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd";
        str = Newtonsoft.Json.JsonConvert.SerializeObject(dt, timeConverter);
        return str;
    }
    
    
}