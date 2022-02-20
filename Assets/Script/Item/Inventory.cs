using UnityEngine;

public class Inventory : MonoBehaviour
{

    GameObject[] Item = new GameObject[20];

    private void Start()
    {
        Item = null;
    }

    //#region Singleton
    //public static Inventory Instance;
    //private void Awake()
    //{
    //    if(Instance!=null)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }
    //    Instance = this;
    //}
    //#endregion

    //public delegate void OnSlotCountChange(int val);
    //public OnSlotCountChange onSlotCountChange;

    //private int slotCnt;
    //public int SlotCnt
    //{
    //    get => slotCnt;
    //    set
    //    {
    //        slotCnt = value;
    //        onSlotCountChange.Invoke(SlotCnt);
    //    }
    //}

    //private void Start()
    //{
    //    slotCnt = 4;
    //}
}
