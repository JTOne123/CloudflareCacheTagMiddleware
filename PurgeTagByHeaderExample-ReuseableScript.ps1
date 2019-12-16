Param(
  [string]$ZoneName,[string]$CTag,[string]$AuthEmail,[string]$AuthKey,[string]$ApiUrl
)

$a = "$ApiUrl/zones"
$h = @{"X-Auth-Email"="$AuthEmail"; "X-Auth-Key"="$AuthKey"; "Content-Type"="application/json"};
$z = IRM "$a/?name=$ZoneName" -Headers $h -ErrorAction Stop

if ($z.result.Count -ne 1) {Throw "Unable to purge cache as $ZoneName lookup found $z.result.Count results but expected 1."}

$i = $z.result.Id

IRM "$a/$zid/purge_cache" -Method Delete -Headers $h -Body $('{"tags":["'+$CTag+'"]}') -ErrorAction Stop