
# SimpleJson http://simplejson.codeplex.com/
# http://bit.ly/simplejson
# License: Apache License 2.0 (Apache)

function ConvertFrom-Json
{
}

function ConvertTo-Json
{
    param(
        [object][Parameter(Mandatory=$true,ValueFromPipeline=$true)] $obj
    )
    
    return [SimpleJson.SimpleJson]::SerializeObject($obj)
}
