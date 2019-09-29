using System;
using System.Threading.Tasks;
using MihaZupan;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace poster.Entities.Posters {
	public class VkPoster : Poster {
		public VkPoster() {
		}

		public override async Task Post(Post post) {
			try {
			} catch (Exception ex) {
				Console.WriteLine($"telegram> {ex.ToString()}");
			}
		}
	}
}
