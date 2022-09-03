Renamer

The Renamer tool removes specific parts of a directory name to make the name scraper friendly to identify a particular movie.
The ideal name after the renaming is "MOVIENAME (MOVIEYEAR)" - which is best to identify the movie in e.g. Plex.

On startup the patterns.txt is loaded.
The "Source pane" accepts directories via drag'n'drop. 
The "Preview pane" show the renamed directories according to the applied patterns.

Directory parts that are in the patterns.txt are removed from the directory name.
This is realized via a regexp match and replacement with '' - which means nothing.

Furthermore Renamer tries to find a four digit number starting with 1 (e.g. 1980) or 2 (e.g. 2010) and makes a finding the MOVIEYEAR.
If found such a MOVIEYEAR in brackets () is appended to the MOVIENAME.

The button "Rename" triggers the rename action on filesystem level.
The button "Clear" removed entries from the panes.
The button "Edit" starts notepad to edit the pattern list.
The button "Refresh" applies the changed patterns.txt to the current entries.


TODO
- add filesystem watcher for patterns.txt
- handle existing directory exception
- optimize refresh behavior 