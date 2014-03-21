[![Stories in Ready](https://badge.waffle.io/jaimalchohan/mini-web-deploy.png?label=ready)](https://waffle.io/jaimalchohan/mini-web-deploy)
[![Build status](https://ci.appveyor.com/api/projects/status/j3rx0d344lurnnuk)](https://ci.appveyor.com/project/jaimalchohan/mini-web-deploy)

#MiniWebDeploy

A really simple website deployment tool providing an easy to understand and discoverable wrapper around the Microsoft.Web.Administration assembly and additional helpers for common deployment tasks.

##Why would I use it?
You already have a method for creating your website deployment package and publishing the website to your server, but once it's on the server you need to configure IIS (create the website & app pool, configure bindings and logging etc).  You either do this manually, or via a script that calls out to [AppCmd.exe](http://technet.microsoft.com/en-us/library/cc772200.aspx). It's not very intuitive, your deployment configuration is separated from your application and it's not testable.

**MiniWebDeploy** enables you to keep your deployment configuration within your website (so it knows how to deploy itself), allows you to unit test your configuration (handy for when you have different configurations on your Development, QA and Production environment) and makes the process repeatable (handy for cloud environments).

##How does it work?

In your web application, create a class which Implements `ISiteInstaller` which has 1 method to implement, `Install`.  This provides you with a fluent-like syntax for configuring your website (if you leave the method empty defaults will be used). When your site has been published and you're ready to configure IIS, you execute `MiniWebDeploy.exe` providing just the path to your site and any additional custom parameters you need to configure your site.


##Show me the money!

Install the **MiniWebDeploy** NuGet package

Create a SiteInstaller class somewhere in your website for example

    public class SiteInstaller : ISiteInstaller
    {
        public void Install(IInstallationConfiguration x)
        {
            x.WithSiteName("MiniWebDeploy")
                .AndDefaultHttpBinding()
                .AndHttpBinding("jaimalchohan.com")
                .AndAutoStart()
                .AndDeleteExistingSite();
            
            x.WithAppPool("MiniWebDeployAppPool")
                .AndDeleteExistingAppPool()
                .AndManagedRuntimeVersion("v4.0")
                .AndStartOnDemand();

            x.WithLogFile()
                .AndDirectory("c:\\iislogs")
                .AndCreateDirectoryWithElevatedPermissions();
        }
    }

Rebuild and publish your site

In your packages directory execute `MiniWebDeploy.exe` with the following command

`\packages\MiniWebDeploy.[version]\tools\MiniWebDeploy.exe [path to your site]`

And that's all,  the executable will scan your site for any class which implements `ISiteInstaller` and use reflection to pick up your configuration and then configure IIS.





[![Bitdeli Badge](https://d2weczhvl823v0.cloudfront.net/jaimalchohan/mini-web-deploy/trend.png)](https://bitdeli.com/free "Bitdeli Badge")

