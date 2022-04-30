using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Product Settings", menuName = "Settings/Product Settings")]
public class ProductSettings : ScriptableObject
{
    [SerializeField]
    int productId = -1;
    [SerializeField]
    GameObject productGO = null;
    [SerializeField]
    Sprite sprite = null;
    [SerializeField]
    uint cost = 0;
    public bool bought = false;

    public int _ProductID { get => productId; set => productId = value; }
    public GameObject _ProductGO { get => productGO; }
    public Sprite _Sprite { get => sprite; }
    public uint _Cost { get => cost; }

}
