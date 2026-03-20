# 🧱 Unity Tetris - Klasik Oyunun Modern Yorumu

Unity Tetris, klasik Tetris mekaniklerini modern bir görsellik ve akıcı bir oynanışla harmanlayan, Unity 6 motoru ile geliştirilmiş yüksek performanslı bir oyun projesidir. Hem geliştiriciler hem de oyuncular için temiz kod yapısı ve zengin özellik seti sunar.

## ✨ Öne Çıkan Özellikler

### 🕹️ Oyun Mekanikleri
- **Dinamik Blok Sistemi:** Orijinal Tetris kurallarına sadık, hatasız çakışma (collision) ve yerleşme mantığı.
- **Hayalet Parça (Ghost Piece):** Parçanın nereye düşeceğini milisaniyelik doğrulukla gösteren gerçek zamanlı izdüşüm sistemi.
- **Parça Saklama (Hold System):** Stratejik hamleler için o anki parçayı yedekleme ve ihtiyaç anında geri çağırma özelliği.
- **Aşamalı Seviye Sistemi:** Skor arttıkça hızlanan tempo ve seviyeye özel zorluk katsayıları.
- **Gelişmiş Puanlama:** Tekli, ikili, üçlü ve efsanevi "Tetris" (dörtlü) temizlemeler için katlanarak artan skor çarpanları.

### 🎨 Görsel & Kullanıcı Deneyimi
- **Smooth Animations:** **DOTween** ile desteklenen, parçaların doğuşundan yok oluşuna kadar eşlik eden yumuşak geçişler.
- **Modern UI Design:** **TextMesh Pro** ve modern canvas sistemleri ile oluşturulmuş, kullanıcı dostu arayüz.
- **Parçacık Efektleri:** Seviye atlamaları ve oyun sonu sahneleri için tasarlanmış özel efekt sistemleri.
- **Ses Yönetimi:** Hareket, dönme, satır silme ve seviye geçişleri için entegre ses motoru.

## 🛠️ Teknik Alt Yapı

- **Engine:** Unity 6
- **Language:** C# (OOP Prensipleri ile Modüler Yapı)
- **Render Pipeline:** Built-in / URP Uyumlu
- **Input:** Unity **New Input System** (Cross-platform desteği)
- **Third-Party Kütüphaneler:**
  - **DOTween (Demigiant)**: Animasyon ve tween işlemleri için.
  - **TextMesh Pro**: Gelişmiş tipografi yönetimi için.

## 🚀 Kurulum Adımları

1. **Hazırlık:** Bilgisayarınızda **Unity Hub** ve **Unity 6** yüklü olduğundan emin olun.
2. **Projeyi Edinme:** Depoyu klonlayın veya `.zip` olarak indirin.
3. **Unity ile Aç:** Unity Hub üzerinden "Add" butonuna basarak proje klasörünü seçin.
4. **Sahne Yükleme:** `Assets/Scenes/` klasörü altındaki `MainScene.unity` dosyasını açın.
5. **Çalıştır:** Üst paneldeki "Play" butonuna basarak oyuna başlayabilirsiniz.

## 🕹️ Kontroller

| Eylem | Klavye (Desktop) | Dokunmatik (Mobil) |
| :--- | :--- | :--- |
| **Sağa/Sola Hareket** | Ok Tuşları (← / →) | Sağa/Sola Sürükle |
| **Aşağı İndir** | Alt Ok Tuşu (↓) | Aşağı Sürükle |
| **Döndür** | Üst Ok Tuşu (↑) | Dokun (Tap) |
| **Parça Saklama** | Boşluk (Space) | UI Butonu |
| **Oyunu Duraklat** | ESC | Duraklat Butonu |

## 📂 Klasör Yapısı

```text
├── Assets/
│   ├── Scripts/            # Tüm C# Kodları
│   │   ├── GameDynamics/   # Board, Shape, Manager mantığı
│   │   ├── UIManagers/     # Arayüz kontrolcüleri
│   │   └── Mobile/         # Dokunmatik giriş motoru
│   ├── Prefabs/            # Bloklar ve oyun objeleri
│   ├── Sounds/             # Ses FX ve müzikler
│   └── Sprites/            # Görsel varlıklar
├── ProjectSettings/        # Proje sürüm ve paket ayarları
└── README.md               # Proje dökümantasyonu
```

## 📜 Lisans

Bu proje **MIT Lisansı** altında lisanslanmıştır. Detaylar için [LICENSE](LICENSE) dosyasına bakabilirsiniz.

---
⭐ Bu projeyi geliştirmemize yardımcı olmak isterseniz yıldız atmayı ve geri bildirim vermeyi unutmayın!
