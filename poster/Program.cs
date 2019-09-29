using System.Linq;
using poster.Entities;
using poster.Entities.Posters;

namespace poster {
	public class Program {
		static void Main(string[] args) {
			Post post = Post.FindPosts(@"C:\Users\Ewgraf\Documents\GitHub\poster\poster\bin\Debug\netcoreapp3.0\posts")
							.First();

			Poster[] posters = Poster.FindPosters(@"C:\Users\Ewgraf\Documents\GitHub\poster\poster\bin\Debug\netcoreapp3.0\posters");

			;

			foreach (Poster poster in posters.Where(p => p is VkPoster)) {
				poster.Post(post).Wait();
			}
		}
	}
}
