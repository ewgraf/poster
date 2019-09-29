using System;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;

namespace poster.Entities.Posters {
	public class VkPoster2 : Poster {
		private readonly string _appId;
		private readonly string _login;
		private readonly string _password;

		public VkPoster2(string appId, string login, string password) {
			_appId = appId;
			_login = login;
			_password = password;
		}

		public override async Task Post(Post post) {
			try {
				var vkClient = new VkApi();
				vkClient.Authorize(new ApiAuthParams {
					AccessToken = _appId,
					Login = _login,
					Password = _password
				});
				var wallPost = new WallPostParams();
				wallPost.Message = post.MultilineText;
				var image = new Photo();
				image.PhotoSrc = new Uri(post.PathToImageToAttach);
				wallPost.Attachments = new[] { image };

				vkClient.Wall.Post(wallPost);

				vkClient.Dispose();
			} catch (Exception ex) {
				Console.WriteLine($"vk> {ex.ToString()}");
			}
		}
	}
}
