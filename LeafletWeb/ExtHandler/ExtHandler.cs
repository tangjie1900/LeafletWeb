﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.SessionState;
using log4net;

namespace ExtHandler
{

    public class PatrolHandler : IHttpHandler, IRequiresSessionState
    {
        //用于记录系统错误日志
        //在Web中，需要先执行此语句，此语句在Global.asax中。log4net.Config.XmlConfigurator.Configure();
        protected static readonly log4net.ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            //为了防止浏览器缓冲服务器返回的内容，通过回应头禁止客户端的缓存，代码如下所示：
            //context.Response.Cache.SetCacheability(System.Web.HttpCacheability.
            //    NoCache);  
            //try
            //{

            //请求必须具有provider和method参数
            string strProvider = context.Request["provider"];
            string strMethod = context.Request["method"];

            ////判断用户是否已经登录，否则返回空
            //if (context.Session["SYSTEMUSERID"] == null && (strProvider != "MMShareBLL.DAL.UserManager,MMShareBLL" && strMethod != "Login"))
            //    return;


            //反射机制
            //创建类
            string objName = strProvider.Substring(0, strProvider.IndexOf("."));
            strProvider = strProvider + "," + objName;//获取类名
            Type ht = Type.GetType(strProvider);

            object obj = Activator.CreateInstance(ht);
            //设置用户ID

            //获取函数
            MethodInfo mi = ht.GetMethod(strMethod);
            //MethodInfo setUserID = ht.GetMethod("setUserID");
            //string[] userIndex = new string[1];

            //if (setUserID != null && context.Request.Cookies["User"]["cLimit"] != null && context.Request.Cookies["User"]["cLimit"]!="")
            //{
            //    userIndex[0] = context.Request.Cookies["User"]["cLimit"];
            //    setUserID.Invoke(obj, userIndex);
            //}

            //获取参数
            ParameterInfo[] parameters = mi.GetParameters();

            string[] args = new string[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                args[i] = context.Request[parameters[i].Name];
            }

            //调用函数
            object objRtn = mi.Invoke(obj, args);
            string jsonData = JsonHelper.ToJSON(objRtn);


            //对象JSON化，并返回到前台
            context.Response.Write(jsonData);

            //context.Response.End();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            //}
            //catch (Exception ex)
            //{
            //    context.Response.Status = ex.Message;
            //    context.Response.StatusCode = 500;
            //    m_Log.Error(ex.Source + ":" + ex.Message);

            //}
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

    }


}
