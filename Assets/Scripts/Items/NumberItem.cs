using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Items {
    public class NumberItem : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI numberText;
        [SerializeField] private Button button;
    
        public UnityEvent<int> selectedPage;
        private void OnEnable() {
            button.onClick.AddListener(showPage);
        }

        private void OnDisable() {
            button.onClick.RemoveListener(showPage);
        }
 
        private void showPage() {
            int.TryParse(numberText.text, out var pageNumber);
            selectedPage?.Invoke(pageNumber);
        }

        public void Init(int number) {
            numberText.text = number.ToString();
        }

        public int GetPageNumber() {
            var a=int.TryParse(numberText.text, out var number);
            return number;
        }
    }
}