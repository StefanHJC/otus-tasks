using System;
using UnityEngine;

namespace ShootEmUp
{
    public class Collision2DObserver : MonoBehaviour
    {
        [SerializeField] 
        private Collider2D collider;

        [SerializeField]
        public event Action<Collision2D> OnCollisionEntered;
        
        [SerializeField]
        public event Action<Collision2D> OnCollisionExited;

        private void OnCollisionEnter2D(Collision2D collision) => OnCollisionEntered?.Invoke(collision);

        private void OnCollisionExit2D(Collision2D collision) => OnCollisionExited?.Invoke(collision);
    }
}