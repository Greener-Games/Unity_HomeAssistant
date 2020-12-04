using System;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;

public class Entity : SerializedMonoBehaviour
{
    public string entityId;
    
    [OdinSerialize][NonSerialized]
    public StateObject rawData;

    public UnityAction DataFetched;

    public string State => rawData.State;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Debug.Log($"Fetching Data {gameObject.name}");
        FetchBaseData();
    }
    
    async Task FetchBaseData()
    {
        rawData = await RequestClient.GetState(entityId);
        await CustomFetchData();
        
        DataFetched?.Invoke();
    }

    protected virtual async Task CustomFetchData()
    {
        
    }

}