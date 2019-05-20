namespace BRG.Entities
{
	public class TorrentMarkEventArgs : ResourceEventArgs
	{

		public string MaskName { get; private set; }

		public HashMark Mark { get; private set; }

		/// <summary>
		/// 创建 <see cref="TorrentMarkEventArgs" />  的新实例(TorrentMaskEventArgs)
		/// </summary>
		/// <param name="torrent"></param>
		/// <param name="maskName"></param>
		/// <param name="mark"></param>
		public TorrentMarkEventArgs(IResourceInfo torrent, string maskName, HashMark mark) : base(torrent)
		{
			MaskName = maskName;
			Mark = mark;
		}
	}
}
