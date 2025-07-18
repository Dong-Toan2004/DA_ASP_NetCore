using Application.DataTransferObjects.Authen;
using Application.DataTransferObjects.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
	public interface IAuthRepository
	{
		Task<ResponseObject<object>> Register(RegisterDto registerDto);
		Task<ResponseObject<object>> Login(LoginDto loginDto);
		Task<IEnumerable<UserDto>> GetAllUser();
		Task<ResponseObject<UserDto>> GetUserById(Guid id);
	}
}
