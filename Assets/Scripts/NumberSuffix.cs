public class NumberSuffix
{
    public static string SetValueWithSuffix(float value)
    {
        int zero = 0;
        var baseValue = value;

        while (value >= 1000)
        {
            ++zero;
            value /= 1000;
        }

        string suffix = string.Empty;

        switch (zero)
        {
            case 1: suffix = "K"; break;
            case 2: suffix = "M"; break;
            case 3: suffix = "B"; break;
            case 4: suffix = "T"; break;
            case 5: suffix = "Qd"; break;
            case 6: suffix = "Qn"; break;
            case 7: suffix = "Sx"; break;
            case 8: suffix = "Sp"; break;
            case 9: suffix = "Oc"; break;
        }

        string valueChanged = value.ToString();
        if (baseValue >= 1000) valueChanged = value.ToString("0.0");

        return $"{valueChanged}{suffix}";
    }
}
