param($installPath, $toolsPath, $package, $project)

Set-StrictMode -version 2.0

##-------------------------------------------------
## Globals
##-------------------------------------------------
[string] $basePath = "Registry::HKEY_CURRENT_USER\Software\Xceed Software"
[string] $licensesPath = $basePath + '\' + 'Licenses'

[byte[]] $NbDaysBits = 2, 7, 12, 17, 22, 26, 31, 37, 42, 47, 51, 55, 59, 62, 0xFF
[byte[]] $ProductCodeBits = 3, 16, 29, 41, 53, 61, 0xFF
[byte[]] $ProductVersionBits = 4, 15, 25, 34, 43, 50, 58, 0xFF
[byte[]] $ChecksumBits = 0, 9, 18, 27, 36, 45, 54, 63, 0xFF
[string] $AlphaNumLookup = "ABJCKTDL4UEMW71FNX52YGP98Z63HRS0"
[int[]] $td1 = 0x7d0, 0xb, 0x11, 0x2d
[int[]] $td2 = 0x7d0, 0xc, 0x11, 0xf
[int[]] $td3 = 0x7d0, 0xb, 0x1, 0x3d

[string[][]] $PackagesMap = 
        ('Xceed.Products.Wpf.DataGrid.Base', 'DGP'),`
        ('Xceed.Products.Wpf.DataGrid.Full', 'DGP'),`
        ('Xceed.Products.Wpf.DataGrid.Themes', 'DGP'),`
        ('Xceed.Products.Wpf.Toolkit.Full', 'WTK'),`
        ('Xceed.Products.Wpf.Toolkit.AvalonDock', 'WTK'),`
        ('Xceed.Products.Wpf.Toolkit.AvalonDock.Themes', 'WTK'),`
        ('Xceed.Products.Wpf.Toolkit.Base', 'WTK'),`
        ('Xceed.Products.Wpf.Toolkit.Base.Themes', 'WTK'),`
        ('Xceed.Products.Wpf.Toolkit.ListBox', 'WTK'),`
        ('Xceed.Products.Wpf.Toolkit.ListBox.Themes', 'WTK'),`
        ('Xceed.Products.Documents.Libraries.Full', 'WDN', 'WBN'),`
        ('Xceed.Products.Zip.Full', 'ZIN'),`
        ('Xceed.Products.Ftp.Full', 'FTN'),`
        ('Xceed.Products.RealTimeZip.Full', 'ZRT'),`
        ('Xceed.Products.SFtp.Full', 'SFT'),`
        ('Xceed.Products.Grid.Full', 'GRD'),`
        ('','')

