# PhoneBookAsync
Bu proje bir telefon reheberi örneğidir. 3 farklı parçadan oluşmaktadır.  
  
*ContractAPI  
*ReportAPI  
*MessageBroker  
  
# Kullanılan Teknolojiler
*PostgreSQL  
*Redis-Server  

# Başlarken;
*Redis-server ve postgreSQL kurulmalı.  
*ContractAPI'nin Appsettings.json dosyasında bulunan ConnectionStrings:DefaultConnection anahtarına PostgreSQL connectionstring bilgisi girilmeli  
ve migrationlar package manager consoledan update-database komutuyla çalıştırılmalıdır (ContractsDB).  
*ReportAPI'nin Appsettings.json dosyasında bulunan ConnectionStrings:DefaultConnection anahtarına PostgreSQL connectionstring bilgisi girilmeli  
ve migrationlar package manager consoledan update-database komutuyla çalıştırılmalıdır (ReportDB).  
*ReportAPI'nin Appsettings.json dosyasında bulunan ContractApi anahtarına ContractAPI'nin çalıştığı adres girilmelidir.  
*ReportAPI'nin Appsettings.json dosyasında bulunan Redis anahtarına redis-server'ın çalıştığı adres girilmelidir.  
*MessageBroker'ın App.config dosyasında bulunan ReportApi anahtarına ReportAPI'nin çalıştığı adres girilmelidir.  
*MessageBroker'ın App.config dosyasında bulunan Redis anahtarına redis-server'ın çalıştığı adres girilmelidir.  

# ContractAPI
Kişilerle ilgili bilgilerin saklandığı veritabanı ile bağlantısı olan endpoind'leri içeren API'dir. Kişi ekleme silme listeleme işlemleri aşağıda detaylandırılan
endpoindler ile sağlanır.  
**Endpoindler ;**  
*[Get] (ApiAdres)/GetContractsAndInfos : Tüm kişileri ve iletişim bilgileri getiren bir liste döndürür.  
*[Get] (ApiAdres)/GetContracts : Tüm kişilerin bilgilerini getiren bir liste döndürür.  
*[Get] (ApiAdres)/GetContractDetails/{ContractId} : Girilen ContractId'sine sahip kişinin bilgilerini ve iletişim bilgilerini döndürür.  
*[Post] (ApiAdres)/AddContract/{Name}/{Surname}/{FirmName} : İsim,Soyisim ve firma bilgilerini girerek yeni bir kişi oluşturur.  
*[Post] (ApiAdres)/AddContractInfo/{ContractId}/{InfoType}/{InfoValue} : Daha önce oluşturulan kişiye iletişim bilgisi ekler.  
*[Delete] (ApiAdres)/RemoveContract/{ContractId} : Girilen ContractId'sine ait kişiye ve tüm iletişim bilgilerini siler.  
*[Delete] (ApiAdres)/RemoveContractInfo/{ContractInfoId} : Girien ContractInfo'id ye ait iletişim bilgisini siler.  

# ReportAPI 
Raporlarla ilgili bilgilerin saklandığı veritabanı ile bağlantısı olan bir API'dir. Rapor isteği geldiğinde istenilen raporların bilgilerini kendi veritabanında tutar.  
**Endpoindler ;**   
*[Get] (ApiAdres) /GetReports : Tüm raporları ve bilgilerini listeler.  
*[Get] (ApiAdres) /GetReportDetails/{ReportId} : Girilen rapor id sinin detaylarını görüntüler.  
*[Put] (ApiAdres) /GetStatisticReport/{Location} : Girilen konumda bulunan telefon sayıları ve kişi sayılarını döndürür.  
  
# MessageBroker  
ReportAPI ile iletişime geçen bir ConsoleAPP'dir. ReportAPI'ye herhangi bir istatistik raporu isteği gittiğinde raporun durumu ile ilgili bilgileri ekranda gösterir.
Aynı zamanda ekrana bir lokasyon bilgisi girilip enter'a basıldığı takdirde ReportAPI ye kendisi istek gönderecektir.  

