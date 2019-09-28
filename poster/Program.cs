using poster.Entities;

namespace poster {
	public class Program {
		static void Main(string[] args) {
			Post post = GetPost();

			IPoster[] posters = GetPosters();

			foreach (IPoster poster in posters) {
				poster.Post(post);
			}
		}
	}
}
