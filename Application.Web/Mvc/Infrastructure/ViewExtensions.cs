namespace Application.Web.Mvc.Infrastructure
{
    using System;
    using System.Collections.Generic;

    public static class ViewExtensions
    {
        //--------------------------------------------------------------------------------
        // 共通
        //--------------------------------------------------------------------------------

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetParameter<T>(this IDictionary<string, object> dictionary, string key, T defaultValue)
        {
            return dictionary.TryGetValue(key, out var value) ? (T)Convert.ChangeType(value, typeof(T)) : defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Then(this bool condition, string value)
        {
            return condition ? value : string.Empty;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="trueValue"></param>
        /// <param name="falseValue"></param>
        /// <returns></returns>
        public static string ThenElse(this bool condition, string trueValue, string falseValue)
        {
            return condition ? trueValue : falseValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Style(this Enum value)
        {
            return String.Concat(value.GetType().Name.ToLower(), "-", value.ToString().ToLower());
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string Joined<T>(this IEnumerable<T> values)
        {
            return String.Join(",", values);
        }

        //--------------------------------------------------------------------------------
        // Format
        //--------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------
        // Models
        //--------------------------------------------------------------------------------
    }
}
