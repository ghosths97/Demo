using Blazored.LocalStorage;
using Demo.Shared.Models.User;
using Demo.SPA.Components;
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
		protected readonly Spinner _spinner;

		public TokenAuthrorizationHandler(ILocalStorageService storageService, Spinner spinner)
		{
			_storageService = storageService;
			_spinner = spinner;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			_spinner.Show();

			if (await _storageService.ContainKeyAsync("User"))
			{
					var userInfo = await _storageService.GetItemAsync<LoginResponse>("User");
					var token = userInfo.Token;
					request.Headers.Add("Authorization", "Bearer " + token);
            }
            else
            {
				_spinner.Hide();
				return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
			}

			var response = await base.SendAsync(request, cancellationToken);
			_spinner.Hide();
			return response;

		}
	}
}
