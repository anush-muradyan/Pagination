using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Items {
    public class PageItem : MonoBehaviour {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI text;
        public UnityEvent<int> OnPageSelected { get; } = new UnityEvent<int>();
        private int pageIndex;

        public void Setup(int index) {
            pageIndex = index;
            setText();
        }

        private void OnEnable() {
            button.onClick.AddListener(onButtonClick);
        }

        private void OnDisable() {
            button.onClick.RemoveListener(onButtonClick);
        }

        private void setText() {
            text.text = pageIndex.ToString();
        }

        private void onButtonClick() {
            OnPageSelected?.Invoke(pageIndex);
        }

        public void Show() {
            gameObject.SetActive(true);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }

}
}