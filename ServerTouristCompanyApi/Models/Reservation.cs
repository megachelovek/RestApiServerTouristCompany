namespace ServerTouristCompanyApi.Models
{
    /// <summary>
    ///     Бронирование
    /// </summary>
    public class Reservation
    {
        /// <summary>
        ///     Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Имя контакта
        /// </summary>
        public string NameContact { get; set; }

        /// <summary>
        ///     Номер телефона
        /// </summary>
        public long NumberContact { get; set; }
    }
}