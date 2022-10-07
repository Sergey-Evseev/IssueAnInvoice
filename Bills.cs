using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueAnInvoice
{
    class Bills : List<Bill>
    {
        public void Serialize(BinaryWriter bw)
        {
            var version = 0;
            bw.Write(version);

            bw.Write(Count);
            foreach (var bill in this)
                bill.Serialize(bw);
        }

        public static Bills Deserialize(BinaryReader br)
        {
            var res = new Bills();

            var version = br.ReadInt32();

            var count = br.ReadInt32();
            for (int i = 0; i < count; i++)
                res.Add(Bill.Deserialize(br));

            return res;
        }
    }
}
