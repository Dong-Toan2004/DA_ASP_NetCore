namespace User.Extension_helper;
using System.Net.Http.Headers;
using System.Security.Claims;

public class JwtTokenHandler : DelegatingHandler
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public JwtTokenHandler(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	protected override Task<HttpResponseMessage> SendAsync(
	HttpRequestMessage request,
	CancellationToken cancellationToken)
	{
		var context = _httpContextAccessor.HttpContext;

		var token = context?.Request.Cookies["token"];

		if (!string.IsNullOrEmpty(token))
		{
			request.Headers.Authorization =
				new AuthenticationHeaderValue("Bearer", token);
		}
		Console.WriteLine(token);
		return base.SendAsync(request, cancellationToken);
	}

}

