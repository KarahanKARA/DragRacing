using UnityEngine;

namespace Player
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private float speed;
        void Update()
        {
            transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        }
    }
}
