using System.Collections.Generic;
using Convertor;
using Data;
using Data.Model;
using Items;
using UnityEngine;

namespace DataLoader {
    public class DataLoader {
        private readonly JsonConvertor convertor = new JsonConvertor();
        private DataResponse dataResponse;
        private int lastPageItemCount; //4
        private List<DataEntryItem> dataEntryItems;

        public int pageCount; //200

        public void Init(int pageDataItemCount,DataEntryItem dataEntryItem, RectTransform container) {
            dataEntryItems = new List<DataEntryItem>();

            dataResponse = loadGameInfos();
            var count = dataResponse.Data.Length > pageDataItemCount ? pageDataItemCount : dataResponse.Data.Length;
            for (int i = 0; i < count; i++) {
                creatInfo(dataEntryItem, container);
            }

            pageCount = dataResponse.Data.Length / pageDataItemCount;
            if (dataResponse.Data.Length % pageDataItemCount != 0) {
                pageCount++;
            }

            lastPageItemCount = dataResponse.Data.Length % pageDataItemCount == 0
                ? pageDataItemCount
                : dataResponse.Data.Length % pageDataItemCount;
            updateInfo(pageDataItemCount,1);
        }

        private DataResponse loadGameInfos() {
            var jsonAsset = Resources.Load<TextAsset>("Data/data");
            if (jsonAsset == null) {
                Debug.LogError("Error loading game infos.");
                return null;
            }

            DataResponse dataResponse = convertor.Deserialize<DataResponse>(jsonAsset.text);
            if (dataResponse == null) {
                Debug.LogError("Error parsing game infos.");
                return null;
            }

            return dataResponse;
        }

        public void LoadGameInfo(int pageDataItemCount,int pageNumber) {
            updateInfo(pageDataItemCount,pageNumber);
        }


        private void updateInfo(int pageDataItemCount,int pageNumber) {
            var j = 0;

            int endNumber = pageNumber == pageCount ? lastPageItemCount : pageDataItemCount;

            for (int i = (pageNumber - 1) * pageDataItemCount; i < (pageNumber - 1) * pageDataItemCount + endNumber; i++) {
                updateDataEntryItemInfo(j, i);
                j++;
            }

            if (endNumber < pageDataItemCount) {
                for (int i = endNumber; i < pageDataItemCount; i++) {
                    dataEntryItems[j].gameObject.SetActive(false);
                    j++;
                }
            }
        }

        private void updateDataEntryItemInfo(int dataEntryIndex, int dataResponseDataIndex) {
            dataEntryItems[dataEntryIndex].gameObject.SetActive(true);
            dataEntryItems[dataEntryIndex].SetName(dataResponse.Data[dataResponseDataIndex].Name);
            dataEntryItems[dataEntryIndex].SetPrice(dataResponse.Data[dataResponseDataIndex].Price);
        }

        private void creatInfo(DataEntryItem dataEntryItem, RectTransform container) {
            var item = Object.Instantiate(dataEntryItem, container);
            dataEntryItems.Add(item);
        }
    }
}