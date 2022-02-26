# Cities Skylines Timelapse Utils
This mod includes (eventually) a few utilities to help with creating timelapses.

## Implemented features
### Auto save
Cities Skylines has their own auto-save feature but you only get one slot. So everytime the game auto saves the old one is overwritten. When making timelapses it's useful to have a save for each frame of your timelapse.

This mod's auto-save feature will create a new save on the set interval (defaulted to once a minute). The save game will be named "AutoSave yyyy-MM-dd HH-mm" by default where the time portion is filled in based on the date and time of the city. You can change the interval or enable / disable the feature in the mod settings.

**Settings in mod option menu**
* Enable auto save
	* Whether the auto save feature is turned on or off. Change this after your game has loaded will stop / start future auto saves. Also note that changing it resets the timer.
	* Enabled by default
* Auto save interval (in seconds)
	* The delay between auto saves. Changing this will reset the timer. This value must be greater than 0. If you put a different value in it will revert to the default.
	* 60 seconds by default
* Auto save format
	* A C# format string that determines the save name. 
	* There are two parameters passed in to the format. {0} is `Threading.renderTime` which is essentially the in game data and time. {1} is `DateTime.Now`; the current date and time of the user's computer.
	* You can use any standard or custom date-time format string provided in C#. See  [Standard date and time format strings](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings) and [Custom date and time format strings](https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings) for available options.
	* Note that if the save name is one that already exists, the old save will be overwritten. This means that if you use the default value while the game is paused saves will be overwritten by the current game state every time until you resume. Furthermore, if you say only include the year day and month in your save name format you will only end up with at most one unique game save per day.
	* AutoSave {0:yyyy-MM-dd HH-mm} by default

## Future ideas
### Custom auto-save names
Look into if there's a way to get the city name automatically included.

### Max save count
It'd be nice to have a configuration option of how many total saves there can be. So if we save more than the limit we would either delete the oldest save or not save at all. This would limit how much disk space is used up by the mod.

### Savable cameras positions
There's an existing mod that does this but it would be nice to include it here as a custom implementation. Basically have the ability for the user to save multiple camera positions so that as they move between saves they can go directly to the exact same spot for their screenshot.

### Auto-screenshotter
It would be really nice to automate the process of manually taking screenshots. The manual process is: 

1. Load save
2. Go to saved camera position
3. Take screenshot
4. Repeat for every save

I think theoretically we can automate enumerating through the saves and taking the screenshots. 

### Camera paths
Again, there's an existing mod for this but I think it'd intended for only a single mod (although I should try it out myself). It would be really neat if you could set a camera begin and end position and then as you move through loads it eases the camera between the two positions creating a moving perspective in the timelapses.
