using Blazored.LocalStorage;
using Demo.Shared.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.SPA.Helpers
{
  	public class TokenAuthrorizationHandler : DelegatingHandler
	{
		private readonly ILocalStorageService _storageService;

		public TokenAuthrorizationHandler(ILocalStorageService storageService)
		{
			_storageService = storageService;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (await _storageService.ContainKeyAsync("User"))
			{
					var userInfo = await _storageService.GetItemAsync<LoginResponse>("User");
					var token = userInfo.Token;
					request.Headers.Add("Authorization", "Bearer " + token);
            }
            else
            {
				return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
			}

			return await base.SendAsync(request, cancellationToken);
		}
	}
}
