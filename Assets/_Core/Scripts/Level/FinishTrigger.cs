using System;
using UnityEngine;

namespace Game
{
    public class FinishTrigger : MonoBehaviour
    {
        public event Action OnCarFinish;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Car>())
            {
                OnCarFinish?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}