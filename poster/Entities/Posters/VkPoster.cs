using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;

namespace poster.Entities.Posters {
	public class VkPoster : Poster {
		private readonly string _accessToken;
		private readonly ulong _appId;
		private readonly string _clientSecret;
		private readonly string _login;
		private readonly string _password;

		public VkPoster(string accessToken, ulong appId, string clientSecret, string login, string password) {
			_accessToken = accessToken;
			_appId = appId;
			_clientSecret = clientSecret;
			_login = login;
			_password = password;

			if (_accessToken == "") {
				GetAccessToken();
			}
		}

		private void GetAccessToken() {
			// https://vk.com/dev/implicit_flow_user #3. Получение access_token
			Console.WriteLine("The auth link will open in your browser now.");
			System.Diagnostics.Process.Start(
				@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
				$"https://oauth.vk.com/authorize?client_id={_appId}&display=page&redirect_uri=https://oauth.vk.com/blank.html/&scope=wall,photos&response_type=code&v=5.101/"
			);
			Console.WriteLine("The browser will redirect to 'http://localhost/?code=<code>'.");
			Console.WriteLine("Copy the 'code' from URI into the 'posters/vk.json' into 'accessToken' fiels.");
			throw new Exception("Uncomment line Process.Start above, remove this Exception and fill the <client_secret> from 'https://vk.com/editapp?id={_appd}&section=option' (Защищённый ключ) and <code> from the step above.");
			//System.Diagnostics.Process.Start(
			//	@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
			//	$"https://oauth.vk.com/access_token?client_id={_appId}&redirect_uri=https://oauth.vk.com/blank.html/&client_secret={_clientSecret}&code=<code>"
			//);
			//
			Console.ReadKey();
		}

		public override async Task Post(Post post) {
			try {
				var vkClient = new VkApi();
				vkClient.Authorize(new ApiAuthParams {
					ApplicationId = _appId,
					AccessToken = _accessToken,
					Login = _login,
					Password = _password,
					Settings = Settings.FromJsonString("wall,photos")
				});

				UploadServerInfo uploadServerInfo = await vkClient.Photo.GetWallUploadServerAsync();
				
				string uploadedFileJson = "";
				using (var client = new HttpClient()) {
					var requestContent = new MultipartFormDataContent();
					byte[] data = File.ReadAllBytes(post.PathToImageToAttach);
					var content = new ByteArrayContent(data);
					content.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
					requestContent.Add(content, "file", $"file.{Path.GetExtension(post.PathToImageToAttach)}");

					var response = await client.PostAsync(uploadServerInfo.UploadUrl, requestContent);
					Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // https://stackoverflow.com/questions/33579661/encoding-getencoding-cant-work-in-uwp-app
					uploadedFileJson = await response.Content.ReadAsStringAsync();
				}

				Photo[] image = vkClient.Photo.SaveWallPhoto(uploadedFileJson, (ulong)uploadServerInfo.UserId).ToArray();

				var wallPost = new WallPostParams();
				wallPost.Message = post.MultilineText
										+ Environment.NewLine
										+ "#"
										+ string.Join(" #", post.Hashtags)
										+ Environment.NewLine;
				wallPost.Attachments = image;
				
				vkClient.Wall.Post(wallPost);

				vkClient.Dispose();
			} catch (Exception ex) {
				Console.WriteLine($"vk> {ex.ToString()}");
				;
			}
		}
	}
}
