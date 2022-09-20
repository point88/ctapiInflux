using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Net.Http;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace influx {
    class Program {
        [DllImport("tagReader.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void tag_read(StringBuilder value, int len);

        [DllImport("tagReader.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void tag_init();

        [DllImport("tagReader.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void tag_close();

        static void Main() {

            StringBuilder sb = new StringBuilder(20);
            Process influx_client = new Process();
            
            tag_init();

            while (true) { 
                tag_read(sb, sb.Capacity);
                Console.WriteLine("Writing Data... :" + sb.ToString());
                influx_client.StartInfo.FileName = "influx.exe";
                influx_client.StartInfo.Arguments = "write --token GqjtwNedHMo2Dfr54hkx26o9w9G2uq8a-e_ymTmCXIMGswXc-Uzstc1QlIG8YFIcKl8mnHzxIgU5zSC01OjT-A== -b trend -o mhysystem -p s \"m,device=BLD_W power=" + sb.ToString() +"\"";
                influx_client.StartInfo.CreateNoWindow = true;
                influx_client.StartInfo.RedirectStandardOutput = true;
                influx_client.StartInfo.UseShellExecute = false;
                influx_client.Start();
                influx_client.WaitForExit();

                System.Threading.Thread.Sleep(60000);
            }
            
            Console.WriteLine("Successfully!");
            tag_close();
            
        }
    }
}
