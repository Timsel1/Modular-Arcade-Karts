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

|![KartSetup](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/01a2781d-ae77-4205-b40a-616bf5dc35cf)|![KartSetup1](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/cdd6da59-acb1-49d2-a8ad-42e9b75b37ff)|
|:-:|:-:|


It is possible to just have the kart's model with all components on it, if you dicide to do this, make sure to set the _turnModel bool to false in your scriptable object.

### Kart Statistics
#### Max Speed:
The maximum forward speed of the kart. This speed can only be exceeded when the kart gets a boost.

|Max Speed 10 and instant acceleration|Max Speed 100 and instant acceleration|
|:-----------------------------------:|:------------------------------------:|
|![Speed10](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/2f6cd706-7ba1-4aca-bad9-25a3eccca5a7)|![Speed100](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/3887c2a5-87cf-4047-8244-536f1ef767ad)|

#### Backward Max Speed:  
The maximum backward speed of the kart. Cannot be exceeded.

|Backward Max Speed 10 and instant acceleration|Backward Max Speed 100 and instant acceleration|
|:-----------------------------------:|:------------------------------------:|
|![BackSpeed10](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/fd5454f7-a49b-4655-b284-8e55e391e551)|![BackSpeed100](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/d778b511-22c4-4620-ab06-fe2f4fb1411c)|

#### Boost Speed
The speed of the kart when boosting. A Higher speed cannot be reached.

|||
|:-:|:-:|
|||

#### Acceleration Time
The time it takes for the kart to reach maximum speed in seconds when accelerating.

|||
|:-:|:-:|
|||

#### Deceleration Time
The time it takes for the kart to come to a full stop when decelerating in seconds.

|||
|:-:|:-:|
|||

#### Speed Based Steering
Determines if steering sharpness is affected by the kart's speed. If true 2 values can be set to determine the widest and sharpest steering angle.
        
|||
|:-:|:-:|
|||

#### Steer Speed Threshold
The speed threshold at which the speed based steering starts to take effect.

|||
|:-:|:-:|
|||

#### Sharp Steer Power
The sharpest the kart can turn, is used at speeds below the steer speed threshold. (Should be higher than _wideSteerPower)

|||
|:-:|:-:|
|||

#### Wide Steer Power
The widest the kart will turn, is used at speeds above the steer speed threshold. Value will transition smoothly from sharp steer power to wide steer power thanks to DOTween (Should be lower than _sharpSteerPower)

|||
|:-:|:-:|
|||

#### Steer Power      
The power of the kart's steering. Is used instead of sharp steer power and wide steer power and can only be used if speed based steering is false. 

|||
|:-:|:-:|
|||

#### Outwards Drift Force
The outward force applied to the kart when drifting.

|||
|:-:|:-:|
|||

#### Drift Speed Threshold
The minimum speed required for the kart to start a drift.

#### Drift Power
The power of the kart's drift, affects the sharpess of the inward and outward drift.

|||
|:-:|:-:|
|||
        
#### Inward Drift Angle
A value used to calculate the sharpness of a drift when steering inward during a drift.

|||
|:-:|:-:|
|||

#### Outward Drift Angle
A value used to calculate the sharpness of a drift when steering  outward during a drift.

|||
|:-:|:-:|
|||
     
#### Turn Model
Determines if the kart model rotates during a drift.
      
|||
|:-:|:-:|
|||

#### Turn Speed
The speed at which the kart model rotates during a drift (purly visual).

|||
|:-:|:-:|
|||

#### Visual Drift Angle
The visual angle of the kart model during a drift (purly visual).

|||
|:-:|:-:|
|||

#### Drift Time Thresholds
The thresholds for triggering different phases of drift boosting.

|||
|:-:|:-:|
|||

#### Boost Phase Durations
The durations of boost phases triggered by drifting.

|||
|:-:|:-:|
|||
      
#### Gravity
The speed the kart will fall down with when it is not grounded.

|||
|:-:|:-:|
|||

#### Layer Mask
The layer masks the raycast will check to see if the kart is grounded.

#### Raycast Distance
Sets the length of the raycast used to check if the kart is grounded, increase size until grounded is true.
      










