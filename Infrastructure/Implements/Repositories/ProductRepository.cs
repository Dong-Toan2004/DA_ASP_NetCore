using Application.DataTransferObjects.Product;
using Application.DataTransferObjects.ViewModels;
using Application.Interfaces.IRepositories;
using AutoMapper;
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
	public class ProductRepository : IProductRepository
	{
		private readonly WebBanDoAnDbContext _dbContext;
		public ProductRepository( WebBanDoAnDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<ResponseObject<ProductDto>> CreateProduct(ProductCreatDto product)
		{
			 var productCheckName = await _dbContext.Products.FirstOrDefaultAsync(p => p.Name == product.Name);
			if (productCheckName != null)
			{
				return new ResponseObject<ProductDto>
				{
					Status = 400,
					Message = "Tên sản phẩm đã tồn tại",
					Data = null
				};
			}
			if (!string.IsNullOrEmpty(product.Name))
			{
				return new ResponseObject<ProductDto>
				{
					Status = 400,
					Message = "Tên sản phẩm không được để trống",
					Data = null
				};
			}
			if (product.Price <= 0)
			{
				return new ResponseObject<ProductDto>
				{
					Status = 400,
					Message = "Giá sản phẩm phải lớn hơn 0",
					Data = null
				};
			}
			var newProduct = new Product
			{
				Id = Guid.NewGuid(),
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				ImageUrl = product.ImageUrl,
				Status = product.Status,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow,
				CategoryId = product.CategoryId,
				DiscountId = product.DiscountId
			};
			_dbContext.Products.Add(newProduct);
			await _dbContext.SaveChangesAsync();
			return new ResponseObject<ProductDto>
			{
				Status = 201,
				Message = "Tạo sản phẩm thành công",
				Data = new ProductDto
				{
					Id = newProduct.Id,
					Name = newProduct.Name,
					Description = newProduct.Description,
					Price = newProduct.Price,
					ImageUrl = newProduct.ImageUrl,
					Status = newProduct.Status,
					CreatedAt = newProduct.CreatedAt,
					UpdatedAt = newProduct.UpdatedAt,
					CategoryId = newProduct.CategoryId,
					DiscountId = newProduct.DiscountId
				}
			};
		}

		public async Task<ResponseObject<ProductDto>> DeleteProduct(Guid id)
		{
			var productToDelete = await _dbContext.Products.FindAsync(id);
			if (productToDelete == null)
			{
				return new ResponseObject<ProductDto>
				{
					Status = 404,
					Message = "Không tìm thấy Id sản phẩm",
					Data = null
				};
			}
			_dbContext.Products.Remove(productToDelete);
			await _dbContext.SaveChangesAsync();
			return new ResponseObject<ProductDto>
			{
				Status = 200,
				Message = "Xoá sản phẩm thành công",
				Data = new ProductDto
				{
					Id = productToDelete.Id,
					Name = productToDelete.Name,
					Description = productToDelete.Description,
					Price = productToDelete.Price,
					ImageUrl = productToDelete.ImageUrl,
					Status = productToDelete.Status,
					CreatedAt = productToDelete.CreatedAt,
					UpdatedAt = productToDelete.UpdatedAt,
					CategoryId = productToDelete.CategoryId,
					DiscountId = productToDelete.DiscountId
				}
			};
		}

		public async Task<IEnumerable<ProductDto>> GetAllProducts()
		{
			var products = await _dbContext.Products.ToListAsync();
			return products.Select(p => new ProductDto
			{
				Id = p.Id,
				Name = p.Name,
				Description = p.Description,
				Price = p.Price,
				ImageUrl = p.ImageUrl,
				Status = p.Status,
				CreatedAt = p.CreatedAt,
				UpdatedAt = p.UpdatedAt,
				CategoryId = p.CategoryId,
				DiscountId = p.DiscountId
			});
		}
		 
		public async Task<ResponseObject<ProductDto>> ProductById(Guid id)
		{
			var product = await _dbContext.Products.FindAsync(id);
			if (product == null)
			{
				return new ResponseObject<ProductDto>
				{
					Status = 404,
					Message = "Không tìm thấy Id sản phẩm",
					Data = null
				};
			}
			return new ResponseObject<ProductDto>
			{
				Status = 200,
				Message = "Lấy sản phẩm thành công",
				Data = new ProductDto
				{
					Id = product.Id,
					Name = product.Name,
					Description = product.Description,
					Price = product.Price,
					ImageUrl = product.ImageUrl,
					Status = product.Status,
					CreatedAt = product.CreatedAt,
					UpdatedAt = product.UpdatedAt,
					CategoryId = product.CategoryId,
					DiscountId = product.DiscountId
				}
			};
		}

		public async Task<ResponseObject<ProductDto>> UpdateProduct(Guid Id,ProductUpdateDto product)
		{
			var productToUpdate = await _dbContext.Products.FindAsync(Id);
			var productCheckName = await _dbContext.Products.FirstOrDefaultAsync(p => p.Name == product.Name && p.Id != Id);
			if (productCheckName != null)
			{
				return new ResponseObject<ProductDto>
				{
					Status = 400,
					Message = "Tên sản phẩm đã tồn tại",
					Data = null
				};
			}
			if (string.IsNullOrEmpty(product.Name))
			{
				return new ResponseObject<ProductDto>
				{
					Status = 400,
					Message = "Tên sản phẩm không được để trống",
					Data = null
				};
			}
			if (product.Price <= 0)
			{
				return new ResponseObject<ProductDto>
				{
					Status = 400,
					Message = "Giá sản phẩm phải lớn hơn 0",
					Data = null
				};
			}
			if (productToUpdate == null)
			{
				return new ResponseObject<ProductDto>
				{
					Status = 404,
					Message = "Không tìm thấy Id sản phẩm",
					Data = null
				};
			}
			else
			{
				productToUpdate.Name = product.Name;
				productToUpdate.Description = product.Description;
				productToUpdate.Price = product.Price;
				productToUpdate.ImageUrl = product.ImageUrl;
				productToUpdate.Status = product.Status;
				productToUpdate.UpdatedAt = DateTime.UtcNow;
				productToUpdate.CategoryId = product.CategoryId;
				productToUpdate.DiscountId = product.DiscountId;
				_dbContext.Products.Update(productToUpdate);
				await _dbContext.SaveChangesAsync();
				return new ResponseObject<ProductDto>
				{
					Status = 200,
					Message = "Cập nhật sản phẩm thành công",
					Data = new ProductDto
					{
						Name = productToUpdate.Name,
						Description = productToUpdate.Description,
						Price = productToUpdate.Price,
						ImageUrl = productToUpdate.ImageUrl,
						Status = productToUpdate.Status,
						CreatedAt = productToUpdate.CreatedAt,
						UpdatedAt = productToUpdate.UpdatedAt,
						CategoryId = productToUpdate.CategoryId,
						DiscountId = productToUpdate.DiscountId
					}
				};
			}
		}
	}
}
