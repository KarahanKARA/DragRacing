using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class WheelsRotation : MonoBehaviour
    {
        [SerializeField] private List<GameObject> wheels;

        private float _lastPosZ;
        private float _radius;
        private float _wheelLength;
        private void Start()
        {
            _lastPosZ = transform.position.z;
            
            //--Calculation of the circumference of the wheel--//
            Mesh mesh = wheels[0].GetComponent<MeshFilter>().mesh;
            Bounds bounds = mesh.bounds;
            _radius = bounds.extents.y;
            _wheelLength = 2 * _radius * (3.14f);
            // 360 degree -> wheelLength //
        }

        private void Update()
        {
            var traveledDistance = _lastPosZ - transform.position.z;
            foreach (var wheel in wheels)
            {
                var rotatingDegree = (360 * traveledDistance) / _wheelLength;
                wheel.transform.Rotate(-rotatingDegree,0,0);
            }

            _lastPosZ = transform.position.z;
        }
    }
}
