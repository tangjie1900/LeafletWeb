using System;
using System.Data;

namespace MMShareBLL.DAL
{
    /// <summary>
    /// User 的摘要说明
    /// </summary>
    public class User
    {
        #region  字段
        private string m_ID;
        private string m_Username;
        private string m_Password;
        private Privilege m_Privilege;
        private Authorization m_Authirization;
        private string m_Alias;
        private string m_MobilePhone;
        #endregion

        #region  构造函数

        public User(string strUserName, string strPassword, Privilege privilege)
        {
            this.m_Username = strUserName;
            this.m_Password = strPassword;
            this.m_Privilege = privilege;
        }

        public User(string strUserName, string strPassword, Privilege privilege, string ID)
            : this(strUserName, strPassword, privilege)
        {
            this.m_ID = ID;
        }

        #endregion

        #region  属性
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            set { this.m_Username = value; }
            get { return this.m_Username; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            set { this.m_Password = value; }
            get { return this.m_Password; }
        }
        /// <summary>
        /// 用户身份
        /// </summary>
        public Privilege Privilege
        {
            get { return this.m_Privilege; }
        }
        /// <summary>
        /// 权限
        /// </summary>
        public Authorization Authorization
        {
            set { this.m_Authirization = value; }
            get { return this.m_Authirization; }
        }

        /// <summary>
        /// ID
        /// </summary>
        public string ID
        {
            set { this.m_ID = value; }
            get { return this.m_ID; }
        }
        /// <summary>
        /// 用户描述
        /// </summary>
        public string Alias
        {
            set { this.m_Alias = value; }
            get { return this.m_Alias; }
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string MobilePhone
        {
            set { this.m_MobilePhone = value; }
            get { return this.m_MobilePhone; }
        }
        #endregion
    }
}