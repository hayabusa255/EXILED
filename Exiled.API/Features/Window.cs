// -----------------------------------------------------------------------
// <copyright file="Window.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.API.Features
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// A wrapper class for <see cref="BreakableWindow"/>.
    /// </summary>
    public class Window
    {
        /// <summary>
        /// A <see cref="List{T}"/> of <see cref="Window"/> on the map.
        /// </summary>
        internal static readonly List<Window> Windowvalue = new List<Window>(20);
        private static readonly Dictionary<BreakableWindow, Window> BreakableWindowToWindow = new Dictionary<BreakableWindow, Window>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Window"/> class.
        /// </summary>
        /// <param name="window">The base <see cref="BreakableWindow"/> for this door.</param>
        public Window(BreakableWindow window)
        {
            BreakableWindowToWindow.Add(window, this);
            Base = window;
            Room = window.GetComponentInParent<Room>();
        }

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/> of <see cref="Door"/> which contains all the <see cref="Door"/> instances.
        /// </summary>
        public static IEnumerable<Window> List => Windowvalue.AsReadOnly();

        /// <summary>
        /// Gets a <see cref="List{T}"/> of <see cref="Door"/> which contains all the <see cref="Door"/> instances.
        /// </summary>
        /// <summary>
        /// Gets the base-game <see cref="BreakableWindow"/> for this window.
        /// </summary>
        public BreakableWindow Base { get; }

        /// <summary>
        /// Gets the <see cref="UnityEngine.GameObject"/> of the window.
        /// </summary>
        public GameObject GameObject => Base.gameObject;

        /// <summary>
        /// Gets the <see cref="Room"/>.
        /// </summary>
        public Room Room { get; }

        /// <summary>
        /// Gets or sets the window's position.
        /// </summary>
        public Vector3 Position
        {
            get => GameObject.transform.position;
            set => GameObject.transform.position = value;
        }

        /// <summary>
        /// Gets a value indicating whether or not this window is breakable.
        /// </summary>
        public bool IsBreakable => !Base.isBroken;

        /// <summary>
        /// Gets or sets a value indicating whether or not this window is broken.
        /// </summary>
        public bool IsBroken
        {
            get => Base.isBroken;
            set => Base.isBroken = value;
        }

        /// <summary>
        /// Gets or sets if the window's remaining health. No effect if the window cannot be broken.
        /// </summary>
        public float Health
        {
            get => Base.health;
            set => Base.health = value;
        }

        /// <summary>
        /// Gets or sets the window's rotation.
        /// </summary>
        public Quaternion Rotation
        {
            get => GameObject.transform.rotation;
            set => GameObject.transform.rotation = value;
        }

        /// <summary>
        /// Gets the window object associated with a specific <see cref="Window"/>, or creates a new one if there isn't one.
        /// </summary>
        /// <param name="breakableWindow">The base-game <see cref="Window"/>.</param>
        /// <returns>A <see cref="Door"/> wrapper object.</returns>
        public static Window Get(BreakableWindow breakableWindow) => BreakableWindowToWindow.ContainsKey(breakableWindow)
            ? BreakableWindowToWindow[breakableWindow]
            : new Window(breakableWindow);

        /// <summary>
        /// Damages the window.
        /// </summary>
        /// <param name="amount">The amount of damage to deal.</param>
        public void DamageDoor(float amount) => Base.ServerDamageWindow(amount);
    }
}
