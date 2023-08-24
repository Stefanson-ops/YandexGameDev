using UnityEngine;

public class Inventory_Controller : MonoBehaviour
{
    [SerializeField] private GameObject _currentEquipment = null;

    public void Equip(GameObject thing)
    {
        Progress_Manager.Instance.playerInfo.Armor = thing.GetComponent<Item_Controller>().Armor;
        _currentEquipment = thing;
    }
}
