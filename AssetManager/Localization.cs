namespace AssetManager
{
    public static class Localization
    {
        public static class Error
        {
            public const string Standard = "Что-то пошло нет так, возможно стоит проверить подключение к базе данных.";
        }
        
        public static class Message
        {
            public const string WrongUsernameOrPassword = "Неверный логин или пароль";

            public const string DifferentPasswords = "Пароли не совпадают";
            public const string UsernameEngaged = "Данное имя пользователя занято";
            public const string RegistrationSuccessful = "Регистрация прошла успешно";
            public const string NotCorrectRegisterFields = "Неправильно введены логин или пароль";
            public const string NotCorrectPost = "Сообщение не может превышать границу в 140 символов";
            public const string NotCorrectOperation = "Неправильно введены данные";
        }
    }
}