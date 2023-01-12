using DG.Tweening;
using UnityEngine;

namespace CharacterNecessity
{
    public class Food : MonoBehaviour
    {
        public void Eat(Transform parent)
        {
            transform.SetParent(parent);
            transform.DOLocalJump(Vector3.zero, 2f, 1, 1f).onComplete = () =>
            {
                FoodNecessityUI.Instance.Eat();
                FoodManager.Instance.Foods.Remove(this);
                Debug.Log("ENDEAT");
                Destroy(this.gameObject);
            };
        }
    }
}