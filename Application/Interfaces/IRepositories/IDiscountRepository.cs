using Application.DataTransferObjects.Discount;
using Application.DataTransferObjects.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
	public interface IDiscountRepository
	{
		Task<IEnumerable<DiscountDto>> GetAll(DiscountSearch discountSearch);
		Task<ResponseObject<DiscountDto>> GetById(Guid id);
		Task<ResponseObject<DiscountDto>> CreateDiscount(DiscountCreateDto discountCreateDto);
		Task<ResponseObject<DiscountDto>> UpdateDiscount(Guid id, DiscountUpdateDto discountUpdateDto);
		Task<ResponseObject<DiscountDto>> DeleteDiscount(Guid id);
	}
}
