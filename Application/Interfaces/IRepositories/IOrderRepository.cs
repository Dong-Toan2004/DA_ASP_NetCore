using Application.DataTransferObjects.Order;
using Application.DataTransferObjects.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
	public interface IOrderRepository
	{
		Task<ResponseObject<IEnumerable<OrderDto>>> GetAll(Guid userId);
		Task<ResponseObject<OrderDto>> GetById(Guid id);
		Task<ResponseObject<OrderDto>> UpdateOrder(Guid id, OrderUpdateDto orderUpdateDto);
		Task<ResponseObject<OrderDto>> DeleteOrder(Guid id);
		Task<ResponseObject<IEnumerable<OrderWithDetailsDto>>> GetGroupByOrder(Guid userId);
	}
}
