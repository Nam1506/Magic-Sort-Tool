using System.Collections.Generic;
using System;
using TMPro;

public static class DropdownUtil
{
    public static void SetupDropdown<T>(TMP_Dropdown dropdown) where T : Enum
    {
        T[] values = (T[])Enum.GetValues(typeof(T));
        List<string> listName = new List<string>();

        for (int i = 0; i < values.Length; i++)
        {
            listName.Add(values[i].ToString());
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(listName);
        dropdown.value = 0;
    }
}
