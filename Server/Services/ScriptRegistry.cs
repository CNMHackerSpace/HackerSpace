// Copyright (c) 2025. All rights reserved.

using Microsoft.AspNetCore.Components;

namespace Server.Services
{
    /// <summary>
    /// Registry for managing render fragments and notifying subscribers of changes.
    /// </summary>
    public class ScriptRegistry
    {
        private readonly Dictionary<Guid, RenderFragment> fragments = [];

        /// <summary>
        /// Occurs when a fragment is registered or unregistered.
        /// </summary>
        public event Action? OnChanged;

        /// <summary>
        /// Registers a render fragment and returns its unique identifier.
        /// </summary>
        /// <param name="fragment">The render fragment to register.</param>
        /// <returns>A unique identifier for the registered fragment.</returns>
        public Guid Register(RenderFragment fragment)
        {
            var id = Guid.NewGuid();
            this.fragments[id] = fragment;
            this.OnChanged?.Invoke();
            return id;
        }

        /// <summary>
        /// Unregisters a render fragment by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the fragment to unregister.</param>
        public void Unregister(Guid id)
        {
            if (this.fragments.Remove(id))
            {
                this.OnChanged?.Invoke();
            }
        }

        /// <summary>
        /// Gets all registered render fragments.
        /// </summary>
        /// <returns>A read-only collection of registered fragments.</returns>
        public IReadOnlyCollection<RenderFragment> GetFragments() => this.fragments.Values;
    }
}
