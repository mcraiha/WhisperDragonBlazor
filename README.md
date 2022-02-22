# WhisperDragonBlazor
 WhisperDragonBlazor is [CommonSecrets](https://github.com/mcraiha/CommonSecrets) compatible password/secrets manager for browsers. Made with [Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor).

## Web link
Not there yet

## Requirements (use)

You have to have newish [internet browser](https://docs.microsoft.com/en-us/aspnet/core/blazor/supported-platforms?view=aspnetcore-6.0)

## Requirements (develop)

.NET 6 or newer

## How to run / develop
Move to [src](src) folder and the run
```
dotnet run
```
  
And then you can open http://localhost:5000 in your browser

## How to publish
Move to [src](src) folder and the run
```
dotnet publish -c Release
```
  
and content should be in **bin\Release\net6.0\publish\wwwroot** folder.

## Docker

In case you want to build a docker image that serves the WhisperDragonBlazor then use following command
```
docker build -t whisperdragonblazor .
```

and if you want to run that then

```
docker run -p 8080:80 whisperdragonblazor
```
and you can access it via browser by using [http://localhost:8080/](http://localhost:8080/)
## Licenses

All code files (*.cs, *.blazor) and HTML files (*.html) are under [Unlicense](https://unlicense.org/)

The CSS file (mvp.css) is under [MIT License](https://github.com/andybrewer/mvp/blob/master/LICENSE).