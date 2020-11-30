using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfitDescriptionBox : MonoBehaviour
{
    List<ProfitDescriptionElement> profitElements = new List<ProfitDescriptionElement>();
    public ProfitDescriptionElement profitDescriptionElementPrefab;

    public void OnDestroy()
    {
        for (int i = 0; i < profitElements.Count; i++)
        {
            Destroy(profitElements[i]);
        }
        profitElements.Clear();
    }


    public void Init(List<CurrencyAmount> currencies)
    {
        foreach(CurrencyAmount currencyData in currencies)
        {
            ProfitDescriptionElement newCurrency = Instantiate<ProfitDescriptionElement>(profitDescriptionElementPrefab, transform.position, Quaternion.identity);
            newCurrency.transform.SetParent(transform, false);
            newCurrency.Init(currencyData);
            profitElements.Add(newCurrency);
        }
    }
}
