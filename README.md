# GTween.NET
 Port of Grant Skinner's GTween to C# for .NET

# About

GTween.NET is a tiny (20 KB) library for programmatic tweening, animation and transitions. It is a C# port of the GTween ActionScript 3 library built by Grant Skinner. This port does not have complete feature parity with the ActionScript 3 library yet. Currently, it only supports building individual tweens with the GTween class. Plug-ins are currently not supported.

All easing classes from the original library are available.

|  | Ease In | Ease Out | Ease In-Out |
|-------------|---------|----------|-------------|
| Back | ![Back Ease In](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Back-EaseIn.png) | ![Back Ease Out](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Back-EaseOut.png) | ![Back Ease In-Out](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Back-EaseInOut.png) |
| Bounce | ![Bounce Ease In](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Bounce-EaseIn.png) | ![Bounce Ease Out](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Bounce-EaseOut.png) | ![Bounce Ease In-Out](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Bounce-EaseInOut.png) |
| Circular | ![Circular Ease In](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Circular-EaseIn.png) | ![Circular Ease Out](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Circular-EaseOut.png) | ![Circular Ease In-Out](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Circular-EaseInOut.png) |
| Cubic | ![Cubic Ease In](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Cubic-EaseIn.png) | ![Cubic Ease Out](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Cubic-EaseOut.png) | ![Cubic Ease In-Out](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Cubic-EaseInOut.png) |
| Elastic | ![Elastic Ease In](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Elastic-EaseIn.png) | ![Elastic Ease Out](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Elastic-EaseOut.png) | ![Elastic Ease In-Out](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Elastic-EaseInOut.png) |
| Exponential | ![Exponential Ease In](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Exponential-EaseIn.png) | ![Exponential Ease Out](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Exponential-EaseOut.png) | ![Exponential Ease In-Out](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Exponential-EaseInOut.png) |
| Linear | ![Linear Ease None](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Linear-EaseNone.png) |  |  |
| Quadratic | ![Quadratic Ease In](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Quadratic-EaseIn.png) | ![Quadratic Ease Out](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Quadratic-EaseOut.png) | ![Quadratic Ease In-Out](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Quadratic-EaseInOut.png) |
| Quintic | ![Quintic Ease In](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Quintic-EaseIn.png) | ![Quintic Ease Out](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Quintic-EaseOut.png) | ![Quintic Ease In-Out](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Quintic-EaseInOut.png) |
| Sine | ![Sine Ease In](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Sine-EaseIn.png) | ![Sine Ease Out](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Sine-EaseOut.png) | ![Sine Ease In-Out](https://raw.githubusercontent.com/pranavnegandhi/GTween.NET/master/assets/Sine-EaseInOut.png) |

# Quick Start

GTween.NET operates on double values. A programmer can get a tween up and running by providing just three parameters.

* A target object to operate on.
* Duration of the tween in seconds.
* A dictionary of property names and their final values.

```
var point = new Point(0, 0);
var values = new Dictionary<string, double>() { { "X": 10 } };
var tween = new GTween(point, 5, values);
```
The above code creates a new Point instance and animates its X value from 0 to 10 in 5 seconds. There are several properties exposed by the GTween class to offer more advanced control over the tween, both at the time of instantiation, as well as afterwards. Instance properties can be set at time of making an instance by using the C# object initializer syntax.

```
var tween = new GTween(point, 5, values)
{
    AutoPlay = false,
    DispatchEvents = false
}
```

They can also be set later on by setting the properties on the instance.

```
tween.DispatchEvents = true;
```

# Reference

GTween exposes the following properties and methods to control the state of the animation.

## Properties

### AutoPlay

Indicates whether the tween should automatically play when an end value is changed.

### CalculatedPosition

The current calculated position of the tween. This is a deterministic value between 0 and duration calculated from the current position based on the duration, repeatCount, and reflect properties. This is always a value between 0 and duration, whereas `Position` can range between `-Delay` and `RepeatCount * Duration`.

### CalculatedPositionOld

The previous calculated position of the tween. See `CalculatedPosition` for more information.

### Data

Allows you to associate arbitrary data with your tween. For example, you might use this to reference specific data when handling event callbacks from tweens.

### DefaultDispatchEvents

Sets the default value of DispatchEvents for new instances.

### DefaultEase

Specifies the default easing function to use with new tweens. Set to GTween.LinearEase by default.

### Delay

The length of the delay in seconds. The delay occurs before a tween reads initial values or starts playing.

### DispatchEvents

If true, it will dispatch Initialized, Changed, and Completed events in addition to calling the InitializedHandler, ChangedHandler and CompleteHandler callbacks. Callbacks provide significantly better performance, whereas events are more standardized and flexible (allowing multiple listeners, for example). By default this will use the value of DefaultDispatchEvents.

### Duration

The length of the tween in seconds. Setting this will also update any child transitions that have SynchDuration set to true.

### Ease

The easing function to use for calculating the tween. This can be any standard tween function provided in the GTween.Motion.Easing namespace, or any other custom tween function that matches the signature of the Easer delegate. New tweens will have this set to `DefaultTween`. Setting this to null will cause GTween to throw null reference errors.

### NextTween

Specifies another GTween instance that will have `Paused` set to false when this tween completes. This happens immediately before CompletedHandler is invoked.

### PauseAll

Setting this to true pauses all tween instances. This does not affect individual tweens' `Paused` property.

### Pause

Plays or pauses a tween. You can still change the position value externally on a paused tween, but it will not be updated automatically. While paused is false, the tween is also prevented from being garbage collected while it is active.

### Position

Gets and sets the position of the tween in seconds. This value will be constrained between `-Delay` and `RepeatCount * Duration`. It will be resolved to a `CalculatedPosition` before being applied.

* Negative values

Values below 0 will always resolve to a calculatedPosition of 0. Negative values can be used to set up a delay on the tween, as the tween will have to count up to 0 before initing.

* Positive values

Positive values are resolved based on the `Duration`, `RepeatCount` and `Reflect` properties.

### PositionOld

The position of the tween at the previous change.

### Ratio

The eased ratio (generally between 0-1) of the tween at the current position.

### RatioOld

The eased ratio (generally between 0-1) of the tween at the previous position.

### Reflect

Indicates whether the tween should use the reflect mode when repeating. If `Reflect` is set to true, then the tween will play backwards on every other repeat.

### RepeatCount

The number of times this tween will run. If 1, the tween will only run once. If 2 or more, the tween will repeat that many times. If 0, the tween will repeat forever.

### SuppressEvents

If true, events/callbacks will not be called. As well as allowing for more control over events, and providing flexibility for extension, this results in a slight performance increase, particularly if `UseCallbacks` is false.

### Target

The target object to tween. This can be any kind of object. You can retarget a tween at any time, but changing the target in mid-tween may result in unusual behaviour.

### TimeScale

Allows you to scale the passage of time for a tween. For example, a tween with a duration of 5 seconds, and a `TimeScale` of 2 will complete in 2.5 seconds. With a `TimeScale` of 0.5 the same tween would complete in 10 seconds.

### TimeScaleAll

Sets the time scale for all tweens. For example to run all tweens at half speed, you can set `TimeScaleAll` to 0.5. It is multiplied against each tweens `TimeScale`. For example a tween with `TimeScale` set to 2 will play back at normal speed if `TimeScaleAll` is set to 0.5.
