using Application.DataTransferObjects.Discount;
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
	public class DiscountRepository : IDiscountRepository
	{
		public readonly WebBanDoAnDbContext _context;

		public DiscountRepository(WebBanDoAnDbContext context)
		{
			_context = context;
		}

		public async Task<ResponseObject<DiscountDto>> CreateDiscount(DiscountCreateDto discountCreateDto)
		{
			// check code có trùng nhau không
			var checkDiscount = await _context.Discounts.FirstOrDefaultAsync(d => d.Code == discountCreateDto.Code);
			if (checkDiscount != null)
			{
				return new ResponseObject<DiscountDto>
				{
					Status = 400,
					Message = "Mã giảm giá đã tồn tại",
					Data = null
				};
			}
			//check title có trùng nhau không
			var checkTitle = await _context.Discounts.FirstOrDefaultAsync(d => d.Title == discountCreateDto.Title);
			if (checkTitle != null)
			{
				return new ResponseObject<DiscountDto>
				{
					Status = 400,
					Message = "Tiêu đề giảm giá đã tồn tại",
					Data = null
				};
			}
			if (string.IsNullOrEmpty(discountCreateDto.Code))
			{
				return new ResponseObject<DiscountDto>
				{
					Status = 400,
					Message = "Mã giảm giá không được để trống",
					Data = null
				};
			}
			if (string.IsNullOrEmpty(discountCreateDto.Title))
			{
				return new ResponseObject<DiscountDto>
				{
					Status = 400,
					Message = "Tiêu đề giảm giá không được để trống",
					Data = null
				};
			}
			if (discountCreateDto.Percentage <= 0)
			{
				return new ResponseObject<DiscountDto>
				{
					Status = 400,
					Message = "Giá trị giảm giá phải lớn hơn 0",
					Data = null
				};
			}
			if (discountCreateDto.StartDate >= discountCreateDto.EndDate)
			{
				return new ResponseObject<DiscountDto>
				{
					Status = 400,
					Message = "Ngày bắt đầu phải nhỏ hơn ngày kết thúc",
					Data = null
				};
			}
			var discount = new Discount
			{
				Id = Guid.NewGuid(),
				Title = discountCreateDto.Title,
				Description = discountCreateDto.Description,
				Code = discountCreateDto.Code,
				Percentage = discountCreateDto.Percentage,
				Status = discountCreateDto.Status,
				StartDate = discountCreateDto.StartDate,
				EndDate = discountCreateDto.EndDate
			};
			await _context.Discounts.AddAsync(discount);
			await _context.SaveChangesAsync();
			return new ResponseObject<DiscountDto>
			{
				Status = 201,
				Message = "Tạo giảm giá thành công",
				Data = new DiscountDto
				{
					Id = discount.Id,
					Title = discount.Title,
					Description = discount.Description,
					Code = discount.Code,
					Percentage = discount.Percentage,
					Status = discount.Status,
					StartDate = discount.StartDate,
					EndDate = discount.EndDate
				}
			};
		}

		public async Task<ResponseObject<DiscountDto>> DeleteDiscount(Guid id)
		{
			var discountToDelete = await _context.Discounts.FindAsync(id);
			if (discountToDelete == null)
			{
				return new ResponseObject<DiscountDto>
				{
					Status = 404,
					Message = "Mã giảm giá không tồn tại",
					Data = null
				};
			}
			_context.Discounts.Remove(discountToDelete);
			await _context.SaveChangesAsync();
			return new ResponseObject<DiscountDto>
			{
				Status = 200,
				Message = "Xóa giảm giá thành công",
				Data = new DiscountDto
				{
					Id = discountToDelete.Id,
					Title = discountToDelete.Title,
					Description = discountToDelete.Description,
					Code = discountToDelete.Code,
					Percentage = discountToDelete.Percentage,
					Status = discountToDelete.Status,
					StartDate = discountToDelete.StartDate,
					EndDate = discountToDelete.EndDate
				}
			};
		}

		public async Task<IEnumerable<DiscountDto>> GetAll(DiscountSearch discountSearch)
		{
			var discountFilter = _context.Discounts.AsQueryable();
			if (!string.IsNullOrEmpty(discountSearch.Title))
			{
				discountFilter = discountFilter.Where(d => d.Title.Contains(discountSearch.Title));
			}
			if (!string.IsNullOrEmpty(discountSearch.Code))
			{
				discountFilter = discountFilter.Where(d => d.Code.Contains(discountSearch.Code));
			}
			if (discountSearch.Status.HasValue)
			{
				discountFilter = discountFilter.Where(d => d.Status == discountSearch.Status.Value);
			}
			if (discountSearch.StartDate.HasValue)
			{
				discountFilter = discountFilter.Where(d => d.StartDate >= discountSearch.StartDate.Value);
			}
			if (discountSearch.EndDate.HasValue)
			{
				discountFilter = discountFilter.Where(d => d.EndDate <= discountSearch.EndDate.Value);
			}
			if (discountSearch.Percentage.HasValue)
			{
				discountFilter = discountFilter.Where(d => d.Percentage == discountSearch.Percentage.Value);
			}
			var discounts = await discountFilter.ToListAsync();
			return discounts.Select(d => new DiscountDto
			{
				Id = d.Id,
				Title = d.Title,
				Description = d.Description,
				Code = d.Code,
				Percentage = d.Percentage,
				Status = d.Status,
				StartDate = d.StartDate,
				EndDate = d.EndDate
			});
		}

		public async Task<ResponseObject<DiscountDto>> GetById(Guid id)
		{
			var discount = await _context.Discounts.FindAsync(id);
			if (discount == null)
			{
				return new ResponseObject<DiscountDto>
				{
					Status = 404,
					Message = "Mã giảm giá không tồn tại",
					Data = null
				};
			}
			return new ResponseObject<DiscountDto>
			{
				Status = 200,
				Message = "Lấy thông tin giảm giá thành công",
				Data = new DiscountDto
				{
					Id = discount.Id,
					Title = discount.Title,
					Description = discount.Description,
					Code = discount.Code,
					Percentage = discount.Percentage,
					Status = discount.Status,
					StartDate = discount.StartDate,
					EndDate = discount.EndDate
				}
			};
		}

		public async Task<ResponseObject<DiscountDto>> UpdateDiscount(Guid id, DiscountUpdateDto discountUpdateDto)
		{
			var discountToUpdate = await _context.Discounts.FindAsync(id);
			if (discountToUpdate == null)
			{
				return new ResponseObject<DiscountDto>
				{
					Status = 404,
					Message = "Mã giảm giá không tồn tại",
					Data = null
				};
			}
			// check code có trùng nhau không
			var checkDiscount = await _context.Discounts.FirstOrDefaultAsync(d => d.Code == discountUpdateDto.Code && d.Id != id);
			if (checkDiscount!=null)
			{
				return new ResponseObject<DiscountDto>
				{
					Status = 400,
					Message = "Mã giảm giá đã tồn tại",
					Data = null
				};
			}
			//check title có trùng nhau không
			var checkTitle = await _context.Discounts.FirstOrDefaultAsync(d => d.Title == discountUpdateDto.Title && d.Id != id);
			if (checkTitle != null)
			{
				return new ResponseObject<DiscountDto>
				{
					Status = 400,
					Message = "Tiêu đề giảm giá đã tồn tại",
					Data = null
				};
			}
			if (string.IsNullOrEmpty(discountUpdateDto.Code))
			{
				return new ResponseObject<DiscountDto>
				{
					Status = 400,
					Message = "Mã giảm giá không được để trống",
					Data = null
				};
			}
			if (string.IsNullOrEmpty(discountUpdateDto.Title))
			{
				return new ResponseObject<DiscountDto>
				{
					Status = 400,
					Message = "Tiêu đề giảm giá không được để trống",
					Data = null
				};
			}
			if (discountUpdateDto.Percentage <= 0)
			{
				return new ResponseObject<DiscountDto>
				{
					Status = 400,
					Message = "Giá trị giảm giá phải lớn hơn 0",
					Data = null
				};
			}
			if (discountUpdateDto.StartDate >= discountUpdateDto.EndDate)
			{
				return new ResponseObject<DiscountDto>
				{
					Status = 400,
					Message = "Ngày bắt đầu phải nhỏ hơn ngày kết thúc",
					Data = null
				};
			}
			discountToUpdate.Title = discountUpdateDto.Title;
			discountToUpdate.Description = discountUpdateDto.Description;
			discountToUpdate.Code = discountUpdateDto.Code;
			discountToUpdate.Percentage = discountUpdateDto.Percentage;
			discountToUpdate.Status = discountUpdateDto.Status;
			discountToUpdate.StartDate = discountUpdateDto.StartDate;
			discountToUpdate.EndDate = discountUpdateDto.EndDate;
			_context.Discounts.Update(discountToUpdate);
			await _context.SaveChangesAsync();
			return new ResponseObject<DiscountDto>
			{
				Status = 200,
				Message = "Cập nhật giảm giá thành công",
				Data = new DiscountDto
				{
					Id = discountToUpdate.Id,
					Title = discountToUpdate.Title,
					Description = discountToUpdate.Description,
					Code = discountToUpdate.Code,
					Percentage = discountToUpdate.Percentage,
					Status = discountToUpdate.Status,
					StartDate = discountToUpdate.StartDate,
					EndDate = discountToUpdate.EndDate
				}
			};
		}
	}
}
