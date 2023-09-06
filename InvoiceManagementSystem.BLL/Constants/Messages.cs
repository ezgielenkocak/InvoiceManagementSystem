using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.BLL.Constants
{
    public static class Messages
    {

        public static string err_null = "err_null"; //null değer
        public static string success = "operation_success"; // işlem başarılı
        public static string unknown_err = "unknown_err"; //bilinmeyen hata oluştu
        public static string data_not_found = "data_not_found"; //data bulunamadı

        public static string delete_operation_is_successfull = "delete_operation_is_successfull"; //silme işlemi başarılı

        public static string delete_operation_fail = "delete_operation_fail"; //silme işlemi başarısız
        public static string update_operation_fail = "update_operation_fail";

        public static string status_updated_already_paid = "status updated to already paid";
        public static string data_is_required = " data_is_required"; //girilen parametre null geldi fakat girilmesi zorunlu alan

        #region User
        public static string user_already_has_an_invoice = "user_already_has_in_invoice"; // işlem başarılı
        public static string user_not_active = "user_not_active"; // işlem başarılı
        public static string user_wrong_password = "user_wrong_password"; // şifre hatalı
        #endregion

        #region Auth Messages
        public static string already_mail_registered = "err_mail_already_registered";
        public static string invalid_email = "err_invalid_email";

        public static string user_not_found = "err_user_not_found";
        public static string already_phone_number_exists = "err_phone_number_already_exists";
        public static string invalid_phone_number = "err_phone_number_already_exists";
        public static string mails_not_matching = " mails_not_matching";
        public static string password_cant_contain_name = "password_cant_contain_name";
        public static string password_cant_contain_surname = "password_cant_contain_surname";
        public static string user_wrong_code = "user_wrong_code";
        public static string password_are_not_match = "password_are_not_match";
        public static string current_password_is_wrong = "current_password_is_wrong";
        public static string user_control_code_is_null = "user_control_code_is_null";
        public static string user_control_code_is_wrong = "user_control_code_is_wrong";
        public static string err_code_expired = "err_code_expired";
        public static string token_not_found = "token_not_found";
        public static string token_expired = "token_expired";
        #endregion

        #region Token
        public static string generate_token_failed = "err_generate_token_failed";

        #endregion

        #region RoleGroup
        public static string role_group_not_active = "role_group_not_active";
        #endregion

        #region Permissions
        public static string permission_not_found = "permission_not_found";

        #endregion

        #region UserRoleGroups
        public static string user_role_group_exist = "user_role_group_exist";
        #endregion
    }
}
