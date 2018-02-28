# CloudflareCacheTagMiddleware

ASP.NET Core middleware for adding the Cloudflare cache tag header to all requests. By using this middleware to add the `cache-tag` header, you can selectively clear assets from your Cloudflare cache based on tags.

More information on Cache Tags can be read on the Cloudflare website at https://support.cloudflare.com/hc/en-us/articles/206596608-How-to-Purge-Cache-Using-Cache-Tags-Enterprise-only-

## Usage

1) Install the CloudflareCacheTagMiddleware package into your project:
```
PM> Install-Package CloudflareCacheTagMiddleware
```

2) In `Startup.cs` within the `Configure` method, add the Cloudflare Cache Tag Middleware:
```
app.UseCloudflareCacheTagMiddleware(new CacheTags("MyApplication,MyEnvironment,MyEnvironment-MyApplication"));
```

You may wish to load the the cache tags using an application setting rather than hard coding them as per the example.

3) Compile, test and enjoy!