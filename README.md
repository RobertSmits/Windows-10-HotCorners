# HotCorners

This program provides hot corners for Windows 10


## Configuration

Upon first run a default config file will be created in the users profile folder.

### Corner Actions

The configuration contains fields for all corner of your screen, following actions can be linkt to these corners:

- `NoAction`
  Don't do anything.
- `Start`
  Open the Start Menu, due to a limitation in windows the start manu will always open on the primary monitor, to work arround this issue see the `Click` action.
- `TaskView`
  Open Windows 10 Task View
- `Desktop`
  Show the desktop 
- `Explorer`
  Open a new Explorer window
- `Click`
  Preform a mouse click. This action can be given to the corner where the Start Button is located to open the Start Menu on the current monitor.

### Further configuration

- `MultiMonitor`
  When enabled all corners on all availables monitors will be used, when disabled only the primairy monitor will be used.
- `DisableOnFullScreen`
  When enabled all actions will be ignored if the current running program is running in full screen.

## Closing the program

When logging is disabled the program runs without interface, to stop the program you must start a second instance which will then stop both running instances.