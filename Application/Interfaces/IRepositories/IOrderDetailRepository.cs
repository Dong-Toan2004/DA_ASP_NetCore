using Application.DataTransferObjects.OrderDetail;
using Application.DataTransferObjects.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
	public interface IOrderDetailRepository
	{
		Task<ResponseObject<IEnumerable<OrderDetailDto>>> GetAll(Guid userId);
		Task<ResponseObject<OrderDetailDto>> GetById(Guid id);

	}
}
