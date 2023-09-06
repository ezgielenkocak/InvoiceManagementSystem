using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.Entity.Enums
{
    public static class Permission
    {
        #region Users
        public static string per_adduser = "user_add";
        public static string per_getbyiduser = "user_get_by_id";
        public static string per_update_user = "user_update";
        public static string per_delete_user = "user_delete";
        public static string per_make_passive = "user_make_passive";
        #endregion

        #region Apartment
        public static string per_addapartment = "apartment_add";
        public static string per_delapartment = "apartment_delete";
        public static string per_get_by_id = "apartment_get_by_id";
        public static string per_update_apartment = "apartment_update";
        public static string per_getlist_apartment = "apartment_getlist";
        #endregion

        #region Bill
        public static string per_add_bill = "bill_add";
        public static string per_update_bill = "bill_update";
        public static string per_delete_bill = "bill_delete";
        public static string per_update_is_bill_payment_status = "bill_update_payment_status";
        #endregion

        #region UserApartment
        public static string per_add_user_apartment = "userapartment_add";
        public static string per_update_user_apartment = "userapartment_update";
        public static string per_getlist_user_apartment = "user_apartment_getlist";
        public static string per_getby_id_user_apartment = "user_apartment_getby_id";
        public static string per_delete_user_apartment = "user_apartment_delete";
        public static string per_add_multiple_user_apartment = "user_apartment_add_multiple";
        #endregion

        #region UserBill
        public static string per_add_user_bill = "user_bill_add";
        public static string per_add_multiple_bill = "user_bill_add_multiple";
        public static string per_get_active_list = "user_bill_get_active_list";
        public static string per_get_passive_list_user_bill = "user_bill_get_passive_list";
        public static string per_getlist_user_bill = "user_bill_getlist";
        public static string per_getby_id_user_bill = "user_bill_getby_id";
        public static string per_update_user_bill = "user_bill_update";
        public static string per_delete_by_passive_user_bill = "user_bill_delete_by_passive";
        public static string per_delete_user_bill = "user_bill_delete";



        #endregion
    }
}
