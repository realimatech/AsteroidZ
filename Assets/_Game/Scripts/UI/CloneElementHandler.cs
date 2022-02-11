using System;
using UnityEngine;
using UnityEngine.UI;

namespace realima.asterioidz
{
    public class CloneElementHandler : MonoBehaviour
    {
        [SerializeField] GameObject _clonedElement;

        public int FillCount { get; private set; }

        private void Awake()
        {
            ClearElements();
        }

        private void OnEnable()
        {
            GameplayManager.onPlayerShipDestroyed += Decrement;
        }

        private void OnDisable()
        {
            GameplayManager.onPlayerShipDestroyed -= Decrement;
        }

        private void Start()
        {
            FillBuffer();
        }

        private void FillBuffer()
        {
            SetCount(GameManager.Instance.gameSave.playerLife);
        }

        private void ClearElements()
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }
            FillCount = 0;
        }

        public void SetCount(int count)
        {
            if (count == FillCount) return;
            else
            {
                if (FillCount < count)
                    for (int i = FillCount; i < count; i++)
                    {
                        Increment();
                    }
                else
                    for (int i = FillCount; i >= count; i--)
                    {
                        Decrement();
                    }
            }
        }

        private void Increment()
        {
            if(transform.childCount-1 <= FillCount) 
                Instantiate(_clonedElement, transform);
            transform.GetChild(FillCount).gameObject.SetActive(true);
            FillCount++;
        }

        private void Decrement()
        {
            transform.GetChild(FillCount - 1).gameObject.SetActive(false);
            FillCount--;
        }
    }
}