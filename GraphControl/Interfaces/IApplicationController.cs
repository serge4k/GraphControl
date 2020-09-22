﻿using System;
using GraphControlCore.Interfaces.Presenters;
using GraphControlCore.Interfaces.Views;

namespace GraphControlCore.Interfaces
{
    public interface IApplicationController : IDisposable
    {
        IApplicationController RegisterInstance<TArgument>(TArgument instance);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        IApplicationController RegisterService<TService, TImplementation>()
            where TImplementation : class, TService;

        TPresenter Run<TPresenter>()
            where TPresenter : class, IPresenter;

        T GetInstance<T>()
            where T : class;
    }
}
