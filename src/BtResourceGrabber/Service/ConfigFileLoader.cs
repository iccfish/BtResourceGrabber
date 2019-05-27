using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace BtResourceGrabber.Service
{
	using System.ComponentModel.Composition;
	using BRG.Service;

	[Export(typeof(IFileConfigLoader))]

	class ConfigFileLoader : IFileConfigLoader
	{
		Dictionary<Type, string> _path = new Dictionary<Type, string>();
		public string Root { get; }

		public ConfigFileLoader()
		{
			Root = Path.Combine(System.Reflection.Assembly.GetExecutingAssembly().GetLocation(), "data");
			Directory.CreateDirectory(Root);
		}

		public T Load<T>(string catalog = null)
		{
			var type = typeof(T);
			var path = Path.Combine(Root, type.Name);

			_path.AddOrUpdate(type, path);

			if (!catalog.IsNullOrEmpty())
				path = Path.Combine(path, catalog);
			if (File.Exists(path))
			{
				try
				{
					return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
				}
				catch (Exception)
				{
				}
			}

			return Activator.CreateInstance<T>();
		}


		public void Save<T>(T obj, string catalog = null)
		{
			var path = _path.GetValue(typeof(T));
			if (!catalog.IsNullOrEmpty())
				path = Path.Combine(path, catalog);
			Directory.CreateDirectory(Path.GetDirectoryName(path));
			File.WriteAllText(path, JsonConvert.SerializeObject(obj));
		}
	}
}
