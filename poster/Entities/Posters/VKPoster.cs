using System;
using System.Threading.Tasks;
using MihaZupan;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace poster.Entities.Posters {
	public class VkPoster : Poster {
		private readonly string _appId;
		private readonly string _login;
		private readonly string _password;

		public VkPoster(string appId, string login, string password) {
			_appId = appId;
			_login = login;
			_password = password;
		}

		public override async Task Post(Post post) {
			try {

			} catch (Exception ex) {
				Console.WriteLine($"vk> {ex.ToString()}");
			}
		}
	}
}
