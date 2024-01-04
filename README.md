![Banner Full 1960x](https://github.com/CarterGames/BuildVersions/assets/33253710/cc6140ef-70b9-4f70-9748-f4f91850f124)


<b>Build Versions</b> is a <b>FREE</b> build version updater tool for Unity that keeps your track of your projects versioning so you don't have to. 

## Badges
![CodeFactor](https://www.codefactor.io/repository/github/cartergames/BuildVersions/badge?style=for-the-badge)
![GitHub all releases](https://img.shields.io/github/downloads/CarterGames/BuildVersions/total?style=for-the-badge)
![GitHub release (latest by date)](https://img.shields.io/github/v/release/CarterGames/BuildVersions?style=for-the-badge)
![GitHub repo size](https://img.shields.io/github/repo-size/CarterGames/BuildVersions?style=for-the-badge)

## Key Features
- Automatic build information updating.
- Choose when the build number updates.
- No user setup required.
- Support to update the player settings build number on build.
- Support to update the Android bundle code on build.

## How To Install
Either download and import the package from the releases section or the <a href="https://assetstore.unity.com/packages/tools/utilities/build-versions-cg-205184">Unity Asset Store</a> and use the package manager. Alternatively, download this repo and copy all files into your project. 

## Setup
None required once imported, though you can change some settings should you wish, more on this below:

This asset is designed to help automate a normally mundane task that most developers forget to change when making builds. The asset requires no setup on your part as it will do this itself when you first make a build. If you want to setup the asset yourself, just create an instance of Build Information & Build Versions Options and store them somewhere in your project. These can be created via the Create Asset menu via the path showed below:

![Build Versions - SO Path](https://user-images.githubusercontent.com/33253710/154333356-804a1fe1-d763-4f93-84c1-1b0c2406c6e9.png)

From here all you need to do is configure the asset to work as you wish. You can change the settings in the options scriptable object directly or via the settings editor window under <b>Tools/Build Verisons | CG/Settings</b>

### Changing Types

![Build Versions - Tool Menu Build Type](https://user-images.githubusercontent.com/33253710/154333394-6eb98a97-b262-4ea7-9833-92ef288835d0.png)

![Build Versions - Tool Menu Build Type - Sub](https://user-images.githubusercontent.com/33253710/154333406-8e5fade2-8e4d-474e-8232-f3326b3b65b5.png)


This is the only element of the asset that is not automated, as we can't read minds 😂 You can select a type of build via the tools menu. We've listed most common build types which we feel covers most of the types you will require. Should you want a different one, you can simply write it into the Build Information scriptable object.

### Player Settings Systematic Versioning

![Build Versions - Tool Menu x x x](https://user-images.githubusercontent.com/33253710/154333445-897da4e7-8b03-4c61-9b12-449e675414a7.png)

When enabled the asset also updates the player settings for which ever platform you are currently on as well. The current version will not be transferred when you change platform. For the version to be correct on the new platform, you will need to set the version in the player settings to the last build's version to keep the version in sync. 

The version number in the settings **MUST** follow the x.x.x or major/minor/build style formatting which is common in the industry. The asset only support 3 numbers separated by a dot (.), any other combinations will cause issues or errors. 

By default the asset will only increment the build number by 1 each build made. Should you wish to update the major or minor number you can do so manually or via the tools menu options.


## Documentation
You can access a online of the documentation here: <a href="https://carter.games/buildversions">Online Documentation</a>. A offline copy if provided with the package and asset if needed. 

## Authors
- <a href="https://github.com/JonathanMCarter">Jonathan Carter</a>

## Licence
MIT Licence
