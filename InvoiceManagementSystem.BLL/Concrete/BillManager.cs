using AutoMapper;
using InvoiceManagementSystem.BLL.Abstract;
using InvoiceManagementSystem.BLL.Constants;
using InvoiceManagementSystem.Core.Result;
using InvoiceManagementSystem.DAL.Abstract;
using InvoiceManagementSystem.Entity.Concrete;
using InvoiceManagementSystem.Entity.Dtos.BillDtos;
using InvoiceManagementSystem.Entity.Dtos.UserBillDtos;
using InvoiceManagementSystem.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagementSystem.BLL.Concrete
{
    public class BillManager : IBillService
    {
        private readonly IBillDal _billDal;
        private readonly ISessionService _sessionService;
        private readonly IBillTypesDal _billTypes;
        public BillManager(IBillDal billDal, ISessionService sessionService, IBillTypesDal billTypes)
        {
            _billDal = billDal;
            _sessionService = sessionService;
            _billTypes = billTypes;
        }

        public IDataResult<object> Add(AddMultipleBillDto addMultipleBillDto)
        {


            foreach (var item in addMultipleBillDto.Bills)
            {
                _billDal.Add(new Bill
                {
                    BillTypeId = item.BillTypeId,
                    BillPrice = item.Price,
                    EndPaymentDate = item.EndPaymentDate,
                    IsBillPayment = false,
                    CreatedDate = DateTime.Now
                });
            }

            var count = addMultipleBillDto.Bills.Count();


            return new SuccessDataResult<object>("Ok", $" {count} tane fatura oluşturuldu", Messages.success);

        }

        public IDataResult<bool> UpdateIsBillPaymentStatus(int id)
        {
            try
            {
                if (id != null)
                {
                    var result = _billDal.Get(x => x.Id == id);
                    if (result.IsBillPayment == true)
                    {
                        return new ErrorDataResult<bool>(true, "fatura durumu daha önce ödenmiş olarak güncellenmiş", Messages.status_updated_already_paid);
                    }
                    result.IsBillPayment = true;

                    _billDal.Update(result);
                    return new SuccessDataResult<bool>(true, "fatura durumu; 'ödenmiş' olarak güncellendi", Messages.success);
                }
                return new ErrorDataResult<bool>(false, " işlem yapmak için id  giriniz", Messages.err_null);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<bool>(false, e.Message, Messages.unknown_err);

            }
        }

        public IDataResult<ListBillDto> GetById(int id)
        {
            try
            {
                if (id == null)
                {
                    return new ErrorDataResult<ListBillDto>(null, "Fatura görüntülemek için id bilgisi giriniz", Messages.err_null);
                }
                var billgetbyid = _billDal.Get(x => x.Id == id);

                var billgetbyidDto = new ListBillDto()
                {
                    Id = billgetbyid.Id,
                    BillTypeId = billgetbyid.Id,
                    Price = billgetbyid.BillPrice,
                    EndPaymentDate = billgetbyid.EndPaymentDate,
                    CreatedDate = billgetbyid.CreatedDate,
                    IsBillPayment = billgetbyid.IsBillPayment


                };
                billgetbyid.Id = billgetbyidDto.Id;


                return new SuccessDataResult<ListBillDto>(billgetbyidDto, "listelendi", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<ListBillDto>(null, e.Message, Messages.unknown_err);
            }

        }

        public IDataResult<List<ListBillDto>> GetList()
        {
            try
            {
                var bills = _billDal.GetList();
                var billListDto = new List<ListBillDto>();

                foreach (var bill in bills)
                {
                    var billName = _billTypes.Get(x => x.Id == bill.BillTypeId).BillName;
                    billListDto.Add(new ListBillDto
                    {
                        Id = bill.Id,
                        BillTypeId=bill.BillTypeId,
                        BillName=billName,
                        Price = bill.BillPrice,
                        EndPaymentDate = bill.EndPaymentDate,
                        CreatedDate = bill.CreatedDate,
                        IsBillPayment = bill.IsBillPayment,



                    });
                }
                return new SuccessDataResult<List<ListBillDto>>(billListDto, "faturalar listelendi", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<List<ListBillDto>>(new List<ListBillDto>(), e.Message, Messages.unknown_err);

            }
        }

        public IDataResult<bool> Update(UpdateBillDto updateBillDto)
        {
            try
            {
                var result = _billDal.Get(x => x.Id == updateBillDto.Id);
                if (result != null)
                {
                    if (updateBillDto == null)
                    {
                        return new ErrorDataResult<bool>(false, "Güncelleme işlemi yapmak için tüm alanları doldurmak zorunludur", Messages.err_null);
                    }
                    #region Bu yapı sayesinde; 2 değer girip diğerlerini girmediğinde eski değerleri kalıyor sıfırlanmıyor
                    if (updateBillDto.BillTypeId != null)
                    {
                        #region VALUE KULLLANMA SEBEBİM
                        //Value:nullable değişken aslında iki farklı değeri barındırabilir.Asıl değeri ve null değeri. Asıl değer nullable değişkenin altında yatan değerdir. Yani 'nullable' değişkenin temel değeridir.

                        //Value: Özelliği yalnızca nullable değerlerin altındaki temel değerlere erişmek için kullanılır. Değişken null değerini ve kendi değerini içerdiğine göre value diyerek nullable değerin altındaki temel değere erişebilirim. DİKKAT: NULLABLE DEĞER ZATEN NULL İSE VE VALUE KULLANDIYSAN 'InvalidOperationException' döner
                        #endregion
                        result.BillTypeId = updateBillDto.BillTypeId.Value;
                    }
                    if (updateBillDto.Price != null)
                    {
                        result.BillPrice = updateBillDto.Price.Value;
                    }
                    if (updateBillDto.EndPaymentDate != null)
                    {
                        result.EndPaymentDate = updateBillDto.EndPaymentDate.Value;
                    }
                    if (updateBillDto.IsBillPayment != null)
                    {
                        result.IsBillPayment = updateBillDto.IsBillPayment.Value;
                    }
                    #endregion
                    _billDal.Update(result);
                    return new SuccessDataResult<bool>(true, "fatura güncellendi", Messages.success);
                }
                return new ErrorDataResult<bool>(false, "fatura güncellenemedi", Messages.update_operation_fail);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<bool>(false, e.Message, Messages.data_not_found);
            }
        }

        public IDataResult<bool> Delete(int id)
        {
            try
            {
                if (id != null)
                {
                    var result = _billDal.Get(x => x.Id == id);
                    _billDal.Delete(result);
                    return new SuccessDataResult<bool>(true, "fatura silindi", Messages.delete_operation_is_successfull);
                }
                return new ErrorDataResult<bool>(false, "fatura silinemedi", Messages.delete_operation_fail);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<bool>(false, e.Message, Messages.unknown_err);

            }
        }

        public IDataResult<Bill> Get(Expression<Func<Bill, bool>> filter)
        {
            try
            {
                var bill = _billDal.Get(filter);
                if (bill == null)
                {
                    return new ErrorDataResult<Bill>(null, "Bill not found", Messages.data_not_found);
                }
                return new SuccessDataResult<Bill>(bill, "Ok", Messages.success);
            }
            catch (Exception e)
            {

                return new ErrorDataResult<Bill>(null, e.Message, Messages.unknown_err);

            }
        }

        public IDataResult<BillListPagingDto> GetListWithPaging(BillGetlistFilterDto billGetlistFilterDto)
        {
            try
            {
                var billListDto = new List<ListBillDto>();
                var checkAllControls = _sessionService.TokenCheck(billGetlistFilterDto.Token);
                if (!checkAllControls.Data.Success)
                {
                    return new ErrorDataResult<BillListPagingDto>(new BillListPagingDto { Data = new List<ListBillDto>(), Paging = new Core.Pagination.PagingDto() }, checkAllControls.Message, checkAllControls.MessageCode);
                }

                var bilss = _billDal.GetList();
                foreach (var bill in bilss)
                {
                    var billName = _billTypes.Get(x => x.Id == bill.BillTypeId).BillName;
                    if (billName==null)
                    {
                        return new ErrorDataResult<BillListPagingDto>(new BillListPagingDto { Data = new List<ListBillDto>(), Paging = new Core.Pagination.PagingDto() }, "Bill not found", Messages.data_not_found);
                    }
                    billListDto.Add(new ListBillDto
                    {

                        Id = bill.Id,
                        BillTypeId = bill.BillTypeId,
                        BillName = billName,
                        EndPaymentDate = bill.EndPaymentDate,
                        CreatedDate = bill.CreatedDate,
                        IsBillPayment = bill.IsBillPayment,
                        Price = bill.BillPrice
                    });
                }
                if (!String.IsNullOrEmpty(billGetlistFilterDto.Search))
                {
                    billListDto = billListDto.Where(x => x.BillName.Trim().Contains(billGetlistFilterDto.Search.Trim().ToLower())).ToList();
                }

                if (billGetlistFilterDto.PagingFilter.Page<=1)
                {
                    billGetlistFilterDto.PagingFilter.Page = 0;
                }
                else
                {
                    billGetlistFilterDto.PagingFilter.Page--;
                }
                int total = billGetlistFilterDto.PagingFilter.Size * billGetlistFilterDto.PagingFilter.Page;
                var totalCount = (double)billListDto.Count;
                var totalPages = Math.Ceiling(totalCount / billGetlistFilterDto.PagingFilter.Size);
                billListDto = billListDto.Skip(total).Take(billGetlistFilterDto.PagingFilter.Size).ToList();

                var resultDto = new BillListPagingDto()
                {
                    Data = billListDto,
                    Paging = new Core.Pagination.PagingDto
                    {
                        Size = billGetlistFilterDto.PagingFilter.Size,
                        Page = billGetlistFilterDto.PagingFilter.Page + 1,
                        TotalCount = (int)totalCount,
                        TotalPage = (int)totalPages
                    }
                };
                return new SuccessDataResult<BillListPagingDto>(resultDto);

            }
            catch (Exception e)
            {

                return new ErrorDataResult<BillListPagingDto>(new BillListPagingDto { Data = new List<ListBillDto>(), Paging = new Core.Pagination.PagingDto() },


                e.Message, Messages.unknown_err) ;
            }
        }

        
    }
}
