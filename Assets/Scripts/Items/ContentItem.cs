using TMPro;
using UnityEngine;

namespace Items {
    public class ContentItem : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI name;
        [SerializeField] private TextMeshProUGUI price;

        public void SetName(string name) {
            this.name.text = name;
        }

        public void SetPrice(decimal price) {
            this.price.text = price.ToString();
        }
    }
}