using System;
using Telegram.Bot;

namespace poster.Entities.Posters {
	public class TelegramPoster : Poster {
		private readonly string _accessToken;

		public TelegramPoster(string accessToken) {
			_accessToken = accessToken;
		}

		public override void Post(Post post) {
			var botClient = new TelegramBotClient(_accessToken);
			var me = botClient.GetMeAsync().Result;
			Console.WriteLine(
			  $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
			);
		}
	}
}
