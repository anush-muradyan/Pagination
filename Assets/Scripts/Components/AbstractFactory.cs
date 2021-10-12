using System.Collections.Generic;
using UnityEngine;

namespace Components {
    public class AbstractFactory<TProduct> : MonoBehaviour where TProduct : AbstractProduct {
        [SerializeField] private RectTransform content;
        [SerializeField] private TProduct productPrefab;

        private List<TProduct> items { get; set; }

        public List<TProduct> Items {
            get => items;
            set {
                items = value;
                
            }
        }
        
        public void ClearItems() {
            if (items == null) {
                return;
            }

            foreach (var item in items) {
                Destroy(item.gameObject);
            }

            items.Clear();
        }

        public void CreateItems(int contentCount) {
            items = new List<TProduct>();
            for (int i = 0; i < contentCount; i++) {
                CreateItem();
            }
        }

        private TProduct createItem() {
            var item = Instantiate(productPrefab, content);
            return item;
        }

        public TProduct CreateItem() {
            var item = createItem();
            items.Add(item);
            return item;
        }
    }
}