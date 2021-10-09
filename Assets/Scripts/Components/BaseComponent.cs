using UnityEngine;

namespace Components {
    public class BaseComponent<T, T1> : MonoBehaviour where T : IDataComponent where T1 : INumberComponent {

        [SerializeField] private T t;
        [SerializeField] private T1 t1;
        
        [SerializeField] private int pageCount;
        [SerializeField] private int dataCount;

        private void Start() {
            t.Setup();
            t1.Setup(dataCount);
            
        }

       
    }
}