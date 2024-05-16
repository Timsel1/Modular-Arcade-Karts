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

|No boost speed|140 boost speed|400 boost speed|
|:------------:|:-------------:|:-------------:|
|![Boost0](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/48cceb5a-eff1-47c7-8b55-1a7b12d50dae)|![Boost140](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/7eae10a1-c89e-4361-8865-224addbc4077)|![Boost400](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/c0ecc96c-5f19-41fa-8b13-ee9ad57d44e1)|

#### Acceleration Time
The time it takes for the kart to reach maximum speed in seconds when accelerating.

|0.01 Acceleration Time|4 Acceleration Time|
|:--------------------:|:-----------------:|
|![0AccelDecel](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/5d8d6bca-ec1c-4ad4-ab5b-45e2fa08c31c)|![4Accel](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/59def50f-ad20-44c7-8ff0-43e8df2a9578)|

#### Deceleration Time
The time it takes for the kart to come to a full stop when decelerating in seconds.

|0.01 Deceleration Time|3 Deceleration Time|
|:--------------------:|:-----------------:|
|![0AccelDecel](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/3e761ddd-936f-444c-85ff-a7651cce37f7)|![3Decel](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/a95270c7-3c05-4814-8c25-f271b33f6f50)|

#### Speed Based Steering
Determines if steering sharpness is affected by the kart's speed. If true 2 values can be set to determine the widest and sharpest steering angle. If turned off a set value will be used which will not be altered based on the kart's current speed.
        
|Below Threshold|Above Threshold|
|:-------------:|:-------------:|
|![BelowThreshold](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/85d1ba2e-1974-40bc-b8e1-be0e5f9030be)|![AboveThreshold](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/46a57068-3efd-4751-a1a2-efbddd761314)|

#### Steer Speed Threshold
The speed threshold at which the speed based steering starts to take effect.

#### Sharp Steer Power
The sharpest the kart can turn, is used at speeds below the steer speed threshold. (Should be higher than _wideSteerPower)

#### Wide Steer Power
The widest the kart will turn, is used at speeds above the steer speed threshold. Value will transition smoothly from sharp steer power to wide steer power thanks to DOTween (Should be lower than _sharpSteerPower)

#### Steer Power      
The power of the kart's steering, a set value that will not be altered. Is used instead of sharp steer power and wide steer power and can only be used if speed based steering is false. 

|50 Steer Power|70 Steer Power|
|:-:|:-:|
|![SteerPower 50](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/1deb2a59-3417-4cf2-be70-9147096a5412)|![SteerPower 70](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/c5d1cbf4-1646-4c0a-adbd-dd917a978836)|

#### Outwards Drift Force
The outward force applied to the kart when drifting.

|0 Outwards Drift Force|50 Outwards Drift Force|
|:---------------------:|:----------------------:|
|![Outward0](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/d180550e-dd6f-41fb-9a79-6b9db8cf298c)|![Outward50](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/cca95fe2-6548-41b0-b01c-bb7a92e73cbf)|

#### Drift Speed Threshold
The minimum speed required for the kart to start a drift.

#### Drift Power
The power of the kart's drift, affects the sharpess of the inward and outward drift.

|70 Drift Power|200 Drift Power|
|:------------:|:-------------:|
|![Outward50](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/ebe50c05-bd28-40de-8359-e78ed7cdda74)|![DriftPower200](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/cfa6645c-8833-4c15-89f9-eeca45368eb0)|
        
#### Inward Drift Angle
A value used to calculate the sharpness of a drift when steering inward during a drift.

|1.5 Inward Drift Angle|4 Inward Drift Angle|
|:-:|:-:|
|![Inward1 5](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/703a4b03-0acb-4183-bf4d-1c9a02d20297)|![Inward4](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/271e3b29-5379-4073-a30e-f373018702ff)|

#### Outward Drift Angle
A value used to calculate the sharpness of a drift when steering  outward during a drift.

|0.2 Outward Drift Factor|1 Outward Drift Factor|
|:---------------------:|:-------------------:|
|![Outward0 2](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/eef2677b-50e7-49ca-89b4-680306c752a6)|![Outward1](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/1873046c-510d-4b94-8ffd-9b26af1435b3)|
     
#### Turn Model
Determines if the kart model rotates during a drift.
      
|Turn Model true|Turn Model false|
|:-------------:|:--------------:|
|![Outward50](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/8db4de10-10be-445a-bbe2-dbbdb28c3082)|![TurnModelFalse](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/18298c32-1cdf-4c89-ac80-ea6d526fd688)|

#### Turn Speed
The speed at which the kart model rotates during a drift (purely visual).

|8 Turn Speed|1000 Turn Speed|
|:---------:|:---------:|
|![Outward50](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/8db4de10-10be-445a-bbe2-dbbdb28c3082)|![Turn1000](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/1f6d2455-aee2-4bd4-8d8f-66780d33ee4b)|

#### Visual Drift Angle
The visual angle of the kart model during a drift (purely visual).

|40 Visual Drift Angle|80 Visual Drift Angle|
|:-------------------:|:-------------------:|
|![Outward50](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/8db4de10-10be-445a-bbe2-dbbdb28c3082)|![Turn80C](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/fd9aa8e4-2552-480f-81fb-0d7ae455f441)|

#### Drift Time Thresholds
The thresholds for triggering different phases of drift boosting.

#### Boost Phase Durations
The durations of boost phases triggered by drifting.
      
#### Gravity
The speed the kart will fall down with when it is not grounded.

|10 Gravity|50 Gravity|
|:--------:|:--------:|
|![Gravity10](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/7dd253af-9b5a-488a-b3a2-0db260335181)|![Gravity50](https://github.com/Timsel1/Modular-Arcade-Karts/assets/90602424/9d4862ec-2436-45af-af63-98d0500366aa)|

#### Layer Mask
The layer masks the raycast will check to see if the kart is grounded.

#### Raycast Distance
Sets the length of the raycast used to check if the kart is grounded, increase size until grounded is true.
      
### Custom Kart Logic
To create custom kart logic the "BasicKart" script can be used as a starting point, any logic can be added to this script to create custom logic for your own kart. So to create a new completely custom kart these are the steps you would need to follow:  
- Create a c# script
- Make sure to inherit from BaseKart
- When you need to use Awake(), Start(), OnDestroy() or Update(), make sure to start the method with "base.MethodName();"
- Add your own code

Standard kart stats can still be edited with the scriptable objects you make. 









