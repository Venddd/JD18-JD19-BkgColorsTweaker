using System.Collections;
using System.Drawing;
using System.IO;
using System.IO.Enumeration;
using System.Reflection.PortableExecutable;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace JdBkgTapeConsole18
{
    class ConsoleApp
    {
        public static class ColorVariables
        {
            public static float color1red;
            public static float color1green;
            public static float color1blue;

            public static float color2red;
            public static float color2green;
            public static float color2blue;

            public static float color3red;
            public static float color3green;
            public static float color3blue;

            public static float color4red;
            public static float color4green;
            public static float color4blue;

            public static float colorslicesred;
            public static float colorslicesgreen;
            public static float colorslicesblue;

            public static bool iscustomslicestransparency;
            public static float customslicestransparency;
        }

        public static class HexVariables
        {
            public static string intersection = "00000004B791419100000008";
            public static byte[] instersectionarray = Convert.FromHexString(intersection);

            public static string defaultslicestranparency = "3DF0F0F2";
        }

        static void Main()
        {
            Awake();
            GetColorRGB();
        }

        static void Awake()
        {
            DirectoryInfo tape = Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"tape");
            DirectoryInfo msh = Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"msh");
            //DirectoryInfo isc = Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"isc");
        }
        static void GetColorRGB()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Vend's flopped JD2018 bkg tape generator");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Please enter colors in rgb format!");

            Info(1);
            Console.WriteLine("Red");
            ColorVariables.color1red = (float) Convert.ToDouble(Console.ReadLine()) / 255;
            Console.WriteLine("Green");
            ColorVariables.color1green = (float) Convert.ToDouble(Console.ReadLine()) / 255;
            Console.WriteLine("Blue");
            ColorVariables.color1blue = (float) Convert.ToDouble(Console.ReadLine()) / 255;

            Info(2);
            Console.WriteLine("Red");
            ColorVariables.color2red = (float)Convert.ToDouble(Console.ReadLine()) / 255;
            Console.WriteLine("Green");
            ColorVariables.color2green = (float)Convert.ToDouble(Console.ReadLine()) / 255;
            Console.WriteLine("Blue");
            ColorVariables.color2blue = (float)Convert.ToDouble(Console.ReadLine()) / 255;

            Info(3);
            Console.WriteLine("Red");
            ColorVariables.color3red = (float)Convert.ToDouble(Console.ReadLine()) / 255;
            Console.WriteLine("Green");
            ColorVariables.color3green = (float)Convert.ToDouble(Console.ReadLine()) / 255;
            Console.WriteLine("Blue");
            ColorVariables.color3blue = (float)Convert.ToDouble(Console.ReadLine()) / 255;

            Info(4);
            Console.WriteLine("Red");
            ColorVariables.color4red = (float)Convert.ToDouble(Console.ReadLine()) / 255;
            Console.WriteLine("Green");
            ColorVariables.color4green = (float)Convert.ToDouble(Console.ReadLine()) / 255;
            Console.WriteLine("Blue");
            ColorVariables.color4blue = (float)Convert.ToDouble(Console.ReadLine()) / 255;

            Info(5);
            Console.WriteLine("Red");
            ColorVariables.colorslicesred = (float)Convert.ToDouble(Console.ReadLine()) / 255;
            Console.WriteLine("Green");
            ColorVariables.colorslicesgreen = (float)Convert.ToDouble(Console.ReadLine()) / 255;
            Console.WriteLine("Blue");
            ColorVariables.colorslicesblue = (float)Convert.ToDouble(Console.ReadLine()) / 255;


            genStandartTape();
        }

        static void Info(short TextID)
        {
            Console.WriteLine(" ");
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            
            switch (TextID)
            {
                case 1:
                    Console.WriteLine("[i] Enter main color.");
                    break;
                case 2:
                    Console.WriteLine("[i]  Enter a gradient in a right bottom corner color.");
                    break;
                case 3:
                    Console.WriteLine("[i] Enter a gradient in a top left corner color.");
                    break;
                case 4:
                    Console.WriteLine("[i] Still enter color for smth tho i dont really know what it is");
                    break;
                case 5: 
                    Console.WriteLine("[i] Enter color for slices (oblique lines in bkg)");
                    break;
                case 6:
                    Console.WriteLine("Each generated folder has instructions on where to put the files.");
                    break;
                case 7:
                    Console.WriteLine("[i] Enter transparency for slices (oblique lines in bkg)");
                    break;
                case 8:
                    Console.WriteLine("[?] Do you want to set custom slices(oblique lines in bkg) transparency?");
                    Console.WriteLine("[i] For example if you want to make them more visible on darker colors.");
                    Console.WriteLine("[i] Enter 'Y' or 'N'.");
                    break;
                default:
                    Console.WriteLine("info");
                    break;
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" ");

            return;
        }

        static void InfoSuccess(string filename, bool isdone)
        {

            Console.WriteLine(" ");
            Console.BackgroundColor = ConsoleColor.DarkGreen;

            if (isdone == false)
            {
                Console.WriteLine(filename + " is Done!");
            }
            else
                Console.WriteLine(filename);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" ");

            return;
        }
        static void genStandartTape()
        {
            WriteHeader();
            static void WriteHeader()
            {
                string header = "000000010000065A9E8454600000009C0000000EC6FED58E0000004C7A9623330F8B1CA5000000010000000000000060000000000000000100000000000000020000000000000004B791419100000008";
                byte[] headerarray = Convert.FromHexString(header);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\standard.tape.ckd", FileMode.Create))
                {
                    using (var write = new BinaryWriter(stream))
                    {
                        write.Write(headerarray);
                    }
                }

                WriteColor3();
            }
            static void WriteColor3()
            {

                byte[] color3redarray = BitConverter.GetBytes(ColorVariables.color3red);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color3redarray);

                byte[] color3greenarray = BitConverter.GetBytes(ColorVariables.color3green);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color3greenarray);

                byte[] color3bluearray = BitConverter.GetBytes(ColorVariables.color3blue);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color3bluearray);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\standard.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream, System.Text.Encoding.BigEndianUnicode, false))
                    {
                        write.Write(color3redarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color3greenarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color3bluearray);
                    }
                }
                WriteMiddle1();
            }

            static void WriteMiddle1()
            {
                string middle = "E68412CA00000044192BE25F4F624839000000010000000000000060000000000000000100000000000000020000000000000004B7914191000000083F33822CC6FED58E0000004CC2C2E28A7E91DEE7000000010000000000000060000000000000000100000000000000030000000000000004B791419100000008";
                byte[] middlearray = Convert.FromHexString(middle);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\standard.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream))
                    {
                        write.Write(middlearray);
                    }
                }

                WriteColor4();
            }
            static void WriteColor4()
            {

                byte[] color4redarray = BitConverter.GetBytes(ColorVariables.color4red);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color4redarray);

                byte[] color4greenarray = BitConverter.GetBytes(ColorVariables.color4green);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color4greenarray);

                byte[] color4bluearray = BitConverter.GetBytes(ColorVariables.color4blue);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color4bluearray);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\standard.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream, System.Text.Encoding.BigEndianUnicode, false))
                    {
                        write.Write(color4redarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color4greenarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color4bluearray);
                    }
                }
                WriteMiddle2();
            }
            static void WriteMiddle2()
            {
                string middle = "E68412CA00000044BB9BBC838E24C85F000000010000000000000060000000000000000100000000000000030000000000000004B7914191000000080000000036A312DC00000044C31E0C3BF37030E700000001000000000000006000000000000000010000000100000004FFFFFFFF00000004B7914191000000080000000000000004FFFFFFFFC6FED58E0000004C345F2C450C39B921000000010000000000000060000000000000000100000000000000000000000000000004B791419100000008";
                byte[] middlearray = Convert.FromHexString(middle);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\standard.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream))
                    {
                        write.Write(middlearray);
                    }
                }

                WriteColor1();
            }
            static void WriteColor1()
            {

                byte[] color1redarray = BitConverter.GetBytes(ColorVariables.color1red);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color1redarray);

                byte[] color1greenarray = BitConverter.GetBytes(ColorVariables.color1green);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color1greenarray);

                byte[] color1bluearray = BitConverter.GetBytes(ColorVariables.color1blue);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color1bluearray);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\standard.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream, System.Text.Encoding.BigEndianUnicode, false))
                    {
                        write.Write(color1redarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color1greenarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color1bluearray);
                    }
                }
                WriteMiddle3();
            }
            static void WriteMiddle3()
            {
                string middle = "C6FED58E0000004C0E769AC1B825CE73000000010000000000000060000000000000000100000000000000010000000000000004B791419100000008";
                byte[] middlearray = Convert.FromHexString(middle);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\standard.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream))
                    {
                        write.Write(middlearray);
                    }
                }

                WriteColor2();
            }
            static void WriteColor2()
            {

                byte[] color2redarray = BitConverter.GetBytes(ColorVariables.color2red);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color2redarray);

                byte[] color2greenarray = BitConverter.GetBytes(ColorVariables.color2green);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color2greenarray);

                byte[] color2bluearray = BitConverter.GetBytes(ColorVariables.color2blue);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color2bluearray);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\standard.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream, System.Text.Encoding.BigEndianUnicode, false))
                    {
                        write.Write(color2redarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color2greenarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color2bluearray);
                    }
                }
                WriteEnding();
            }

            static void WriteEnding()
            {
                string ending = "E68412CA000000444F286473D018BBCE000000010000000000000060000000000000000100000000000000010000000000000004B7914191000000083F071811E68412CA00000044C831551292CC7A47000000010000000000000018000000000000000100000002000000000000000000000004B7914191000000083DF0F0E9E68412CA000000446AF6DCAE92CC7A470000000100000018000000300000000000000001000000020000000000000000000000044DE6D87100000024000000003F34B4AF000000003E0CA106424000003DF0F0E9419C96983DFD95DDE68412CA00000044EACB481D92CC7A470000000100000048000000180000000000000001000000020000000000000000000000044DE6D87100000024000000003EC8C8CD000000003E17EBAF41C000003DF0F0E9411C96983DFD95DDE68412CA000000449804897C32EC5674000000010000000000000060000000000000000100000002000000010000000000000004B7914191000000083F800000E68412CA000000448E566A8FBE74F854000000010000000000000060000000000000000100000002000000020000000000000004B7914191000000083F800000E68412CA000000441EF981A993E1CF34000000010000000000000060000000000000000100000002000000030000000000000004B7914191000000083D20A0F5000000000000000000000003000000240000000000000003626B67000000000000002400000000000000062873656C66290000000000000024000000000000000A626B675F736C69636573000000000000000600000001000000000000000B776964676574735F636F6D";
                byte[] endingarray = Convert.FromHexString(ending);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\standard.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream))
                    {
                        write.Write(endingarray);
                    }
                }

                InfoSuccess("Standard.Tape", false);
                genLoadingTape();
            }
        }

        static void genLoadingTape()
        {
            WriteHeader();
            static void WriteHeader()
            {
                string header = "000000010000078E9E8454600000009C00000011C6FED58E0000004C65B84B250F8B1CA50000000100000000000000D7000000000000000100000000000000020000000000000004B791419100000008";
                byte[] headerarray = Convert.FromHexString(header);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\loading.tape.ckd", FileMode.Create))
                {
                    using (var write = new BinaryWriter(stream))
                    {
                        write.Write(headerarray);
                    }
                }

                WriteColor3();
            }
            static void WriteColor3()
            {

                byte[] color3redarray = BitConverter.GetBytes(ColorVariables.color3red);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color3redarray);

                byte[] color3greenarray = BitConverter.GetBytes(ColorVariables.color3green);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color3greenarray);

                byte[] color3bluearray = BitConverter.GetBytes(ColorVariables.color3blue);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color3bluearray);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\loading.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream, System.Text.Encoding.BigEndianUnicode, false))
                    {
                        write.Write(color3redarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color3greenarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color3bluearray);
                    }
                }
                WriteMiddle1();
            }

            static void WriteMiddle1()
            {
                string middle = "E68412CA0000004432D311774F6248390000000100000000000000D7000000000000000100000000000000020000000000000004B7914191000000083F33822CC6FED58E0000004CF108FD407E91DEE70000000100000000000000D7000000000000000100000000000000030000000000000004B791419100000008";
                byte[] middlearray = Convert.FromHexString(middle);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\loading.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream))
                    {
                        write.Write(middlearray);
                    }
                }

                WriteColor4();
            }
            static void WriteColor4()
            {

                byte[] color4redarray = BitConverter.GetBytes(ColorVariables.color4red);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color4redarray);

                byte[] color4greenarray = BitConverter.GetBytes(ColorVariables.color4green);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color4greenarray);

                byte[] color4bluearray = BitConverter.GetBytes(ColorVariables.color4blue);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color4bluearray);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\loading.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream, System.Text.Encoding.BigEndianUnicode, false))
                    {
                        write.Write(color4redarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color4greenarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color4bluearray);
                    }
                }
                WriteMiddle2();
            }
            static void WriteMiddle2()
            {
                string middle = "E68412CA00000044168D907B8E24C85F0000000100000000000000D7000000000000000100000000000000030000000000000004B7914191000000080000000036A312DC00000044A2326A13F37030E70000000100000000000000D700000000000000010000000100000004FFFFFFFF00000004B7914191000000080000000000000004FFFFFFFFC6FED58E0000004CE26736800C39B9210000000100000000000000D7000000000000000100000000000000000000000000000004B791419100000008";
                byte[] middlearray = Convert.FromHexString(middle);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\loading.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream))
                    {
                        write.Write(middlearray);
                    }
                }

                WriteColor1();
            }
            static void WriteColor1()
            {

                byte[] color1redarray = BitConverter.GetBytes(ColorVariables.color1red);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color1redarray);

                byte[] color1greenarray = BitConverter.GetBytes(ColorVariables.color1green);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color1greenarray);

                byte[] color1bluearray = BitConverter.GetBytes(ColorVariables.color1blue);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color1bluearray);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\loading.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream, System.Text.Encoding.BigEndianUnicode, false))
                    {
                        write.Write(color1redarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color1greenarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color1bluearray);
                    }
                }
                WriteMiddle3();
            }
            static void WriteMiddle3()
            {
                string middle = "C6FED58E0000004C36328CA7B825CE730000000100000000000000D7000000000000000100000000000000010000000000000004B791419100000008";
                byte[] middlearray = Convert.FromHexString(middle);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\\loading.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream))
                    {
                        write.Write(middlearray);
                    }
                }

                WriteColor2();
            }
            static void WriteColor2()
            {

                byte[] color2redarray = BitConverter.GetBytes(ColorVariables.color2red);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color2redarray);

                byte[] color2greenarray = BitConverter.GetBytes(ColorVariables.color2green);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color2greenarray);

                byte[] color2bluearray = BitConverter.GetBytes(ColorVariables.color2blue);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color2bluearray);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\loading.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream, System.Text.Encoding.BigEndianUnicode, false))
                    {
                        write.Write(color2redarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color2greenarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color2bluearray);
                    }
                }
                WriteEnding();
            }

            static void WriteEnding()
            {
                string ending = "E68412CA0000004438BA5083D018BBCE0000000100000000000000D7000000000000000100000000000000010000000000000004B7914191000000083F071811E68412CA00000044A06BAA1F281DC796000000010000000000000018000000000000000100000002000000000000000000000004B7914191000000083DF0F0E9E68412CA000000448EB0BCBC281DC7960000000100000018000000300000000000000001000000020000000000000000000000044DE6D87100000024000000003F34B4AF000000003E0CA106424000003DF0F0E9419C96983DFD95DDE68412CA0000004438CD08C9281DC7960000000100000048000000180000000000000001000000020000000000000000000000044DE6D87100000024000000003EC8C8CD000000003E17EBAF41C000003DF0F0E9411C96983DFD95DDE68412CA000000448A84A319281DC796000000010000006000000018000000000000000100000002000000000000000000000004B7914191000000083DF0F0E9E68412CA000000443B8D04B9281DC7960000000100000078000000300000000000000001000000020000000000000000000000044DE6D87100000024000000003F34B4AF000000003E0CA106424000003DF0F0E9419C96983DFD95DDE68412CA000000445738C15B281DC79600000001000000A8000000180000000000000001000000020000000000000000000000044DE6D87100000024000000003EC8C8CD000000003E17EBAF41C000003DF0F0E9411C96983DFD95DDE68412CA000000449D1189E5526FAE250000000100000000000000C0000000000000000100000002000000030000000000000004B7914191000000083D20A0F5E68412CA00000044CFCB7082CE026CF10000000100000000000000C0000000000000000100000002000000020000000000000004B7914191000000083F800000E68412CA00000044C00CC68E8B9F7D730000000100000000000000C0000000000000000100000002000000010000000000000004B7914191000000083F800000000000000000000000000003000000240000000000000003626B67000000000000002400000000000000062873656C66290000000000000024000000000000000A626B675F736C69636573000000000000000600000001000000000000000B776964676574735F636F6D";
                byte[] endingarray = Convert.FromHexString(ending);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\loading.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream))
                    {
                        write.Write(endingarray);
                    }
                }

                InfoSuccess("Loading.Tape", false);
                genIdleTape();
            }
        }

        static void genIdleTape()
        {
            WriteHeader();
            static void WriteHeader()
            {
                string header = "000000010000065A9E8454600000009C0000000EC6FED58E0000004C35D4603E410460F7000000010000000000000060000000000000000100000000000000020000000000000004B791419100000008";
                byte[] headerarray = Convert.FromHexString(header);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\idle.tape.ckd", FileMode.Create))
                {
                    using (var write = new BinaryWriter(stream))
                    {
                        write.Write(headerarray);
                    }
                }

                WriteColor3();
            }
            static void WriteColor3()
            {

                byte[] color3redarray = BitConverter.GetBytes(ColorVariables.color3red);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color3redarray);

                byte[] color3greenarray = BitConverter.GetBytes(ColorVariables.color3green);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color3greenarray);

                byte[] color3bluearray = BitConverter.GetBytes(ColorVariables.color3blue);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color3bluearray);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\idle.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream, System.Text.Encoding.BigEndianUnicode, false))
                    {
                        write.Write(color3redarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color3greenarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color3bluearray);
                    }
                }
                WriteMiddle1();
            }

            static void WriteMiddle1()
            {
                string middle = "C6FED58E0000004C44A8DEA2B5C940E6000000010000000000000060000000000000000100000000000000030000000000000004B791419100000008";
                byte[] middlearray = Convert.FromHexString(middle);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\idle.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream))
                    {
                        write.Write(middlearray);
                    }
                }

                WriteColor4();
            }
            static void WriteColor4()
            {

                byte[] color4redarray = BitConverter.GetBytes(ColorVariables.color4red);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color4redarray);

                byte[] color4greenarray = BitConverter.GetBytes(ColorVariables.color4green);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color4greenarray);

                byte[] color4bluearray = BitConverter.GetBytes(ColorVariables.color4blue);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color4bluearray);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\idle.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream, System.Text.Encoding.BigEndianUnicode, false))
                    {
                        write.Write(color4redarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color4greenarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color4bluearray);
                    }
                }
                WriteMiddle2();
            }
            static void WriteMiddle2()
            {
                string middle = "C6FED58E0000004CFD52F5E6F37030E7000000010000000000000060000000000000000100000000000000000000000000000004B791419100000008";
                byte[] middlearray = Convert.FromHexString(middle);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\idle.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream))
                    {
                        write.Write(middlearray);
                    }
                }

                WriteColor1();
            }
            static void WriteColor1()
            {

                byte[] color1redarray = BitConverter.GetBytes(ColorVariables.color1red);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color1redarray);

                byte[] color1greenarray = BitConverter.GetBytes(ColorVariables.color1green);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color1greenarray);

                byte[] color1bluearray = BitConverter.GetBytes(ColorVariables.color1blue);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color1bluearray);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\idle.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream, System.Text.Encoding.BigEndianUnicode, false))
                    {
                        write.Write(color1redarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color1greenarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color1bluearray);
                    }
                }
                WriteMiddle3();
            }
            static void WriteMiddle3()
            {
                string middle = "C6FED58E0000004CFFD1797A083EE06A000000010000000000000060000000000000000100000000000000010000000000000004B791419100000008";
                byte[] middlearray = Convert.FromHexString(middle);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\idle.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream))
                    {
                        write.Write(middlearray);
                    }
                }

                WriteColor2();
            }
            static void WriteColor2()
            {

                byte[] color2redarray = BitConverter.GetBytes(ColorVariables.color2red);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color2redarray);

                byte[] color2greenarray = BitConverter.GetBytes(ColorVariables.color2green);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color2greenarray);

                byte[] color2bluearray = BitConverter.GetBytes(ColorVariables.color2blue);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(color2bluearray);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\idle.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream, System.Text.Encoding.BigEndianUnicode, false))
                    {
                        write.Write(color2redarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color2greenarray);
                        write.Write(HexVariables.instersectionarray);
                        write.Write(color2bluearray);
                    }
                }
                WriteEnding();
            }

            static void WriteEnding()
            {
                string ending = "36A312DC000000447A0A85846B5F859B00000001000000000000006000000000000000010000000100000004FFFFFFFF00000004B7914191000000080000000000000004FFFFFFFFE68412CA00000044365B4203B825CE73000000010000000000000060000000000000000100000000000000010000000000000004B7914191000000083F071811E68412CA0000004461BFC05A0F8B1CA5000000010000000000000060000000000000000100000000000000020000000000000004B7914191000000083F33822CE68412CA000000445DDC79587E91DEE7000000010000000000000060000000000000000100000000000000030000000000000004B79141910000000800000000E68412CA000000449B9092B2899CF58E000000010000000000000018000000000000000100000002000000000000000000000004B7914191000000083DF0F0E9E68412CA0000004440C2445E899CF58E0000000100000018000000300000000000000001000000020000000000000000000000044DE6D87100000024000000003F34B4AF000000003E0CA106424000003DF0F0E9419C96983DFD95DDE68412CA00000044BEFA9BEB899CF58E0000000100000048000000180000000000000001000000020000000000000000000000044DE6D87100000024000000003EC8C8CD000000003E17EBAF41C000003DF0F0E9411C96983DFD95DDE68412CA000000446DF9882911643D10000000010000000000000060000000000000000100000002000000010000000000000004B7914191000000083F800000E68412CA00000044EF56F52592CC7A47000000010000000000000060000000000000000100000002000000020000000000000004B7914191000000083F800000E68412CA000000447679AD3432EC5674000000010000000000000060000000000000000100000002000000030000000000000004B7914191000000083D20A0F5000000000000000000000003000000240000000000000003626B67000000000000002400000000000000062873656C66290000000000000024000000000000000A626B675F736C69636573000000000000000600000001000000000000000B776964676574735F636F6D";
                byte[] endingarray = Convert.FromHexString(ending);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"tape\idle.tape.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream))
                    {
                        write.Write(endingarray);
                    }
                }

                InfoSuccess("Idle.Tape", false);
                genSlices1Msh();
            }
        }

        static void genSlices1Msh()
        {
            WriteHeader();
            static void WriteHeader()
            {
                string header = "0000000100000370E6A935E1000001C00000000000000001000000000000000000000000000000800000000000000000000000000000000A0000000000000064000000000000000000000000000000000000000000000000000000000000000000000000000000070000003800000001000000000000000100000001";
                byte[] headerarray = Convert.FromHexString(header);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"msh\bkg_slices_1.msh.ckd", FileMode.Create))
                {
                    using (var write = new BinaryWriter(stream))
                    {
                        write.Write(headerarray);
                    }
                }

                WriteColor1();
            }
            static void WriteColor1()
            {
                byte[] defaultslicesarray = Convert.FromHexString(HexVariables.defaultslicestranparency);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(defaultslicesarray);

                byte[] colorslicesredarray = BitConverter.GetBytes(ColorVariables.colorslicesred);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(colorslicesredarray);

                byte[] colorslicesgreenarray = BitConverter.GetBytes(ColorVariables.colorslicesgreen);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(colorslicesgreenarray);

                byte[] colorslicesbluearray = BitConverter.GetBytes(ColorVariables.colorslicesblue);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(colorslicesbluearray);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"msh\bkg_slices_1.msh.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream, System.Text.Encoding.BigEndianUnicode, false))
                    {
                      
                         write.Write(colorslicesbluearray);
                         write.Write(colorslicesgreenarray);
                         write.Write(colorslicesredarray);
                         write.Write(defaultslicesarray);
                      
                    }
                }
                WriteEnding();
            }

            static void WriteEnding()
            {
                string ending = "000000010000000200000030000000000000000000000001000000003DE1307B3F0000003F000000000000003F8000003F8000003F0000003F00000000000030BC23D70A000000000000000100000000000000003F0000003F000000000000003E8000003F8000003F0000003F0000000000000A00000038000000010000000000000001000000013F8000003F8000003F8000003F800000000000010000000300000030000000000000000000000001000000003DE1307B3F0000003F000000000000003F8000003F8000003F0000003F00000000000030BBA3D70A000000000000000100000000000000003F0000003F000000000000003F8000003F8000003F0000003F0000000000003000000000000000000000000000000000000000003F0000003F000000000000003F8000003F8000003F0000003F0000000000000A00000038000000010000000000000001000000013F8000003F8000003F8000003F800000000000020000000200000030000000000000000000000001000000003DE1307B3F0000003F000000000000003F8000003F8000003F0000003F00000000000030BBE56042000000000000000100000000000000003F0000003F000000000000003F0000003F8000003F0000003F000000000000020000003800000001000000000000000200000001000000003F4CCCCE3F8000003D20A0A1000000030000000200000030BA83126F0000000000000001000000003DE1307B3F0000003F000000000000003E8000003F8000003F0000003F0000000000003000000000000000000000000000000000000000003F0000003F000000000000003F8000003F8000003F0000003F000000";
                byte[] endingarray = Convert.FromHexString(ending);

                using (var stream = System.IO.File.Open(AppDomain.CurrentDomain.BaseDirectory + @"msh\bkg_slices_1.msh.ckd", FileMode.Append))
                {
                    using (var write = new BinaryWriter(stream))
                    {
                        write.Write(endingarray);
                    }
                }

                InfoSuccess("Bkg_slices_1.msh", false);
                Finish();
            }
        }

        static void Finish()
        {
            string pathTape = "Put all files from this folder into bundlelogic_wii.ipk\\cache\\itf_cooked\\wii\\world\\ui\\widgets_com\\grp_bkg_gen\\animations";
            string pathMsh = "Put file from this folder into bundlelogic_wii.ipk\\cache\\itf_cooked\\wii\\world\\ui\\materials";
            string pathIsc = "Put file from this folder into bundlelogic_wii.ipk\\cache\\itf_cooked\\wii\\world\\ui\\screens\\title";

            using (FileStream settings = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"tape\instructions.txt", FileMode.Create))
            {
                byte[] InBinary = System.Text.Encoding.Default.GetBytes(pathTape);
                settings.Write(InBinary);
            }

            using (FileStream settings = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"msh\instructions.txt", FileMode.Create))
            {
                byte[] InBinary = System.Text.Encoding.Default.GetBytes(pathMsh);
                settings.Write(InBinary);
            }

            Info(6);

            InfoSuccess("Finished!", true);

            Console.WriteLine("Exiting in 10 seconds.");

            Console.WriteLine(" ");

            Thread.Sleep(10000);
            Environment.Exit(0);
        }
        
   


    }
}