using UnityEngine;

namespace Components.Pagination.Basic {
    public class FixedCountContainer : AbstractContainer {
        [SerializeField] private BasicContentFactory contentFactory;

        public BasicContentFactory ContentFactory {
            get => contentFactory;
            set => contentFactory = value;
        }

        public void Setup(int contentCount) {
            contentFactory.ClearItems();
            contentFactory.CreateItems(contentCount);
        }
    }
}