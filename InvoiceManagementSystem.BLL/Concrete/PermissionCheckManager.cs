using InvoiceManagementSystem.BLL.Abstract;
using InvoiceManagementSystem.BLL.Constants;
using InvoiceManagementSystem.Core.Result;
using InvoiceManagementSystem.DAL.Abstract;
using InvoiceManagementSystem.Entity.Dtos.PermissionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.BLL.Concrete
{
    public class PermissionCheckManager : IPermissionCheckService
    {
        private readonly IUserRoleGroupDal _userRoleGroupDal;
        private readonly IRoleGroupDal _roleGroupDal;
        private readonly IPermissionsDal _permissionsDal;
        private readonly IRoleGroupPermissionsDal _roleGroupPermissions;
        public PermissionCheckManager(IUserRoleGroupDal userRoleGroupDal, IRoleGroupDal roleGroupDal, IPermissionsDal permissionsDal, IRoleGroupPermissionsDal roleGroupPermissions)
        {
            _userRoleGroupDal = userRoleGroupDal;
            _roleGroupDal = roleGroupDal;
            _permissionsDal = permissionsDal;
            _roleGroupPermissions = roleGroupPermissions;
        }

        public IDataResult<bool> CheckPermission(PermissionCheckDto permissionCheckDto)
        {
            //try
            //{
                var userRoleGroup = _userRoleGroupDal.Get(x => x.UserId == permissionCheckDto.UserId);
                var roleGroup = _roleGroupDal.Get(x => x.Id == userRoleGroup.RoleGroupId);
                if (roleGroup.Status==false || roleGroup.ExpireDate<DateTime.Now)
                {
                    roleGroup.Status = false;
                    _roleGroupDal.Update(roleGroup);

                    var userList = _userRoleGroupDal.GetList(x => x.RoleGroupId == roleGroup.Id);
                    if (userList != null)
                    {
                        var admiRole = _roleGroupDal.Get(x => x.Name == "Admin");
                        var memberRole = _roleGroupDal.Get(x => x.Name == "Member");

                        foreach (var user in userList)
                        {
                            var matchUserRoleGroup = _userRoleGroupDal.Get(x => x.UserId == user.UserId);
                            matchUserRoleGroup.RoleGroupId = admiRole.Id;
                            _userRoleGroupDal.Update(matchUserRoleGroup);
                        }
                    }
                    return new ErrorDataResult<bool>(false, "Role Group Not Active", Messages.role_group_not_active);
                }
                var roleGroupPermissions = _roleGroupPermissions.GetList(x => x.RoleGroupId == roleGroup.Id);
                var apiPermission = _permissionsDal.Get(x => x.Name == permissionCheckDto.Permission && x.Status == true)?.Id;
                var permissionCheck = roleGroupPermissions.FirstOrDefault(x => x.PermissionId == apiPermission);
                if (permissionCheck != null)
                {
                    return new SuccessDataResult<bool>(true, "Ok", Messages.success);
                }
                return new ErrorDataResult<bool>(false, "Permission not found", Messages.permission_not_found);

            //}
            //catch (Exception e)
            //{
            //    return new ErrorDataResult<bool>(false,e.Message, Messages.unknown_err);

            //}
        }
    }
}
