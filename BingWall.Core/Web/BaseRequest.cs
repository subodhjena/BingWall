using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BingWall.Core.Web
{
	public class BaseRequest : IBaseRequest
	{
		protected string _securityToken = string.Empty;
		protected string _urlPrefix = string.Empty;

		public BaseRequest (string urlPrefix, string securityToken)
		{
			if (String.IsNullOrEmpty (urlPrefix))
				throw new ArgumentNullException ("urlPrefix");

			if (!urlPrefix.EndsWith ("/"))
				urlPrefix = string.Concat (urlPrefix, "/");

			_urlPrefix = urlPrefix;
			_securityToken = securityToken.StartsWith ("Bearer ") ? securityToken.Substring (7) : securityToken;
		}

		protected async Task<T> GetAsync<T> (string url)
		{
			HttpClient httpClient = new HttpClient ();
			httpClient.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Bearer", _securityToken);

			var response = await httpClient.GetStringAsync (url);
			return await Task.Run (() => JsonConvert.DeserializeObject<T> (response));
		}

		protected async Task GetAsync (string url)
		{
			HttpClient httpClient = new HttpClient ();
			httpClient.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Bearer", _securityToken);

			await httpClient.GetStringAsync (url);
		}

		protected async Task<byte[]> GetByteArrayAsync<T>(string url)
		{
			HttpClient httpClient = new HttpClient ();
			httpClient.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Bearer", _securityToken);

			return await httpClient.GetByteArrayAsync (url);
		}

		public async Task<T> PostAsync<T, U> (string url, U entity)
		{
			var httpClient = new HttpClient ();
			httpClient.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Bearer", _securityToken);

			var content = JsonConvert.SerializeObject (entity);
			var response = await httpClient.PostAsync (url, new StringContent (content, Encoding.UTF8, "application/json"));

			string responseContent = await response.Content.ReadAsStringAsync ();
			return await Task.Run (() => JsonConvert.DeserializeObject<T> (responseContent));
		}

		public async Task PostAsync<T> (string url, T entity)
		{
			var httpClient = new HttpClient ();
			httpClient.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Bearer", _securityToken);

			var content = JsonConvert.SerializeObject (entity);
			var response = await httpClient.PostAsync (url, new StringContent (content, Encoding.UTF8, "application/json"));

			response.EnsureSuccessStatusCode ();
		}

		public async Task PostAsync (string url)
		{
			var httpClient = new HttpClient ();
			httpClient.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Bearer", _securityToken);

			var response = await httpClient.PostAsync (url, null);
			response.EnsureSuccessStatusCode ();
		}

		public async Task PutAsync<T> (string url, T entity)
		{
			var httpClient = new HttpClient ();
			httpClient.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Bearer", _securityToken);

			var content = JsonConvert.SerializeObject (entity);
			var response = await httpClient.PutAsync (url, new StringContent (content, Encoding.UTF8, "application/json"));

			response.EnsureSuccessStatusCode ();
		}

		public async Task DeleteAsync (string url)
		{
			var httpClient = new HttpClient ();
			httpClient.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Bearer", _securityToken);
			var response = await httpClient.DeleteAsync (url);

			response.EnsureSuccessStatusCode ();
		}

		public void RefreshToken (string securityToken)
		{
			_securityToken = securityToken;
		}
	}
}
