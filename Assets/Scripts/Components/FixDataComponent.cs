using System.Collections.Generic;
using Convertor;
using Data;
using Data.Model;
using Items;
using UnityEngine;

namespace Components {
    public class FixDataComponent : MonoBehaviour, IDataComponent {
        [SerializeField] private DataEntryItem dataEntryItem;
        [SerializeField] private RectTransform container;
        [SerializeField] private int dataCount;
        
        private JsonConvertor convertor;
        public List<DataEntryItem> DataEntryItems;

        public void Setup() {
            convertor = new JsonConvertor();
            DataEntryItems = new List<DataEntryItem>();

            for (int i = 0; i < dataCount; i++) {
                var dataItem = Instantiate(dataEntryItem, container);
                DataEntryItems.Add(dataItem);
            }
        }

       
        
        public void ShowSelectedPageData(int pageDataItemCount,int pageNumber,DataResponse dataResponse) {
            var j = 0;
            var lastPageItemCount = dataResponse.Data.Length % pageDataItemCount == 0
                ? pageDataItemCount
                : dataResponse.Data.Length % pageDataItemCount;

            int endNumber = pageNumber == pageCount ? lastPageItemCount : pageDataItemCount;

            for (int i = (pageNumber - 1) * pageDataItemCount;
                i < (pageNumber - 1) * pageDataItemCount + endNumber;
                i++) {
                updateDataEntryItemInfo(j, i,dataResponse);
                j++;
            }

            if (endNumber < pageDataItemCount) {
                for (int i = endNumber; i < pageDataItemCount; i++) {
                    DataEntryItems[j].gameObject.SetActive(false);
                    j++;
                }
            }
        }
        
        private void updateDataEntryItemInfo(int dataEntryIndex, int dataResponseDataIndex,DataResponse dataResponse) {
            DataEntryItems[dataEntryIndex].gameObject.SetActive(true);
            DataEntryItems[dataEntryIndex].SetName(dataResponse.Data[dataResponseDataIndex].Name);
            DataEntryItems[dataEntryIndex].SetPrice(dataResponse.Data[dataResponseDataIndex].Price);
        }
        
        public int pageCount { get; set; }
    }
}