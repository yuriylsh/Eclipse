$global:ClearCodeConfiguration = @{
	ToRemoveDirectories=@('bin',
                    'obj',
                    'ClientApp\node_modules');
	ToRemoveFiles=@('ClientApp\package-lock.json');
	Destinations=@('Adpredictive code root;C:\code\adpredictive','Frontent Project;C:\code\adpredictive\campaign-management\frontend\src\Frontend')
}
