using System;
using UnityEngine;

namespace Character
{
    public class ListenerMover : MonoBehaviour
    {
        [SerializeField] private Transform _character;

        private void FixedUpdate()
        {
            transform.position = _character.position;
        }
    }
}