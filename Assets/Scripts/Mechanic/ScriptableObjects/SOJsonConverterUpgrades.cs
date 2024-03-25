using UnityEngine;

//SO stands for ScriptableObject
public class SOJsonConverterUpgrades : MonoBehaviour
{   
    enum Convertion
    {
        ToJson,
        ToSO
    }
    [SerializeField] Convertion _convert;
    [SerializeField] Upgrade[] _upgrade;

    private void Start()
    {
        if (_convert == Convertion.ToJson) { ConvertToJson(); }
        else { ConvertToSO(); }
    }
    public void ConvertToJson()
    {
        
    }

    public void ConvertToSO()
    {
        
    }

    private void ReadJson()
    {
        
    }
}