using System;
using System.Collections.Generic;

enum CihazTuru
{
    Lamba = 1,
    Klima = 2,
    GuvenlikSistemi = 3
}

class Cihaz
{
    public int Kimlik { get; set; }
    public string Ad { get; set; }
    public CihazTuru Tur { get; set; }
    public bool AcikMi { get; private set; }

    public Cihaz(int kimlik, string ad, CihazTuru tur)
    {
        Kimlik = kimlik;
        Ad = ad;
        Tur = tur;
        AcikMi = false;
    }

    public void Ac()
    {
        if (AcikMi)
        {
            Console.WriteLine($"'{Ad}' zaten açık durumda.");
        }
        else
        {
            AcikMi = true;
            Console.WriteLine($"'{Ad}' açıldı.");
        }
    }

    public void Kapat()
    {
        if (!AcikMi)
        {
            Console.WriteLine($"'{Ad}' zaten kapalı durumda.");
        }
        else
        {
            AcikMi = false;
            Console.WriteLine($"'{Ad}' kapatıldı.");
        }
    }

    public void BilgileriGoster()
    {
        string durum = AcikMi ? "Açık" : "Kapalı";
        Console.WriteLine($"Kimlik: {Kimlik} | Ad: {Ad} | Tür: {Tur} | Durum: {durum}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Cihaz> cihazlar = new List<Cihaz>
        {
            new Cihaz(1, "Salon Lambası", CihazTuru.Lamba),
            new Cihaz(2, "Oturma Odası Kliması", CihazTuru.Klima),
            new Cihaz(3, "Giriş Kapısı Alarmı", CihazTuru.GuvenlikSistemi)
        };

        bool devam = true;

        while (devam)
        {
            Console.WriteLine("\n--- EV CİHAZLARI YÖNETİM SİSTEMİ ---");
            Console.WriteLine("1. Cihazları Listele");
            Console.WriteLine("2. Yeni Cihaz Ekle");
            Console.WriteLine("3. Cihaz Aç");
            Console.WriteLine("4. Cihaz Kapat");
            Console.WriteLine("0. Çıkış");
            Console.Write("Seçiminiz: ");

            string secim = Console.ReadLine();
            Console.WriteLine();

            switch (secim)
            {
                case "1":
                    CihazlariListele(cihazlar);
                    break;
                case "2":
                    YeniCihazEkle(cihazlar);
                    break;
                case "3":
                    CihazDurumDegistir(cihazlar, true);
                    break;
                case "4":
                    CihazDurumDegistir(cihazlar, false);
                    break;
                case "0":
                    devam = false;
                    Console.WriteLine("Sistemden çıkılıyor...");
                    break;
                default:
                    Console.WriteLine("Hata: Geçersiz bir seçim yaptınız.");
                    break;
            }
        }
    }

    static void CihazlariListele(List<Cihaz> cihazlar)
    {
        if (cihazlar.Count == 0)
        {
            Console.WriteLine("Sistemde kayıtlı cihaz bulunmuyor.");
            return;
        }

        Console.WriteLine("--- Kayıtlı Cihazlar ---");
        foreach (Cihaz cihaz in cihazlar)
        {
            cihaz.BilgileriGoster();
        }
    }

    static void YeniCihazEkle(List<Cihaz> cihazlar)
    {
        Console.Write("Cihaz Kimliği (Sayı): ");
        if (!int.TryParse(Console.ReadLine(), out int kimlik))
        {
            Console.WriteLine("Hata: Kimlik geçerli bir tam sayı olmalıdır.");
            return;
        }

        if (CihazBul(cihazlar, kimlik) != null)
        {
            Console.WriteLine($"Hata: {kimlik} kimliğine sahip bir cihaz zaten sistemde mevcut.");
            return;
        }

        Console.Write("Cihaz Adı: ");
        string ad = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(ad))
        {
            Console.WriteLine("Hata: Cihaz adı boş bırakılamaz.");
            return;
        }

        Console.WriteLine("Cihaz Türü Seçin: 1-Lamba, 2-Klima, 3-GuvenlikSistemi");
        Console.Write("Tür (1-3): ");
        if (!int.TryParse(Console.ReadLine(), out int turSecim) || !Enum.IsDefined(typeof(CihazTuru), turSecim))
        {
            Console.WriteLine("Hata: Geçersiz cihaz türü seçtiniz.");
            return;
        }

        CihazTuru secilenTur = (CihazTuru)turSecim;
        cihazlar.Add(new Cihaz(kimlik, ad, secilenTur));
        Console.WriteLine($"'{ad}' başarıyla eklendi (Başlangıç durumu: Kapalı).");
    }

    static void CihazDurumDegistir(List<Cihaz> cihazlar, bool acilsinMi)
    {
        Console.Write("İşlem yapılacak cihazın kimliği: ");
        if (!int.TryParse(Console.ReadLine(), out int kimlik))
        {
            Console.WriteLine("Hata: Geçerli bir kimlik numarası giriniz.");
            return;
        }

        Cihaz bulunanCihaz = CihazBul(cihazlar, kimlik);
        if (bulunanCihaz == null)
        {
            Console.WriteLine($"Hata: {kimlik} kimliğine sahip bir cihaz bulunamadı.");
            return;
        }

        if (acilsinMi)
            bulunanCihaz.Ac();
        else
            bulunanCihaz.Kapat();
    }

    static Cihaz CihazBul(List<Cihaz> cihazlar, int kimlik)
    {
        foreach (Cihaz cihaz in cihazlar)
        {
            if (cihaz.Kimlik == kimlik)
                return cihaz;
        }
        return null;
    }
}
