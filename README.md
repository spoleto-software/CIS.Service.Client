# CIS.Service.Client

Чтобы подключить этот клиент, надо инициализировать две зависимости (например, в `Startup.cs`):
```
services.AddSingleton<ICisServiceProvider, CisServiceProvider>();
services.AddSingleton<ICisServiceTokenProvider, CisServiceNegotiateTokenProvider>();
```


Плюс дополнительно можно указать конвертеры для объектов:
```
services.AddSingleton<IContainerModelConverter, ContainerModelConverter>();
services.AddSingleton<IModelConverter, IIdentityConverter>().
```
Если не указать конвертеры, то вышеуказанные конвертеры будут использованы по умолчанию.
