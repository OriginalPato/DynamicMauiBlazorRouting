﻿namespace Maui.Blazor.CompositeUi;

public static class ServiceHelper
{
    public static T GetService<T>() => (Current ?? throw new InvalidOperationException()).GetService<T>() ?? throw new InvalidOperationException();

    private static IServiceProvider Current =>
        #if WINDOWS10_0_17763_0_OR_GREATER
            MauiWinUIApplication.Current.Services;
        #elif ANDROID
            MauiApplication.Current.Services;
        #elif IOS || MACCATALYST
            MauiUIApplicationDelegate.Current.Services;
        #else
            null;
        #endif
}