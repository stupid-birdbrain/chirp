using MiniaudioSharp;
using System.Text;
using static MiniaudioSharp.Miniaudio;

namespace Sample;

internal sealed unsafe class App {
    public void Run(string[] args) {
        var config = ma_device_config_init(ma_device_type.ma_device_type_playback);
        config.playback.format = ma_format.ma_format_f32;
        config.playback.channels = 2;
        config.sampleRate = 48000;

        ma_device device;
        ma_engine engine;

        var result = ma_device_init(null, &config, &device);
        
        if(result != ma_result.MA_SUCCESS) {
            throw new Exception("failed to init maudio device");
        }
        
        result = ma_engine_init(null, &engine);
        if(result != ma_result.MA_SUCCESS) {
            throw new Exception("failed to init maudio engine");
        }
        
        ma_device_start(&device);

        var bytes = Encoding.UTF8.GetBytes("sound.wav" + '\0');
        fixed (byte* p = bytes) {
            ma_engine_play_sound(&engine, (sbyte*)p, null);
        }
        
        Console.WriteLine("playing sound... press any key to exit");
        Console.ReadKey();
        
        ma_device_uninit(&device);
        ma_engine_uninit(&engine);
    }
}