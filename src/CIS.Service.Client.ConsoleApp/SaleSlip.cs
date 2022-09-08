using System;
using CIS.Service.Client.Interfaces;

namespace CIS.Service.Client.ConsoleApp
{
    public class SaleSlip : IdentityObject, IBody
    {
        /// <summary>
        /// Дата открытия
        /// </summary>
        public DateTime DateBegin { get; set; }

        /// <summary>
        /// Дата закрытия
        /// </summary>
        public DateTime? DateEnd { get; set; }

        /// <summary>
        /// Примечание
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Номер чека
        /// </summary>
        public long Number { get; set; }

        /// <summary>
        /// Магазин
        /// </summary>
        public Guid PointSiteTDId { get; set; }

        public Guid? FiscalRegisterId { get; set; }

        public Guid MainStateId { get; set; }

        public Guid AdditionalStateId { get; set; }
    }
}
