using Application.DataTransferObjects.Category;
using Application.DataTransferObjects.ViewModels;
using Application.Interfaces.IRepositories;
using Domain.Entity;
using Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implements.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		public readonly WebBanDoAnDbContext _dbContext;

		public CategoryRepository(WebBanDoAnDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<ResponseObject<CategoryDto>> CreateCategory(CategoryCreateDto categoryCreateDto)
		{
			 var categoryCheckName = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == categoryCreateDto.Name);
			if (categoryCheckName != null)
			{
				return new ResponseObject<CategoryDto>
				{
					Status = 400,
					Message = "Tên danh mục đã tồn tại",
					Data = null
				};
			}
			if (string.IsNullOrEmpty(categoryCreateDto.Name))
			{
				return new ResponseObject<CategoryDto>
				{
					Status = 400,
					Message = "Tên danh mục không được để trống",
					Data = null
				};
			}
			var newCategory = new Category
			{
				Id = Guid.NewGuid(),
				Name = categoryCreateDto.Name,
				ImageUrl = categoryCreateDto.ImageUrl
			};
			await _dbContext.Categories.AddAsync(newCategory);
			await _dbContext.SaveChangesAsync();
			return new ResponseObject<CategoryDto>
			{
				Status = 200,
				Message = "Tạo danh mục thành công",
				Data = new CategoryDto
				{
					Id = newCategory.Id,
					Name = newCategory.Name,
					ImageUrl = newCategory.ImageUrl
				}
			};
		}

		public async Task<ResponseObject<CategoryDto>> DeleteCategory(Guid id)
		{
			var categoryToDelete = await _dbContext.Categories.FindAsync(id);
			if (categoryToDelete == null)
			{
				return new ResponseObject<CategoryDto>
				{
					Status = 404,
					Message = "Danh mục không tồn tại",
					Data = null
				};
			}
			_dbContext.Categories.Remove(categoryToDelete);
			await _dbContext.SaveChangesAsync();
			return new ResponseObject<CategoryDto>
			{
				Status = 200,
				Message = "Xóa danh mục thành công",
				Data = new CategoryDto
				{
					Id = categoryToDelete.Id,
					Name = categoryToDelete.Name,
					ImageUrl = categoryToDelete.ImageUrl
				}
			};
		}

		public async Task<IEnumerable<CategoryDto>> GetAllCategories()
		{
			var categories = await _dbContext.Categories.ToListAsync();
			return categories.Select(c => new CategoryDto
				{
					Id = c.Id,
					Name = c.Name,
					ImageUrl = c.ImageUrl
				}
			);
		}

		public async Task<ResponseObject<CategoryDto>> GetCategoryById(Guid id)
		{
			var category = await _dbContext.Categories.FindAsync(id);
			if (category == null)
			{
				return new ResponseObject<CategoryDto>
				{
					Status = 404,
					Message = "Danh mục không tồn tại",
					Data = null
				};
			}
			return new ResponseObject<CategoryDto>
			{
				Status = 200,
				Message = "Lấy danh mục thành công",
				Data = new CategoryDto
				{
					Id = category.Id,
					Name = category.Name,
					ImageUrl = category.ImageUrl
				}
			};
		}

		public async Task<ResponseObject<CategoryDto>> UpdateCategory(Guid id, CategoryUpdateDto category)
		{
			var categoryToUpdate = await _dbContext.Categories.FindAsync(id);
			if (categoryToUpdate == null)
			{
				return new ResponseObject<CategoryDto>
				{
					Status = 404,
					Message = "Danh mục không tồn tại",
					Data = null
				};
			}
			var categoryCheckName = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == category.Name && c.Id != id);
			if (categoryCheckName != null)
			{
				return new ResponseObject<CategoryDto>
				{
					Status = 400,
					Message = "Tên danh mục đã tồn tại",
					Data = null
				};
			}
			if (string.IsNullOrEmpty(category.Name))
			{
				return new ResponseObject<CategoryDto>
				{
					Status = 400,
					Message = "Tên danh mục không được để trống",
					Data = null
				};
			}
			categoryToUpdate.Name = category.Name;	
			categoryToUpdate.ImageUrl = category.ImageUrl;
			_dbContext.Categories.Update(categoryToUpdate);
			await _dbContext.SaveChangesAsync();
			return new ResponseObject<CategoryDto>
			{
				Status = 200,
				Message = "Cập nhật danh mục thành công",
				Data = new CategoryDto
				{
					Id = categoryToUpdate.Id,
					Name = categoryToUpdate.Name,
					ImageUrl = categoryToUpdate.ImageUrl
				}
			};
		}
	}
}
