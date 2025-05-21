using UnityEngine;

// The code in this class was was written by chatGPT
// I think I saw a simmilar solution in StackOverFlow which is why I was willing to trust this

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
