using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Product List", menuName = "Lists/Product List")]
public class ProductList : ScriptableObject
{
    [SerializeField]
    List<ProductSettings> products = new List<ProductSettings>();

    public List<ProductSettings> _Products { get => products; }
}
