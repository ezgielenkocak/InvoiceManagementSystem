using Autofac;
using AutoMapper;
using InvoiceManagementSystem.BLL.Abstract;
using InvoiceManagementSystem.BLL.Abstract.RabitMQ;
using InvoiceManagementSystem.BLL.Concrete;
using InvoiceManagementSystem.BLL.Concrete.RabitMQ;
using InvoiceManagementSystem.Core.Security;
using InvoiceManagementSystem.DAL.Abstract;
using InvoiceManagementSystem.DAL.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Module = Autofac.Module;

namespace InvoiceManagementSystem.BLL.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<ApartmentManager>().As<IApartmentService>();
            builder.RegisterType<EfApartmentDal>().As<IApartmentDal>();

            builder.RegisterType<UserApartmentManager>().As<IUserApartmentService>();
            builder.RegisterType<EfUserApartmentDal>().As<IUserApartmentDal>();

            builder.RegisterType<BillManager>().As<IBillService>();
            builder.RegisterType<EfBillDal>().As<IBillDal>();

            builder.RegisterType<UserBillManager>().As<IUserBillService>();
            builder.RegisterType<EfUserBillDal>().As<IUserBillDal>();

            builder.RegisterType<EfBillTypesDal>().As<IBillTypesDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
          
            builder.RegisterType<EfRoleGroupDal>().As<IRoleGroupDal>();
            //builder.RegisterType<RoleGroupManager>().As<IRoleGroupService>();


            builder.RegisterType<EfUserSecurityHistoriesDal>().As<IUserSecurityHistoriesDal>();

            builder.RegisterType<EfUserSessionsDal>().As<IUserSessionsDal>();

            builder.RegisterType<EfRoleGroupPermissionsDal>().As<IRoleGroupPermissionsDal>();

            builder.RegisterType<SessionManager>().As<ISessionService>();


            builder.RegisterType<EfUserRoleGroupsDal>().As<IUserRoleGroupDal>();
            builder.RegisterType<UserRoleGroupManager>().As<IUserRoleGroupService>();



            builder.RegisterType<EfPermissionsDal>().As<IPermissionsDal>();

            builder.RegisterType<PermissionCheckManager>().As<IPermissionCheckService>();

            builder.RegisterType<BraingTreeManager>().As<IBraintreeService>();

            builder.RegisterType<RabitMQProducer>().As<IRabitMQProducer>();
        }
    }
}
