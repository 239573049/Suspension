namespace Gotrays.Contract.Services;

public interface IWindowService
{
    void Minimize();
    
    void Maximize();

    bool IsMaximized();

    void Close(bool allWindow = false);
}