﻿using System.IO;

namespace MiniWebDeploy.Deployer.Infrastructure
{
    public class DirectoryWrapper : IDirectory
    {
        public void CreateDirectory(string directory)
        {
            Directory.CreateDirectory(directory);
        }
    }
}
