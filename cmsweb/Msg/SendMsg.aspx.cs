using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

    public partial class Msg_SendMsg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string Message = content.Text.Trim();
            string MobileNo = mobile.Text.Trim();
            if (Message != null && MobileNo!=null)
            {
                string sendURL = "http://106.3.37.122:9999/sms.aspx?action=send";
                sendURL += "&userid=12";
                sendURL += "&account=三盟";
                sendURL += "&password=123";
                sendURL += "&mobile=15023239810,13527576163";
                sendURL += "&content=" + System.Web.HttpUtility.UrlEncode(Message);
                sendURL += "&sendTime=";
                sendURL += "&content=测试数据";
                //发送请求
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(sendURL);
                //接受请 求
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                Stream receiveStream = myHttpWebResponse.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, System.Text.Encoding.UTF8);
                //此为要取页面的返回值输出的返回结果
                string returnValue = readStream.ReadToEnd();
            }
           

        }
}