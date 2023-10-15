public interface ICubeObserver
{
    public void UpdateCube(CubeNumber cube) { }
}


public interface ICubeObservable
{
    public void AddObserver(ICubeObserver observer) {}

    private void NotifyObservers() {}
}
