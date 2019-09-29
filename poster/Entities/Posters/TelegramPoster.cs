using System;
using System.Threading.Tasks;
using MihaZupan;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace poster.Entities.Posters {
	public class TelegramPoster : Poster {
		private readonly string _accessToken;
		private readonly ChatId _chatId;

		public TelegramPoster(string accessToken, long chatId) {
			_accessToken = accessToken;
			_chatId = new ChatId(chatId);
		}

		public override async Task Post(Post post) {
			try {
				// @Tor Browser\Browser\TorBrowser\Data\Tor\torrc
				// +SocksPort 127.0.0.1:9050
				var botClient = new TelegramBotClient(_accessToken, new HttpToSocks5Proxy("127.0.0.1", 9050));
				var imageStream = System.IO.File.OpenRead(post.PathToImageToAttach);
				var mediaPhoto = new InputMediaPhoto();
				mediaPhoto.Media = new InputMedia(imageStream, "test-file-name");
				mediaPhoto.Caption = post.MultilineText
										+ Environment.NewLine
										+ "#"
										+ string.Join(" #", post.Hashtags)
										+ Environment.NewLine;
				mediaPhoto.ParseMode = Telegram.Bot.Types.Enums.ParseMode.Markdown;

				var message = await botClient.SendMediaGroupAsync(
					new[] { mediaPhoto },
					_chatId
				);
			} catch (Exception ex) {
				Console.WriteLine($"telegram> {ex.ToString()}");
			}
		}
	}
}
