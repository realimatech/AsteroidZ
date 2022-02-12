# AsteroidZ
A demo asteroid project for portfolio purposes.

## Project Archtecture
The project's archtecture focus on grouping assets based on specific priorities, so that the development team can focus on a specific folder depending on the activity.
Take a look on the order below:
<div style="-webkit-column-count: 2; -moz-column-count: 2; column-count: 2; -webkit-column-rule: 1px dotted #e0e0e0; -moz-column-rule: 1px dotted #e0e0e0; column-rule: 1px dotted #e0e0e0;">
    <div style="display: inline-block;">
        - <b>Game Assets vs. Imported Assets</b> (The not yet used and dependant assets must stay out of the _Game folder for assurance of future update dependencies)
- <b>Build Assets vs. Embed Assets</b> (Scripts could be placed on the Common folder, which would be updated only in new builds, but I prefer not to mix it with art/music files to keep the repository separable by submodules)
- <b>Categories</b> (Depending on the folder, each group of assets must be bundled depending on their purpose and bond with other elements, also, assets that share their characteristics like parent prefabs, could be placed in a more external folder for the content build to create a more organized structure)
    </div>
    <div style="display: inline-block;">
        ![project-screenshot](https://i.imgur.com/mbpoLgM.png "project-screenshot")
    </div>
</div>

### Bundles
The bundles folder organization still isn't well organized. The idea is to separate the files by each entity instance and a shared folder for base assets like parent prefabs.

## Code Archtecture
The project scripts must take in consideration their file organization and assembly definitions, either in the assets folder or in the packages.
This project currently uses the dependency of:
- <b>2D Sprite</b> (for UI multiple cuts and border configurations)
- <b>New Input System</b> (for a better control over the inputs and scalability for multiplatform)
- <b>Device Simulation</b> (to toggle the device orientation and check notch)
- <b>TextMesh Pro</b>

Other packages are already imported for possible updates:
- <b>Addressables</b> Content Management, Memory Performance
- <b>Cinemachine</b> Camera Handling (Shakes and Zooms)
- <b>Timeline</b> Complex Animations (Cutscenes)
- <b>DoTween</b> Simple Animations (Intros, Enemy Ships, Bullet Motion Patterns, UI Motion)

<b>Universal RP</b> could be added in the future for better graphic results, but it was best for development performance not to install it for now.

### Game Design 
In a small project like this one, the fastest tool to help in the development process is to create ScriptableObjects ans Prefabs.
The naming pattern also helps in the process to help game designers to find the desired asset. The main scriptables from the project, are found by just searching files with '@default' in their names. This is what would be displayed. Notice that I've already saved this kind of research in the favorites to even help to quickly access the files.
![default-search](https://i.imgur.com/x9Eg97g.png "default-search")

### Ingame Scripts
I've separated this projects scripts by their roles in the project:
- Entities
- Interfaces
- Managers
- Mechanics
- UI

There's also a naming pattern for their roles to better separate the components into parallel functionalities (prioritizing CPU performance). Most of the classes don't have the update method as the most frequent to have this behaviour are the ones connected with the physics and input scripting. The others that have a short time parallel operation use Coroutines to wait a condition or a Callback event for specific actions that must be shared by the managers.

## Unfinished Features
For the `delivery-time` tag, I couldn't finish some of the desired features, that could bring a more interesting experience for the game:
- Asteroid division in smaller versions
- Asteroid timed respawn (affraid to lose control over spawn rate and don't have time to baleance the game)
- Level balancing variables
- Sound effects and music
- Visual effects (like particles and UI juiciness)
- Improve graphics
  
## Desired Features
In the long term, I wish to launch this game with some interesting features like:
- Upgrade system
- Shop system (Buy ships, Customize parts)
- Smarter threats
- Backend services
- Localization
- Ads
- Idle mode
- Campaign mode
- Multiplatform bridge