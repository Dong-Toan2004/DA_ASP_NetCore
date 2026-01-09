using Application.DataTransferObjects.Authen;
using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthenController : ControllerBase
	{
		public readonly IAuthRepository _authRepository;

		public AuthenController(IAuthRepository authRepository)
		{
			_authRepository = authRepository;
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
		{
			var result = await _authRepository.Login(loginDto);
			return Ok(result);
		}
		[HttpPost]
		public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
		{
			var result = await _authRepository.Register(registerDto);
			return Ok(result);
		}
		[HttpGet]
		public async Task<IActionResult> GetAllUser()
		{
			var users = await _authRepository.GetAllUser();
			return Ok(users);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetUserById(Guid id)
		{
			var user = await _authRepository.GetUserById(id);
			return Ok(user);
		}
	}
}
