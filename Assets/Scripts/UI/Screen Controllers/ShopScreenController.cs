using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

namespace UI
{
    public class ShopScreenController : MonoBehaviour
    {
        [SerializeField]
        ProductList productList = null;
        [SerializeField]
        ProductTemplate productTemplateGO = null;
        [SerializeField]
        RectTransform productsContainer = null;
        Dictionary<int, ProductTemplate> productTemplateDictionary = new Dictionary<int, ProductTemplate>();
        int currentSelectedProductTemplate = 0;
        [SerializeField]
        GameEvent closeShopScreenEvent = null;

        private void Awake()
        {
            if (productList)
            {
                ProductTemplate productTemplate = null;
                int id = 0;
                foreach (ProductSettings p in productList._Products)
                {
                    productTemplate = Instantiate(productTemplateGO).GetComponent<ProductTemplate>();
                    productTemplate.AssignProductSettings(id, p);
                    productTemplate.transform.SetParent(productsContainer);
                    productTemplateDictionary.Add(id, productTemplate);
                    id++;
                }
            }
            StartCoroutine(SelectFirstProductTemplateCoroutine());
        }

        public void ChangeSelectedProduct(int id)
        {
            if (id != currentSelectedProductTemplate)
            {
                productTemplateDictionary[currentSelectedProductTemplate].Deselect();
                currentSelectedProductTemplate = id;
            }
        }

        public void OnCloseButton()
        {
            closeShopScreenEvent.Invoke();
            gameObject.SetActive(false);
        }

        IEnumerator SelectFirstProductTemplateCoroutine()
        {
            yield return new WaitForEndOfFrame();
            if (productTemplateDictionary.Count > 0)
            {
                productTemplateDictionary[currentSelectedProductTemplate].Select();
            }
        }
    }
}
