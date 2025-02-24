using System.Collections.Generic;
using ChatGPT.Model.Plugins;
using ChatGPT.Model.Services;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace ChatGPT.Services;

public class PluginsService : IPluginsService
{
    private readonly List<IChatPlugin> _plugins = new();
    private readonly IPluginContext _pluginContext;

    public PluginsService(IPluginContext pluginContext)
    {
        _pluginContext = pluginContext;
    }

    public void DiscoverPlugins()
    {
        var plugins = Ioc.Default.GetServices<IChatPlugin>();

        foreach (var plugin in plugins)
        {
            _plugins.Add(plugin);
        }
    }

    public void InitPlugins()
    {
        foreach (var plugin in _plugins)
        {
            plugin.Initialize(_pluginContext);
        }
    }

    public void StartPlugins()
    {
        foreach (var plugin in _plugins)
        {
            plugin.Start();
        }
    }

    public void ShutdownPlugins()
    {
        foreach (var plugin in _plugins)
        {
            plugin.Shutdown();
        }
    }
}
