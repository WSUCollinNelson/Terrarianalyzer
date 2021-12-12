WELCOME TO TERRARIAnalyzer

The purpose of this project is to create a simple application that mines Terraria world saves for useful information. 

To run the demo of the application navigate to the "Demo Build" directory in this folder. 
	Run Terrarianalyzer.exe (Note this is Windows Forms Application)

It will immediately open an OpenFileDialog. Select one of the .wld files in the "Demo Build/DemoWorldSaves" directory, which contains one played-in world saved and two different untouched world saves.

Give it a few seconds to load data from the file, and then you can view your results.

THE APPLICATION

DEPTH MAPS

The first tab in the app allows you to produce graphs which plot frequency of occurance for any tile in the game by depth

Select a tile in the dropdown at the top of the screen, the resulting graph plots depths top to bottom of the world on the X axis with frequency along the y-axis

GENERAL INFO

This tab displays general world info. On the left are a few interesting stats. On the right there is a second tab window that allows you to view global pie charts highlighting the most frequently occuring tiles, and the most frequently occuring items in chests counted by unique occurances. 

CHEST INFO

This tab allows you to view the occurances of any item in the game within the world's saved chests. To use...

Select an item from the dropdown. The list contains ALL possible items, not all can be in naturally occuring chests.

This will display a full list of the locations of all of chests that contain that item as well as a plot that shows the relative locations of those chests on the world map.



