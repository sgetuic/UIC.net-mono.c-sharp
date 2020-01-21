# Azure Project Agent for UIC

`UIC.Communication.Azure.ProjectAgent` adds Remote configuration capabilities to the UIC.

It leverages use of [Azure storage](https://azure.microsoft.com/en-us/services/storage/) 
for UIC project configuration database.

## Configuration

The project agent configuration contains link to the Azure Storage collection 
where each project configuration is stored as a blob (serialized JSON).

The Agent uses UIC.Framework.DefaultImplementation.SgetUicProject as a project configuration implementation.

### Configuration sample:

```
{
  "AzureStorageConnectionString": "https://sgetuicprojects.blob.core.windows.net/projects"
}
```

See project-26895846c960465ebd89f28d10e6460c.json as a project sample.

The json is serialized object instance constructed in `UIC.SGeT.Launcher.PstUicProject.cs`

## Limitations

Only public access blobs are supported. Futire implementation will support authorized access to the Azure storage.

