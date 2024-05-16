using UnityEngine;

namespace Meijvogel.ModularArcadeKarts.Base
{
    [CreateAssetMenu(fileName = "NewKart", menuName = "Modular Arcade Karts/Kart Stats")]
    public class KartStats : ScriptableObject
    {
        [Tooltip("The maximum forward speed of the kart.")]
        [Range(1, 200)]
        public float _maxSpeed = 60;

        [Tooltip("The maximum backward speed of the kart.")]
        [Range(-100, -1)]
        public float _backwardMaxSpeed = -40;

        [Tooltip("The speed of the kart when boosting.")]
        [Range(1, 400)]
        public float _boostSpeed = 100;

        [Tooltip("The time it takes for the kart to reach maximum speed in seconds when accelerating.")]
        [Range(0.01f, 10)]
        public float _accelerationTime = 4;

        [Tooltip("The time it takes for the kart to come to a stop when decelerating in seconds.")]
        [Range(0.01f, 10)]
        public float _decelerationTime = 3;

        [Tooltip("Determines if steering sharpness is affected by the kart's speed.")]
        public bool _speedBasedSteering = true;

        [Tooltip("The speed threshold at which the kart starts to steer less sharp.")]
        public float _steerSpeedThreshold = 30f;

        [Tooltip("The sharpest the kart can turn, is used at the lowest speeds. (Should be higher than _wideSteerPower)")]
        [Range(0.01f, 200)]
        public float _sharpSteerPower = 100f;

        [Tooltip("The widest the kart will turn, is used at the highest speeds. (Should be lower than _sharpSteerPower)")]
        [Range(0.01f, 200)]
        public float _wideSteerPower = 70f;

        [Tooltip("The power of the kart's steering.")]
        [Range(0.01f, 200)]
        public float _steerPower = 70;

        [Tooltip("The force applied to the kart when drifting.")]
        [Range(1, 100)]
        public float _outwardsDriftForce = 50;

        [Tooltip("The speed threshold required for the kart to start drifting.")]
        [Range(0.01f, 100)]
        public float _driftSpeedThreshold = 30f;

        [Tooltip("The power of the kart's drift, affects the sharpess of the inward and outward drift.")]
        [Range(0.01f, 200)]
        public float _driftPower = 70;

        [Tooltip("A value used to calculate the sharpness of a drift when steering inward during a drift.")]
        [Range(0.01f, 10)]
        public float _inwardDriftFactor = 1.5f;

        [Tooltip("A value used to calculate the sharpness of a drift when steering outward during a drift.")]
        [Range(0.01f, 10)]
        public float _outwardDriftFactor = 0.2f;

        [Tooltip("Determines if the kart model rotates during a drift.")]
        public bool _turnModel = true;

        [Tooltip("The speed at which the kart model rotates during a drift (purely visual).")]
        public float _turnSpeed = 8;

        [Tooltip("The visual angle of the kart model during a drift (purely visual).")]
        [Range(0.01f, 360)]
        public float _visualDriftAngle = 20;

        [Tooltip("The thresholds for triggering different phases of drift boosting.")]
        public float[] _driftTimeThresholds = { 1.5f, 4f, 7f };

        [Tooltip("The durations of boost phases triggered by drifting.")]
        public float[] _boostPhaseDurations = { 0.75f, 1.5f, 2.5f };

        [Tooltip("The speed the kart will fall down with when it is not grounded.")]
        public float _gravity = 50;

        public LayerMask _layerMask;

        [Tooltip("Sets the length of the raycast used to check if the kart is grounded, increase size until grounded is true.")]
        public float _raycastDistance = 1f;
    }
}