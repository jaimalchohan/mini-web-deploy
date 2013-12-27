using System;
using System.Collections.Generic;
using System.IO;

namespace MiniWebDeploy.Deployer
{
    public class ArgsParser
    {
        public Dictionary<string, string> Parse(string[] args)
        {
            var result = new Dictionary<string, string>();
            
            if(args.Length > 0)
            {
                var path = args[0];
                path = !Path.IsPathRooted(path) ? Path.GetFullPath(path) : path;
                result.Add("__SITEPATH", path);
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
