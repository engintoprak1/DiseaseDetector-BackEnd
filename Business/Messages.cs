using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public static class Messages
    {
        public static string UserRegistered = "Kayıt başarılı.";
        public static string PasswordIncorrect = "Şifre hatalı.";
        public static string AccessTokenCreated = "Token oluşturuldu.";

        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string NoUserFoundWithThisGsm = "Bu telefon numarası ile kullanıcı bulunamadı.";
        public static string UserAlreadyExistWithGsm = "Bu telefon ile kullanıcı mevcut.";
        public static string UserNotFoundWithIdentificationNumber = "Bu kimlik numarası ile kullanıcı bulunmamaktadır.";
    }
}
