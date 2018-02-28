using System.Collections.Generic;

namespace ForefrontSolutions.CloudflareCacheTagMiddleware
{
	/// <summary>
	/// The Cache Tags class.
	/// Manages the the cache tags that will be used in the middleware.
	/// </summary>
	public class CacheTags
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CacheTags"/> class.
		/// </summary>
		/// <param name="cacheTags">A comma seperated list of cache tags or a single cache tag.</param>
		public CacheTags(string cacheTags)
		{
			this.Tags = cacheTags;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CacheTags"/> class.
		/// </summary>
		/// <param name="cacheTags">A collection of cache tags.</param>
		public CacheTags(IEnumerable<string> cacheTags)
		{
			foreach (var tag in cacheTags)
			{
				if (this.Tags.Length > 0)
				{
					this.Tags = this.Tags + ",";
				}

				this.Tags = this.Tags + tag;
			}
		}

		internal string Tags { get; }
	}
}