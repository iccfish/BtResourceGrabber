namespace BRG.Entities
{
	using System;

	public class ResourceEventArgs : EventArgs
	{
		public IResourceInfo Torrent { get; private set; }

		/// <summary>
		/// 创建 <see cref="TorrentEventArgs" />  的新实例(TorrentEventArgs)
		/// </summary>
		/// <param name="torrent"></param>
		public ResourceEventArgs(IResourceInfo torrent)
		{
			Torrent = torrent;
		}

	}
}