namespace BRG.Service
{
	using BRG.Entities;

	public interface IResourceProcessor : IAddin
	{
		/// <summary>
		/// 资源已处理，准备供显示
		/// </summary>
		/// <param name="items"></param>
		void ResourcesFetched(IResourceSearchInfo items);
		/// <summary>
		/// 资源已加载，但是尚未处理
		/// </summary>
		/// <param name="searchResult"></param>
		void ResourcesLoaded(BRG.Entities.IResourceSearchInfo searchResult);
	}
}