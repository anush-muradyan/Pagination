using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.UI;

public class Startup : MonoBehaviour {
    [SerializeField] private NumberItem numberItemPrefab;
    [SerializeField] private DataEntryItem dataEntryItemPrefab;

    [SerializeField] private RectTransform numberItemContainer;
    [SerializeField] private RectTransform dataItemContainer;

    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    [SerializeField] private int pageCount = 5;
    [SerializeField] private int pageDataItemCount = 5;

    private DataLoader.DataLoader dataLoader;
    private List<NumberItem> pageNumberItems;


    private void Start() {
        dataLoader = new DataLoader.DataLoader();
        init();
    }

    private void OnEnable() {
        previousButton.onClick.AddListener(onPreviousButtonClick);
        nextButton.onClick.AddListener(onNextButtonClick);
    }

    private void OnDisable() {
        previousButton.onClick.RemoveListener(onPreviousButtonClick);
        nextButton.onClick.RemoveListener(onNextButtonClick);
    }

    private void init() {
        pageNumberItems = new List<NumberItem>();

        for (int i = 0; i < pageCount; i++) {
            var item = Instantiate(numberItemPrefab, numberItemContainer);
            pageNumberItems.Add(item);
            item.selectedPage.AddListener(loadSelectedPageData);
            item.Init(i + 1);
        }

        dataLoader.Init(pageDataItemCount,dataEntryItemPrefab, dataItemContainer);
    }

    private void loadSelectedPageData(int pageNumber) {
        dataLoader.LoadGameInfo(pageDataItemCount,pageNumber);
    }

    private void onPreviousButtonClick() {
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
        var lastNumberElement = pageNumberItems[pageCount - 1].GetPageNumber();
        if (lastNumberElement >= dataLoader.pageCount) {
            return;
        }

        var j = 0;
        int i = lastNumberElement;
        if (i + pageCount > dataLoader.pageCount) {
            i = dataLoader.pageCount - pageCount;
        }

        var number = i;
        for (; i < number + pageCount; i++) {
            pageNumberItems[j].Init(i + 1);
            j++;
        }
    }
}