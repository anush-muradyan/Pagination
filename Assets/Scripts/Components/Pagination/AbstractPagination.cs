using Components.Pagination.Basic;
using UnityEngine;

namespace Components.Pagination {
    public abstract class AbstractPagination<TContainer, TNavigation> : MonoBehaviour where TContainer : AbstractContainer
        where TNavigation : MonoBehaviour {
        private const int MAX_CONTENT_COUNT = 6;
        [SerializeField] private TContainer container;
        [SerializeField] private TNavigation navigation;
        [SerializeField, Range(1, MAX_CONTENT_COUNT)] private int pageCount;

        public int PageCount => pageCount;

        public TContainer Container => container;

        public TNavigation Navigation => navigation;

        public abstract void Setup();
    }
}