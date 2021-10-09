using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.UI;

namespace Components {
    public class NumberItemComponent : MonoBehaviour, INumberComponent {
        
        [SerializeField] private NumberItem numberItem;
        [SerializeField] private RectTransform numberItemContainer;
        [SerializeField] private Button previousButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private int pageCount;
        
        public List<NumberItem> pageNumberItems;

        public void Setup(int pageCount) {
            pageNumberItems = new List<NumberItem>();
            for (int i = 0; i < pageCount; i++) {
                var item = Instantiate(numberItem, numberItemContainer);
                pageNumberItems.Add(item);
                //item.selectedPage.AddListener(loadSelectedPageData);
                item.Init(i + 1);
            }
        }

        private void OnEnable() {
            previousButton.onClick.AddListener(onPreviousButtonClick);
            nextButton.onClick.AddListener(onNextButtonClick);
        }

        private void OnDisable() {
            previousButton.onClick.RemoveListener(onPreviousButtonClick);
            nextButton.onClick.RemoveListener(onNextButtonClick);
        }

        private void onPreviousButtonClick() {
            Debug.Log("Previous button clicked.");
            Debug.Log(pageCount);
            if (pageNumberItems[0].GetPageNumber() == 1) {
                return;
            }

            var j = 0;
            var i = pageNumberItems[0].GetPageNumber() - 1 - pageCount;

            if (i + pageCount < pageCount) {
                i = 0;
            }

            var number = i;
            for (; i < number + pageCount; i++) {
                pageNumberItems[j].Init(i + 1);
                j++;
            }
        }

        private void onNextButtonClick() {
            Debug.Log("Next button clicked.");
        }
    }
}