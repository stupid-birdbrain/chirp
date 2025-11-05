using Miniaudio;
using System.Runtime.InteropServices;

namespace Sample;

internal sealed unsafe class App {
    public void Run(string[] args) {
        ma_engine* engine = (ma_engine*)NativeMemory.Alloc((nuint)sizeof(ma_engine));
        ma.engine_init(null, engine);

        string filePath2 = "sound.wav";
        ma_sound* sound2 = (ma_sound*)NativeMemory.Alloc((nuint)sizeof(ma_sound));
        fixed (void* p = filePath2) {
            ma.sound_init_from_file_w(engine, (ushort*)p, (uint)ma_sound_flags.MA_SOUND_FLAG_LOOPING, null, null, sound2);
        }
        ma.sound_start(sound2);

        Console.ReadKey();
        ma.sound_stop(sound2);

        ma.engine_uninit(engine);
        NativeMemory.Free(engine);
        NativeMemory.Free(sound2);
    }
}