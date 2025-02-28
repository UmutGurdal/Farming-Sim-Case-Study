using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class Extensions
{
    public static void InvokeAfterSeconds(this MonoBehaviour mono, float seconds, Action target)
    {
        mono.StartCoroutine(InvokeCoroutine(seconds, target));
    }

    private static IEnumerator InvokeCoroutine(float seconds, Action target)
    {
        yield return new WaitForSeconds(seconds);
        target.Invoke();
    }

    public static async Task<UnityWebRequest> MakeWebRequest(this MonoBehaviour _, string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        var operation = request.SendWebRequest();

        while (!operation.isDone)
        {
            await Task.Yield();
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Web request failed: " + request.error);
            request.Dispose();
            return null;
        }

        Debug.Log("Request sent successfully");
        return request;
    }

    public static string ReformatAsTimeString(this float value) 
    {
        var minute = value / 60;
        var second = value % 60;

        return minute.ToString("#0") + ":" + second.ToString("00");
    }

    public static string ReformatAsString(this double value)
    {
        if (value < 1E+3)
            return value.ToString("0.#");
        else if (value >= 1E+3 && value < 1E+6)
            return (value / 1E+3).ToString("0.##" + "K");
        else if (value >= 1E+6 && value < 1E+9)
            return (value / 1E+6).ToString("0.##" + "M");
        else if (value >= 1E+9 && value < 1E+12)
            return (value / 1E+9).ToString("0.##" + "B");
        else if (value >= 1E+12 && value < 1E+15)
            return (value / 1E+12).ToString("0.##" + "T");
        else
            return value.ToString("0.0#e+00");
    }

    public static string ReformatAsString(this float value)
    {
        if (value < 1E+3)
            return value.ToString("0.#");
        else if (value >= 1E+3 && value < 1E+6)
            return (value / 1E+3).ToString("0.##" + "K");
        else if (value >= 1E+6 && value < 1E+9)
            return (value / 1E+6).ToString("0.##" + "M");
        else if (value >= 1E+9 && value < 1E+12)
            return (value / 1E+9).ToString("0.##" + "B");
        else if (value >= 1E+12 && value < 1E+15)
            return (value / 1E+12).ToString("0.##" + "T");
        else
            return value.ToString("0.0#e+00");
    }

    public static string ReformatAsString(this int value)
    {
        if (value < 1E+3)
            return value.ToString("0");
        else if (value >= 1E+3 && value < 1E+6)
            return (value / 1E+3).ToString("0.##" + "K");
        else if (value >= 1E+6 && value < 1E+9)
            return (value / 1E+6).ToString("0.##" + "M");
        else if (value >= 1E+9 && value < 1E+12)
            return (value / 1E+9).ToString("0.##" + "B");
        else if (value >= 1E+12 && value < 1E+15)
            return (value / 1E+12).ToString("0.##" + "T");
        else
            return value.ToString("0.0#e+00");
    }
}
