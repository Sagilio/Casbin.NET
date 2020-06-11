﻿using NetCasbin.Rbac;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCasbin.Model
{
    /// <summary>
    /// 断言
    /// </summary>
    public class Assertion
    {
        public string Key { set; get; }

        public string Value { set; get; }

        public string[] Tokens { set; get; }

        private List<List<string>> _policy;

        private IRoleManager _rm;

        public IRoleManager RM => _rm;

        public List<List<string>> Policy
        {
            set => _policy = value;
            get => _policy;
        }

        public Assertion()
        {
            _policy = new List<List<string>>();
            _rm = new DefaultRoleManager(0);
        }

        public void BuildRoleLinks(IRoleManager rm)
        {
            _rm = rm;
            var count =  Value.ToCharArray().Count(x => x == '_');
            foreach (var rule in _policy)
            {
                if (count < 2)
                {
                    throw new Exception("the number of \"_\" in role definition should be at least 2");
                }
                if (rule.Count < count)
                {
                    throw new Exception("grouping policy elements do not meet role definition");
                }

                if (count == 2)
                {
                    rm.AddLink(rule[0], rule[1]);
                }
                else if (count == 3)
                {
                    rm.AddLink(rule[0], rule[1], rule[2]);
                }
                else if (count == 4)
                {
                    rm.AddLink(rule[0], rule[1], rule[2], rule[3]);
                }
            }

        }
    }
}
