Digtal clock with alarm [v1.3]


With this package you can add to your application or game unique electronic alarm clock. You can set the time or synchronize it with the real-time. Set an alarm clock that will ring in time is necessary.

Package contains the following elements:

 - Two 3D models of electronic clocks (Middle and low poly)
 - Texture map for the models
 - Digital font
 - ClockManager and ClockManagerEditor scripts (C#)
 - Necessary text shader
 - Special alarm sound
 - Clock prefab

Manual:

1. Pick and drag to the scene "Prefabs/DigitalClock" prefab;
2. Find the Clock Manager component and try to change following options:
 
 - System time. if true then the clock will display the current time on your system. Otherwise, you will be available three fields for set the current time.
 - Time Control. Set the direction of time
 - Time speed. This field controls the speed of time.
 - Alarm clock. If true then you can set the alarm on clock by setting fields.
 - Duration. This field allows you to set the duration of the alarm in seconds.
 - Color. This field is responsible for color digits and lights.
 - Events. With this field you can create events. Just click to add, assign a unique name for the event and set the time. Then you can call the "GetEvent(string eventName)" method from your script to check the event.

Use the following methods in the scripting:

To access methods use GetComponent function (http://unity3d.com/support/documentation/ScriptReference/GameObject.GetComponent.html)

Using C#:

 1. GetCurrentTime(); //Use it when you want to show current time

Example: 

MyStringText = "Current time is " + GameObject.Find("DigitalClock").GetComponent<ClockManager>().GetCurrentTime();

-----------------------

 2. SetCurrentTime(int hours, int minutes, int seconds); //Use it when you need to set the current time.

Example: 

GameObject.Find("DigitalClock").GetComponent<ClockManager>().SetCurrentTime(13, 15, 59);

-----------------------

 3. SetAlarmTime(int hours, int minutes, int seconds)

-----------------------

 4. Switch(bool enableClock); //Use it to turn on/off the clock

Example: 

GameObject.Find("DigitalClock").GetComponent<ClockManager>().Switch(true)

-----------------------

 5. GetEvent(string eventName); //Calls the event checking. 

Example:

if (GameObject.Find("DigitalClock").GetEvent("MyEvent") == true)
	Debug.Log("The event just happened!");

-----------------------

 6. SetTimeControl(TimeControl type, int speed); //Use it when you need to change speed and direction of time

Example:

GameObject.Find("DigitalClock").SetTimeControl(TimeControl.Backward, 10)

___

Changelog:

V1.0 - v1.3

*Some bugs fixed
*Added new methods
*Added event manager
*Added low poly digital clock model
