using Microsoft.AspNetCore.Builder;

namespace ForefrontSolutions.CloudflareCacheTagMiddleware
{
	/// <summary>
	/// The Application Builder Extensions class.
	/// Contains all the extension methods that extend the IApplicationBuilder interface.
	/// </summary>
	public static class ApplicationBuilderExtensions
	{
		/// <summary>
		/// Registers the <see cref="CacheTagMiddleware"/>.
		/// </summary>
		/// <param name="app">IApplicationBuilder instance.</param>
		/// <param name="cacheTags">A CacheTags object containing the cache tags to be added.</param>
		public static IApplicationBuilder UseCloudflareCacheTagMiddleware(this IApplicationBuilder app, CacheTags cacheTags)
		{
			return app.UseMiddleware<CacheTagMiddleware>(cacheTags);
		}
	}
}