using Application.DataTransferObjects.Category;
using Application.DataTransferObjects.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
	public interface ICategoryRepository
	{
		Task<IEnumerable<CategoryDto>> GetAllCategories();
		Task<ResponseObject<CategoryDto>> GetCategoryById(Guid id);
		Task<ResponseObject<CategoryDto>> CreateCategory(CategoryCreateDto categoryCreateDto);
		Task<ResponseObject<CategoryDto>> UpdateCategory(Guid id, CategoryUpdateDto categoryUpdateDto);
		Task<ResponseObject<CategoryDto>> DeleteCategory(Guid id);
		
	}
}
