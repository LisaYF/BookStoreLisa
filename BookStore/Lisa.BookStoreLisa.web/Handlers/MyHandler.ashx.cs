using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lisa.BookStoreLisa.web.Handlers
{
    /// <summary>
    /// MyHandler 的摘要说明
    /// </summary>
    public class MyHandler : IHttpHandler//继承自IHttpHandler
    {
        //动态网站的所有请求都是交给Handler来处理

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";//类型返回TEXT
            context.Response.Write("<html>");//生成html文档
            context.Response.Write("<head>");
            context.Response.Write("<Title>BookStoreLisa");
            context.Response.Write("</Title>");
            context.Response.Write("</head>");
            context.Response.Write("<body>");
            context.Response.Write("Hello World");//返回HELLOWORLD字符串
            context.Response.Write("</body>");
            context.Response.Write("</html>");//全是字符串
        }//处理请求
        //从数据库取出来交给HTML
        public bool IsReusable//是否可以被重用
        {
            get
            {
                return false;
            }
        }
    }
}