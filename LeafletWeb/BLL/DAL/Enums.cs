using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMShareBLL.DAL
{
    /// <summary>
    /// 用户身份
    /// </summary>
    public enum Privilege
    {
        Forecaster,
        Reviser,
    }
    /// <summary>
    /// 用户权限
    /// </summary>
    public enum Authorization
    {
        Forecaster,
        Admin,
        Default,
    }
    /// <summary>
    /// 操作所属模块
    /// </summary>
    public enum OperateType
    {
        ForecastReport,
        AlarmReport,
        GISRegion,
        SystemManager,
        DailyMaintain,
    }

    /// <summary>
    /// 操作结果
    /// </summary>
    public enum OperateResult
    {
        Success,
        Fail,
        Undone,
    }
}
