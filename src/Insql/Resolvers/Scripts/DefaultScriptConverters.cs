﻿using Jint;
using Jint.Native;
using Jint.Runtime.Interop;
using System;

namespace Insql.Resolvers.Scripts
{
    internal class ScriptDateTimeConverter : IObjectConverter
    {
        private static readonly DateTime Min = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private readonly Engine engine;

        public ScriptDateTimeConverter(Engine engine)
        {
            this.engine = engine;
        }

        public bool TryConvert(object value, out JsValue result)
        {
            if (value == null)
            {
                result = JsValue.Null; return false;
            }

            if (value is DateTime dateTimeValue)
            {
                if (dateTimeValue == DateTime.MinValue)
                {
                    result = JsValue.FromObject(this.engine, Min); return true;
                }
            }
            if (value is DateTimeOffset dateTimeOffsetValue)
            {
                if (dateTimeOffsetValue == DateTimeOffset.MinValue)
                {
                    result = JsValue.FromObject(this.engine, Min); return true;
                }
            }

            result = JsValue.Null; return false;
        }
    }

    internal class ScriptEnumConverter : IObjectConverter
    {
        public static ScriptEnumConverter Instance = new ScriptEnumConverter();

        public bool TryConvert(object value, out JsValue result)
        {
            if (value == null)
            {
                result = JsValue.Null; return false;
            }

            if (value.GetType().IsEnum)
            {
                result = new JsValue(value.ToString()); return true;
            }

            result = JsValue.Null; return false;
        }
    }
}
