using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Runtime.InteropServices;
namespace ConsoleApplication1
{
    class Program
    {
        [DllImport(“kernel32.dll”)]
        public static extern void GlobalMemoryStatusEx
        (ref MEMORYSTATUSEX hafıza);
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct MEMORYSTATUSEX
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;
        }
        static int secim = 0;
        static int sekil = 0;
        static int sekilalancevre = 0;
        static void Main(string[] args)
        {

        secenekler:
            Console.WriteLine(“Sistem Özellikleri(1)”);
            Console.WriteLine(“Geometrik Şekillerle Alan ve Çevre Hesaplama(2)”);
            Console.Write(“Lütfen Bir Değer Giriniz(1 / 2) : “);
            secim = Int32.Parse(Console.ReadLine());
            if (secim >= 1 && secim <= 2)
            {
                switch (secim)
                {
                    case 1:
                        pcozellikleri();
                        break;
                    case 2:
                        sekiller();
                        break;
                }
            }
            else
            {
                Console.WriteLine(“Yanlış Cevap Girdiniz!!”);
                goto secenekler;
            }
            Console.ReadKey();
        }
        static void pcozellikleri()
        {
            Console.WriteLine(“Bilgisayarınızla ilgili temel bilgileri görüntüleyin”);
            Console.WriteLine();
            Console.WriteLine(“—Windows Sürümü—————————-“);

            ManagementObjectSearcher search = new ManagementObjectSearcher(“SELECT * FROM Win32_OperatingSystem”);

            foreach (ManagementObject share in search.Get())
            {
                string surum;
                string[] surum1;
                surum = share[“name”].ToString();
                surum1 = surum.Split(‘|’);
                Console.WriteLine(surum1[0].Substring(10));
            }

            OperatingSystem OS = Environment.OSVersion;
            string sistem = OS.ToString();

            if (sistem.IndexOf(“Service Pack 1”) != -1)
            {
                Console.WriteLine(“Service Pack 1”);
            }

            Console.WriteLine(“———————————————“);
            Console.WriteLine();
            Console.WriteLine(“—İşlemci———————————–“);
            ManagementObjectSearcher mos = new ManagementObjectSearcher(“root\\CIMV2”, “SELECT * FROM Win32_Processor”);
            foreach (ManagementObject mo in mos.Get())
            {
                Console.WriteLine(“İşlemci: ” +mo[“Name”].ToString());
            }

            MEMORYSTATUSEX hafıza = new MEMORYSTATUSEX();
            hafıza.dwLength = 64;

            GlobalMemoryStatusEx(ref hafıza);
            double ram = (hafıza.ullTotalPhys / (1024 * 1024));
            string ram1 = ram.ToString();
            if (ram1.Length == 4)
            {
                Console.WriteLine(“Yüklü Bellek(Ram): ” +ram1.Substring(0, 1) + “, 00 GB”);
            }
            else if (ram1.Length == 5)
            {
                Console.WriteLine(“Yüklü Bellek(Ram): ” +ram1.Substring(0, 2) + “, 00 GB”);
            }
            else
            {
                Console.WriteLine(“Yüklü Bellek(Ram): ” +ram1.Substring(0, 3) + ” MB”);
            }

            if (Environment.Is64BitOperatingSystem) Console.WriteLine(“64 bit İşletim Sistemi”);
            else Console.WriteLine(“32 bit İşletim Sistemi”);
            Console.WriteLine(“———————————————“);
            Console.WriteLine();
            Console.WriteLine(“—Bilgisayar adı—————————-“);
            Console.WriteLine(“Bilgisayar Adı:” +Environment.MachineName);
            Console.WriteLine(“Tam Bilgisayar Adı:” +Environment.MachineName);
            Console.WriteLine(“———————————————“);
        }
        static void sekiller()
        {
        sekiller:
            Console.WriteLine();
            Console.WriteLine(“—Geometrik Şekiller—————-“);
            Console.WriteLine(“Kare(1)”);
            Console.WriteLine(“Dikdörtgen(2)”);
            Console.WriteLine(“Daire(3)”);
            Console.WriteLine(“Dik Üçgen(4)”);
            Console.Write(“Hesaplamak İstediğiniz Şekli Seçiniz: “);
            sekil = Int32.Parse(Console.ReadLine());
            if (sekil >= 1 && sekil <= 4)
            {
            hesaplama:
                Console.WriteLine();
                Console.WriteLine(“——————————-“);
                Console.WriteLine(“Alan Hesaplama(1)”);
                Console.WriteLine(“Çevre Hesaplama(2)”);
                Console.Write(“Lütfen Bir Değer Giriniz(1 / 2) : “);
                sekilalancevre = Int32.Parse(Console.ReadLine());
                Console.WriteLine();
                if (sekilalancevre >= 1 && sekilalancevre <= 2)
                    hesapla(sekil, sekilalancevre);
                else
                {
                    Console.WriteLine(“Yanlış Cevap Girdiniz!!”);
                    goto hesaplama;
                }

            }
            else
            {
                Console.WriteLine(“Yanlış Cevap Girdiniz!!”);
                goto sekiller;
            }
        }
        static void hesapla(int sekil, int tur)
        {
            if (sekil == 1)
            {
                int kenar_uzunluk = 0, alan, cevre;
                Console.Write(“Bir Kenar Uzunluğu Giriniz: “);
                kenar_uzunluk = Int32.Parse(Console.ReadLine());
                alan = kenar_uzunluk * kenar_uzunluk;
                cevre = 4 * kenar_uzunluk;
                Console.WriteLine();
                if (tur == 1)
                {
                    Console.WriteLine(“Karenin Alanı: ” +alan.ToString());
                }
                else if (tur == 2)
                {
                    Console.WriteLine(“Karenin Çevresi: ” +cevre.ToString());
                }
            }
            else if (sekil == 2)
            {
                int kisa, uzun, alan, cevre;
                Console.Write(“Kısa Kenar Giriniz: “);
                kisa = Int32.Parse(Console.ReadLine());
                Console.Write(“Uzun Kenar Giriniz: “);
                uzun = Int32.Parse(Console.ReadLine());
                alan = kisa * uzun;
                cevre = 2 * (kisa + uzun);
                Console.WriteLine();
                if (tur == 1)
                {
                    Console.Write(“Dikdörtgenin Alanı: ” +alan.ToString());
                }
                else if (tur == 2)
                {
                    Console.Write(“Dikdörtgenin Çevresi: ” +cevre.ToString());
                }
            }
            else if (sekil == 3)
            {
                int r, alan, cevre;
                Console.Write(“Dairenin Yarı Çapını Giriniz: “);
                r = Int32.Parse(Console.ReadLine());
                alan = 3 * r * r;
                cevre = 2 * 3 * r;
                Console.WriteLine();
                if (tur == 1)
                {
                    Console.Write(“Dairenin Alanı: ” +alan.ToString());
                }
                else if (tur == 2)
                {
                    Console.Write(“Dairenin Çevresi: ” + cevre.ToString());
}
}
else if (sekil == 4)
{
double kisa, uzun, alan, cevre, yan;
Console.Write(“1.Dik Kenarı Giriniz : “);
kisa = Int32.Parse(Console.ReadLine());
Console.Write(“2.Dik Kenarı Giriniz : “);
uzun = Int32.Parse(Console.ReadLine());
alan = (kisa * uzun) / 2;
yan = kisa * kisa + uzun * uzun;
yan = Math.Sqrt(yan);
cevre = kisa + uzun + yan;
Console.WriteLine();
if (tur == 1)
{
Console.Write(“Dik Üçgenin Alanı : ” + alan.ToString());
}
else if (tur == 2)
{
Console.Write(“Dik Üçgenin Çevresi : ” + cevre.ToString());
}
Console.Read();
}
}

}
}
