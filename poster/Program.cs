using System.Linq;
using poster.Entities;

namespace poster {
	public class Program {
		static void Main(string[] args) {
			Post post = Post.FindPosts(@"C:\Users\Ewgraf\Documents\GitHub\poster\poster\bin\Debug\netcoreapp3.0\posts")
							.First();

			Poster[] posters = Poster.FindPosters(@"C:\Users\Ewgraf\Documents\GitHub\poster\poster\bin\Debug\netcoreapp3.0\posters");

			;

			foreach (Poster poster in posters) {
				poster.Post(post).Wait();
			}
		}
	}
}
