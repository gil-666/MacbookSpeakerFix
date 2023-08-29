using System;
using System.Runtime.InteropServices;

public class Program
{
    [DllImport("winmm.dll", EntryPoint = "waveOutGetNumDevs")]
    private static extern int WaveOutGetNumDevs();

    [DllImport("winmm.dll", EntryPoint = "waveOutSetSpeakerConfig")]
    private static extern int WaveOutSetSpeakerConfig(IntPtr hwo, uint dwConfig);

    // Speaker configuration
    private const uint WAVE_FORMAT_2M16 = 0x00000040; // Quadrophonic, 16-bit
    // a don pendejo se le olvido declarar las demas configuraciones
    private const uint WAVE_FORMAT_1M16 = 0x00000001; // Mono16
    private const uint WAVE_FORMAT_1S16 = 0x00000002; // Stereo16

    public static void Main(string[] args)
    {
        Console.WriteLine("Select a speaker configuration:");
        Console.WriteLine("1. Mono");
        Console.WriteLine("2. Stereo");
        Console.WriteLine("3. Quadrophonic");
        // Display other options as needed
        Console.Write("Enter the option number: ");

        if (int.TryParse(Console.ReadLine(), out int option))
        {
            uint speakerConfig = GetSpeakerConfigFromOption(option);

            if (speakerConfig != 0)
            {
                ChangeSpeakerConfiguration(speakerConfig);
            }
            else
            {
                Console.WriteLine("Invalid option selected.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }
    }

    private static uint GetSpeakerConfigFromOption(int option)
    {
        switch (option)
        {
            case 1:
                return WAVE_FORMAT_1M16;
            case 2:
                return WAVE_FORMAT_1S16;
            case 3:
                return WAVE_FORMAT_2M16;
            default:
                return 0; // invalid
        }
    }

    private static void ChangeSpeakerConfiguration(uint speakerConfig)
    {
        try
        {
            int numDevices = WaveOutGetNumDevs();
            if (numDevices > 0)
            {
                IntPtr hwo = IntPtr.Zero;
                int result = WaveOutSetSpeakerConfig(hwo, speakerConfig);

                if (result != 0)
                {
                    Console.WriteLine("Failed to change speaker configuration.");
                }
                else
                {
                    Console.WriteLine("Speaker configuration changed successfully.");
                }
            }
            else
            {
                Console.WriteLine("No audio output devices found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
