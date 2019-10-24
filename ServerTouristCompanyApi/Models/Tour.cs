using System;

namespace ServerTouristCompanyApi.Models
{
    /// <summary>
    /// Тур
    /// </summary>
    public class Tour
    {
        /// <summary>
        /// Дата создания тура
        /// </summary>
        public DateTime Create { get; set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Из какого города, страны
        /// </summary>
        public string FromCity { get; set; }

        /// <summary>
        /// В какой город, страну
        /// </summary>
        public string ToCity { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public int Price { get; set; }
    }
}