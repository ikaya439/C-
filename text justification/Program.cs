using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
namespace textjustification
{
    class Program
    {
        static ArrayList yeni = new ArrayList();
        static ArrayList uzaklik = new ArrayList();
        static public int  greedYazdir(int[] _satirBaslangici, int _satirUzunlugu)
        {
            int y = 0;
            int boslukKare = 0;
            int kelimeUzunlugu = 0;
            int atlamaSayac = 0;
            int kelimeSayisi = 0;
            int araBosluk = 0;
            Console.WriteLine("Greedy \n");

            while (y >= 0)
            {
                kelimeUzunlugu = 0;
                kelimeSayisi = 0;
                for (int i = y; i < _satirBaslangici[atlamaSayac]; i++)
                {
                    kelimeUzunlugu += uzunlukBul(i);
                    kelimeSayisi++;
                }
                kelimeUzunlugu += kelimeSayisi - 1;
                araBosluk = _satirUzunlugu - kelimeUzunlugu;
                boslukKare += Convert.ToInt32(Math.Pow((_satirUzunlugu - kelimeUzunlugu), 2));
                ArrayList arrayBosluk = new ArrayList();
                for (int i = y; i < _satirBaslangici[atlamaSayac]; i++)
                {
                    arrayBosluk.Add(yeni[i]);
                }
                if (kelimeSayisi == 1)
                {
                    Console.Write(arrayBosluk[0]);
                    Console.WriteLine();
                    y = _satirBaslangici[atlamaSayac];
                    atlamaSayac++;
                    continue;
                }
                kelimeBosluk(arrayBosluk, araBosluk, kelimeSayisi);
                arrayListYazdir(arrayBosluk);
                Console.WriteLine();   
                y = _satirBaslangici[atlamaSayac];
                atlamaSayac++;
                if (y == yeni.Count)
                    break;
            }
            Console.WriteLine();
            return boslukKare; 
        }
        private static void greddyaltsatiragecme(int [,]_badness,int []_atlama)
        {
            int sayyy = 0;
            for (int i = 0; i <yeni.Count; i++)
            {
                for (int j = 0; j < yeni.Count; j++)
                {
                    if (_badness[i, j] == int.MaxValue)
                    {
                        i = j;
                        _atlama[sayyy] = j;
                        sayyy++;


                    }
                }

            }
            _atlama[sayyy] = yeni.Count;
        }
        public static int uzunlukBul(int i)
        {
            string str = yeni[i].ToString();
            return str.Length;
        }
        public static void satirasianelemansayisi(int [,]_uzaklik)
        {
            for (int i = 0; i < yeni.Count; i++)
            {
                int hes = 0;
                for (int j = i; j < yeni.Count; j++)
                {
                    hes = hes + uzunlukBul(j);

                    _uzaklik[i, j] = hes;
                    hes++;
                }
            }
        }
        private static void badnessTablosu(int[,] _uzaklik,int [,]_badness,int satirUzunlugu)
        {
            for (int i = 0; i < yeni.Count; i++)
            {
                for (int j = i; j < yeni.Count; j++)
                {
                    if (satirUzunlugu >= _uzaklik[i, j])
                        _badness[i, j] = (satirUzunlugu - (_uzaklik[i, j])) * (satirUzunlugu - (_uzaklik[i, j]));
                    else
                    {
                        _badness[i, j] = int.MaxValue;
                    }
                }

            }

        }

        public static void kelimeBol(string _line)
        {
            int yeniBaslangıc = 0;
            int sayacHarfSayisi = 0;
            int sayacBaslangıcYeri = 0;
            for (int i = 0; i < _line.Length; i++)
            {
                if (yeniBaslangıc > 0)
                {
                    if (_line[i] != ' ')
                    {
                        yeniBaslangıc = 0;
                    }
                    else
                    {
                        sayacBaslangıcYeri = i + 1;
                        continue;
                    }

                }
                sayacHarfSayisi++;
                if (_line[i] == ' ' || i == _line.Length - 1)
                {
                    yeniBaslangıc++;
                    if (i == _line.Length - 1)
                    {
                        yeni.Add(_line.Substring(sayacBaslangıcYeri, sayacHarfSayisi));
                    }
                    else
                    {
                        yeni.Add(_line.Substring(sayacBaslangıcYeri, sayacHarfSayisi - 1));
                        sayacBaslangıcYeri = i + 1;
                    }
                    sayacHarfSayisi = 0;
                }

            }

        }
      
