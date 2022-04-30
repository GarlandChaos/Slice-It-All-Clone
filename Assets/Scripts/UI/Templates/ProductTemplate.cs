using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EventSystem;
using GameSystem;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class ProductTemplate : MonoBehaviour
    {
        int id = -1;
        ProductSettings productSettings = null;
        [SerializeField]
        Button buyButton = null;
        Image templateBackgroundImage = null;
        [SerializeField]
        Image productImage = null;
        [SerializeField]
        TMP_Text productCostText = null;
        Color defaultProductTemplateColor = Color.green;
        [SerializeField]
        Color selectedProductTemplateColor = Color.yellow;
        Color defaultCostTextColor = Color.white;
        Color selectedCostTextColor = Color.black;
        [SerializeField]
        GameEvent selectedProductTemplateEvent = null;
        [SerializeField]
        GameEvent buyProductEvent = null;
        [SerializeField]
        GameEvent setProductIdEvent = null;

        private void Awake()
        {
            buyButton = GetComponent<Button>();
            templateBackgroundImage = GetComponent<Image>();
            defaultProductTemplateColor = templateBackgroundImage.color;
            defaultCostTextColor = productCostText.color;
        }

        private void OnEnable()
        {
            StartCoroutine(CheckAvailabilityCoroutine());
        }

        public void AssignProductSettings(int _id, ProductSettings _productSettings)
        {
            id = _id;
            if (_productSettings)
            {
                productSettings = _productSettings;
                productImage.sprite = productSettings._Sprite;
                ChangeProductCostText(false);
            }
        }

        void ChangeProductCostText(bool isSelected)
        {
            if (isSelected)
            {
                productCostText.text = "Selecionado";
                productCostText.color = selectedCostTextColor;
            }
            else
            {
                if (productSettings.bought)
                {
                    productCostText.text = "Comprado";
                }
                else
                {
                    productCostText.text = "$ " + productSettings._Cost.ToString();
                }
                productCostText.color = defaultCostTextColor;
            }

        }

        void ChangeTemplateBackgroundImageColor(Color color)
        {
            templateBackgroundImage.color = color;
        }

        void BuyProduct()
        {
            if (!productSettings.bought && (int)GameManager.instance._Money - (int)productSettings._Cost >= 0)
            {
                productSettings.bought = true;
                buyProductEvent.Invoke(productSettings._Cost);
                setProductIdEvent.Invoke(productSettings._ProductID);
            }
            else if (productSettings.bought)
            {
                setProductIdEvent.Invoke(productSettings._ProductID);
            }
        }

        public void Select()
        {
            //attempt to buy the product
            BuyProduct();

            //Change elements to selected
            ChangeTemplateBackgroundImageColor(selectedProductTemplateColor);
            ChangeProductCostText(true);

            selectedProductTemplateEvent.Invoke(id);
        }

        public void Deselect()
        {
            //Change elements to unselected
            ChangeTemplateBackgroundImageColor(defaultProductTemplateColor);
            ChangeProductCostText(false);
        }

        IEnumerator CheckAvailabilityCoroutine()
        {
            yield return new WaitForEndOfFrame();
            if (productSettings && GameManager.instance)
            {
                bool canBuy = (!productSettings.bought && (int)GameManager.instance._Money - (int)productSettings._Cost >= 0) || productSettings.bought;
                buyButton.interactable = canBuy;
            }
        }
    }
}
