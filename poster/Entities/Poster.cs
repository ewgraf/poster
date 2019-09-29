using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using poster.Entities.Posters;

namespace poster.Entities {
	public abstract class Poster {
		public abstract Task Post(Post post);

		public static Poster[] FindPosters(string pathToFolderOfPosterJsons) {
			var posters = new List<Poster>();

			string[] posterJsonPaths = Directory.GetFiles(pathToFolderOfPosterJsons, "*.json");

			foreach (string posterJsonPath in posterJsonPaths) {
				string posterName = Path.GetFileNameWithoutExtension(posterJsonPath);
				Poster poster = null;
				if (posterName == "telegram") {
					string telegramPosterSettingsJson = File.ReadAllText(posterJsonPath);
					JObject telegramPosterSettings = JsonConvert.DeserializeObject<JObject>(telegramPosterSettingsJson);
					string accessToken = telegramPosterSettings.GetValue("accessToken").Value<string>();
					long chatId = telegramPosterSettings.GetValue("chatId").Value<long>();
					poster = new TelegramPoster(accessToken, chatId);
					posters.Add(poster);
				}
			}

			return posters.Where(p => p != null).ToArray();
		}
	}
}
