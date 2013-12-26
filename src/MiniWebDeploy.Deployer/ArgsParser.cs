using System;
using System.Collections.Generic;

namespace MiniWebDeploy.Deployer
{
    public class ArgsParser
    {
        public Dictionary<string, string> Parse(string[] args)
        {
            var result = new Dictionary<string, string>();
            
            if(args.Length > 0)
            {
                result.Add("__SITEPATH", args[0]);
            }

            if (args.Length > 1)
            {
                for(var i = 1; i < args.Length; i += 2)
                {
                    result.Add(args[i].Substring(2).ToUpper(), args[i+1]);
                }
            }

            return result;
        }
    }
}
