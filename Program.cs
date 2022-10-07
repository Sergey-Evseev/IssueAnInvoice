/*Lesson 10: Разработать класс «Счет для оплаты». В классе предусмотреть следующие поля:
■■ оплата за день;
■■ количество дней;
■■ штраф за один день задержки оплаты;
■■ количество дней задержи оплаты;
■■ сумма к оплате без штрафа (вычисляемое поле);
■■ штраф(вычисляемое поле);
■■ общая сумма к оплате (вычисляемое поле).
В классе объявить статическое свойство типа bool, значение которого влияет на процесс форматирования объектов этого класса. 
Если значение этого свойства равно true, тогда сериализуются и десериализируются все поля, если false — вычисляемые поля не сериализуются.
Разработать приложение, в котором необходимо продемонстрировать использование этого класса, результаты должны записываться и считываться из файла.*/

namespace IssueAnInvoice
{
    class Program
    {
        static void Main(string[] args)
        {
            var bill = new Bill { PaymentPerDay = 100, Days = 50, FinePerDay = 20, DaysDelayedPayment = 10 };

            //save single bill (mode 1)
            Bill.SerializeComputableFields = false;
            Save(bill, "c:\\temp1.bill");

            //save single bill (mode 2)
            Bill.SerializeComputableFields = true;
            Save(bill, "c:\\temp2.bill");

            //load bill
            bill = LoadBill("c:\\temp2.bill");

            //create list of bills
            var bills = new Bills();
            bills.Add(bill);
            bills.Add(bill);
            bills.Add(bill);

            //save bills (mode 2)
            Save(bills, "c:\\temp1.bills");

            //load bills
            bills = LoadBills("c:\\temp1.bills");
        }

        static void Save(Bill bill, string fileName)
        {
            using (var file = File.Create(fileName))
            using (var bw = new BinaryWriter(file))
                bill.Serialize(bw);
        }

        static Bill LoadBill(string fileName)
        {
            using (var file = File.OpenRead(fileName))
            using (var br = new BinaryReader(file))
                return Bill.Deserialize(br);
        }

        static void Save(Bills bills, string fileName)
        {
            using (var file = File.Create(fileName))
            using (var bw = new BinaryWriter(file))
                bills.Serialize(bw);
        }

        static Bills LoadBills(string fileName)
        {
            using (var file = File.OpenRead(fileName))
            using (var br = new BinaryReader(file))
                return Bills.Deserialize(br);
        }
    }
}