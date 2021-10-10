# Welcome to Expresso Bits Pools 👋
![Version](https://img.shields.io/badge/version-1.1.8-blue.svg?cacheSeconds=2592000)
[![Documentation](https://img.shields.io/badge/documentation-yes-brightgreen.svg)](todo-doc)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](MIT)
[![Twitter: ScriptsEngineer](https://img.shields.io/twitter/follow/ScriptsEngineer.svg?style=social)](https://twitter.com/ScriptsEngineer)

Unity performance issues generating and destroying multiple objects?
This simple library solves your problem in a simple way. With few steps your object can be reused several times, avoiding the *garbage collector*.
Create Simple and Easy Pool of objects! This extension creates easy use of Pool with objects that are instantiated and destroyed, avoiding excessive use of memory and processing.

## Features

✔️ Simple use,  Change only 2 line of your code!

✔️ Interfaces for events.

✔️ Pooler component for Unity Events.

## Usage
Simple change Instantiate/Destroy method for this.InstantianteInPool/this.DestroyInPool!.

### Before
```csharp
      Instantiate(prefab);
      ...
      Destroy(gameObject);
```

### After
```csharp
    this.InstantiateFromPool(prefab);
    ...
    this.DestroyInPool(gameObject);
```

### Advanced Usages

For more advanced use, scriptable Pool can be created by <b>Expresso Bits/Pools/Pool</b>

![Pool](https://raw.githubusercontent.com/wiki/ExpressoBits/EBPools/Images/Pool.png)

In the code just use:

```csharp
    pool.Instantiate();
    ...
    pool.Destroy();
```


## Install

### With Git Url
To install open <b>Window</b> > <b>Package Manager</b> and click on the + package icon and choose <b>"Add package from git url"</b> and type:

> https://github.com/ExpressoBits/Pools.git

and you're done!

### With OpenUPM
1. Add openupm registry. Click on the menu <b>Edit -> Project settings...</b>, and add a scoped registry like so:

Name: OpenUPM

Url: https://package.openupm.com

Scopes:

```com.openupm```
```br.com.expressobits```


2. Close the project settings
3. Open the package manager. Click on menu Window -> Package Manager and select "Packages: My Registries", select the latest version of EB Pools and click install
4. You may come back to the package manager to unistall EB Pools or upgrade it.
