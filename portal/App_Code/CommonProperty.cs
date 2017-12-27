using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServices;

/// <summary>
/// CommonProperty 的摘要说明
/// </summary>
public class CommonProperty
{
	public CommonProperty()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    static string _ManageDepartmentName = "";
    public  static string ManageDepartmentName
    {
        get {
             _ManageDepartmentName = System.Configuration.ConfigurationManager.AppSettings["ManageDepartmentName"].ToString();
            return _ManageDepartmentName;
        }
    }

    static string _City = "";
    public  static string City
    {
        get
        {
            if (_City.Trim() == "") _City = System.Configuration.ConfigurationManager.AppSettings["City"].ToString();
            return _City;
        }
    }

    static string _PortalResourceID = "";
    public static string PortalResourceID
    {
        get
        {
            if (_PortalResourceID.Trim() == "")
            {
                WebServices.Services Resource = new WebServices.Services();
                string des = Common.GetValueByKey("门户列表说明"); 
                _PortalResourceID = Resource.GetResourceIDByDescription(des);
            }
            return _PortalResourceID;
        }
    }

    static DataTable _PortalRights = null;
    public static DataTable PortalRights(UserInfo oUser)
    {
        WebServices.Services Resource = new WebServices.Services();
        if (_PortalRights == null)
        {
           _PortalRights= Resource.GetAllPortalOperationByResourceIDAndUserID(oUser.ID, _PortalResourceID,true );
        }
        else if (_PortalRights.Rows.Count == 0)
        {
            _PortalRights = Resource.GetAllPortalOperationByResourceIDAndUserID(oUser.ID, _PortalResourceID,true);
        }
        return _PortalRights;
    }    
}