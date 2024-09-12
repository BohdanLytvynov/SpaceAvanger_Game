﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ViewModelBaseLibDotNetCore.VM;

namespace WPF.UI.MonoGameControls;



public interface IMonoGameViewModel : IDisposable
{
    public bool IsDebugging { get; set; }

    IGraphicsDeviceService GraphicsDeviceService { get; set; }

    void Initialize();
    void LoadContent();
    void UnloadContent();
    void Update(GameTime gameTime);
    void Draw(GameTime gameTime);
    void AfterRender();
    void OnActivated(object sender, EventArgs args);
    void OnDeactivated(object sender, EventArgs args);
    void OnExiting(object sender, EventArgs args);

    void OnMouseDown(MouseStateArgs mouseState);
    void OnMouseMove(MouseStateArgs mouseState);
    void OnMouseUp(MouseStateArgs mouseState);

    void OnDrop(DragStateArgs dragState);
    void OnMouseWheel(MouseStateArgs args, int delta);
   
    void SizeChanged(object sender, SizeChangedEventArgs args);

    void KeyDownHandler(object sender, KeyEventArgs e);

    void EnableDebugging();
}

public class MonoGameViewModel : ViewModelBase, IMonoGameViewModel //Here Must be ViewModel
{
    public static Action? OnContentUnloaded;

    public MonoGameViewModel()
    {
    }

    public virtual void Dispose()
    {
        Content?.Dispose();
    }

    public IGraphicsDeviceService GraphicsDeviceService { get; set; } = default!;
    protected GraphicsDevice GraphicsDevice => GraphicsDeviceService?.GraphicsDevice!;
    protected MonoGameServiceProvider Services { get; private set; } = default!;
    protected ContentManager Content { get; set; } = default!;
    protected List<IGameComponent> Components { get; } = new();

    public bool IsDebugging { get; set; }

    public virtual void Initialize()
    {
        Services = new MonoGameServiceProvider();
        Services.AddService(GraphicsDeviceService);
        Content = new ContentManager(Services) { RootDirectory = "Content" };
    }

    protected void PostInitialize()
    {
        foreach (var component in Components)
            component.Initialize();
    }

    public virtual void LoadContent() { }
    public virtual void UnloadContent() 
    {
        OnContentUnloaded?.Invoke();
    }
    public virtual void Update(GameTime gameTime)
    {
        foreach (var component in Components)
            if (component is IUpdateable updateable && updateable.Enabled)
                updateable.Update(gameTime);
    }


    public virtual bool BeginDraw() => true;

    public virtual void EndDraw() { }
    public virtual void Draw(GameTime gameTime) { }
    void IMonoGameViewModel.Draw(GameTime gameTime)
    {
        if (BeginDraw())
        {
            foreach (var component in Components)
                if (component is IDrawable drawable && drawable.Visible)
                    drawable.Draw(gameTime);
            Draw(gameTime);
            EndDraw();
        }
    }
    public virtual void AfterRender() { }
    public virtual void OnActivated(object sender, EventArgs args) { }
    public virtual void OnDeactivated(object sender, EventArgs args) { }
    public virtual void OnExiting(object sender, EventArgs args) { }
    public virtual void OnMouseDown(MouseStateArgs mouseState) { }
    public virtual void OnMouseMove(MouseStateArgs mouseState) { }
    public virtual void OnMouseUp(MouseStateArgs mouseState) { }
    public virtual void OnMouseWheel(MouseStateArgs args, int delta) { }
    public virtual void OnDrop(DragStateArgs dragState) { }
    public virtual void SizeChanged(object sender, SizeChangedEventArgs args) { }

    public virtual void KeyDownHandler(object sender, KeyEventArgs e) 
    {
    
    }

    public void EnableDebugging()
    {
        IsDebugging = true;
    }
}
