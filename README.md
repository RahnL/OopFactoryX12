# Project Description 
(From original codeplex site)

This is an open source .NET C# implementation of an X12 Parser.

The parser allows for a specification of any X12 transaction set to create a generic X12 xml representation of the hierarchical data contained within the X12 document.

No database integration is required by design, though  you can use the ImportX12 app to parse into a SQL Server database and skip the XML.

## Motivation
The motivation for this project is to provide a quick and easy way to examine edi files without the overhead of creating databases as the target for parsed EDI files.

It aims to demystify the X12 standard and make parsing it accessible to the masses, not to just very expensive parsing tools.
It also aims to reduce the overhead of getting started with X12 parsing by not requiring any database integration to solve the problem of parsing the file.

