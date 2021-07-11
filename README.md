
# Twitcher

Twitcher is a C# library based on [TwitchLib](https://github.com/TwitchLib/TwitchLib) with some features

## Usage

To create TwitcherClient you have to use TwitchLib ConnectionCredentials
```c#
 TwitcherClient client = new TwitcherClient()
            .UseTwitchLibProvider(new ConnectionCredentials("liphitc", "somerandomnotrealtoken")) 
```
Then you can JoinChannels
```c#
 client.JoinChannel("LiphiTC");
```
or
```c#
client.JoinChannels(new string[] { "LiphiTC", "pajaDank" } );
```

And Connect Bot
```c#
client.Connect(); 
```

## Controllers
One of the feature of Twitcher is Controller

To Use Controllers 
```c#
ControllerBuilder builder = client.UseControllers();
```
It will сreate instance of ControllerBuilder

To Build it
```c#
builder.BuildControllers();
```


Then you can create a controller class
```c#
public class TestController : Controller
{
        [Same("WoahBlanket")]
        public void TestController() {
                Send("WoahBlanket");
        }
}
``` 
Attributes for controllers and controller methods:
```c#
[Vip]
[Mod]
[Subscriber]
[Subscriber(int)]  Min Subscriber Mounth
[User(string)]  User, allowed to use this controller. "any" for all users. By default "any"
[Channel(string)]  Channel, where controller can be used. "any" for all connected channels. By default "any"
```
Also, for methods you can use:
```c#
[NonCommand] 
[StartWith(string, RegiterCheck(bool), IsFullWord(bool))]
[Contains(string, RegiterCheck(bool))]
[Same(string, RegiterCheck(bool))]
[Single]
```
By default all methods of all matching controllers will be executed\
If you want only a specific method to be called add [Single]\

Controller has 2 Methods: Send(string) and SendAnswer(string message)\
and 2 Properties: Message and Client


## Controller Services
Controller services are provided as parameters to the controller constructor  

To create you service create Factory, Service class and Service Settings

Example Factory: 
```c#
public class ExampleServicerFactory : IServiceFactory<ExampleService, ExampleServiceSettings>
 {
        public ExampleService ServiceFactory(ExampleServiceSettings settings, TwitcherClient client, ChatMessage message)
        {
            return new ExampleService();
        }
 }
```
Then you can register service in ControllerBuilder
```c#
builder.RegisterService<ExampleService, ExampleServiceSettings, ExampleServiceSettings>(new ExampleServiceSettings());
```
And use it in controller
```c#
public class TestController : Controller
{
        public TestController(ExampleService service) 
        {
        }
}
``` 
## Json Helper

## Project uses
[TwitchLib](https://github.com/TwitchLib/TwitchLib)
[Config .NET](https://github.com/aloneguid/config)
[Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)


## License
[MIT](https://choosealicense.com/licenses/mit/)
