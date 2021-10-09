using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Components.Pagination.Basic {
    public class FixedCountViewport : MonoBehaviour {
        [SerializeField] private RectTransform content;
        [SerializeField] private ContentItem contentItemPrefab;

        public List<ContentItem> contentItems { get; set; }

        public void Setup(int contentCount) {
            resetItems();
            creatItems(contentCount);
        }

        private void resetItems() {
            if (contentItems == null) {
                return;
            }

            foreach (var item in contentItems) {
                Destroy(item.gameObject);
            }

            contentItems.Clear();
        }

        private void creatItems(int contentCount) {
            contentItems = new List<ContentItem>();
            for (int i = 0; i < contentCount; i++) {
                var item = creatItem();
                contentItems.Add(item);
            }
        }

        private ContentItem creatItem() {
            var item = Instantiate(contentItemPrefab, content);
            return item;
        }
    }
}