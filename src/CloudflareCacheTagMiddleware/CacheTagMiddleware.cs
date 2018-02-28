using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace ForefrontSolutions.CloudflareCacheTagMiddleware
{
	/// <summary>
	/// The Cache Tag Middleware class.
	/// Middleware class that adds the cache tags to all requests.
	/// </summary>
	public class CacheTagMiddleware
	{
		private readonly RequestDelegate next;

		private readonly CacheTags cacheTags;

		private readonly CacheTagFileTypeFilter[] fileTypeFilters;

		private readonly bool useFileTypeFilter;

		public CacheTagMiddleware(RequestDelegate next, CacheTags cacheTags)
		{
			this.next = next;
			this.cacheTags = cacheTags;
		}

		public CacheTagMiddleware(RequestDelegate next, CacheTags cacheTags, params CacheTagFileTypeFilter[] fileTypeFilters)
		{
			this.next = next;
			this.cacheTags = cacheTags;
			this.fileTypeFilters = fileTypeFilters;
			this.useFileTypeFilter = true;
		}

		public Task Invoke(HttpContext context)
		{
			if (!this.useFileTypeFilter)
			{
				this.AddHeaders(context);
				return this.next(context);
			}

			var requestedFileName = Path.GetFileName(context.Request.Path.ToUriComponent());
			if (this.fileTypeFilters.Any(fileType => requestedFileName.EndsWith(fileType.ToString())))
			{
				this.AddHeaders(context);
				return this.next(context);
			}

			return this.next(context);
		}

		private void AddHeaders(HttpContext context)
		{
			var headers = context.Response.Headers;

			headers["Cache-Tag"] = this.cacheTags.Tags;
		}
	}
}