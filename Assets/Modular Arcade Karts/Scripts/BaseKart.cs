using Meijvogel.ModularArcadeKarts.Input;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

namespace Meijvogel.ModularArcadeKarts.Base
{
    public abstract class BaseKart : MonoBehaviour
    { //References
        [SerializeField] protected Rigidbody _rb;
        [SerializeField] protected GameObject _kartModel;
        [SerializeField] protected KartStats kartStats;
        protected KartInputActions _kartInputActions;
        protected InputAction _accelerateInput;
        protected InputAction _reverseInput;
        protected InputAction _steerInput;
        protected InputAction _driftInput;
        protected Tween _activeSpeedTween;
        protected Tween _activeDecelerationTween;
        protected Tween _activeSteeringTween;
        protected CancellationTokenSource _cancellationTokenSource;
        protected LayerMask _layerMask;

        //Speed Settings
        protected float _maxSpeed;
        protected float _backwardMaxSpeed;
        protected float _boostSpeed;
        protected float _accelerationTime;
        protected float _decelerationTime;
        protected float _currentSpeed;
        protected bool _isAccelerating;
        protected bool _isDecelerating;
        protected bool _isReversing;
        protected bool _speedTweenActive;

        //Steering Settings
        protected bool _speedBasedSteering;
        protected float _steerSpeedThreshold;
        protected float _sharpSteerPower;
        protected float _wideSteerPower;
        protected float _steerPower;
        protected float _horizontalInput;
        protected float _verticalInput; // can be used to aim items forwards or backwards
        protected bool _changingSteerFactor;
        protected bool _updateSteerFactor;

        //Drift Settings
        protected float _outwardsDriftForce;
        protected float _driftSpeedThreshold;
        protected float _driftPower;
        protected float _inwardDriftFactor;
        protected float _outwardDriftFactor;
        protected bool _turnModel;
        protected float _turnSpeed;
        protected float _visualDriftAngle;
        protected float[] _driftTimeThresholds;
        protected float driftTime;
        protected bool _driftingLeft = false;
        protected bool _driftingRight;
        protected bool _isDrifting;

        //Boost Settings
        protected bool _isBoosting;
        protected float _boostTime;
        protected float[] _boostPhaseDurations;

        //Gravity Settings
        protected float _gravity;

        //Raycast Settings
        protected float _raycastDistance;
        protected bool _grounded;

        private float CalculateCorrectedDecelTime(float maxSpeed) => _currentSpeed / maxSpeed * _decelerationTime;
        private float CalculateCorrectedAccelTime(float maxSpeed) => _accelerationTime - (_currentSpeed / maxSpeed * _accelerationTime);

        private void Awake()
        {
            _kartInputActions = new();
            _cancellationTokenSource = new();

            _accelerateInput = _kartInputActions.Kart.Accelerate;
            _reverseInput = _kartInputActions.Kart.Reverse;
            _steerInput = _kartInputActions.Kart.Steer;
            _driftInput = _kartInputActions.Kart.Drift;

            _accelerateInput.Enable();
            _reverseInput.Enable();
            _steerInput.Enable();
            _driftInput.Enable();

            _accelerateInput.started += OnAccelerate;
            _accelerateInput.canceled += OnEndAccelerate;
            _reverseInput.performed += OnReverse;
            _reverseInput.canceled += OnEndReverse;
            _driftInput.performed += OnDrift;
            _driftInput.canceled += OnEndDrift;
        }

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();

            _driftInput.Disable();
            _reverseInput.Disable();
            _steerInput.Disable();
            _accelerateInput.Disable();

            _accelerateInput.started -= OnAccelerate;
            _accelerateInput.canceled -= OnEndAccelerate;
            _reverseInput.performed -= OnReverse;
            _reverseInput.canceled -= OnEndReverse;
            _driftInput.performed -= OnDrift;
            _driftInput.canceled -= OnEndDrift;
        }

        private void Update()
        {
            AddVelocity();

            if (_isBoosting)
                _currentSpeed = _boostSpeed;

            OnSteer();

            if (_isDrifting)
            {
                Drift();
                driftTime += Time.deltaTime;
            }

            if (_speedBasedSteering)
            {
                if (_currentSpeed >= _steerSpeedThreshold && _updateSteerFactor && !_isDecelerating)
                {
                    _updateSteerFactor = false;
                    float duration = (_maxSpeed - _currentSpeed) / (_maxSpeed / _accelerationTime);
                    UpdateSteerAmount(_wideSteerPower, duration);
                }
                else if (_isDecelerating && _updateSteerFactor && _steerPower != _sharpSteerPower)
                {
                    _updateSteerFactor = false;
                    float duration = (_currentSpeed - _steerSpeedThreshold) / (_currentSpeed / _decelerationTime);
                    UpdateSteerAmount(_sharpSteerPower, duration);
                }
            }

            _grounded = Physics.Raycast(transform.position, -transform.up, out _, _raycastDistance, _layerMask);
            Debug.DrawRay(transform.position, -transform.up, Color.green, _raycastDistance);
        }

