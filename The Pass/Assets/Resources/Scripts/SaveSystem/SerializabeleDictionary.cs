using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializabeleDictionary<TKey, TValue> : Dictionary<TKey,TValue>, ISerializationCallbackReceiver
{
   [SerializeField] private List<TKey> keys = new List<TKey>();
   [SerializeField] private List<TValue> values = new List<TValue>();


    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();

        foreach(KeyValuePair<TKey,TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
            //Debug.Log(" pair.Key: " + pair.Key + " pair.Value: " + pair.Value);
        }
    }
    public void OnAfterDeserialize()
    {
        
        this.Clear();


        if(keys.Count != values.Count)
        {
            Debug.LogError(" Nie mo�na SerializabeleDictionary d�ugo�� listy Kluczy: " + keys.Count + " Nie odpowiada d�ugosci listy warto�ci: " + values.Count);
        }

        for(int i=0; i <keys.Count;i++)
        {
            this.Add(keys[i], values[i]);
        }
        
    }
}
