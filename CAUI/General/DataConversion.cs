namespace CA.UI.General
{
    public class DataConversion
    {
        public static decimal ValueRound2(decimal value)
        {
            string value1 = value.ToString("N2");
            return Convert.ToDecimal(value1);
        }
    }
}
