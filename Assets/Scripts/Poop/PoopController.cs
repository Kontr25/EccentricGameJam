using CameraChanger;
using DG.Tweening;
using UnityEngine;

namespace Poop
{
    public class PoopController : MonoBehaviour
    {
        [SerializeField] private float _appirianceDelay;
        [SerializeField] private PoopTalker _poopTalker;

        public PoopTalker poopTalker
        {
            get => _poopTalker;
            set => _poopTalker = value;
        }

        public void StandUp()
        {
            CameraChanger.CameraChanger.Instance.ActivateCamera(NameCamera.PoopCamera);
            transform.DOScale(Vector3.one, _appirianceDelay).onComplete = () =>
            {
                transform.localScale = Vector3.one;
                _poopTalker.StartJoke();
            };
        }
    }
}