# SimpleJson https://github.com/facebook-csharp-sdk/simple-json
# License: MIT License
# Version:

function ConvertFrom-Json
{
    param(
        [Switch] $AsPSObject,
        [Parameter(Mandatory=$true,ValueFromPipeline=$true)][String]$json
    )
    
    $obj= [SimpleJson.SimpleJson]::DeserializeObject($json)
    
    if($AsPSObject)
    {
        $obj = ConvertJsonObjectToPsObject($obj)
    }
    
    return $obj
}

function ConvertTo-Json
{
    param(
        [object][Parameter(Mandatory=$true,ValueFromPipeline=$true)] $obj
    )
    
    return [SimpleJson.SimpleJson]::SerializeObject($obj)
}

function ConvertJsonObjectToPsObject
{
    param(
        [Object] $obj
    )
    
    if($obj -eq $null)
    {
        return $null
    }
    if($obj -is [System.Collections.Generic.IDictionary[string,object]])
    {
        $hash = @{}
        foreach($kvp in $obj)
        {
            $hash[$kvp.Key] = ConvertJsonObjectToPsObject($kvp.Value)
        }
        
        return $hash
    }
    if($obj -is [system.collections.generic.list[object]])
    {
        $arr = New-Object object[] $obj.Count
        
        for ( $i = 0; $i -lt $obj.count; $i++ )
        { 
            $arr[$i] = ConvertJsonObjectToPsObject($obj[$i])
        }
        
        return $arr
    }
    
    return  $obj
}
