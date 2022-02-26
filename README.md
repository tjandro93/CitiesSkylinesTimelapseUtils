# Cities Skylines Timelapse Utils
This mod includes (eventually) a few utilities to help with creating timelapses.

## Implemented features
### Auto save
Cities Skylines has their own auto-save feature but you only get one slot. So everytime the game auto saves the old one is overwritten. When making timelapses it's useful to have a save for each frame of your timelapse.

This mod's auto-save feature will create a new save on the set interval (defaulted to once a minute). The save game will be named "AutoSaveComponent yyyy-MM-dd HH-mm" where the time portion is filled in based on the date and time of the city. You can change the interval or enable / disable the feature in the mod settings.

## Future ideas
### Custom auto-save names
Add mod settings option to let user specify their own string format for the auto save name. Look into making it so they can use real time or city time if they want. Also look into if there's a way to get the city name automatically included.

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

