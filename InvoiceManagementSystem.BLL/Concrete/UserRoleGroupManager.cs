using InvoiceManagementSystem.BLL.Abstract;
using InvoiceManagementSystem.BLL.Constants;
using InvoiceManagementSystem.Core.Result;
using InvoiceManagementSystem.DAL.Abstract;
using InvoiceManagementSystem.Entity.Concrete;
using InvoiceManagementSystem.Entity.Dtos.UserRoleGroupDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.BLL.Concrete
{
    public class UserRoleGroupManager : IUserRoleGroupService
    {
		private readonly IUserRoleGroupDal _userRoleGroupDal;

		public UserRoleGroupManager(IUserRoleGroupDal userRoleGroupDal)
		{
			_userRoleGroupDal = userRoleGroupDal;
		}

		public IDataResult<bool> AddForRegister(UserRoleGroupAddDto userRoleGroupAddDto)
        {
			try
			{
				var userExists=_userRoleGroupDal.Get(x=>x.UserId==userRoleGroupAddDto.UserId);
				if (userExists != null)
				{
					return new ErrorDataResult<bool>(false, "Kullanıcıya zaten rol atanmış", Messages.user_role_group_exist);
				}
				_userRoleGroupDal.Add(new UserRoleGroups
				{
					UserId=userRoleGroupAddDto.UserId,
					RoleGroupId=userRoleGroupAddDto.RoleGroupId,	
				});
				return new SuccessDataResult<bool>(true, "Ok", Messages.success);
			}
			catch (Exception e)
			{

				return new ErrorDataResult<bool>(false, e.Message, Messages.unknown_err);
			}
        }
    }
}
