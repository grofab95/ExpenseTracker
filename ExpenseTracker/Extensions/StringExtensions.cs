using System;

namespace BookStore.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNotExist(this String value)
            => string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
    }
}