using Application.DataTransferObjects.Product;
using Application.DataTransferObjects.ViewModels;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
	public interface IProductRepository
	{		
		Task<IEnumerable<ProductDto>> GetAllProducts();
		Task<ResponseObject<ProductDto>> ProductById(Guid id);
		Task<ResponseObject<ProductDto>> CreateProduct(ProductCreatDto product);
		Task<ResponseObject<ProductDto>> UpdateProduct(Guid Id,ProductUpdateDto product);
		Task<ResponseObject<ProductDto>> DeleteProduct(Guid id);
	}
}
