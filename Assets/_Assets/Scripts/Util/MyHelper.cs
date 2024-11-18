using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public static class MyHelper
{
    public static void Shuffle<T>(List<T> list)
    {
        if (list.Count == 1) return;

        List<T> listClone = new List<T>(list);

        System.Random rng = new System.Random();
        do
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        while (CompareList(list, listClone));
    }

    public static bool CompareList<T>(List<T> list1, List<T> list2)
    {
        if (list1.Count != list2.Count)
            return false;

        int numElement = list1.Count;
        for (int i = 0; i < list1.Count; i++)
        {
            if (!EqualityComparer<T>.Default.Equals(list1[i], list2[i]))
                return false;
        }

        return true;
    }

    public static void SetPositionZ(this Transform transform, float z)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }
    public static void SetPositionX(this Transform transform, float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    public static void SetPositionY(this Transform transform, float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    public static void SetLocalPositionX(this Transform transform, float x)
    {
        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
    }

    public static void SetLocalPositionY(this Transform transform, float y)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
    }

    public static void SetLocalPositionZ(this Transform transform, float z)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
    }

    public static void SetLocalScaleX(this Transform transform, float scaleX)
    {
        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }

    public static void SetLocalScaleY(this Transform transform, float scaleY)
    {
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }

    public static DateTime AddDuration(DateTime time, int duration)
    {
        return time.AddSeconds(duration);
    }

    public static string DateTimeToString(this DateTime dateTime)
    {
        return dateTime.ToString("MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
    }

    public static DateTime StringToDate(this string date)
    {
        if (string.IsNullOrEmpty(date))
        {
            //return DateTime.Now;
            return DateTime.MinValue;
        }

        try
        {
            return DateTime.ParseExact(date, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        return GetDateTimeNow();
        //bool success = DateTime.TryParse(date, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);

        //return result;
    }

    public static DateTime GetDateTimeNow()
    {
        return DateTime.Now;
    }
}
