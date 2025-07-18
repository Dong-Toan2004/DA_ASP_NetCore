using Application.DataTransferObjects.Authen;
using Application.DataTransferObjects.ViewModels;
using Application.Interfaces.IRepositories;
using Domain.Entity;
using Infrastructure.Database.AppDbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implements.Repositories
{
	public class AuthRepository : IAuthRepository
	{
		private readonly IConfiguration _configuration;
		// Thao tác với người dùng trong Identity và db
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly WebBanDoAnDbContext _dbContext;
		public AuthRepository(IConfiguration configuration, UserManager<ApplicationUser> userManager, WebBanDoAnDbContext dbContext)
		{
			_configuration = configuration;
			_userManager = userManager;
			_dbContext = dbContext;
		}

		public async Task<ResponseObject<object>> Login(LoginDto loginDto)
		{
			var user = await _userManager.FindByEmailAsync(loginDto.Email);
			if (user == null)
			{
				return new ResponseObject<object>
				{
					Status = 404,
					Message = "Tài khoản không tồn tại",
					Data = null
				};
			}
			var checkPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);
			if (!checkPassword)
			{
				return new ResponseObject<object>
				{
					Status = 400,
					Message = "Mật khẩu không đúng",
					Data = null
				};
			}
			// Tạo token
			var token = GenerateToken(user);
			return new ResponseObject<object>
			{
				Status = 200,
				Message = "Đăng nhập thành công",
				Data = token
			};
		}

		// Hàm tạo jwt token
		private string GenerateToken(ApplicationUser user)
		{
			var jwtSettings = _configuration.GetSection("JwtSettings");
			var secretKey = jwtSettings["SecretKey"];
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Email),
				new Claim("id", user.Id.ToString()),
				new Claim(ClaimTypes.Name, user.UserName ?? ""),
				new Claim(ClaimTypes.Role, user.Role.ToString())
			};

			var token = new JwtSecurityToken(
				issuer: jwtSettings["Issuer"],
				audience: jwtSettings["Audience"],
				claims: claims,
				expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpirationInMinutes"])),
				signingCredentials: creds
				);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public async Task<ResponseObject<object>> Register(RegisterDto registerDto)
		{
			var user = new ApplicationUser
			{
				UserName = registerDto.Email,
				Email = registerDto.Email,
				Name = registerDto.Name,
				Address = registerDto.Address,
			};

			var result = await _userManager.CreateAsync(user, registerDto.Password);
			if (result.Succeeded)
			{
				var token = GenerateToken(user);
				//Tạo gỉỏ hàng cho người dùng mới
				_dbContext.Carts.Add(new Cart
				{
					Id = user.Id,
					UserId = user.Id,
				});
				await _dbContext.SaveChangesAsync();
				return new ResponseObject<object>
				{
					Data = new { User = registerDto, Token = token },
					Message = "Đăng ký thành công",
					Status = 201 
				};
			}
			else
			{
				return new ResponseObject<object>
				{
					Data = null,
					Message = string.Join(", ", result.Errors.Select(e => e.Description)),
					Status = 400 
				};
			}
		}

		public async Task<IEnumerable<UserDto>> GetAllUser()
		{
			var users = await _userManager.Users.ToListAsync();
			return users.Select(user => new UserDto
			{
				Id = user.Id,
				Name = user.Name,
				Email = user.Email,
				Address = user.Address,
				Role = user.Role
			});
		}

		public async Task<ResponseObject<UserDto>> GetUserById(Guid id)
		{
			var user = await _userManager.FindByIdAsync(id.ToString());
			if (user == null)
			{
				return new ResponseObject<UserDto>
				{
					Status = 404,
					Message = "Người dùng không tồn tại",
					Data = null
				};
			}
			return new ResponseObject<UserDto>
			{
				Status = 200,
				Message = "Lấy thông tin người dùng thành công",
				Data = new UserDto
				{
					Id = user.Id,
					Name = user.Name,
					Email = user.Email,
					Address = user.Address,
					Role = user.Role
				}
			};
		}
	}
}
