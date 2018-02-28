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

		public CacheTagMiddleware(RequestDelegate next, CacheTags cacheTags)
		{
			this.next = next;
			this.cacheTags = cacheTags;
		}

		public Task Invoke(HttpContext context)
		{
			var headers = context.Response.Headers;

			headers["Cache-Tag"] = this.cacheTags.Tags;

			return this.next(context);
		}
	}
}