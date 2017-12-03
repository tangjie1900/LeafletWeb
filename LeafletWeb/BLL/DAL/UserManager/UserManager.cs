using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Readearth.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using log4net;

namespace MMShareBLL.DAL
{
    public class UserManager
    {
        protected static readonly log4net.ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        OraDatabase m_Database;
        //public string m_Authority;

        public UserManager()
        {
            m_Database = new OraDatabase();
        }
        public UserManager(OraDatabase db)
        {
            m_Database = db;
        }
        private string GetTime(string alias,int JB)
        {
            DateTime dtNow = DateTime.Now;
            //LoginTime loginTime = new LoginTime();
            //loginTime.Local = dtNow.ToString("yyyy年MM月dd日");
            //loginTime.Universal = dtNow.ToUniversalTime().ToString("yyyy年MM月dd日");

            //return loginTime;
            //return "{Local:'2011年03月05日',Universal:'2011年03月05日'}";
            return "Alias:'" + alias + "',Local:'" + dtNow.ToString("yyyy年MM月dd日") + "',JB:" + JB.ToString();
        }
        /// <summary>
        /// 通过用户名和密码，验证用户的合法性
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="pssword">密码</param>
        /// <returns>成功登录返回用户别名</returns>
        public string Login(string userName, string pssword)
        {
            string strSQL = "SELECT * FROM TBUSERS WHERE USERNAME = '" + userName + "' AND Password = '" + pssword + "'";
            string strAlias = "{}";
            try
            {
                using (DataTable dt = m_Database.GetDataset(strSQL).Tables[0])
                {
                    if (dt.Rows.Count>0)
                    {

                        strAlias = "{\"UserName\":\"" + dt.Rows[0]["Username"].ToString() + "\",\"Alias\":\"" + dt.Rows[0]["UserAlias"].ToString() + "\"}";
                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return strAlias;
        }
    }
}
