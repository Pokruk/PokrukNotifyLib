# PokrukNotifyLib

### About
It's a BepInEx plugin for displaying notifications to players. It does nothing on its own, as it is intended for use by developers, not players, similar to Utilla, for example.

This library is a kind of fork of a notification library made by someone not actually known. The code was forked many times and, I guess, skidded too.

### Main idea
* I wanted to have a single library so you don't need to code it yourself or think about workarounds to make your notification library avoid conflicts with another mod's notification library.
* Built-in soft dependency support - the ability to use the notification library as an optional (soft) dependency, so if a user hasn't installed your notifications plugin, your plugin will still work.
* Implement a bunch of bug fixes and features listed below.

### Features / Bug Fixes
1. Optimization - notifications themselves are far more optimized now.
2. Message stacking:
   Instead of
   ```
   Hello
   Hello
   Hello
   ```
   You'll get
   ```
   Hello (3)
   ```
3. Fixed positioning, so the camera is less shaky when you jump/run.
4. Fixed message order bug, which led to unpredictable message clearing if messages had different clear times.
5. Fixed the clear mechanism itself, so you don't have to worry about new lines in the message.
6. Fixed infinite clear time caused by the same scenario.
7. Fixed memory leaks and errors caused by using `Task` instead of coroutines (multithreading issues).
8. Added the ability to delete messages before the clear time.
9. Added the ability to use different clear times without any of the bugs described above.

### Installation
As with any regular Gorilla Tag BepInEx plugin, you need to place it in the `../BepInEx/plugins` folder.

# Usage

## Hard dependency
1. Add the plugin DLL to your plugin references.
2. Add the BepInDependency attribute `[BepInDependency("org.pragmate.pokruk.notifications")]` to your `BaseUnityPlugin` child.

   Example:
   ```cs
   [BepInDependency("org.pragmate.pokruk.notifications")]
   [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
   public class Plugin : BaseUnityPlugin {
       //...
   }
   ```
3. Use the `PokrukNotifyLib.Notifications.NotifiLib` class.

## Soft (optional) dependency
1. Add the plugin DLL to your plugin references.
2. Add the BepInDependency attribute `[BepInDependency("org.pragmate.pokruk.notifications", BepInDependency.DependencyFlags.SoftDependency)]` to your `BaseUnityPlugin` child.

   Example:
   ```cs
   [BepInDependency("org.pragmate.pokruk.notifications", BepInDependency.DependencyFlags.SoftDependency)]
   [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
   public class Plugin : BaseUnityPlugin {
       //...
   }
   ```
3. Copy `PokrukNotifyLib.Notifications.NotificationsAPI` to your project.
4. Use the copied class.

# Dependents
* [AntiCheatReportNotifier](https://github.com/Pokruk/AntiCheatReportNotifier) - a simple mod notifying through PokrukNotifyLib (hard dependency) that your client reports someone
