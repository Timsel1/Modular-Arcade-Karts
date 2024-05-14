using Meijvogel.ModularArcadeKarts.Base;

namespace Meijvogel.ModularArcadeKarts.Karts
{

    public class BasicKart : BaseKart
    {
        private void Start()
        {
            _layerMask = kartStats._layerMask;
            _maxSpeed = kartStats._maxSpeed;
            _backwardMaxSpeed = kartStats._backwardMaxSpeed;
            _boostSpeed = kartStats._boostSpeed;
            _accelerationTime = kartStats._accelerationTime;
            _decelerationTime = kartStats._decelerationTime;
            _speedBasedSteering = kartStats._speedBasedSteering;
            _steerSpeedThreshold = kartStats._steerSpeedThreshold;
            _sharpSteerPower = kartStats._sharpSteerPower;
            _wideSteerPower = kartStats._wideSteerPower;
            _outwardsDriftForce = kartStats._outwardsDriftForce;
            _driftSpeedThreshold = kartStats._driftSpeedThreshold;
            _driftPower = kartStats._driftPower;
            _inwardDriftFactor = kartStats._inwardDriftFactor;
            _outwardDriftFactor = kartStats._outwardDriftFactor;
            _turnModel = kartStats.turnModel;
            _turnSpeed = kartStats._turnSpeed;
            _visualDriftAngle = kartStats.visualDriftAngle;
            _driftTimeThresholds = kartStats._driftTimeThresholds;
            _boostPhaseDurations = kartStats._boostPhaseDurations;
            _raycastDistance = kartStats._raycastDistance;
            _gravity = kartStats._gravity;

            if (!base._speedBasedSteering)
                _steerPower = kartStats._steerPower;
            else
                _steerPower = kartStats._sharpSteerPower;
        }
    }
}
