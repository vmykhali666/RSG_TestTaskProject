using System.Collections.Generic;
using System.Linq;
using Content.Features.StorageModule.Scripts;
using UnityEngine;

namespace Content.Features.ShopModule.Scripts
{
    public class Trader : MonoBehaviour
    {
        public float SellAllItemsFromStorage(IStorage storage)
        {
            var sumOfMoney = storage.GetAllItems<SellableItem>().Aggregate(0f, (current, item) => current + item.Price);
            storage.RemoveAllItems();
            Debug.LogError("Recieved " + sumOfMoney);
            return sumOfMoney;
        }

        public float SellItemFromStorage(SellableItem item, IStorage storage)
        {
            storage.RemoveItem(item);

            return item.Price;
        }

        public float SellItemsFromStorage(List<SellableItem> items, IStorage storage)
        {
            storage.RemoveItems(items.Cast<Item>().ToList()); //better to write your own ContravariantList<in T>


            var sumOfMoney = items.Aggregate(0f, (current, item) => current + item.Price);

            return sumOfMoney;
        }
    }
}