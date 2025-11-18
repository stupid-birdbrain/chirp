using MiniaudioSharp;

namespace Chirp;

public unsafe struct MaDevice : IDisposable {
    private ma_device* _handle;

    public void Dispose() {
        Miniaudio.ma_device_uninit(_handle);
        _handle = null;
    }
}