[string[][]] $ProductIds = 
        ('','','',''),`
        ('ZIP','','',''),`
        ('SFX','','',''),`
        ('BKP','','',''),`
        ('WSL','','',''),`
        ('FTP','','',''),`
        ('SCO','','',''),`
        ('BEN','','',''),`
        ('CRY','','',''),`
        ('FTB','','',''),`
        ('ZIN','','Xceed Zip for .NET and .NET Standard','https://xceed.com/en/our-products/product/zip-for-net'),`
        ('ABZ','','',''),`
        ('GRD','','Xceed Grid for WinForms','https://xceed.com/xceed-grid-for-winforms/'),`
        ('SCN','','',''),`
        ('ZIC','','',''),`
        ('SCC','','',''),`
        ('SUI','','',''),`
        ('SUN','','',''),`
        ('FTN','','Xceed FTP for .NET and .NET Standard','https://xceed.com/en/our-products/product/ftp-for-net'),`
        ('FTC','','',''),`
        ('CHT','','',''),`
        ('DWN','','',''),`
        ('CHW','','',''),`
        ('IVN','','',''),`
        ('RDY','','',''),`
        ('EDN','','',''),`
        ('ZIL','','',''),`
        ('TAN','','',''),`
        ('DGF','','',''),`
        ('DGP','','Xceed DataGrid Pro for WPF','https://www.nuget.org/packages/Xceed.Products.Wpf.Datagrid.Full'),`
        ('WAN','','',''),`
        ('SYN','','',''),`
        ('ZIX','','',''),`
        ('ZII','','',''),`
        ('SFN','','',''),`
        ('ZRT','','Xceed Real-Time Zip for .NET and .NET Standard','https://xceed.com/en/our-products/product/real-time-zip-for-net'),`
        ('ZRC','','',''),`
        ('UPS','','',''),`
        ('TDV','','',''),`
        ('ZRS','','',''),`
        ('XPT','','',''),`
        ('OFT','','',''),`
        ('GLT','','',''),`
        ('MET','','',''),`
        ('LET','','',''),`
        ('WST','','',''),`
        ('DGS','','',''),`
        ('LBS','','',''),`
        ('ZRP','','',''),`
        ('UPP','','',''),`
        ('LBW','','',''),`
        ('BLD','','',''),`
        ('SFT','','Xceed SFtp for .NET and .NET Standard','https://xceed.com/en/our-products/product/sftp-for-net'),`
        ('WTK','','Xceed Toolkit Plus for WPF','https://www.nuget.org/packages/Xceed.Products.Wpf.Toolkit.Full'),`
        ('ZRX','','',''),`
        ('ZXA','','',''),`
        ('FXA','','',''),`
        ('SXA','','',''),`
        ('WDN','','Xceed Words for .NET, .NET Standard and .NET5','https://www.nuget.org/packages/Xceed.Products.Documents.Libraries.Full/'),`
        ('PDF','','Xceed PDF Creator for .NET, .NET Standard and .NET5','https://www.nuget.org/packages/Xceed.Products.Documents.Libraries.Full/'),`
        ('DGJ','','',''),`
        ('WBN','Xceed_Workbooks_NET', 'Xceed Workbooks for .NET, .NET Standard and .NET 5','https://www.nuget.org/packages/Xceed.Products.Documents.Libraries.Full/')

  
function shl{
param([System.UInt32] $value, [byte] $nb = 1)

    for([System.Int32] $i=0;$i -lt $nb;$i++)
    {
        $value = $value -band 0x7FFFFFFF
        $value *= 2
    }
    
    return $value
}


function shr{
param([System.UInt32] $value, [byte] $nb = 1)

    for([System.Int32] $i=0;$i -lt $nb;$i++)
    {
        $value = (($value-($value%2)) / 2)
    }
    
    return $value
}

##-------------------------------------------------
## Functions
##-------------------------------------------------
function MapBits{
param([System.Collections.BitArray] $barray, [System.UInt32] $val, [byte[]] $codeBits)

      for( [int] $i = 0; $i -lt ($codeBits.Length - 1); $i++ )
      {
        [int] $x = shl 1 $i
        $ba[ $codeBits[ $i ] ] = ( $val -band $x ) -ne 0
      }
}

function GetBytes{
param([System.Collections.BitArray] $ba)

    [byte[]] $array = New-Object System.Byte[] (9)
    for( [byte] $i = 0; $i -lt $ba.Length; $i++ )
    {
        if($ba[$i])
        {
            [int] $mod = ($i % 8)
            [int] $index = ( $i - $mod ) / 8
            $array[ $index ] = ($array[ $index ]) -bor ([byte]( shr 128 $mod ))
        }
    }
    return $array

}

function CalculateChecksum{
param([System.UInt16[]] $b )

    [System.UInt16] $dw1 = 0
    [System.UInt16] $dw2 = 0

    for([int] $i=0;$i -lt $b.Length;$i++)
    {
        $dw1 += $b[ $i ]
        $dw2 += $dw1
    }    

    ##Reduce to 8 bits
    [System.UInt16] $r1 = ($dw2 -bxor $dw1)
    [byte] $r2 = (shr $r1  8) -bxor ($r1 -band 0x00FF)

    return $r2
}

function GenAlpha {
param([System.Collections.BitArray] $ba)

  [string] $suffix = ''
  [int] $mask = 0x10
  [int] $value = 0
  for( [int] $i = 0; $i -lt $ba.Length;$i++)
  {
    if( $mask -eq 0 )
    {
      $suffix += $AlphaNumLookup[ $value ]
      $value = 0
      $mask = 0x10
    }

    if( $ba[ $i ] )
    {
      $value = $value -bor $mask
    }

    $mask = shr $mask
  }

  $suffix += $AlphaNumLookup[ $value ]

  return $suffix + 'A';
}

function FindPackageTasks {
param([string] $packageId)

    [array] $packageTasks = $null
    
    for( [int] $i = 0; $i -lt $PackagesMap.Length; $i++)
    {
        if($PackagesMap[$i][0] -eq $packageId)
        {
            $packageTasks = $PackagesMap[$i]
            break
        }
    }
    
    return $packageTasks
}

function FindIndex {
param([string] $prodId)
    
    if($prodId -ne '')
    {
      for( [int] $i = 0; $i -lt $ProductIds.Length; $i++)
      {
        if($ProductIds[$i][0] -eq $prodId)
        {
            return $i
        }
      }
    }
    
    return -1
}

function Create {
param([int] $pIndex, [int] $maj, [int] $min, [int[]] $d)

    ## Harcode others values that we dont need to customize.   
    $ba = New-Object System.Collections.BitArray 65
    $ba[6] = $true
    $ba[64] = $true

    [System.DateTime] $date = New-Object -t DateTime -a $d[0],$d[1],$d[2]
    [int] $days = [DateTime]::Today.Subtract($date).Days
    [int] $verNo = ($maj*10) + $min
    [string] $pPrefix = $ProductIds[$pIndex][0]
    [string] $prodId = "$pPrefix$verNo"
    
    MapBits $ba $pIndex $ProductCodeBits
    MapBits $ba $verNo $ProductVersionBits
    MapBits $ba $days $NbDaysBits
    
    [char[]] $a1 = $prodId.ToCharArray()
    [byte[]] $a2 = GetBytes $ba
    [System.UInt16[]] $a = New-Object System.UInt16[] ($a1.Length + $a2.Length)
    
    [System.Array]::Copy($a1,0,$a,0,$a1.Length)
    [System.Array]::Copy($a2,0,$a,$a1.Length,$a2.Length)
        
    [byte] $checksum = CalculateChecksum $a
    
    MapBits $ba $checksum $ChecksumBits

    return $prodId + (GenAlpha $ba)
}

function TestAndCreate {
param([string] $path)

    if(!(Test-Path $path))
    {
        $dump = New-Item $path
    }
}

function TestAndCreateDirectory {
param([string] $path)

    if(!(Test-Path $path))
    {
        $dump = New-Item -ItemType directory -Path $path
        
    }
}

function Setup {
param([int] $pIndex, [int] $major, [int] $minor)

    try
    {
        if($pIndex -lt 0)
        {
            Write-Host "Failed to find the product."
            return
        }        

        if(($major -lt 0) -or ($major -gt 9) -or ($minor -lt 0) -or ($minor -gt 9))
        {
            Write-Host "Failed to generate a license key."
            return
        }

        [int[]] $d = $null
        if( ( $pIndex -eq 58 ) -or ( $pIndex -eq 59 ) )
        {
            $d = $td2
        }
        elseif( $pIndex -eq 61 )
        {
            $d = $td3
        }
        else
        {
            $d = $td1
        }

 
        ## Create the trial key
        [string] $k = Create $pIndex $major $minor $d

        ## Begin - Write trial key to text file
        $trialpath = "C:\Xceed Trial Keys"
        TestAndCreateDirectory  $trialpath
            
        $pname = $ProductIds[$pIndex][2]
        [string] $prodVer = "$major.$minor"

        [DateTime] $e = [DateTime]::Today.AddDays($d[3])

        $key = "This is your trial key for """ + $pname + """ version " + $prodVer + ": " + $k + "`r`n"
        $key = $key + "`r`n"
        $key = $key + "Set the Licenser.LicenseKey property with this license key. See the component documentation topic on 'How to license the component' for details." +  "`r`n"
        $key = $key + "You can find the documentation of all Xceed components on line at: https://xceed.com/en/support/documentation-center/" +  "`r`n"
        $key = $key + "`r`n"
        $key = $key + "The key will expire on " + $e.ToString("MMMM dd yyyy") +  ".`r`n"
        $key = $key + "After the trial period is over, you can purchase a subscription to the component here: " + $ProductIds[$pIndex][3] +  "`r`n"
        $key = $key + "`r`n"
        $key = $key + "                                                     ***`r`n"
        
        $keyfile = "C:\Xceed Trial Keys\" + $package.Id + "_Key.txt"
        $key | Out-File -Append -FilePath $keyfile
        ## End - Write trial key to text file

        ## Write trial key to registry
        [string] $prodPath = $licensesPath + '\' + $ProductIds[$pIndex][0]
       
        TestAndCreate $basePath
        TestAndCreate $licensesPath
        TestAndCreate $prodPath

        [Microsoft.Win32.RegistryKey] $path = Get-Item $prodPath
        if($null -eq $path.GetValue($prodVer, $null))
        {
            Set-ItemProperty -Path $prodPath -Name $prodVer -Value $k
            
        }
    }
    catch{}
}


function ParseVersion {
param([string] $version)

    try
    {
        return [System.Version]::Parse($version)
    }
    catch
    {
        return $null
    }
}

function GetVSVersion {

    try
    {
        return ParseVersion $dte.Version
    }
    catch
    {
        return $null
    }
}

function GetPackageVersion {

    [System.Version] $vs = GetVSVersion
    [System.Version] $retval = $null

    if($null -ne $vs)
    {
        ## Visual Studio 2015 and later
        if($vs.Major -ge 14)
        {
            $retval = ParseVersion $package.Version
        }
        ## Visual Studio 2013 and earlier
        elseif($vs.Major -gt 0)
        {
            $retval = $package.Version.Version
        }
    }

    return $retval
}

function ExtractPackageVersion {

    [System.Version] $version = GetPackageVersion
    
    if($null -ne $version)
    {
        return $version.Major, $version.Minor
    }
    else
    {
        return -1, -1
    }
}

##-------------------------------------------------
## Entry Point (Main)
##-------------------------------------------------

# $dte = @{}
# $dte.Version = New-Object -TypeName System.Version -ArgumentList "14.0.0"

# $package = @{}
# $package.Id = 'Xceed.Products.Documents.Libraries.Full'
# $package.Version = New-Object -TypeName System.Version -ArgumentList "2.0.0"

# [string] $toolsPath = 'D:\Components\NET40\DocumentLibraries\Dev\Trunk\Installer\DocumentLibraries\NuGet\Temp'

[System.Collections.Hashtable] $assemblyProperties = $null
[string] $assemblyPropertiesFile = Join-Path -Path $toolsPath -ChildPath AssemblyProperties.txt
if( Test-Path -Path $assemblyPropertiesFile -PathType Leaf )
{
    [string] $assemblyPropertiesFileData = Get-Content -Raw -Path $assemblyPropertiesFile
    $assemblyProperties = ConvertFrom-StringData -StringData $assemblyPropertiesFileData
}

[array] $tasks = FindPackageTasks $package.Id

if($tasks -ne $null)
{
    for( [int] $i = 1; $i -lt $tasks.Length; $i++)
    {
        [string] $productCode = $tasks[$i]
        [int] $major = -1
        [int] $minor = -1

        [int] $pIndex = FindIndex $productCode

        [string] $assemblyName = $ProductIds[$pIndex][1]
        if( $assemblyName -ne '' )
        {
            if( ( $assemblyProperties -ne $null ) -and $assemblyProperties.ContainsKey( $assemblyName + '_Major' ) -and $assemblyProperties.ContainsKey( $assemblyName + '_Minor' ) )
            {
                $major = $assemblyProperties[ $assemblyName + '_Major' ]
                $minor = $assemblyProperties[ $assemblyName + '_Minor' ]
            }
            else
            {
                Write-Host "Failed to find assembly properties for '" + $assemblyName + "'."
                return
            }
        }
        else
        {
            $major, $minor = ExtractPackageVersion
        }

        Setup $pIndex $major $minor
        
        [string] $pUrl = $ProductIds[$pIndex][3]
        if($pUrl.Length -gt 0)
        {
            [void] $project.DTE.ItemOperations.Navigate($pUrl)
        }
    }
}