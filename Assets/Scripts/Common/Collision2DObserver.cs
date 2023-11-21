using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Collider2D))]
    public class Collision2DObserver : MonoBehaviour
    {
        public event Action<Collision2D> OnCollisionEntered;
        public event Action<Collision2D> OnCollisionExited;

        private void OnCollisionEnter2D(Collision2D collision) => OnCollisionEntered?.Invoke(collision);

        private void OnCollisionExit2D(Collision2D collision) => OnCollisionExited?.Invoke(collision);
    }
}