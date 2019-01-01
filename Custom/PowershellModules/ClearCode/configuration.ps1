$global:ClearCodeConfiguration = @{
	ToRemoveDirectories=@('bin',
                    'obj',
                    'ClientApp\node_modules');
	ToRemoveFiles=@('ClientApp\package-lock.json')
}
