using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.User;

namespace Manager.Controllers
{
    public class UserController : BaseController
    {

        /// <summary>
        /// 用户列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="userFilter">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetList([FromBody]dynamic userFilter)
        {
            Util.Return.ReturnValue<Models.Pagination<Models.User.UserInfoViewModel>> returnValue = new Util.Return.ReturnValue<Models.Pagination<Models.User.UserInfoViewModel>>();
            try
            {
                Models.User.UserFilterViewModel userFilterViewModel = 
                    Newtonsoft.Json.JsonConvert.DeserializeObject<Models.User.UserFilterViewModel>(
                    userFilter.ToString());
                Models.Pagination<Models.User.UserInfoViewModel> pagination = new Models.Pagination<Models.User.UserInfoViewModel>()
                {
                    CurrentPageIndex = userFilterViewModel.CurrentPageIndex
                    ,
                    PageSize = userFilterViewModel.PageSize
                };
                DataBll.User.UserInfoDataBll userInfoDataBll = new DataBll.User.UserInfoDataBll();
                Int32 totalRow;
                IList<UserInfo> users;
                (totalRow, users) = userInfoDataBll.Query(
                    userFilterViewModel.UserName?.Trim()
                    , userFilterViewModel.State.HasValue ? (Int32)userFilterViewModel.State.Value : 0
                    , userFilterViewModel.CurrentPageIndex, userFilterViewModel.PageSize);

                if (null != users && users.Count > 0)
                {
                    IList<Models.User.UserInfoViewModel> userViewModels = users.Select(
                        x => new Models.User.UserInfoViewModel()
                        {
                            Code = x.Code
                            ,
                            CreateTime = x.CreateTime.AddHours(8).ToString("yyyy-MM-dd HH:mm:ss")
                            ,
                            Creator = x.Creator
                            ,
                            Description = x.Description
                            ,
                            Id = x.ID
                            //, LastUpdateTime = x.LastUpdateTime
                            //, LastUpdator = x.LastUpdator
                            ,
                            Remark = x.Remark
                            ,
                            State = x.State.ToString()
                            ,
                            UserName = x.UserName
                        }
                        ).ToList();
                    pagination.Data = userViewModels;
                }
                pagination.TotalRow = totalRow;

                returnValue.Value = pagination;
                returnValue.IsOperateSuccess = true;
                returnValue.Description = "Success";
            }
            catch (Exception ex)
            {
                returnValue.IsOperateSuccess = false;
                returnValue.Description = $"Happened a error in server.The error message is {ex.Message}";
                ProcessException(returnValue.Description, ex);
            }

            return new JsonResult(returnValue);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        private IActionResult EditState(Model.User.UserInfo argUserInfo)
        {
            Util.Return.ReturnResult returnResult = new Util.Return.ReturnResult();
            try
            {
                DataBll.User.UserInfoDataBll userInfoDataBll = new DataBll.User.UserInfoDataBll();
                //1. 获取用户信息
                Model.User.UserInfo userInfo = userInfoDataBll.Get(argUserInfo.ID);
                //2. 进行用户信息的校验
                if (null != userInfo && userInfo.ID > 0)
                {
                    if (argUserInfo.State != userInfo.State)
                    {
                        //3. 更改用户状态
                        userInfo.State = argUserInfo.State;
                        userInfo.LastUpdateTime = System.DateTime.UtcNow;
                        userInfo.LastUpdator = UserName;
                        userInfoDataBll.Update(userInfo);
                    }
                }

                returnResult.IsOperateSuccess = true;
                returnResult.Description = "Success";
            }
            catch (Exception ex)
            {
                returnResult.IsOperateSuccess = false;
                returnResult.Description = $"Happened a error in server.The error message is {ex.Message}";
                ProcessException(returnResult.Description, ex);
            }

            return new JsonResult(returnResult);
        }

        /// <summary>
        /// 启用用户
        /// </summary>
        /// <param name="argUserInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EnableUser([FromBody]Model.User.UserInfo argUserInfo)
        {
            if (null == argUserInfo) argUserInfo = new UserInfo();
            argUserInfo.State = Model.EnumType.UserState.Normal;
            return EditState(argUserInfo);
        }

        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="argUserInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DisableUser([FromBody]Model.User.UserInfo argUserInfo)
        {
            if (null == argUserInfo) argUserInfo = new UserInfo();
            argUserInfo.State = Model.EnumType.UserState.Disable;
            return EditState(argUserInfo);
        }

    }
}