        #region Accelerate Forward Backward
        private void OnAccelerate(CallbackContext context)
        {
            if (_isBoosting)
            {
                _isAccelerating = true;
                _updateSteerFactor = true;

                if (_speedTweenActive)
                    KillSpeedTween(_activeSpeedTween, _currentSpeed);
                if (_isDecelerating)
                    KillSpeedTween(_activeDecelerationTween, _currentSpeed);

                StartSpeedTween(_boostSpeed, 0.1f);
            }
            else
            {
                _isAccelerating = true;
                _updateSteerFactor = true;

                if (_speedTweenActive)
                    KillSpeedTween(_activeSpeedTween, _currentSpeed);
                if (_isDecelerating)
                    KillSpeedTween(_activeDecelerationTween, _currentSpeed);

                StartSpeedTween(_maxSpeed, _currentSpeed > 0 ? CalculateCorrectedAccelTime(_maxSpeed) : _accelerationTime);
            }
        }

        private void OnEndAccelerate(CallbackContext context)
        {
            _driftingLeft = false;
            _driftingRight = false;
            _isDrifting = false;
            _isAccelerating = false;
            _updateSteerFactor = true;

            if (_currentSpeed != 0)
            {
                if (_speedTweenActive)
                    KillSpeedTween(_activeSpeedTween, _currentSpeed);
                if (_isDecelerating)
                    KillSpeedTween(_activeDecelerationTween, 0);

                Decelerate(0, CalculateCorrectedDecelTime(_maxSpeed));
            }
        }

        private void OnReverse(CallbackContext context)
        {
            if (!_isAccelerating && !_isBoosting)
            {
                _isReversing = true;
                _updateSteerFactor = true;
                _driftingLeft = false;
                _driftingRight = false;
                _isDrifting = false;

                if (_speedTweenActive)
                    KillSpeedTween(_activeSpeedTween, _currentSpeed);

                StartSpeedTween(_backwardMaxSpeed, CalculateCorrectedAccelTime(_backwardMaxSpeed));
            }
        }

        private void OnEndReverse(CallbackContext context)
        {
            _isReversing = false;

            if (_currentSpeed != 0)
            {
                if (_speedTweenActive)
                    KillSpeedTween(_activeSpeedTween, _currentSpeed);

                Decelerate(0, CalculateCorrectedDecelTime(_backwardMaxSpeed));
            }
        }

        private void StartSpeedTween(float targetSpeed, float duration)
        {
            _activeSpeedTween = DOTween.To(() => _currentSpeed, x => _currentSpeed = x, targetSpeed, duration)
                .OnStart(() => _speedTweenActive = true)
                .OnKill(() => _speedTweenActive = false)
                .OnComplete(() => _speedTweenActive = false)
                .SetRecyclable(true);
        }

        private void KillSpeedTween(Tween tween, float initialSpeed)
        {
            tween.Kill();
            _currentSpeed = initialSpeed;
        }

        private void Decelerate(float desiredSpeed, float decelerationTime)
        {
            if (_isDecelerating)
                _activeDecelerationTween.Kill();

            _isDecelerating = true;

            if (_isAccelerating) //Will decelerate to max speed after a boost
                _activeDecelerationTween = DOTween.To(() => _currentSpeed, x => _currentSpeed = x, desiredSpeed, decelerationTime)
                       .OnComplete(() => _isDecelerating = false)
                       .OnKill(() => _isDecelerating = false);
            else if (!_isAccelerating && !_isReversing && !_isBoosting)
                _activeDecelerationTween = DOTween.To(() => _currentSpeed, x => _currentSpeed = x, desiredSpeed, decelerationTime)
                       .OnComplete(() => { _currentSpeed = 0; _isDecelerating = false; })
                       .OnKill(() => _isDecelerating = false);
            else _isDecelerating = false;
        }

        private void AddVelocity()
        {
            Vector3 vel = transform.forward * _currentSpeed;
            if (_grounded)
                vel.y = 0;
            else
                vel.y = -1 * 50f;
            _rb.velocity = vel;
        }

        #endregion

        #region Steering
        private void OnSteer()
        {
            Vector2 inputDirection = _steerInput.ReadValue<Vector2>();
            _horizontalInput = inputDirection.x;

            if (!_isDrifting && _currentSpeed != 0)
            {
                if (_currentSpeed > _steerSpeedThreshold)
                    _updateSteerFactor = true;

                float steerAmount = _horizontalInput * _steerPower * Time.deltaTime;
                transform.Rotate(Vector3.up, steerAmount);
            }
        }

