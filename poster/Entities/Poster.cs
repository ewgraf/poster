using System.Collections.Generic;
using System.IO;
using System.Linq;
using poster.Entities.Posters;

namespace poster.Entities {
	public abstract class Poster {
		public abstract void Post(Post post);

		public static Poster[] FindPosters(string pathToFolderOfPosterJsons) {
			var posters = new List<Poster>();

			string[] posterJsonPaths = Directory.GetFiles(pathToFolderOfPosterJsons, "*.json");

			foreach (string posterJsonPath in posterJsonPaths) {
				string posterName = Path.GetFileNameWithoutExtension(posterJsonPath);
				Poster poster = null;
				if (posterName == "telegram") {
					string telegramPosterJson = File.ReadAllText(posterJsonPath);
					poster = new TelegramPoster();
					
					posters.Add(poster);
				}
			}

			return posters.Where(p => p != null).ToArray();
		}
	}
}
