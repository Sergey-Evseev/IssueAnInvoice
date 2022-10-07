using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueAnInvoice
{
    public class Bill
    {
        public decimal PaymentPerDay;      //   Оплата за день;
        public uint Days;                  //   Количество дней;
        public decimal FinePerDay;         //   Штраф за один день задержки оплаты
        public uint DaysDelayedPayment;    //   Количество дней задержи оплаты;

        public decimal TotalScore => PaymentPerDay * Days;          //•   Сумма к оплате без штрафа(вычисляемое поле);
        public decimal TotalFine => FinePerDay * DaysDelayedPayment;//•  Штраф(вычисляемое поле);
        public decimal ResultPayment => TotalScore + TotalFine;     //• Общая сумма к оплате(вычисляемое поле).

        public static bool SerializeComputableFields = false;

        public void Serialize(BinaryWriter bw)
        {
            var version = SerializeComputableFields ? 1 : 0;
            bw.Write(version);

            bw.Write(PaymentPerDay);
            bw.Write(Days);
            bw.Write(FinePerDay);
            bw.Write(DaysDelayedPayment);

            if (SerializeComputableFields)
            {
                bw.Write(TotalScore);
                bw.Write(TotalFine);
                bw.Write(ResultPayment);
            }
        }

        public static Bill Deserialize(BinaryReader br)
        {
            var res = new Bill();

            var version = br.ReadInt32();

            res.PaymentPerDay = br.ReadDecimal();
            res.Days = br.ReadUInt32();
            res.FinePerDay = br.ReadDecimal();
            res.DaysDelayedPayment = br.ReadUInt32();

            if (version == 1)
            {
                br.ReadDecimal();
                br.ReadDecimal();
                br.ReadDecimal();
            }

            return res;
        }
    }
}