        private void UpdateSteerAmount(float desiredSteerfactor, float duration)
        {
            if (_changingSteerFactor)
                _activeSteeringTween.Kill();

            _activeSteeringTween = DOTween.To(() => _steerPower, x => _steerPower = x, desiredSteerfactor, duration)
                .OnStart(() => _changingSteerFactor = true)
                .OnComplete(() => { _changingSteerFactor = false; _updateSteerFactor = false; })
                .OnKill(() => _updateSteerFactor = false)
                .SetRecyclable(true);
        }

        #endregion

        #region Drifting

        private void OnDrift(CallbackContext context)
        {
            if (_grounded && _currentSpeed >= _driftSpeedThreshold && _isAccelerating)
            {
                if (_horizontalInput > 0)
                {
                    _driftingRight = true;
                    _driftingLeft = false;
                    _isDrifting = true;
                    if (_turnModel)
                        _kartModel.transform.DOLocalRotate(new Vector3(0, _visualDriftAngle, 0), 0.1f, RotateMode.Fast);
                }
                else if (_horizontalInput < 0)
                {
                    _driftingRight = false;
                    _driftingLeft = true;
                    _isDrifting = true;
                }
            }
        }

        private void Drift()
        {
            if (_horizontalInput != 0 && _isAccelerating && _isDrifting)
            {
                if (_driftingLeft && !_driftingRight)
                {
                    _horizontalInput = _horizontalInput < 0 ? -_inwardDriftFactor : -_outwardDriftFactor;
                    ApplyDriftEffects(-_visualDriftAngle);
                }
                else if (_driftingRight && !_driftingLeft)
                {
                    _horizontalInput = _horizontalInput > 0 ? _inwardDriftFactor : _outwardDriftFactor;
                    ApplyDriftEffects(_visualDriftAngle);
                }
            }
            else if (_isAccelerating && _isDrifting)
            {
                if (_driftingLeft && !_driftingRight)
                {
                    _horizontalInput = -_inwardDriftFactor;
                    ApplyDriftEffects(-_visualDriftAngle);
                }
                else if (_driftingRight && !_driftingLeft)
                {
                    _horizontalInput = _inwardDriftFactor;
                    ApplyDriftEffects(_visualDriftAngle);
                }
            }

            float steerAmount = _horizontalInput * _driftPower * Time.deltaTime;
            transform.Rotate(Vector3.up, steerAmount);
        }

        private void ApplyDriftEffects(float angle)
        {
            if (_isDrifting && _grounded)
                if (_driftingRight)
                    _rb.AddForce(_outwardsDriftForce * 1000 * Time.deltaTime * -transform.right, ForceMode.Acceleration);
                else if (_driftingLeft)
                    _rb.AddForce(_outwardsDriftForce * 1000 * Time.deltaTime * transform.right, ForceMode.Acceleration);

            if (_turnModel)
                _kartModel.transform.localRotation = Quaternion.Lerp(_kartModel.transform.localRotation, Quaternion.Euler(0, angle, 0), _turnSpeed * Time.deltaTime);
        }

        private void OnEndDrift(CallbackContext context)
        {
            _driftingLeft = false;
            _driftingRight = false;
            _isDrifting = false;

            if (_turnModel)
                _kartModel.transform.rotation = new Quaternion(0, 0, 0, 0);

            if (_isBoosting)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = new();
            }

            BoostKart(_cancellationTokenSource.Token);
        }

        private async void BoostKart(CancellationToken token)
        {
            int index = 0;
            try
            {
                foreach (var time in _driftTimeThresholds)
                {
                    if (driftTime >= time && _isAccelerating && _boostPhaseDurations.Length >= index + 1)
                        _boostTime = _boostPhaseDurations[index];
                    else if (_boostPhaseDurations.Length < index + 1)
                    {
                        Debug.LogError($"No boost phase duration has been set for this index! The last phase at index {index - 1} will be used.");
                        break;
                    }
                    else if (_boostPhaseDurations.Length == 0)
                    {
                        Debug.LogError($"No boost phase durations have been set! Make sure to add boost phase durations in the inspector.");
                        break;
                    }
                    else break;

                    index++;
                }

                driftTime = 0;

                if (_boostTime > 0)
                {
                    _isBoosting = true;
                    _currentSpeed = _boostSpeed;
                    await UniTask.WaitForSeconds(_boostTime, cancellationToken: token);
                    _isBoosting = false;
                    if (_currentSpeed > _maxSpeed && _isAccelerating)
                        Decelerate(_maxSpeed, 1);
                    else if (!_isAccelerating)
                        Decelerate(0, CalculateCorrectedDecelTime(_maxSpeed));

                    _boostTime = 0;
                }
            }
            catch (OperationCanceledException)
            {
                Debug.LogError("Boost was Canceled");
            }
        }

        #endregion
    }
}