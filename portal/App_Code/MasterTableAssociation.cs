
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebServices;

/// <summary>
/// MasterTableAssociation 的摘要说明
/// </summary>
public class MasterTableAssociation
{
    /// <summary>
    /// ID
    /// </summary>
    public string MID { get; set; }
    /// <summary>
    /// 主表关键字
    /// </summary>
    public string MasterKeyWord { get; set; }
    /// <summary>
    /// 子表关键字
    /// </summary>
    public string ChildKeyWord { get; set; }
    /// <summary>
    /// 子表排序号
    /// </summary>
    public int ChildOrderNo { get; set; }
    /// <summary>
    /// 子表允许添加
    /// </summary>
    public bool AllowAdd { get; set; }
    /// <summary>
    /// 子表允许编辑
    /// </summary>
    public bool AllowEdit { get; set; }
    /// <summary>
    /// 子表允许删除
    /// </summary>
    public bool AllowDel { get; set; }
    /// <summary>
    /// 子表允许导出
    /// </summary>
    public bool AllowExport { get; set; }
    /// <summary>
    /// 台帐类型
    /// </summary>
    public string TZType { get; set; }
    /// <summary>
    /// 台帐主子表关联字段
    /// </summary>
    public string LedgerConditions { get; set; }
    /// <summary>
    /// 台帐子表引用字段
    /// </summary>
    public string LedgerChildKey { get; set; }
    /// <summary>
    /// 台账主表资源ID
    /// </summary>
    public string RSResID { get; set; }

    /// <summary>
    /// 默认排序
    /// </summary>
    public string DefaultSort { get; set; }
    /// <summary>
    /// 初始筛选条件
    /// </summary>
    public string InitialQueryStr { get; set; }
    /// <summary>
    /// 是否有添加修改删除后操作
    /// </summary>
    public bool HasLastOperation = false;
    /// <summary>
    /// 是否有添加修改删除前操作
    /// </summary>
    public bool HasFirstOperation = false;
    /// <summary>
    /// 子表资源ID
    /// </summary>
    public string ChildResId { get; set; }
    /// <summary>
    /// 显示标题
    /// </summary>
    public string ShowTitle { get; set; }
    /// <summary>
    /// 可查看的部门
    /// </summary>
    public string ViewDepartment { get; set; }
    /// <summary>
    /// 不可查看的部门
    /// </summary>
    public string NoViewDepartment { get; set; }
    /// <summary>
    /// 可查看的账号
    /// </summary>
    public string ViewAccount { get; set; }
    /// <summary>
    /// 不可查看的账号
    /// </summary>
    public string NoViewAccount { get; set; }
    /// <summary>
    /// 视图主表资源ID(当子表是视图时用)
    /// </summary>
    public string ViewBaseTableResid { get; set; }

