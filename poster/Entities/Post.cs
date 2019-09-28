namespace poster.Entities {
	public class Post {
		public string MultilineMessage { get; set; }
		public string[] Hashtags { get; set; }
		public string[] PathesToImagesToAttach { get; set; }
	}

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

	}
}
