# Welcome to Expresso Bits Pools 👋
![Version](https://img.shields.io/badge/version-0.9.2-blue.svg?cacheSeconds=2592000)
[![Documentation](https://img.shields.io/badge/documentation-yes-brightgreen.svg)](todo-doc)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](MIT)
[![Twitter: ScriptsEngineer](https://img.shields.io/twitter/follow/ScriptsEngineer.svg?style=social)](https://twitter.com/ScriptsEngineer)

## Features

✔️ Simple use,  Change only 2 line of your code!

✔️ Interfaces for events.

✔️ Pooler component for Unity Events.

## Install

To install open <b>Window</b> > <b>Package Manager</b> and click on the + package icon and choose <b>"Add package from git url"</b> and type:

> https://github.com/ExpressoBits/EBPools.git

and you're done!


## Usage
Simple change Instantiate/Destroy method for this.InstantianteInPool/this.DestroyInPool!.

Before
´´´csharp
      Instantiate(prefab);
      ...
      Destroy(gameObject);
´´´

After
´´´csharp
    this.InstantiateInPool(prefab);
    ...
    this.DestroyInPool(gameObject);
´´´

![PoolersComponent](https://github.com/ExpressoBits/PoolSimply/blob/master/Assets/3rd-Party/Expresso%20Bits/PoolSimply/Textures/Editor/Pool.png)
Unity performance issues generating and destroying multiple objects?
This simple library solves your problem in a simple way. With few steps your object can be reused several times, avoiding the *garbage collector*.

Create Simple and Easy Pool of objects! This extension creates easy use of Pool with objects that are instantiated and destroyed, avoiding excessive use of memory and processing.
