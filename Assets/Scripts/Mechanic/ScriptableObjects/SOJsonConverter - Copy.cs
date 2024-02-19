using UnityEngine;

//SO stands for ScriptableObject
public class SOJsonConverterCopy : MonoBehaviour
{   
    enum Convertion
    {
        ToJson,
        ToSO
    }
    [SerializeField] Convertion _convert;
    [SerializeField] string _path;

    private void Start()
    {
        if (_convert == Convertion.ToJson) { ConvertToJson(); }
        else { ConvertToSO(); }
    }
    public virtual void ConvertToJson()
    {
        
    }

    public virtual void ConvertToSO()
    {
        
    }
}