    /// <summary>
    /// 获取主子表关联
    /// </summary>
    /// <param name="argDT"></param>
    /// <param name="argDepartmentName"></param>
    /// <param name="argUserID"></param>
    /// <param name="argIdstr"></param>
    /// <returns></returns>
    public static string GetMasterTableAssociations(DataTable argDT, string argDepartmentName, string argUserID, out string argIdstr)
    {
        List<MasterTableAssociation> vMasterTableAssociations = new List<MasterTableAssociation>();
        argIdstr = "";
        if (argDT.Rows.Count > 0)
        {
            foreach (DataRow vDr in argDT.Rows)
            {
                bool Isadd = false;
                string ViewDepartment = vDr["ViewDepartment"].ToString();
                string NoViewDepartment = vDr["NoViewDepartment"].ToString();

                string ViewAccount = vDr["ViewAccount"].ToString();
                string NoViewAccount = vDr["NoViewAccount"].ToString();


                //判断部门权限
                if (string.IsNullOrEmpty(argDepartmentName))
                {
                    Isadd = true;
                }
                else if (!string.IsNullOrEmpty(ViewDepartment))
                {
                    if (ViewDepartment.Contains("[" + argDepartmentName + "]"))
                    {
                        Isadd = true;
                    }
                    else
                    {
                        Isadd = false;
                    }
                }
                else if (!string.IsNullOrEmpty(NoViewDepartment))
                {
                    if (NoViewDepartment.Contains("[" + argDepartmentName + "]"))
                    {
                        Isadd = false;
                    }
                    else
                    {
                        Isadd = true;
                    }
                }
                else
                {
                    Isadd = true;
                }

                //判断账号权限
                if (!string.IsNullOrEmpty(argUserID))
                {
                    if (!string.IsNullOrEmpty(ViewAccount))
                    {
                        if (ViewAccount.Contains("[" + argUserID + "]"))
                        {
                            Isadd = true;
                        }
                    }
                    else if (!string.IsNullOrEmpty(NoViewAccount))
                    {
                        if (NoViewAccount.Contains("[" + argUserID + "]"))
                        {
                            Isadd = false;
                        }
                    }
                }

                if (Isadd)
                {
                    vMasterTableAssociations.Add(new MasterTableAssociation()
                    {
                        MID = vDr["id"].ToString(),
                        MasterKeyWord = vDr["MasterKeyWord"].ToString(),
                        ChildKeyWord = vDr["ChildKeyWord"].ToString(),
                        ChildOrderNo = string.IsNullOrEmpty(vDr["ChildOrderNo"].ToString()) ? 10 : Convert.ToInt32(vDr["ChildOrderNo"].ToString()),
                        AllowAdd = Convert.ToBoolean(vDr["AllowAdd"].ToString() == "1"),
                        AllowEdit = Convert.ToBoolean(vDr["AllowEdit"].ToString() == "1"),
                        AllowDel = Convert.ToBoolean(vDr["AllowDel"].ToString() == "1"),
                        AllowExport = Convert.ToBoolean(vDr["AllowExport"].ToString() == "1"),
                        TZType = vDr["TZType"].ToString(),
                        LedgerConditions = vDr["LedgerConditions"].ToString(),
                        LedgerChildKey = vDr["LedgerChildKey"].ToString(),
                        RSResID = vDr["RSResID"].ToString(),
                        ChildResId = vDr["ChildResId"].ToString(),
                        DefaultSort = vDr["DefaultSort"].ToString(),
                        InitialQueryStr = vDr["InitialQueryStr"].ToString(),
                        HasLastOperation = Convert.ToBoolean(vDr["HasLastOperation"].ToString() == "1"),
                        HasFirstOperation = Convert.ToBoolean(vDr["HasFirstOperation"].ToString() == "1"),
                        ViewDepartment = vDr["ViewDepartment"].ToString(),
                        NoViewDepartment = vDr["NoViewDepartment"].ToString(),
                        ViewAccount = vDr["ViewAccount"].ToString(),
                        NoViewAccount = vDr["NoViewAccount"].ToString(),
                        ViewBaseTableResid = vDr["ViewBaseTableResid"].ToString()
                    });

                    argIdstr += (string.IsNullOrEmpty(argIdstr) ? vDr["id"].ToString() : "," + vDr["id"].ToString());
                }
            }
        }
        return Newtonsoft.Json.JsonConvert.SerializeObject(vMasterTableAssociations);
    }



    public static MasterTableAssociation GetChileMasterTableAssociation(string argFatherKey, string argChileKey, string argTZ, string argMasterTableAssociationID)
    {
        MasterTableAssociation child = null;

        WebServices.Services Resource = new WebServices.Services();
        string[] changePassWord = Common.getChangePassWord();
        DataTable vdt = Resource.SelectData("select * from MasterTableAssociation  where id = '" + argMasterTableAssociationID + "'", changePassWord[0], changePassWord[1], changePassWord[2]).Tables[0];

        if (vdt == null && vdt.Rows.Count == 0) return child;

        DataRow vDr = vdt.Rows[0];
        child = new MasterTableAssociation()
        {
            MID = vDr["id"].ToString(),
            MasterKeyWord = vDr["MasterKeyWord"].ToString(),
            ChildKeyWord = vDr["ChildKeyWord"].ToString(),
            ChildOrderNo = string.IsNullOrEmpty(vDr["ChildOrderNo"].ToString()) ? 10 : Convert.ToInt32(vDr["ChildOrderNo"].ToString()),
            AllowAdd = Convert.ToBoolean(vDr["AllowAdd"].ToString() == "1"),
            AllowEdit = Convert.ToBoolean(vDr["AllowEdit"].ToString() == "1"),
            AllowDel = Convert.ToBoolean(vDr["AllowDel"].ToString() == "1"),
            AllowExport = Convert.ToBoolean(vDr["AllowExport"].ToString() == "1"),
            TZType = vDr["TZType"].ToString(),
            LedgerConditions = vDr["LedgerConditions"].ToString(),
            LedgerChildKey = vDr["LedgerChildKey"].ToString(),
            RSResID = vDr["RSResID"].ToString(),
            ChildResId = vDr["ChildResId"].ToString(),
            DefaultSort = vDr["DefaultSort"].ToString(),
            InitialQueryStr = vDr["InitialQueryStr"].ToString(),
            HasLastOperation = Convert.ToBoolean(vDr["HasLastOperation"].ToString() == "1"),
            HasFirstOperation = Convert.ToBoolean(vDr["HasFirstOperation"].ToString() == "1"),
            ViewDepartment = vDr["ViewDepartment"].ToString(),
            NoViewDepartment = vDr["NoViewDepartment"].ToString(),
            ViewAccount = vDr["ViewAccount"].ToString(),
            NoViewAccount = vDr["NoViewAccount"].ToString(),
            ViewBaseTableResid = vDr["ViewBaseTableResid"].ToString()
        };
        return child;
    }
}

 