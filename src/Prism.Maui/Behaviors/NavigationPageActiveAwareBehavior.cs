﻿using System.ComponentModel;
using Prism.Common;
using PropertyChangingEventArgs = Microsoft.Maui.Controls.PropertyChangingEventArgs;

namespace Prism.Behaviors;

public class NavigationPageActiveAwareBehavior : BehaviorBase<NavigationPage>
{
    protected override void OnAttachedTo(NavigationPage bindable)
    {
        bindable.PropertyChanging += NavigationPage_PropertyChanging;
        bindable.PropertyChanged += NavigationPage_PropertyChanged;
        base.OnAttachedTo(bindable);
    }

    protected override void OnDetachingFrom(NavigationPage bindable)
    {
        bindable.PropertyChanging -= NavigationPage_PropertyChanging;
        bindable.PropertyChanged -= NavigationPage_PropertyChanged;
        base.OnDetachingFrom(bindable);
    }

    private void NavigationPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "CurrentPage")
        {
            PageUtilities.InvokeViewAndViewModelAction<IActiveAware>(AssociatedObject.CurrentPage, (obj) => obj.IsActive = true);
        }
    }

    private void NavigationPage_PropertyChanging(object sender, PropertyChangingEventArgs e)
    {
        if (e.PropertyName == "CurrentPage")
        {
            PageUtilities.InvokeViewAndViewModelAction<IActiveAware>(AssociatedObject.CurrentPage, (obj) => obj.IsActive = false);
        }
    }
}
