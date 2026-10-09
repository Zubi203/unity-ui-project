# Unity UI/UX Tech Demo
This is a portfolio project made to showcase DOTween UI animations and the MVC architecture. It features a simple title screen and a shop menu.

### Key Features
- Model-View-Controller architecture to control communication between UI components.
- the Model: a PlayerStatsModel script to store player data. This includes the player inventory and currency.
- The View: 3 separate view scripts to handle the user-facing display and button logic (UIView, ShopView, InventoryView).
- The Controller: a UIController script that is responsible for communication connecting methods and events between the Model and the View
- The Service Locator: Singleton class that allows the Model and View scripts to be registered as services. The Controller can then fetch a reference to these scripts using the Service Locator. This decouples the game systems by allowing the Controller to access the Model and the View without a direct reference.
- Bouncy button animations using DOTween.
- A customizable AutoTweenUIComponent script that automatically animates a UI element as it loads in or when it becomes active.
- A customizable ContainerAnimation script that automatically animates all child UI elements as it loads in or when it becomes active.

### Project Info
- This project was made in the Unity Engine and makes use of the DOTween package from the Unity asset store.
- The code base for this project was written in C#.
- The project can be played in browser on itch.io at this link: https://zubi-dev.itch.io/unity-uiux-tech-demo

### Credits
- All the code for this project was written by me
- UI assets: https://cupnooble.itch.io/sprout-lands-ui-pack
- Inventory and shop icons: https://clockworkraven.itch.io/raven-fantasy-icons
- Sound Effects: https://jdsherbert.itch.io/ultimate-ui-sfx-pack
- Music: https://pizzadoggy.itch.io/cozy-tunes

