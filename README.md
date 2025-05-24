# CalMod
CalMod is a modular BepInEx mod for The Black Grimoire: Cursebreaker.

Inspired by and partially based on functionality from [Stack Size](https://www.nexusmods.com/theblackgrimoirecursebreaker/mods/1) by m0nster and [StackSize Updated](https://www.nexusmods.com/theblackgrimoirecursebreaker/mods/2) by skybro

## Features
### - Making all items stackable (enabled by default)
This also increases the stack limit of limited stackable items to the max value.
### - Setting the drop rates for all mob drops to 100% (disabled by default)
(Except some rare instances like the barbarian ambusher/foragers, which have their drop tables and stats hardcoded when they are created)
### - Disabling the barbarian ambushes (disabled by default)
Do note that some items can only be obtained as drops from the ambushers.

## Bugs/Quirks
- **!!! Scrapping stacked items will only yield the scrap for 1 of that item and WILL CONSUME THE ENTIRE STACK !!!**
  - **!!! DON'T SCRAP STACKED ITEMS !!!**
- When you disable the stack mod, items will stay stacked but they don't show the number in inventory. Shouldn't result in any loss of items.
- Forge Fires spell from smithing trait will only smelt 1 ore from a stack, and leave the rest of the stack unsmelted. Won't result in any item losses but is a minor bummer.


## Installing BepInEx
Before you can build the mod or install it, you'll need to install BepInEx

You should download **the latest v5 version** from [the BepInEx releases page](https://github.com/BepInEx/BepInEx/releases)

Then install it as per the instructions on [the BepInEx installation guide](https://docs.bepinex.dev/articles/user_guide/installation/index.html), or just extract the contents of the zip from the above link into the folder where Cursebreaker.exe is

## Installation
- Make sure BepInEx is installed as above
- Make a folder called CalMod inside Cursebreaker\BepInEx\plugins
- Download the latest version of the mod from the [releases page](https://github.com/Calynxx/Cursebreaker-CalMod/releases)
- Place it into the new CalMod folder
- Run the game to generate the config files (will be in Cursebreaker\BepInEx\config)
- (OPTIONALLY) Install [BepInEx Configuration Manager](https://github.com/BepInEx/BepInEx.ConfigurationManager) to manage the configs in-game using a GUI

## Building
- Clone the repo
- Load up CalMod.sln in Visual Studio
- Fix assembly references (probably under Solution Explorer -> CalMod -> Dependencies -> Assemblies or in CalMod.csproj in the HintPath tags)
  - You should be able to find all of the referenced dlls inside \steamapps\common\Cursebreaker\Cursebreaker_Data\Managed
- You should be able to build once all references are fixed
