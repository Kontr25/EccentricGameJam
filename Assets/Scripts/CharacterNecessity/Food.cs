using DG.Tweening;
using UnityEngine;

namespace CharacterNecessity
{
    public class Food : MonoBehaviour
    {
        public void Eat(Transform parent)
        {
            transform.SetParent(parent);
            transform.DOScale(2f, .5f).onComplete = () =>
            {
                transform.DOScale(0.1f, 0.5f);
            };
            transform.DOLocalJump(Vector3.zero, 1f, 1, 1f).onComplete = () =>
            {
                CharacterNecessityUI.Instance.Eat();
                FoodManager.Instance.Eat(this);
                Destroy(this.gameObject);
            };
        }
    }
}