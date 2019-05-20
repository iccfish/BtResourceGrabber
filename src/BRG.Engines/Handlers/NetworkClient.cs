namespace BRG.Engines.Handlers
{
	using BRG.Entities;
	using FSLib.Network.Http;

	public class NetworkClient : HttpClient
	{
		public NetworkClient(object requestSource)
			: base(new HttpSetting(), new NetworkHandler())
		{
			RequestSource = requestSource;
			Setting.Timeout = AppContext.Instance.Options.NetworkTimeout * 1000;
			AppContext.Instance.Options.PropertyChanged += Options_PropertyChanged;
		}

		private void Options_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(Options.NetworkTimeout))
			{
				Setting.Timeout = AppContext.Instance.Options.NetworkTimeout * 1000;
			}
		}

		public object RequestSource { get; private set; }
	}
}
