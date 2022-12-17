﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Global.Managers.UserInterface
{
    using Global.Components.UserInterface;

    public class UIManager : BaseManager
    {
        public override Type ManagerType => typeof(UIManager);

#pragma warning disable
        [SerializeField] private List<WindowContainer> windowContainers;
#pragma warning restore

        private Dictionary<Type, BaseWindow> windowsDictionary = new Dictionary<Type, BaseWindow>();

        protected override bool OnInit()
        {
            InitDictionary();
            return true;
        }

        private void InitDictionary()
        {
            foreach (WindowContainer container in windowContainers)
            {
                windowsDictionary.Add(container.WindowType, container.Window);
            }
        }

        public T ShowWindow<T>() where T : BaseWindow
        {
            T window = GetWindow<T>();
            if (window != null)
            {
                SetTopOrder(window);
                window.Show();
            }

            return window;
        }

        public BaseWindow ShowWindow(Type t)
        {
            BaseWindow window = GetWindow(t);
            if (window != null)
            {
                SetTopOrder(window);
                window.Show();
            }

            return window;
        }

        public T HideWindow<T>() where T : BaseWindow
        {
            T window = GetWindow<T>();
            if (window != null)
            {
                SetLowestOrder(window);
                window.Hide();
            }

            return window;
        }

        public BaseWindow HideWindow(Type t)
        {
            BaseWindow window = GetWindow(t);
            if (window != null)
            {
                SetLowestOrder(window);
                window.Hide();
            }

            return window;
        }

        public T GetWindow<T>() where T : BaseWindow
        {
            if (windowsDictionary.ContainsKey(typeof(T)))
            {
                return (T)windowsDictionary[typeof(T)];
            }

            return null;
        }

        public BaseWindow GetWindow(Type t)
        {
            return windowsDictionary.ContainsKey(t) ? windowsDictionary[t] : null;
        }

        private void SetTopOrder(BaseWindow window)
        {
            window.Order = windowContainers.Select(container => container.Window.Order).Prepend(-1).Max() + 1;
        }

        private void SetLowestOrder(BaseWindow window)
        {
            window.Order = -1;
        }
    }

    [Serializable]
    public class WindowContainer
    {
#pragma warning disable
        [SerializeField] private BaseWindow window;
#pragma warning restore

        public Type WindowType => window.GetType();
        public BaseWindow Window => window;
    }
}