        static public int minDegerBul(int []_dizi)
        {
            int minDeger = int.MaxValue;
            for (int i = 0; i < _dizi.Length; i++)
            {
                if (_dizi[i] <= 0)
                    continue;
                if (_dizi[i] < minDeger)
                {
                    minDeger = _dizi[i];
                }

            }
            return minDeger;
        }
        static public void tADiziyiDoldur(int [] _ta,int [,]_badness,int baslangicYeri,int []_satirBaslangici)
        {
            int[] yeni2 = new int[yeni.Count - baslangicYeri];
            int yeni2sayac = 0;
            for (int i = baslangicYeri; i < yeni.Count; i++)
            {
                yeni2[yeni2sayac] = _badness[baslangicYeri, i] + _ta[i + 1];
                yeni2sayac++;
            }
           _ta[baslangicYeri]= minDegerBul(yeni2);
           _satirBaslangici[baslangicYeri]=minDegerYeriBul(yeni2) + baslangicYeri;
        }
        static public int minDegerYeriBul (int[]_dizi)
        {
            int minDeger = int.MaxValue;
            int minDegerYeri = 0;
            for (int i = 0; i < _dizi.Length; i++)
            {
                if (_dizi[i] <= 0)
                    continue;
                if (_dizi[i] < minDeger)
                {
                    minDeger = _dizi[i];
                    minDegerYeri = i + 1;
                }
            }
            return minDegerYeri;
        }
          static public void dosyadanOku()
        {
            string line;
            List<string> text = new List<string>();
            Console.Write("Disk Adi giriniz : ");
            string diskAdi = Console.ReadLine();
            Console.Write("Dosya Adi giriniz : ");
            string dosyaAdi = Console.ReadLine();
            try
            {
                string path = diskAdi + @":\" + dosyaAdi + ".txt";
                if (File.Exists(path) != true)
                {
                    throw new dosyadanOkumaHatasi();
                }
                System.IO.StreamReader file = new System.IO.StreamReader(path);
                while ((line = file.ReadLine()) != null)
                {
                    kelimeBol(line);
                }

            }
            catch (dosyadanOkumaHatasi e)
            {
                //Console.Write(e);
                dosyadanOku();

            } 
        }
          static public void kelimeBosluk(ArrayList bolukluKelime, int boslukSayisi, int kelimeSayisi)
          {
              while (boslukSayisi > 0)
              {
                  for (int i = 0; i < kelimeSayisi-1 && boslukSayisi > 0; i++)
                  {
                      bolukluKelime[i] += " ";
                      boslukSayisi--;
                  }
              }

          }
          static public void arrayListYazdir(ArrayList bosluklukelime)
          {
              int count = 0;
          foreach(string elements in bosluklukelime) {
              count++;
          }
          int k = 0;
          for (int i = 0; i < count-1; i++)
          {
              Console.Write(bosluklukelime[i]+" ");
              k = i;
          }
          Console.Write(bosluklukelime[k+1] + " ");
          }
          static public int dynamicYazdir(int []_satirBaslangici,int _satirUzunlugu)
          {
              int y=0;
              int boslukKare = 0;
              int kelimeUzunlugu=0;
              int araBosluk = 0;
              int kelimeSayisi=0;
              Console.WriteLine("Dynamic \n");
              while (y >= 0)
              {
                  kelimeUzunlugu = 0;
                  kelimeSayisi = 0;
                  for (int i = y; i < _satirBaslangici[y]; i++)
                  {
                      kelimeUzunlugu += uzunlukBul(i);
                      kelimeSayisi++;
                  }
                  kelimeUzunlugu += kelimeSayisi - 1;        
                  //Console.Write(kelimeUzunlugu+" ");
                //  Console.Write(kelimeSayisi+" ");
                  araBosluk = _satirUzunlugu - kelimeUzunlugu;
               //  Console.Write(araBosluk + " ");
                  int aralik = kelimeSayisi - 1;
                  ArrayList bosluk = new ArrayList();
                  for (int i = y; i < _satirBaslangici[y]; i++)
                  {    
                      bosluk.Add(yeni[i]);
                  }
                  if (kelimeSayisi == 1)
                  {
                      Console.Write(bosluk[0]);
                      Console.WriteLine();
                      y = _satirBaslangici[y];
                      continue;
                  }
                  boslukKare += Convert.ToInt32(Math.Pow((_satirUzunlugu - kelimeUzunlugu), 2));
                  kelimeBosluk(bosluk, araBosluk, kelimeSayisi);
                  arrayListYazdir(bosluk);
                  Console.WriteLine();
                      y = _satirBaslangici[y];
                      if (y == yeni.Count)
                          break;
              }
              Console.WriteLine();
              return boslukKare;
          }
       static  void Main(string[] args)
        {
              dosyadanOku();
              int[,] uzaklik = new int[yeni.Count, yeni.Count];
              satirasianelemansayisi(uzaklik);   
              int[,] badness = new int[yeni.Count, yeni.Count];
              Console.Write("Satir Boyutu Giriniz : ");
              int satirBoyutu=Convert.ToInt32(Console.ReadLine());
              int max = 0;
              for (int i = 0; i < yeni.Count; i++)
              {
                  if (max < uzunlukBul(i))
                  {
                      max = uzunlukBul(i);
                  }
              }
              max++;
              while (max > satirBoyutu)
              {
                  Console.Write("Texte satir boyutundan buyuk kelime var min : " + max + "\n");
                  Console.Write("Satir Boyutu Giriniz : ");
                  satirBoyutu = Convert.ToInt32(Console.ReadLine());
              }
              badnessTablosu(uzaklik, badness, satirBoyutu);
                int [] atlama = new int [yeni.Count];
                greddyaltsatiragecme(badness, atlama);
                Console.WriteLine();    
                int[] ta = new int[yeni.Count+1];
                int[] satirBaslangici = new int[yeni.Count];
                ta[yeni.Count]=0;

                for (int i = yeni.Count; i >0; i--)
                {
                    tADiziyiDoldur(ta, badness, i - 1, satirBaslangici);
                }
                Console.Write(dynamicYazdir(satirBaslangici, satirBoyutu) + " karakter ile yana yaslama islemi yapildi.\n");
                Console.WriteLine();
                Console.Write("*********************************************");
                Console.WriteLine();
                Console.Write(greedYazdir(atlama, satirBoyutu) + " karakter ile yana yaslama islemi yapildi.");
                    Console.ReadKey();
        }
    }
}
