﻿using NetCasbin.Util.Function;
using System;
using System.Collections.Generic;

namespace NetCasbin.Model
{
    public class FunctionMap
    {
        private IDictionary<string, AbstractFunction> _fm;

        public IDictionary<string, AbstractFunction> FunctionDict => _fm;

        public void AddFunction(string name, AbstractFunction function)
        {
            _fm.Add(name, function);
        }

        public static FunctionMap LoadFunctionMap()
        {
            var fm = new FunctionMap();
            fm._fm = new Dictionary<string, AbstractFunction>();
            fm.AddFunction("keyMatch", new KeyMatchFunc());
            fm.AddFunction("keyMatch2", new KeyMatch2Func());
            fm.AddFunction("regexMatch", new RegexMatchFunc());
            fm.AddFunction("ipMatch", new IPMatchFunc());
            return fm;
        }
    }
}
