# User input
$zoneName = "$(INPUT_CLOUDFLARE_ZONE_NAME)"
$cacheTag = "$(INPUT_CLOUDFLARE_CACHE_TAG)"

# User secrets
$auth_email = "$(INPUT_CLOUDFLARE_AUTH_EMAIL)"
$auth_key = "$(INPUT_CLOUDFLARE_AUTH_KEY)"

$api_url = "$(INPUT_CLOUDFLARE_API_URL)"
$zonesApiUrl = "${api_url}/zones"
$headers = @{"X-Auth-Email"="$auth_email"; "X-Auth-Key"="$auth_key"; "Content-Type"="application/json"};

Write-Host "Finding zone(s) for zone name $zoneName as $auth_email user"
Write-Host "Cloudflare zones API is at ${zonesApiUrl}/"
 
$zones = Invoke-RestMethod -Uri "${zonesApiUrl}/?name=${zoneName}" -Method Get -Headers $headers -ErrorAction Stop

if ($zones.result.Count -ne 1)
{
    $zoneCount = $zones.result.Count
    Throw "Lookup for $zoneName zone found $zoneCount. 1 zone should of been found. Unable to purge cache."
}

$zoneId = $zones.result.Id

Write-Host "Found zone id $zoneId for $zoneName"

$purgeRequestBody = '{"tags":["' + $cacheTag + '"]}'

Write-Host "Purging cache tag $cacheTag from $zoneName (${zoneId})"

Invoke-RestMethod -Uri "$zonesApiUrl/$zoneId/purge_cache" -Method Delete -Headers $headers -Body $purgeRequestBody -ErrorAction Stop