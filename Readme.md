# Event Bus
A Event Bus with Channel support to use in Unity applications.

### Limitation
Known Limitations:
- The Events are fire and forget, so we do not throw error if there are no listeners for the events being raised.  
- We also do not throw error if you try to add the same event multiple times or try to remove events that were never added.

### Idea
An Event Bus plugin for Unity which is useful for projects where the requirements change frequently.  
Should be able to create custom events and channels for each of these custom events, then new events are created that follow the channels and treat the channel as a stand alone event bus.

### Future Improvements
#### Messsage Bus
Can later be improved into a **MessageBus** or we can add a separate **message bus** in same package later.
- *Primary Scope*: **Cross-Process / Distributed**: Can send data between threads, microservices, or networks.
- *Communication Style*: **Multi-Pattern**: Sub, Point-to-Point (Queues), Request-Response (RPC), and Command routing.
- *Payload Nature*: **Commands & Messages**: Can be Events, Commands ("Do X"), or Queries ("Get Y").
- *Reliability & Persistence*: Supports queuing, persistence, retries, and dead-letter channels for guaranteed delivery.
- *Coupling*: Low temporal coupling (producer can push a message even if the consumer is offline).

#### Assembly Binders
I would like to run a reflection based Event Bus that can find all events that are created in the app and auto add all listeners and clear them on app quit, without depending on user to remember to add or remove them.
This can be achieved by following the ideas of [git-amend](https://www.youtube.com/@git-amend) in the video [Learn to Build an Advanced Event Bus | Unity Architecture](https://www.youtube.com/watch?v=4_DTAnigmaQ). 


### How does it work?
#### Channel Based Event Bus
How does our channel based event system work:  
- Base Setup
  - We define a base event (`IAudioEventBase`)
  - We define a channel for this base event (`AudioModuleEventChannel`)
  - we define a new event (child of base event) running on defined channel (`PlayAudioEvent`)
- In listener modules of our app 
    - we define a handler to run when the new event (child of base event) is fired on specified channel (`TestHandlerWithChannels.OnPlayAudio`)
    - we add listeners(subsribe) and remove listeners(unsubscribe) as needed on the channel (`(IEventChannel<AudioModuleEventChannel, IAudioEventBase>).Subscribe<PlayAudioEvent>(OnPlayAudio)` and `(IEventChannel<AudioModuleEventChannel, IAudioEventBase>).Unsubscribe<PlayAudioEvent>(OnPlayAudio)`)
- in trigger module of the app 
    - we trigger the new event (child of base event) from the channel specified (`(IEventChannel<AudioModuleEventChannel, IAudioEventBase>).Publish(new PlayAudioEvent("SFX_SwordHit"))`)  

**Pros**:
- We can keep all events grouped into channels, 
- We can find dependencies as well as remove or update the dependencies and references in modular groups.
- If we choose to remove the current EventBus and shift to something like a MessageBus, the code only updates in Channels and the user modules can be left unaltered.

**Cons**:
- Every module has access to all channels and all events, so removing or changing the events in the bus will be hard as code knows about all channels and we cannot remove channels safely.
  - This also means, there is no encapsulation, the channels and events are public and accessible to all. Any module can raise or listen to any other without any restrictions.

> Here everyone knows the Channel and the event, they do not know event bus and how data transmits, but know about all channels made in the app.

#### Event Listeners & Event Binders
**Event Listeners**: We can add Event Listeners that listen to events on a channel and then define which methods or handlers are attached to an event.  
This is generally done so our modules that are listening to events listeners, do not know of the event bus or event channel and only communicate with the event listeners. We can also change the event bus or channels with minimal change in our event listener code as the rest of the logic is independent of the event bus.

**Event Bindings**: Bindings will define event actions or delegates, so we can add a listener for the events using the bindings.
Channels will add all bindings that bind events related to this channel.
When a event happens, we tell binding to raise the event, binding will tell channel to raise it and transmit it ahead.
There are 2 ways to handle it:
- Make a binder for the listener and another binder for the trigger
  - This will allow us to trigger the event or define the handler without having access to the other part, it is very useful for bridge or middleware type of modules that should only listen or trigger the events.
- Make a single binder code for both handler and trigger
  - This will allow us to talk to same events while having access to both event trigger as well as handlers

How does our binder based event system work:
- Base Setup:
  - The package has a base interface for all child classes: `IBinderBasedEvent`
  - We also have defined a class called `RaiserListenerHandler` which has both `IBinderEventRaiser` and `IBinderEventListener` which can be used as binders for listening and triggerring the events.
    - you can bypass this and implement class that will be derived from `IBinderEventRaiser` and `IBinderEventListener` if you want them to have segregation of listener and raiser modules in the app.
  - We define a base event: `ICombatEventBase`
  - Now we need concrete events that will be triggered and listened to: `IWeaponCombatEventBase`, `IHandToHandCombatEventBase`, etc.
  - We then define a channle for this base event: `CombatChannel`
    - The channel defines the binders for base events (`HandToHand`, `Weapon`) and our modules use the binders to raise and listen to events.
- In listener modules of our app
  - In a new class, we find and call `Subscribe()` method on `Listener` of `RaiserListenerHandler` (`PunchFXReceiver._handToHandListener`)
- In trigger module of the app
  - In a new class, we find and call `Raise()` method on `Raiser`  of `RaiserListenerHandler` (`PlayerMelee.__handToHand`)
    
Pros:
- The Modules can either only have access to `IBinderEventRaiser` or `IBinderEventListener` child classes, this way, we can keep binders segregated and our modules only depend on the binders.
  - This can be enforced using the Assembly definitions ensuring we give limited access across assemblies
- The Channel or Event Bus can be edited or replaced without effecting the user modules.

Cons:
- For each new event we create, we will have to create 'event binder' and 'event listener' for the new event
  - We can make the base of binder event (`IHandToHandCombatEventBase`) as a base and group multiple events, but this may have it's own issues.
- Someone can skip listener and try to add event handler methods directly in the 'event binder'
  - `IBinderEventListener` has `Subscribe()` and `Unsubscribe()`, but there is no restriction stopping anyone else from calling `BinderBasedEventBus.Subscribe(handler)` as written in `RaiserListenerHandler`
- For a simple system, this is a lot of code to track the flow in case of bugs.

> Events know the binder, binder knows the channel, channel talks to the event bus, event bus triggers events in channel, channel events are listened to by event listeners, modules of the app subscribe and unsubscribe to event listeners.

### How to Use
#### Channels Based Event Bus
To use the package, you can follow the steps:
- Channel Based
  - Create a Interface which will be base for all your events, for e.g., `IAudioEventBase`  
  - Create a Channel `AudioModuleEventChannel` on which all events of type `IAudioEventBase` will be published and subscribed to.
  - Now, we can define events like `PlayAudioEvent` which will be published and subscribed for in Channel `AudioModuleEventChannel`
  - This event `PlayAudioEvent` can now be used as seen in  `TestHandlerWithChannels` and `TestRaisingChannelBasedEvents`
- Binder Based
  - Create a Interface which will be base for all your events, for e.g., `ICombatEventBase` which should be a child of `IBinderBasedEvent`
  - Create a few child classes of the base which will be used for binders, for e.g., `IHandToHandCombatEventBase`
  - Crate a channel `CombatChannel` which defines `RaiserListenerHandler` variables for the derived or concrete classes, for e.g., `CombatChannel`
  - Now, when someone wants to raise or trigger events of the concrete types, we call `Raise()` method of the `RaiserListenerHandler` variable, for e.g., `PlayerMelee.__handToHand.Raise()`
  - We can also listen for these events using the `Subscribe()` and `Unsubscribe()` methods of the `RaiserListenerHandler` variable, for e.g., `PunchFXReceiver.__handToHandListener.Subscribe()`


### Samples
Samples can be found in "Samples" folder.     
No Scene is added so user can add and explore on their own.  
This Runtime script only works on code front without a Gameobject or Scriptable object requirement.  


### How to add this package?
See [How to add this package](HowToAddPackage.md) for detailed instructions.


References: 
- https://www.youtube.com/watch?v=4_DTAnigmaQ
- https://www.youtube.com/watch?v=95eFgUENnTc&t=36s
- https://www.youtube.com/watch?v=4_DTAnigmaQ&t=2s


