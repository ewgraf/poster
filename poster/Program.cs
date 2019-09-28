using System;
using System.Linq;
using poster.Entities;

namespace poster {
	public class Program {
		static void Main(string[] args) {
			string pathToSettingsJson = args.First();

			Post post = GetPost(args);

			IPoster[] posters = GetPosters(args);

			foreach (IPoster poster in posters) {
				poster.Post(post);
			}
		}
	}
}
