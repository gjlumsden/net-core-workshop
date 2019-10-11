using System;

namespace DiscountCalculator.Core
{
    public class DiscountEngine
    {
        public double GetDiscountAmount(DateTime dateJoined)
        {
            var diff = DateTime.Today - dateJoined;

            switch ((int)Math.Floor(diff.TotalDays / 365))
            {
                case 0:
                    return 0;
                case 1:
                    return 0.1;
                case 2:
                case 3:
                case 4:
                    return 0.15;
                case 5:
                    return 0.2;
                default:
                    return 0.3;
            }
        }
    }
}
