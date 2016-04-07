using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace poetionbot
{

  
    class w32
    {
        //Get a list of open processes (optionally by a filter)
        public static Process[] GetProcessList(string filter)
        {
            if (filter == "")
            {
                return Process.GetProcesses();
            }
            return Process.GetProcessesByName(filter);
        }

        //Attach to a process
        const int PROCESS_WM_READ = 0x0010;
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        public static IntPtr AttachProcess(Process process)
        {
            return OpenProcess(PROCESS_WM_READ, false, process.Id);
        }

        //Read process memory
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess,
          int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        public static int ReadProcessMemory(IntPtr handle, int address)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[4];

            var didRead = ReadProcessMemory((int)handle, address, buffer, buffer.Length, ref bytesRead);
            return BitConverter.ToInt32(buffer, 0);            
        }


        public static int ReadProcessMemoryOffset(IntPtr handle, pointerset ps)
        {
            //Start with the base address's value
            var addr = ps.baseAddress; // ReadProcessMemory(handle, ps.baseAddress);
            Console.WriteLine("0:", addr.ToString("X"));
            for (var i = 0; i < ps.offsets.Length; i++) {
                //Get each offset
                var offset = ReadProcessMemory(handle, addr + ps.offsets[i]);
               
                //Console.WriteLine((i+1)+"Offset:"+offset.ToString("X"));
                //Set the offset
                addr = offset;
                //Console.WriteLine((i + 1) + "Addr:" + addr.ToString("X"));
            }
          //  Console.WriteLine("Last addr:"+addr.ToString("X"));
            return addr;
        }

        //Send Input
        public const int INPUT_MOUSE = 0;
        public const int INPUT_KEYBOARD = 1;
        public const int INPUT_HARDWARE = 2;
        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            int dx;
            int dy;
            uint mouseData;
            uint dwFlags;
            uint time;
            IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HARDWAREINPUT
        {
            uint uMsg;
            ushort wParamL;
            ushort wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct INPUT
        {
            [FieldOffset(0)]
            public int type;
            [FieldOffset(4)] //*
            public MOUSEINPUT mi;
            [FieldOffset(4)] //*
            public KEYBDINPUT ki;
            [FieldOffset(4)] //*
            public HARDWAREINPUT hi;
        }

        [DllImport("User32.dll")]
        protected static extern uint SendInput(uint numberOfInputs, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] INPUT[] input, int structSize);
      
        //Send a raw input array
        public static bool SendInput(INPUT[] inputs)
        {
            uint ret = SendInput((uint)inputs.Length, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));
            return (ret == 1);
        }

        //Send keyboard based input, use PressKey for a simpler handler.
        public static bool SendInput(KEYBDINPUT keyboardInput)
        {            
            INPUT[] inputs = new INPUT[1];
            inputs[0].type = INPUT_KEYBOARD;
            inputs[0].ki.dwFlags = keyboardInput.dwFlags;
            inputs[0].ki.wScan = keyboardInput.wScan;
            return SendInput(inputs);
        }

        //Press the given keycode for delayMilliseconds duration, then release.
        public static bool PressKey(UInt16 keyCode, int delayMilliseconds)
        {
            KEYBDINPUT keyboardInput = new KEYBDINPUT();
            keyboardInput.wScan = keyCode;
            keyboardInput.dwFlags = 0x0008; //Press hold key
            keyboardInput.time = 0;
            var keyState = SendInput(keyboardInput);
            if (!keyState) return false;
            //time.sleep(delayMilliseconds);
            keyboardInput.dwFlags = 0x0002 | 0x0008; //release hold key
            return SendInput(keyboardInput);
        }


        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        

    }
}
       