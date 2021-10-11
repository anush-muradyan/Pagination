using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Components.Pagination.Basic {
    public abstract class AbstractContent : MonoBehaviour {
        [SerializeField] private RectTransform content;
        [SerializeField] private AbstractItem itemPrefab;
        public List<AbstractItem> items { get; set; }


        protected void resetItems() {
            if (items == null) {
                return;
            }

            foreach (var item in items) {
                Destroy(item.gameObject);
            }

            items.Clear();
        }

        protected void creatItems(int contentCount) {
            items = new List<AbstractItem>();
            for (int i = 0; i < contentCount; i++) {
                var item = creatItem();
                items.Add(item);
            }
        }

        protected virtual AbstractItem creatItem() {
            var item = Instantiate(itemPrefab, content);
            return item;
        }
    }
}