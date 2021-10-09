using UnityEngine;

namespace Components.Pagination.Basic {
    public class BasicPagination : MonoBehaviour {
        [SerializeField] private FixedCountViewport viewport;
        [SerializeField] private NavigationBar navigationBar;
        [SerializeField] private int test;

        private const int MAX_CONTENT_COUNT = 6;
        
        [SerializeField, Range(1, MAX_CONTENT_COUNT)]
        private int pageCount;
        
        private void Start() {
            Setup();
        }

        public void Setup() {
            navigationBar.Setup(test, pageCount);
            viewport.Setup(pageCount);
        }

        private void OnEnable() {
            navigationBar.OnPageSelected.AddListener(onPageSelected);
        }

        private void OnDisable() {
            navigationBar.OnPageSelected.RemoveListener(onPageSelected);
        }

        private void onPageSelected(int pageIndex) {
            for (int i = 0; i < pageCount; i++) {
                viewport.contentItems[i].SetName("hi hi");
                viewport.contentItems[i].SetPrice(pageIndex);
            }
        }
    }
}