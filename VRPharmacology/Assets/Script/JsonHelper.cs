using UnityEngine;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        return JsonUtility.FromJson<Wrapper<T>>("{\"Items\":" + json + "}").Items;
    }

    public static string ToJson<T>(T[] array)
    {
        return JsonUtility.ToJson(new Wrapper<T> { Items = array });
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
