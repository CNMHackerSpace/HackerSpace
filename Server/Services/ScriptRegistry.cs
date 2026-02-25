using Microsoft.AspNetCore.Components;

public class ScriptRegistry
{
    private readonly Dictionary<Guid, RenderFragment> _fragments = new();
    public event Action? OnChanged;

    public Guid Register(RenderFragment fragment)
    {
        var id = Guid.NewGuid();
        _fragments[id] = fragment;
        OnChanged?.Invoke();
        return id;
    }

    public void Unregister(Guid id)
    {
        if (_fragments.Remove(id))
        {
            OnChanged?.Invoke();
        }
    }

    public IReadOnlyCollection<RenderFragment> GetFragments() => _fragments.Values;
}
