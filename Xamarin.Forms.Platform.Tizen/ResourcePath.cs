using System;
using System.IO;
using IOPath = System.IO.Path;
using AppFW = Tizen.Applications;

namespace Xamarin.Forms.Platform.Tizen
{
	public static class ResourcePath
	{
		public static string GetPath(string res)
		{
			if (IOPath.IsPathRooted(res))
			{
				return res;
			}

			foreach (AppFW.ResourceManager.Category category in Enum.GetValues(typeof(AppFW.ResourceManager.Category)))
			{
				var path = AppFW.ResourceManager.TryGetPath(category, res);

				if (path != null)
				{
					return path;
				}
			}

			AppFW.Application app = AppFW.Application.Current;
			if (app != null)
			{
				string resPath = app.DirectoryInfo.Resource + res;
				if (File.Exists(resPath))
				{
					return resPath;
				}
			}

			return res;
		}

		internal static string GetPath(ImageSource icon)
		{
			if (icon is FileImageSource filesource)
			{
				return GetPath(filesource.File);
			}
			return string.Empty;
		}
	}
}
