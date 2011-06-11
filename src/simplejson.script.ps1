
# SimpleJson http://simplejson.codeplex.com/
# http://bit.ly/simplejson
# License: Apache License 2.0 (Apache)

function ConvertFrom-Json
{
    param(
        [string][Parameter(Mandatory=$true,ValueFromPipeline=$true)] $json
    )
    
    $obj = [SimpleJson.SimpleJson]::DeserializeObject($json)
    
    return $obj
}

function ConvertTo-Json
{
    param(
        [object][Parameter(Mandatory=$true,ValueFromPipeline=$true)] $obj
    )
    
    return [SimpleJson.SimpleJson]::SerializeObject($obj)
}
