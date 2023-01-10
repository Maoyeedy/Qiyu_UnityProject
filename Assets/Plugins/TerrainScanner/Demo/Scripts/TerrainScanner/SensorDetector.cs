using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainScannerDEMO
{
    public class SensorDetector : MonoBehaviour
    {
        [SerializeField] TerrainScanner.CameraEffect _sensorCameraEffect;

        [SerializeField] Material _sensorMaterial;

        [SerializeField] GameObject _sensorOrigin;

        [SerializeField] private float _maxDistance;
        [SerializeField] private float _speed;

        [SerializeField] private float _killTime;

        private AudioSource _sensorStart;

        private bool _startSensor;
        private float _duration;
        private float _timer;

        private bool _killSensor;
        private float _timerKill;

        private float _EmissionCacher;

        private Vector3 _origin;

        private float _currentRadius;
        private static readonly int OverlayEmission = Shader.PropertyToID("_OverlayEmission");
        private static readonly int Radius1 = Shader.PropertyToID("_Radius");
        private static readonly int RevealOrigin = Shader.PropertyToID("_RevealOrigin");

        public Vector3 Origin
        {
            get { return _origin; }
        }

        public bool SensorOn
        {
            get { return _startSensor; }
        }

        public float Radius
        {
            get { return _currentRadius; }
        }

        public float Duration
        {
            get { return _duration; }
        }

        private void Start()
        {
            _sensorCameraEffect.material = _sensorMaterial;
            _EmissionCacher = _sensorMaterial.GetFloat(OverlayEmission);
            _sensorMaterial.SetFloat(Radius1, 0);
            _sensorStart = GetComponent<AudioSource>();
            _sensorCameraEffect.enabled = false;
        }
        
        void Update()
        {
            SensorStatus();
            if (Input.GetKey(KeyCode.E))
                if(Input.GetKeyDown(KeyCode.Q))
                    StartSensor();
        }
        
        public void OnDisable()
        {
            _sensorMaterial.SetFloat(OverlayEmission, _EmissionCacher);
            _sensorMaterial.SetFloat(Radius1, 0);
        }

        public void StartSensor()
        {
            if (_startSensor)
            {
                return;
            }

            if (_killSensor)
            {
                return;
            }

            _sensorCameraEffect.enabled = true;
            _sensorStart.Play();
            _duration = _maxDistance / _speed;
            _EmissionCacher = _sensorMaterial.GetFloat(OverlayEmission);
            _sensorMaterial.SetVector(RevealOrigin, transform.position);
            _origin = transform.position;
            _startSensor = true;
        }

        public void SensorStatus()
        {
            if (_startSensor)
            {
                _currentRadius = Mathf.Min(_speed * _timer, _maxDistance);
                _sensorMaterial.SetFloat(Radius1, _currentRadius);

                if (_timer >= _duration)
                {
                    _timer = 0f;
                    _killSensor = true;
                    _startSensor = false;
                }

                _timer += Time.deltaTime;
            }

            if (_killSensor)
            {
                if (_timerKill >= _killTime)
                {
                    _timerKill = 0f;
                    _sensorMaterial.SetFloat(Radius1, 0);
                    _sensorMaterial.SetFloat(OverlayEmission, _EmissionCacher);
                    _sensorCameraEffect.enabled = false;
                    _killSensor = false;
                    _currentRadius = 0;
                }

                _sensorMaterial.SetFloat(OverlayEmission, Mathf.Lerp(_EmissionCacher, 0, _timerKill / _killTime));

                _timerKill += Time.deltaTime;
            }
        }
    }
}
