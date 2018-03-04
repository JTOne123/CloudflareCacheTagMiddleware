# User input
$zoneDomainName = 'my-domain-name-here'
$cacheTag = 'my-cache-tag'

# User secrets
$auth_email = "my-cloudflare-user@my-domain.com"
$auth_key = "my-auth-key"

$zonesApiUrl = "https://api.cloudflare.com/client/v4/zones"
$headers = @{"X-Auth-Email"="$auth_email"; "X-Auth-Key"="$auth_key"; "Content-Type"="application/json"};
 
$zones = Invoke-RestMethod -Uri "$zonesApiUrl/?name=$zoneDomainName" -Method Get -Headers $headers -ErrorAction Stop

if ($zones.result.Count -ne 1)
{
    Throw "Lookup for $zoneDomainName zone found $($zones.result.Count) results. 1 result should of been found. Unable to purge cache."
}

Invoke-RestMethod -Uri "$zonesApiUrl/$($zones.result.Id)/purge_cache" -Method Delete -Headers $headers -Body $('{"tags":["' + $cacheTag + '"]}') -ErrorAction Stop