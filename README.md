# Modular Arcade Karts
Provides a quick way to set up arcade style karts, based on stats that can be altered with scriptable objects.

## Table of Contents
- [Prerequisites](#prerequisite)
- [How To Use](#how-to-use)

## Prerequisites
This Project makes use of the following packages:
- [Unity New Input System](#unity-new-input-system)
- [DOTween](#dotween)
- [UniTask](#unitask)

If you don't know how to intergrate these packages into your project, you can find the steps below.

### [Unity New Input System](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.8/manual/Installation.html)  
The steps for intergrating the new input system into your project can be followed by clicking the provided header link. 

If you make use of unity's old input system (legacy input system), make sure to still install this package. After installing go into
Edit -> Project Settings -> Player -> Other Settings and scroll down to Configuration, once you're here make sure Active Input Handling is set to Both (see Instruction Example 1).

|Instruction Example 1|
|:-----------------:|
|![InstructionInput](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/fa408d9b-6abe-43d2-b603-eb7a82dff86b)|
  
### [DOTween](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)  
DOTween can be installed from the unity asset store, the provided header link will lead directly to the free version of DOTween. After installing it make sure to set it up properly by Using the DOTween Utility Panel.  
To do so follow these steps:
  - If it's not already opened, open the Utility Panel like shown in Instruction Example 2.
    
|Instruction Example 2|
|:-------------------:|
|![InstructionDOTween](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/736c0e6e-be5a-45d9-a759-61a8de302ff5)|

  - Click Setup DOTween
  - the default setting can be applied, like in Instruction Example 3.
    
    |Instruction Example 3|  
    |:-------------------:|
    |![InstructionDOTween2](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/b4acb045-1a64-430b-b1b8-e00b481c7210)|
    
  - Click create assembly definition
  - All Done
  
### [UniTask](https://github.com/Cysharp/UniTask?tab=readme-ov-file#upm-package)  
The steps for intergrating Unitask into your project can be followed by clicking the provided header link.

## How To Use
### Kart Setup:  
It is recommended to have this structure for your kart:
  - Empty object containing your kart script of choice, a rigid body and optionally the collider.
    - Kart model, collider is recommended to have the collider on the model.

|![KartSetup](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/001ccf02-80d0-44b4-8abf-07dc54b7519d)|![KartSetup1](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/8d66561f-fcd7-47bf-9469-2215ff6edc4f)|
|:-:|:-:|


It is possible to just have the kart's model with all components on it, if you dicide to do this, make sure to set the _turnModel bool to false.













