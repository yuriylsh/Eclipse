﻿using System.Collections.Generic;

namespace Tests
{
    public static class TestHelper
    {
        public static IEnumerable<string> GetTestData()
        {
            return new[]
            {
                "# Skip this line.",
                "6|Seattle|Washington|I-5;I-90",
                "27 |Chicago|Illinois|I-94;I-90;I-88;I-57;I-55",
                "10 |San Jose|California|I-5;I-80"
            };
        }
    }
}
