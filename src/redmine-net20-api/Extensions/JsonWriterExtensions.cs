﻿/*
   Copyright 2016 - 2019 Adrian Popescu.

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Redmine.Net.Api.Types;
using RedmineClient.Internals.Serialization;


namespace Redmine.Net.Api.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class JsonExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonWriter"></param>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        public static void WriteIdIfNotNull(this JsonWriter jsonWriter, string tag, IdentifiableName value)
        {
            if (value != null)
            {
                jsonWriter.WritePropertyName(tag);
                jsonWriter.WriteValue(value.Id);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonWriter"></param>
        /// <param name="tag"></param>
        /// <param name="ident"></param>
        /// <param name="emptyValue"></param>
        public static void WriteIdOrEmpty(this JsonWriter jsonWriter, string tag, IdentifiableName ident, string emptyValue = null)
        {
            if (ident != null)
            {
                jsonWriter.WriteProperty(tag, ident.Id);
            }
            else
            {
                jsonWriter.WriteProperty(tag, emptyValue);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonWriter"></param>
        /// <param name="tag"></param>
        /// <param name="val"></param>
        /// <param name="dateFormat"></param>
        public static void WriteDateOrEmpty(this JsonWriter jsonWriter, string tag, DateTime? val, string dateFormat = "yyyy-MM-dd")
        {
            if (!val.HasValue || val.Value.Equals(default(DateTime)))
            {
                jsonWriter.WriteProperty(tag, string.Empty);
            }
            else
            {
                jsonWriter.WriteProperty(tag, string.Format(NumberFormatInfo.InvariantInfo, "{0}", val.Value.ToString(dateFormat, CultureInfo.InvariantCulture)));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonWriter"></param>
        /// <param name="tag"></param>
        /// <param name="val"></param>
        public static void WriteValueOrEmpty<T>(this JsonWriter jsonWriter, string tag, T? val) where T : struct
        {
            if (!val.HasValue || EqualityComparer<T>.Default.Equals(val.Value, default(T)))
            {
                jsonWriter.WriteProperty(tag, string.Empty);
            }
            else
            {
                jsonWriter.WriteProperty(tag, val.Value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonWriter"></param>
        /// <param name="tag"></param>
        /// <param name="val"></param>
        public static void WriteValueOrDefault<T>(this JsonWriter jsonWriter, string tag, T? val) where T : struct
        {
            jsonWriter.WriteProperty(tag, val ?? default(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonWriter"></param>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        public static void WriteProperty(this JsonWriter jsonWriter, string tag, object value)
        {
            jsonWriter.WritePropertyName(tag);
            jsonWriter.WriteValue(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonWriter"></param>
        /// <param name="tag"></param>
        /// <param name="collection"></param>
        public static void WriteArrayIds(this JsonWriter jsonWriter, string tag, IEnumerable<IdentifiableName> collection)
        {
            if (collection == null)
            {
                return;
            }

            jsonWriter.WritePropertyName(tag);
            jsonWriter.WriteStartArray();

            //var value = string.Join(",", collection.Select(x => x.Id.ToString(CultureInfo.InvariantCulture)));
            //jsonWriter.WriteValue(value);

            jsonWriter.WriteEndArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonWriter"></param>
        /// <param name="tag"></param>
        /// <param name="collection"></param>
        public static void WriteArrayNames(this JsonWriter jsonWriter, string tag, IEnumerable<IdentifiableName> collection)
        {
            if (collection == null)
            {
                return;
            }

            jsonWriter.WritePropertyName(tag);
            jsonWriter.WriteStartArray();

            //var value = string.Join(",", collection.Select(x => x.Name));
            //jsonWriter.WriteValue(value);

            jsonWriter.WriteEndArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonWriter"></param>
        /// <param name="tag"></param>
        /// <param name="collection"></param>
        public static void WriteArray<T>(this JsonWriter jsonWriter, string tag, ICollection<T> collection) where T : IJsonSerializable
        {
            if (collection == null)
            {
                return;
            }

            jsonWriter.WritePropertyName(tag);
            jsonWriter.WriteStartArray();

            foreach (var item in collection)
            {
                item.WriteJson(jsonWriter);
            }

            jsonWriter.WriteEndArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonWriter"></param>
        /// <param name="tag"></param>
        /// <param name="collection"></param>
        public static void WriteListAsProperty(this JsonWriter jsonWriter, string tag, ICollection collection)
        {
            if (collection == null)
            {
                return;
            }

            foreach (var item in collection)
            {
                jsonWriter.WriteProperty(tag, item);
            }
        }
    }
}