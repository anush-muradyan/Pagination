using Data;
using Data.Model;

namespace Components.Pagination.Basic {
    public class BasicPagination : AbstractPagination<FixedCountContainer,NavigationBar> {
        private DataResponse dataResponse = new DataResponse();
        private DataLoad dataLoad = new DataLoad();

        private void Start() {
            dataLoad.LoadData(out dataResponse);
            Setup();
        }

        public override void Setup() {
            Navigation.Setup(dataResponse.Data.Length, PageCount);
            Container.Setup(PageCount);
        }

        private void OnEnable() {
            Navigation.OnPageSelected.AddListener(onPageSelected);
        }

        private void OnDisable() {
            Navigation.OnPageSelected.RemoveListener(onPageSelected);
        }

        private void onPageSelected(int pageIndex) {
            setData(pageIndex);
        }

        private void setData(int pageIndex) {
           var j = 0;
           for (int i = (pageIndex - 1) * PageCount; i < (pageIndex - 1) * PageCount + PageCount; i++) {
               if (i > dataResponse.Data.Length - 1) {
                   Container.ContentFactory.Items[j].gameObject.SetActive(false);
               }
               else {
                   Container.ContentFactory.Items[j].gameObject.SetActive(true);
                   var contentItem = Container.ContentFactory.Items[j];
                   contentItem.SetName(dataResponse.Data[i].Name);
                   contentItem.SetPrice(dataResponse.Data[i].Price);
               }

               j++;
           }
        }
    }
}