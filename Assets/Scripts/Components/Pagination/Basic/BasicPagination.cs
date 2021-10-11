using Data;
using Data.Model;
using Items;
using UnityEngine;

namespace Components.Pagination.Basic {
    public class BasicPagination : MonoBehaviour {
        private const int MAX_CONTENT_COUNT = 6;
        [SerializeField] private FixedCountViewport viewport;
        [SerializeField] private NavigationBar navigationBar;

        [SerializeField, Range(1, MAX_CONTENT_COUNT)]
        private int pageCount;

        private DataResponse dataResponse = new DataResponse();
        private DataLoad dataLoad = new DataLoad();

        private void Start() {
            dataLoad.LoadData(out dataResponse);
            Setup();
        }

        public void Setup() {
            navigationBar.Setup(dataResponse.Data.Length, pageCount);
            viewport.Setup(pageCount);
        }

        private void OnEnable() {
            navigationBar.OnPageSelected.AddListener(onPageSelected);
        }

        private void OnDisable() {
            navigationBar.OnPageSelected.RemoveListener(onPageSelected);
        }

        private void onPageSelected(int pageIndex) {
            setData(pageIndex);
        }

        private void setData(int pageIndex) {
            var j = 0;
            for (int i = (pageIndex - 1) * pageCount; i < (pageIndex - 1) * pageCount + pageCount; i++) {
                if (i > dataResponse.Data.Length - 1) {
                    viewport.items[j].gameObject.SetActive(false);
                }
                else {
                    viewport.items[j].gameObject.SetActive(true);
                    var contentItem = (ContentItem) viewport.items[j];
                    contentItem.SetName(dataResponse.Data[i].Name);
                    contentItem.SetPrice(dataResponse.Data[i].Price);
                }

                j++;
            }
        }
    }
}