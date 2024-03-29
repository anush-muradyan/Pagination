using System;
using Items;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Components.Pagination.Basic {
    public class NavigationBar : AbstractContent {
        [Serializable]
        private class NavigationInfo {
            public int Total { get; }
            public int Maximum { get; }
            public int Step { get; }
            public int PageCount { get; }

            private int sectionIndex;

            public NavigationInfo(int total, int step, int pageCount) {
                Total = total;
                Step = step;
                PageCount = pageCount;
                Maximum = total % (pageCount * step) == 0
                    ? Mathf.RoundToInt((float) total / pageCount / step)
                    : Mathf.CeilToInt((float) total / pageCount / step);
                sectionIndex = 0;
            }

            public int CurrentSectionPage() {
                return sectionIndex * Step + 1;
            }

            public void NextSectionPage(out int index, out int count) {
                if (sectionIndex == Maximum - 1) {
                    index = CurrentSectionPage();
                    var pages = (Total % PageCount) == 0 ? Total / PageCount : Total / PageCount + 1;
                    count = pages - sectionIndex * Step;
                    return;
                }

                sectionIndex++;
                index = CurrentSectionPage();
                var p = (Total % PageCount) == 0 ? Total / PageCount : Total / PageCount + 1;
                count = p - sectionIndex * Step;
            }

            public bool PreviousSectionPage(out int index) {
                if (sectionIndex == 0) {
                    index = CurrentSectionPage();
                    return false;
                }

                sectionIndex--;
                index = CurrentSectionPage();
                return true;
            }

            public override string ToString() {
                return
                    $"{nameof(sectionIndex)}: {sectionIndex}, {nameof(Maximum)}: {Maximum}, {nameof(Step)}: {Step}, {nameof(PageCount)}: {PageCount}";
            }
        }

        private const int MAX_CONTENT_COUNT = 7;
        [SerializeField] private Button previousButton;
        [SerializeField] private Button nextButton;
        [SerializeField, Range(1, MAX_CONTENT_COUNT)]
        private int contentCount = 7;

        public UnityEvent<int> OnPageSelected { get; } = new UnityEvent<int>();

        private NavigationInfo navigationInfo;

       

        private void OnEnable() {
            previousButton.onClick.AddListener(onPreviousButtonClick);
            nextButton.onClick.AddListener(onNextButtonClick);
        }

        private void OnDisable() {
            previousButton.onClick.RemoveListener(onPreviousButtonClick);
            nextButton.onClick.RemoveListener(onNextButtonClick);
        }

        public void Setup(int total, int pageContentCount) {
            resetItems();
            var count = contentCount < total / pageContentCount
                ? contentCount
                : (total / pageContentCount > 1
                    ? (total % pageContentCount == 0 ? total / pageContentCount : total / pageContentCount + 1)
                    : 1);
            creatItems(count);
            calculateBar(total, pageContentCount);
        }

        private void calculateBar(int total, int pageContentCount) {
            navigationInfo = new NavigationInfo(total, contentCount, pageContentCount);
            updateNavigationBar(navigationInfo.CurrentSectionPage(), contentCount);
        }

        private void updateNavigationBar(int start, int count) {
            for (int i = 0; i < items.Count; i++) {
                var pageItem =(PageItem) items[i];
                if (i < count) {
                    pageItem.Show();
                    pageItem.Setup(start + i);
                }
                else {
                    pageItem.Hide();
                }
            }
        }

        protected override AbstractItem creatItem() {
            var item = base.creatItem();
            var pageItem = (PageItem) item;
            pageItem.OnPageSelected.AddListener(onPageSelected);
            return pageItem;
        }

        private void onPageSelected(int index) {
            OnPageSelected?.Invoke(index);
        }

        private void onNextButtonClick() {
            navigationInfo.NextSectionPage(out var start, out var count);
            updateNavigationBar(start, count);
        }

        private void onPreviousButtonClick() {
            navigationInfo.PreviousSectionPage(out var start);
            updateNavigationBar(start, contentCount);
        }
    }
}