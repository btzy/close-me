# Close Me

Annoying program that will open more windows when you close one of them.

## Details

This program will open windows that are always on top of all other windows.  Upon closing one window, 2<sup>*n*</sup> more will appear (where *n* is the number of windows already closed, including this one).  Thirty seconds after the opening the first window, all windows will close automatically.

Use this program at your own risk.

## Command-line Arguments

 * `start=HH:mm:ss` - Open the windows at a specific time of the day (use the 24-hour clock).
 * `wait=n` - Wait for `n` seconds before opening windows.
 * `random` - Used with `wait` only.  Will wait for a random amount of time of up to `n` seconds before opening windows instead.
 * `repeat` - Used with either `start` or `wait`.  If used with `start`, will open windows at the specified time every day (this won't work once the user has shut down their computer).  If used with `wait`, will open windows every `n` seconds after the previous window-opening spree ends.
