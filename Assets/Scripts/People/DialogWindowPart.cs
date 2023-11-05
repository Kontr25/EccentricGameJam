using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace People
{
    public class DialogWindowPart : MonoBehaviour
    {
        [SerializeField] private GameObject _dialogWindow;
        [SerializeField] private bool _destroyer;
        private void OnEnable()
        {
            StartCoroutine(DestroyRoutine());
        }

        private IEnumerator DestroyRoutine()
        {
            yield return new WaitForSeconds(3f);
            transform.DOScale(.01f, 1f).onComplete = () =>
            {
                if (_destroyer)
                {
                    Destroy(_dialogWindow);
                }
            };
        }
    }
}