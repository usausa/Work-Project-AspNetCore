﻿namespace WebApplication.Infrastructure.Resolver
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    using Smart.Resolver.Bindings;
    using Smart.Resolver.Scopes;

    /// <summary>
    ///
    /// </summary>
    public class RequestScopeStorage : IScopeStorage
    {
        private static readonly string StorageKey = "__RequestScopeStorage";

        private readonly IHttpContextAccessor accessor;

        /// <summary>
        ///
        /// </summary>
        /// <param name="accessor"></param>
        public RequestScopeStorage(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="instance"></param>
        public void Remember(IBinding binding, object instance)
        {
            var context = accessor.HttpContext;

            object value;
            context.Items.TryGetValue(StorageKey, out value);
            var dictionary = (Dictionary<IBinding, object>)value;
            if (dictionary == null)
            {
                dictionary = new Dictionary<IBinding, object>();
                context.Items[StorageKey] = dictionary;
            }

            dictionary[binding] = instance;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
        public object TryGet(IBinding binding)
        {
            var context = accessor.HttpContext;

            object value;
            context.Items.TryGetValue(StorageKey, out value);
            var dictionary = (Dictionary<IBinding, object>)value;
            if (dictionary == null)
            {
                return null;
            }

            object instance;
            return dictionary.TryGetValue(binding, out instance) ? instance : null;
        }

        /// <summary>
        ///
        /// </summary>
        public void Clear()
        {
            var context = accessor.HttpContext;

            object value;
            context.Items.TryGetValue(StorageKey, out value);
            var dictionary = (Dictionary<IBinding, object>)value;
            if (dictionary == null)
            {
                return;
            }

            foreach (var instance in dictionary.Values)
            {
                (instance as IDisposable)?.Dispose();
            }

            context.Items.Remove(StorageKey);
        }
    }
}