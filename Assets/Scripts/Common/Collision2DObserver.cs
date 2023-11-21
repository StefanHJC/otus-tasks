using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Collider2D))]
    public class Collision2DObserver : MonoBehaviour
    {
        public event Action<Collision2D> CollisionEntered;
        public event Action<Collision2D> CollisionExited;

        private void OnCollisionEnter2D(Collision2D collision) => CollisionEntered?.Invoke(collision);

        private void OnCollisionExit2D(Collision2D collision) => CollisionExited?.Invoke(collision);
    }
}