using System.Net.Http.Headers;

namespace User.Extension_helper
{
	public class AuthorizationClient
	{
		private readonly IHttpClientFactory _clientFactory;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public AuthorizationClient(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
		{
			_clientFactory = clientFactory;
			_httpContextAccessor = httpContextAccessor;
		}

		private HttpClient CreateClientWithToken()
		{
			var client = _clientFactory.CreateClient("ApiClient");

			var token = _httpContextAccessor.HttpContext?
				.Request.Cookies["token"];

			if (!string.IsNullOrEmpty(token))
			{
				client.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", token);
			}

			return client;
		}

		public async Task<T> GetAsync<T>(string url)
		{
			var client = CreateClientWithToken();
			var response = await client.GetAsync(url);
			if (!response.IsSuccessStatusCode)
			{
				throw new UnauthorizedAccessException("Token không hợp lệ hoặc đã hết hạn");
			}
			var result = await response.Content.ReadFromJsonAsync<T>();
			return result;
		}

		public async Task<T> PostAsync<T>(string url, HttpContent content)
		{
			var client = CreateClientWithToken();
			var response = await client.PostAsync(url, content);
			if (!response.IsSuccessStatusCode)
			{
				throw new UnauthorizedAccessException("Token không hợp lệ hoặc đã hết hạn");
			}
			var result = await response.Content.ReadFromJsonAsync<T>();
			return result;
		}

		public async Task<T> PutAsync<T>(string url, HttpContent content)
		{
			var client = CreateClientWithToken();
			var response = await client.PutAsync(url, content);
			if (!response.IsSuccessStatusCode)
			{
				throw new UnauthorizedAccessException("Token không hợp lệ hoặc đã hết hạn");
			}
			var result = await response.Content.ReadFromJsonAsync<T>();
			return result;
		}

		public async Task<T> DeleteAsync<T>(string url)
		{
			var client = CreateClientWithToken();
			var response = await client.DeleteAsync(url);
			if (!response.IsSuccessStatusCode)
			{
				throw new UnauthorizedAccessException("Token không hợp lệ hoặc đã hết hạn");
			}
			var result = await response.Content.ReadFromJsonAsync<T>();
			return result;
		}
	}
}
