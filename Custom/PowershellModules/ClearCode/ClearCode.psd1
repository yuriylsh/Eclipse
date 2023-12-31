@{

# Script module or binary module file associated with this manifest.
RootModule = '.\ClearCode.dll'

# Version number of this module.
ModuleVersion = '0.1'

# ID used to uniquely identify this module
GUID = '6a86f77b-d06f-4504-8a3d-46cd9750f26f'

# Author of this module
Author = 'Yuriy Lyeshchenko'

# Company or vendor of this module
CompanyName = 'Yuriy Lyeshchenko'

# Copyright statement for this module
Copyright = '(c) 2018 Yuriy Lyeshchenko. All rights reserved.'

# Description of the functionality provided by this module
# Description = ''

# Assemblies that must be loaded prior to importing this module
RequiredAssemblies = @()

# Modules to import as nested modules of the module specified in RootModule/ModuleToProcess
NestedModules = @()

# Functions to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no functions to export.
FunctionsToExport = @('Clear-Project')

# Cmdlets to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no cmdlets to export.
CmdletsToExport = $()

# Variables to export from this module
VariablesToExport = @()

# Aliases to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no aliases to export.
AliasesToExport = @()

# Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
PrivateData = @{

    PSData = @{
	
    } 
} # End of PrivateData hashtable
}

