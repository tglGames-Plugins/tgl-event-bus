# Change Log

### 2026-11-09 (1.1.0)
This Allows user to choose between 2 event bus implementation:
- **Channel Based Event Bus**: allows event bus to be made based on channels, so that each bus has multiple channels, and each channel has multiple events that the channel manages.
- **Binders Based Event Bus**: allows events to be bound by binders and listeners, the app only talks to the event binders and listeners. The binders and listeners manage the event bus without exposing the working to the modules of the app. 


### 2026-08-15 (initial)
A simple Event bus for use in my applications.  
It allows users to create a channel for a type and users can subscribe and publish to the channel.  
Each channel can be treated as a standalone event bus.This way the events can be clearly compartmentalized with less chance for confusion in responsiblity.



