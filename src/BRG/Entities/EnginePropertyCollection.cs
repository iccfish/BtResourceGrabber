namespace BRG.Entities
{
	using System;
	using System.Collections.Generic;

	public class EnginePropertyCollection : Dictionary<string, string>
	{
		public EnginePropertyCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
			
		}
	}
}
