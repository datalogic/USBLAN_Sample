# USBLAN Sample
Demonstrates how to use the [datalogic-ce-sync library](https://www.nuget.org/packages/datalogic-ce-sync/).

## To compile
cd into the "USBLAN_Sample directory". Now run the following command:

  dotnet publish -c release -r win10-x64 --self-contained


This will generate a folder containing USBLAN_Sample.exe along with all necessary dll's, such that you can just drop the folder onto any Windows 7/8/10 PC and run the executable.  The folder is generated at:

  bin/release/netcoreapp2.0/win10-x64/publish

A compiled copy of that folder is availalbe [here](win10-x64) for your convenience as well.


