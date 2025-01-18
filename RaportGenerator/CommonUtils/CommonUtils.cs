using System;

namespace RaportGenerator.CommonUtils
{
    public static class CommonUtils
    {
        public static string GetDocumentNumber(string lastNumber)
        {
            int number = 1;
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;

            if (lastNumber == null)
            {
                return $"{number}/{currentMonth}/{currentYear}";
            }

            string[] tabOfNumbersFromDocument = lastNumber.Split("/", StringSplitOptions.RemoveEmptyEntries);
            int docNumber = int.Parse(tabOfNumbersFromDocument[0]);
            int month = int.Parse(tabOfNumbersFromDocument[1]);
            int year = int.Parse(tabOfNumbersFromDocument[2]);

            if (currentMonth > month || currentYear > year)
            {
                return $"{number}/{currentMonth}/{currentYear}";
            }

            return $"{++docNumber}/{currentMonth}/{currentYear}";
        }
    }
}