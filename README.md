# CIS.Service.Client

Чтобы подключить этот клиент, надо инициализировать две зависимости (например, в `Startup.cs`):
```
services.AddSingleton&lt;ICisServiceProvider, CisServiceProvider&gt;();
services.AddSingleton&lt;ICisServiceTokenProvider, CisServiceNegotiateTokenProvider&gt;();
```


Плюс дополнительно можно указать конвертеры для объектов:
```
services.AddSingleton&lt;IContainerModelConverter, ContainerModelConverter&gt;();
services.AddSingleton&lt;IModelConverter, IIdentityConverter&gt;().
```
Если не указать конвертеры, то вышеуказанные конвертеры будут использованы по умолчанию.
