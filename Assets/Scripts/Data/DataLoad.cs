using System;
using Data.Model;
using Newtonsoft.Json;
using UnityEngine;

namespace Data {
    public class DataLoad {
        private const string DATA_PATH = "Data/data";

        public bool LoadData(out DataResponse dataResponse) {
            var textAsset = Resources.Load<TextAsset>(DATA_PATH);
            if (textAsset == null) {
                Debug.LogError("Error loading game infos.");
                dataResponse = null;
                return false;
            }

            try {
                dataResponse = JsonConvert.DeserializeObject<DataResponse>(textAsset.text);
                if (dataResponse == null) {
                    throw new NullReferenceException("Data is null");
                }

                return true;
            }
            catch (Exception e) {
                dataResponse = null;
                return false;
            }
        }
    }
}