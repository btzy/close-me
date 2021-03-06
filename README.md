# Close Me

Program that will open more windows when you close one of them.  Works on Windows with .NET Framework 4.5.2 or later.  *Note: May cause frustration to less technically inclined users.*

## Download

[Download the executable file here](https://btzy.github.io/close-me/CloseMe.exe).

## Details

This program will open windows that are always on top of all other windows.  Upon closing one window, 2<sup>*n*</sup> more will appear (where *n* is the number of windows already closed, including this one).

Each window is opened at a randomly chosen location and is given a background of a randomly chosen color.

Thirty seconds after the opening the first window, all windows will close automatically.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

## Command-line Arguments

 * `start=HH:mm:ss` - Open the windows at a specific time of the day (use the 24-hour clock).
 * `wait=n` - Wait for `n` seconds before opening windows (`n` must be a positive integer).
 * `random` - Used with `wait` only.  Will wait for a random amount of time of up to `n` seconds before opening windows instead.
 * `repeat` - Used with either `start` or `wait`.  If used with `start`, will open windows at the specified time every day (this won't work once the user restarts their computer - to do this, Close Me has to be added to the startup programs).  If used with `wait`, will open windows every `n` seconds after the previous window-opening spree ends.
 
