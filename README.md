# AspNetCore.Consul.Config

Consul como proveedor de configuracion para ASP.NET Core

Ejecutar run.sh o los siguientes comandos:

```
dotnet publish -c Release
docker-compose down
docker-compose up --build --force-recreate -d
```

Dentro de Consul, en KV crear una carpeta llamada `WebApi/` y dentro una llave `appsettings.json` con el siguiente contenido:

```
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Debug",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "Console"
      }
    ],
    "Properties": {
      "ApplicationName": "AspNetCore.Consul.Config"
    }
  },
  "AllowedHosts": "*",
  "App": {
    "Name": "Consul App Configuration"
  }
}
```

TODO: Iniciar Consul con esta configuracion ya cargada.

Luego reiniciar el contenedor de WebApi para que pueda configurar correctamente SeriLog:

`docker container restart webapi`
