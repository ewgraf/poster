using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace poster.Entities {
	public class Post {
		public string MultilineText { get; set; }
		public string PathToImageToAttach { get; set; }
		public string[] Hashtags { get; set; }

		/// <summary>
		/// Ex: C:\posts
		///             \post1
		///                   \*.txt
		///                   \*.jpg
		///             \post2
		///                   \*.txt
		///                   \*.jpg
		/// string pathToFolderWithFoldersOfPosts = "C:\posts";
		/// </summary>
		/// <param name="pathToFolderWithFoldersOfPosts"></param>
		/// <returns></returns>
		public static Post[] FindPosts(string pathToFolderWithFoldersOfPosts) {
			var posts = new List<Post>();

			string[] pathesOfFolderOfPosts = Directory.GetDirectories(pathToFolderWithFoldersOfPosts);

			foreach (string pathOfFolderOfPost in pathesOfFolderOfPosts) {
				string pathOfText = Directory.GetFiles(pathOfFolderOfPost, "*.txt").Single();
				string pathOfHashtags = Directory.GetFiles(pathOfFolderOfPost, "*.#").Single();
				string pathOfImages = Directory.GetFiles(pathOfFolderOfPost, "*.jpg").Single();
				var post = new Post();
				post.MultilineText = File.ReadAllText(pathOfText);
				post.PathToImageToAttach = pathOfImages;
				post.Hashtags = File.ReadAllText(pathOfHashtags)
					.Replace(" ", "") // "#наруто #тест #автоматической #постилки\r\n"
									  // → "#наруто#тест#автоматической#постилки\r\n"
					.Replace(Environment.NewLine, "")
									  // → "#наруто #тест #автоматической #постилки"
					.Split('#', StringSplitOptions.RemoveEmptyEntries)
									  // → "наруто", "тест", "автоматической", "постилки"
					;
				posts.Add(post);
			}

			return posts.ToArray();
		}
	}
}
