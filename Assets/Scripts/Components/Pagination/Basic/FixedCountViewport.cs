namespace Components.Pagination.Basic {
    public class FixedCountViewport : AbstractContent {

        public void Setup(int contentCount) {
            resetItems();
            creatItems(contentCount);
        }
    }

    
}